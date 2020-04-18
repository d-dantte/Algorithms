using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.ServerUpdate
{
	public class Challenge1
	{
		public int minimumDays(int rows, int columns, int[,] grid)
		{
			var updatedServers = new HashSet<Server>();

			//get updated servers
			for (int cntr = 0; cntr < grid.GetLength(0); cntr++)
			{
				for(int cntc = 0; cntc < grid.GetLength(1); cntc++)
				{
					var server = grid[cntr, cntc];
					if (server == 1)
						updatedServers.Add(new Server { Column = cntc, Row = cntr });
				}
			}

			if (updatedServers.Count == 0)
				return -1;

			var recentlyUpdated = updatedServers.ToArray();
			var daysCount = 0;
			while(updatedServers.Count != grid.Length)
			{
				recentlyUpdated = recentlyUpdated
					.SelectMany(server => GetAndUpdateAdjacentUnupdatedServers(server, grid, updatedServers))
					.ToArray();

				daysCount++;
			}

			return daysCount;
		}

		private List<Server> GetAndUpdateAdjacentUnupdatedServers(Server server, int[,] grid, HashSet<Server> servers)
		{
			var newServers = new List<Server>();

			//top
			int row = server.Row - 1;
			int column = server.Column;
			Server newServer;
			if (row >= 0 && grid[row, column] == 0)
			{
				grid[row, column] = 1;
				newServers.Add(newServer = new Server
				{
					Column = column,
					Row = row
				});
				servers.Add(newServer);
			}

			//bottom
			row = server.Row + 1;
			column = server.Column;
			if(row < grid.GetLength(0) && grid[row, column] == 0)
			{
				grid[row, column] = 1;
				newServers.Add(newServer = new Server
				{
					Column = column,
					Row = row
				});
				servers.Add(newServer);
			}

			//left
			row = server.Row;
			column = server.Column - 1;
			if (column >= 0 && grid[row, column] == 0)
			{
				grid[row, column] = 1;
				newServers.Add(newServer = new Server
				{
					Column = column,
					Row = row
				});
				servers.Add(newServer);
			}

			//right
			row = server.Row;
			column = server.Column + 1;
			if (column < grid.GetLength(1) && grid[row, column] == 0)
			{
				grid[row, column] = 1;
				newServers.Add(newServer = new Server
				{
					Column = column,
					Row = row
				});
				servers.Add(newServer);
			}

			return newServers;
		}

	}

	public class Server
	{
		public int Row { get; set; }

		public int Column { get; set; }

		public override string ToString() => $"[{Row},{Column}]";

		public override bool Equals(object obj)
		{
			var d = obj as Server;
			return d != null
				&& d.Row == Row
				&& d.Column == Column;
		}

		public override int GetHashCode() => ToString().GetHashCode();
	}
}
