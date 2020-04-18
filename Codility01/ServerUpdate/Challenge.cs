using System.Collections.Generic;
using System.Linq;

namespace Codility01.ServerUpdate
{

	public class Challenge
	{
		public int minimumDays(int rows, int columns, int[,] grid)
		{
			var recentlyUpdated = new List<Server>();

			//get updated servers
			for (int cntr = 0; cntr < grid.GetLength(0); cntr++)
			{
				for (int cntc = 0; cntc < grid.GetLength(1); cntc++)
				{
					var server = grid[cntr, cntc];
					if (server == 1)
						recentlyUpdated.Add(new Server { Column = cntc, Row = cntr });
				}
			}

			if (recentlyUpdated.Count == 0)
				return -1;

			else if (recentlyUpdated.Count == grid.Length)
				return 1;

			else
			{
				var daysCount = 0;
				var updatedServerCount = recentlyUpdated.Count;
				while (updatedServerCount < grid.Length)
				{
					recentlyUpdated = recentlyUpdated
						.SelectMany(server => GetAndUpdateAdjacentUnupdatedServers(server, grid))
						.ToList();

					updatedServerCount += recentlyUpdated.Count;
					daysCount++;
				}

				return daysCount;
			}
		}

		private List<Server> GetAndUpdateAdjacentUnupdatedServers(Server server, int[,] grid)
		{
			var newServers = new List<Server>();

			//top
			int row = server.Row - 1;
			int column = server.Column;
			if (row >= 0 && grid[row, column] == 0)
			{
				grid[row, column] = 1;
				newServers.Add(new Server
				{
					Column = column,
					Row = row
				});
			}

			//bottom
			row = server.Row + 1;
			column = server.Column;
			if (row < grid.GetLength(0) && grid[row, column] == 0)
			{
				grid[row, column] = 1;
				newServers.Add(new Server
				{
					Column = column,
					Row = row
				});
			}

			//left
			row = server.Row;
			column = server.Column - 1;
			if (column >= 0 && grid[row, column] == 0)
			{
				grid[row, column] = 1;
				newServers.Add(new Server
				{
					Column = column,
					Row = row
				});
			}

			//right
			row = server.Row;
			column = server.Column + 1;
			if (column < grid.GetLength(1) && grid[row, column] == 0)
			{
				grid[row, column] = 1;
				newServers.Add(new Server
				{
					Column = column,
					Row = row
				});
			}

			return newServers;
		}

		public class Server
		{
			public int Row { get; set; }

			public int Column { get; set; }

			public override string ToString() => $"[{Row},{Column}]";
		}

	}

}
