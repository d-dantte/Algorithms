using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.Helpers
{
	public enum HugeIntegerSign
	{
		Positive = 0, 
		Negative = 1
	}

	public struct HugeInteger: IComparable<HugeInteger>
	{
		#region Members
		private readonly byte[] _digits;
		private int? _hashCode;
		private string _stringValue;


		public bool IsNegative => Sign == HugeIntegerSign.Negative;

		public bool IsPositive => Sign == HugeIntegerSign.Positive;

		public HugeIntegerSign Sign { get; }

		public bool IsZero => _digits == null;
		#endregion


		#region Constructor
		public HugeInteger(string value)
		{
			_stringValue = null;
			value = value.Trim();

			if (!value.StartsWith("-"))
				Sign = HugeIntegerSign.Positive;

			else
			{
				Sign = HugeIntegerSign.Negative;
				value = value
					.Substring(1)
					.TrimStart('0');
			}

			_digits = value
				.Select(digit => byte.Parse(digit.ToString()))
				.Reverse()
				.ToArray();

			if (_digits.Length == 0)
			{
				_digits = null;
				Sign = HugeIntegerSign.Positive;
			}

			_hashCode = null;
		}

		private HugeInteger(HugeIntegerSign sign, byte[] digits)
		{
			_stringValue = null;
			digits = StripLeadingZeros(digits);
			if (digits == null || digits.Length == 0)
			{
				_digits = null;
				Sign = HugeIntegerSign.Positive;
			}

			else
			{
				var tempDigits = new List<byte>(digits);
				for (int cnt = tempDigits.Count - 1; cnt >= 0; cnt--)
				{
					if (tempDigits[cnt] != 0)
						break;

					else tempDigits.RemoveAt(cnt);
				}

				Sign = tempDigits.Count == 0 ? HugeIntegerSign.Positive : sign;
				_digits = tempDigits.ToArray();
			}

			_hashCode = null;
		}
		#endregion


		#region Methods
		public HugeInteger Power(HugeInteger power)
		{
			if (power.Sign == HugeIntegerSign.Negative)
				throw new Exception("Negative powers not supported");

			else if (power == 0)
				return 1;

			else if (power == 1)
				return this;

			else
			{
				var accumulator = this;
				for (HugeInteger cnt = "2"; cnt <= power; cnt++)
				{
					accumulator *= this;
				}

				return accumulator;
			}
		}

		public HugeInteger ReverseSign() => new HugeInteger(Flip(Sign), _digits);

		public int CompareTo(HugeInteger other)
		{
			if (this.IsPositive && other.IsNegative)
				return 1;

			else if (this.IsNegative && other.IsPositive)
				return -1;

			else if (this.IsPositive) //both positive
				return Compare(this._digits, other._digits);

			else //both negative
				return Compare(other._digits, this._digits);
		}

		public override bool Equals(object obj)
		{
			return obj is HugeInteger h
				&& this.CompareTo(h) == 0;
		}

		public override int GetHashCode()
		{
			return _hashCode == null
				? (_hashCode = ToString().GetHashCode()).Value
				: _hashCode.Value;
		}

		public override string ToString()
		{
			if (_stringValue == null)
			{
				if (_digits == null)
					_stringValue = "0";

				else 
					_stringValue = (Sign == HugeIntegerSign.Negative ? "-" : "") + AsString(_digits);
			}

			return _stringValue;
		}

		#endregion


		#region private methods
		private HugeInteger Combine(HugeInteger integer)
		{
			TryMaxMin(this, integer, out var max, out var min);

			if (this.IsPositive ^ integer.IsPositive)
			{
				var digits = Subtract(max._digits, min._digits);
				return new HugeInteger(max.Sign, digits);
			}

			else
			{
				var digits = Add(max._digits, min._digits);
				return new HugeInteger(Sign, digits);
			}
		}

		private HugeInteger Multiply(HugeInteger integer)
		{
			TryMaxMin(this, integer, out var max, out var min);

			var digits = Multiply(max._digits, min._digits);

			if (this.IsPositive ^ integer.IsPositive)
				return new HugeInteger(HugeIntegerSign.Negative, digits);

			else
				return new HugeInteger(this.Sign, digits);
		}

		private HugeInteger TryDivide(
			HugeInteger integer, 
			out HugeInteger remainder)
		{
			var digits = Divide(this._digits, integer._digits, out var r);
			remainder = new HugeInteger(HugeIntegerSign.Positive, r);

			if (this.IsPositive ^ integer.IsPositive)
				return new HugeInteger(HugeIntegerSign.Negative, digits);

			else
				return new HugeInteger(this.Sign, digits);
		}
		#endregion


		#region static helpers
		public static HugeInteger Absolute(HugeInteger integer)
		{
			switch(integer.Sign)
			{
				case HugeIntegerSign.Negative:
					return integer.ReverseSign();

				case HugeIntegerSign.Positive:
					return integer;

				default:
					throw new Exception("Invalid integer sign: " + integer.Sign);
			}
		}

		private static HugeIntegerSign Flip(HugeIntegerSign sign)
		{
			switch(sign)
			{
				case HugeIntegerSign.Positive: return HugeIntegerSign.Negative;
				case HugeIntegerSign.Negative: return HugeIntegerSign.Positive;
				default: throw new Exception("Invalid sign: " + sign);
			}
		}

		private static byte[] Add(byte[] larger, byte[] smaller)
		{
			byte carry = 0;
			var result = new List<byte>();

			for (int cnt = 0; cnt < larger.Length; cnt++)
			{
				var s = cnt < smaller.Length ? smaller[cnt] : 0;
				var r = carry + s + larger[cnt];

				if (r > 9)
				{
					carry = 1;
					r %= 10;
				}

				else carry = 0;

				result.Add((byte)r);
			}

			if (carry > 0)
				result.Add(carry);

			return StripLeadingZeros(result.ToArray());
		}

		private static byte[] Subtract(byte[] larger, byte[] smaller)
		{
			bool borrowed = false;
			var result = new List<byte>();

			for (int cnt = 0; cnt < larger.Length; cnt++)
			{
				var s = cnt < smaller.Length ? smaller[cnt] : 0;
				var l = larger[cnt] - (borrowed ? 1 : 0);

				if (s > l)
					borrowed = true;

				else
					borrowed = false;

				var r = ((borrowed ? 10 : 0) + l) - s;

				result.Add((byte)r);
			}

			return StripLeadingZeros(result.ToArray());
		}

		private static byte[] Multiply(byte[] larger, byte[] smaller)
		{
			byte mcarry = 0;
			var result = new List<byte>();

			for (int cnt = 0; cnt < smaller.Length; cnt++)
			{
				byte acarry = mcarry = 0;
				for (int cntt = 0; cntt < larger.Length; cntt++)
				{
					mcarry = (byte) IntegerDivide((smaller[cnt] * larger[cntt]) + mcarry, 10, out var r);

					var acount = cnt + cntt;

					if (result.Count == acount)
					{
						acarry = (byte)IntegerDivide(acarry + r, 10, out r);
						result.Add((byte)r);
					}
					else
					{
						acarry = (byte)IntegerDivide((result[acount] + acarry + r), 10, out r);
						result[acount] = (byte)r;
					}
				}

				if (acarry != 0 || mcarry != 0)
				{
					var ucarry = (byte)IntegerDivide(acarry + mcarry, 10, out var r);
					result.Add((byte)r);

					if (ucarry != 0)
						result.Add(ucarry);
				}
			}

			return StripLeadingZeros(result.ToArray());
		}

		private static byte[] Divide(byte[] numerator, byte[] denominator, out byte[] remainder)
		{
			if (denominator == null)
				throw new DivideByZeroException();

			var comparison = Compare(numerator, denominator);

			if(comparison < 0)
			{
				remainder = numerator;
				return null;
			}
			else if(comparison == 0)
			{
				remainder = null;
				return new[] { (byte)1 };
			}
			else
			{
				var result = new List<byte>();
				var iterator = new ByteIterator(numerator.Reverse());
				IEnumerable<byte> subnumeratorDigits = new byte[0];

				while(iterator.TryNext(1, out var subdigit))
				{
					subnumeratorDigits = subdigit.Concat(subnumeratorDigits);
					var subnumerator = subnumeratorDigits.ToArray();

					if (Compare(denominator, subnumerator) > 0)
					{
						result.Add(0);
						continue;
					}

					//How many times can we increase the denominator till we are greater than the numerator
					byte[] previous, accumulator = denominator;
					int compare, cnt = 0;
					do
					{
						cnt++;
						previous = accumulator;
						accumulator = Multiply(denominator, GetDigits(cnt).ToArray());
					}
					while ((compare = Compare(accumulator, subnumerator)) < 0);

					//add the quotient digits
					if (compare == 0)
					{
						result.AddRange(GetDigits(cnt));
						subnumeratorDigits = new byte[0];
					}
					else //if(compare > 0)
					{
						result.AddRange(GetDigits(cnt - 1));
						subnumeratorDigits = Subtract(subnumerator, previous);
					}
				}

				remainder = subnumeratorDigits.ToArray();

				return StripLeadingZeros(result.AsEnumerable().Reverse().ToArray());
			}
		}

		private static int Compare(byte[] first, byte[] second)
		{
			if (first == null && second == null)
				return 0;

			else if (first.Length > second.Length)
				return 1;

			else if (second.Length > first.Length)
				return -1;

			else
			{
				for (int cnt = first.Length - 1; cnt >= 0; cnt--)
				{
					if (first[cnt] > second[cnt])
						return 1;

					else if (first[cnt] < second[cnt])
						return -1;
				}

				return 0;
			}
		}

		private static string AsString(byte[] digits)
		{
			return string.Join("", digits.Reverse().ToArray());
		}

		private static int IntegerDivide(int first, int second, out int remainder)
		{
			remainder = first % second;

			return first / second;
		}

		private static byte[] StripLeadingZeros(byte[] digits)
		{
			if (digits == null)
				return null;

			for (int cnt = digits.Length - 1; cnt >= 0; cnt--)
			{
				if (digits[cnt] != 0)
					return digits.Take(cnt + 1).ToArray();
			}

			return null;
		}

		private static IEnumerable<byte> GetDigits(int source)
		{
			int individualFactor = 0;
			int tennerFactor = Convert.ToInt32(Math.Pow(10, source.ToString().Length));
			do
			{
				source -= tennerFactor * individualFactor;
				tennerFactor /= 10;
				individualFactor = source / tennerFactor;

				yield return (byte)individualFactor;
			} 
			while (tennerFactor > 1);
		}

		public static bool TryMaxMin(
			HugeInteger first, 
			HugeInteger second,
			out HugeInteger max,
			out HugeInteger min)
		{
			var comparison = first.CompareTo(second);
			if (comparison >= 0)
			{
				max = first;
				min = second;

				return comparison > 0;
			}

			else
			{
				max = second;
				min = first;

				return true;
			}

		}
		#endregion


		#region Operator overloads
		public static HugeInteger operator +(HugeInteger first, HugeInteger second)
		{
			return first.Combine(second);
		}

		public static HugeInteger operator -(HugeInteger first, HugeInteger second)
		{
			return first.Combine(second.ReverseSign());
		}

		public static HugeInteger operator *(HugeInteger first, HugeInteger second)
		{
			return first.Multiply(second);
		}

		public static HugeInteger operator /(HugeInteger first, HugeInteger second)
		{
			var quotient = first.TryDivide(second, out var remainder);

			return quotient;
		}

		public static HugeInteger operator %(HugeInteger first, HugeInteger second)
		{
			var quotient = first.TryDivide(second, out var remainder);

			return remainder;
		}

		public static HugeInteger operator ++(HugeInteger value)
		{
			return value + 1;
		}

		public static HugeInteger operator --(HugeInteger value)
		{
			return value - 1;
		}

		public static bool operator >(HugeInteger first, HugeInteger second)
		{
			return first.CompareTo(second) > 0;
		}

		public static bool operator <(HugeInteger first, HugeInteger second)
		{
			return first.CompareTo(second) < 0;
		}

		public static bool operator >=(HugeInteger first, HugeInteger second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static bool operator <=(HugeInteger first, HugeInteger second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static bool operator ==(HugeInteger first, HugeInteger second)
		{
			return first.CompareTo(second) == 0;
		}

		public static bool operator !=(HugeInteger first, HugeInteger second)
		{
			return first.CompareTo(second) != 0;
		}

		public static implicit operator HugeInteger(byte value) => new HugeInteger(value.ToString());

		public static implicit operator HugeInteger(sbyte value) => new HugeInteger(value.ToString());

		public static implicit operator HugeInteger(short value) => new HugeInteger(value.ToString());

		public static implicit operator HugeInteger(ushort value) => new HugeInteger(value.ToString());

		public static implicit operator HugeInteger(int value) => new HugeInteger(value.ToString());

		public static implicit operator HugeInteger(uint value) => new HugeInteger(value.ToString());

		public static implicit operator HugeInteger(long value) => new HugeInteger(value.ToString());

		public static implicit operator HugeInteger(ulong value) => new HugeInteger(value.ToString());

		public static implicit operator HugeInteger(string value) => new HugeInteger(value);
		#endregion

		internal class ByteIterator
		{
			private byte[] _array;
			private int _cursor = 0;

			public int Count => _array.Length - _cursor;

			public ByteIterator(IEnumerable<byte> array)
			{
				_array = array?.ToArray() ?? throw new ArgumentNullException();
			}

			public bool TryNext(int count, out IEnumerable<byte> output)
			{
				count = Math.Abs(count);
				output = null;

				if (count == 0)
					return true;

				else
				{
					var list = new List<byte>();
					if (Count < count)
						return false;

					else
					{
						for (int cnt = 0; cnt < count; cnt++)
						{
							list.Add(_array[cnt + _cursor]);
						}

						_cursor += count;
						output = list;

						return true;
					}
				}
			}
		}
	}
}
