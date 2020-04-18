using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.CompetingNumbers
{
	class Challenge
	{
		public int[] CellCompete(int[] states, int days)
		{
			var tempState = states;
			for (int cnt = 0; cnt < days; cnt++)
			{
				tempState = UpdateState(tempState);
			}

			return tempState;
		}

		private int[] UpdateState(int[] state)
		{
			var newState = new List<int>();
			for (int cnt = 0; cnt < state.Length; cnt++)
			{
				if (LeftState(state, cnt) ^ RightState(state, cnt))
					newState.Add(1);

				else newState.Add(0);
			}

			return newState.ToArray();
		}

		private bool LeftState(int[] state, int index)
		{
			if (index == 0)
				return false;

			else return state[index - 1] == 1;
		}

		private bool RightState(int[] state, int index)
		{
			if (index == state.Length - 1)
				return false;

			else return state[index + 1] == 1;
		}
	}
}
