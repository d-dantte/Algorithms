using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.Misc
{
	static class DictionaryAssigner
	{
		public static TDestination AssignTo<TDestination>(this 
			Dictionary<string, object> values, 
			TDestination destination)
			where TDestination: class
		{
			if (destination == null)
				return null;

			else if (values == null || values.Count == 0)
				return destination;

			else
			{
				var type = typeof(TDestination);
				foreach (var prop in type.GetProperties())
				{
					if (values.ContainsKey(prop.Name))
						prop.SetValue(destination, values[prop.Name]);
				}

				return destination;
			}
		}
	}
}
