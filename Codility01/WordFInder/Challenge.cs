using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.WordFInder
{
	class Challengex
    {
        static public bool Exist(char[][] board, string word)
        {
            //find all start chars
            var positions = new List<Position>();
            for (int cntr = 0; cntr < board.Length; cntr++)
            {
                for (int cntc = 0; cntc < board.Length; cntc++)
                {
                    if (board[cntr][cntc] == word[0]) positions.Add(new Position
                    {
                        Row = cntr,
                        Column = cntc
                    });
                }
            }

            return positions
                .Any(p => Find(p, board, word, "", new HashSet<Position>()));
        }

        static public bool Find(Position current, char[][] board, string word, string matched, HashSet<Position> context)
        {
            var sub = matched + board[current.Row][current.Column];

            if (word.Equals(sub))
                return true;

            else if (word.StartsWith(sub))
            {
                context.Add(current);

                foreach (var newPosition in NextPositions(current, board))
                {
                    if (!context.Contains(newPosition)
                       && Find(newPosition, board, word, sub, context))
                    {
                        return true;
                    }
                }

                context.Remove(current);

            }

            return false;
        }


        static public Position[] NextPositions(Position current, char[][] board)
        {
            var positions = new List<Position>();

            //Up
            if (current.Row > 0) positions.Add(new Position
            {
                Row = current.Row - 1,
                Column = current.Column
            });

            //Down
            if (current.Row < board.Length - 1) positions.Add(new Position
            {
                Row = current.Row + 1,
                Column = current.Column
            });

            //Left
            if (current.Column > 0) positions.Add(new Position
            {
                Column = current.Column - 1,
                Row = current.Row
            });

            //Right
            if (current.Column < board[0].Length - 1) positions.Add(new Position
            {
                Column = current.Column + 1,
                Row = current.Row
            });

            return positions.ToArray();
        }

    }

    class Challenge
    {
        static public bool Exist(char[][] board, string word)
        {
            if (string.IsNullOrEmpty(word))
                return false;

            //Load valid positions
            var validPositions = new Dictionary<string, Position>();
            var startPositions = new List<Position>();
            for(int cntr = 0; cntr < board.Length; cntr++)
            {
                for(int cntc = 0; cntc < board[cntr].Length; cntc++)
                {
                    var letter = board[cntr][cntc].ToString();
                    if (word.Contains(letter))
                    {
                        var pos = new Position(
                            board[cntr][cntc],
                            cntr,
                            cntc);
                        validPositions[Position.GetId(cntr, cntc)] = pos;

                        if (word.StartsWith(letter))
                            startPositions.Add(pos);
                    }
                }
            }

            return startPositions
                .Any(p => Find(p, board, word, "", new HashSet<Position>(), validPositions));
        }

        static public bool Find(
            Position current, 
            char[][] board, 
            string word, 
            string matched, 
            HashSet<Position> visited,
            Dictionary<string, Position> validPositions)
        {
            var sub = matched + board[current.Row][current.Column];

            if (word.Equals(sub))
                return true;

            else if (word.StartsWith(sub))
            {
                visited.Add(current);

                foreach (var newPosition in NextPositions(current, board, validPositions))
                {
                    if (!visited.Contains(newPosition)
                       && Find(newPosition, board, word, sub, visited, validPositions))
                    {
                        return true;
                    }
                }

                visited.Remove(current);

            }

            return false;
        }

        static public Position[] NextPositions(Position current, char[][] board, Dictionary<string, Position> validPosition)
        {
            var positions = new List<Position>();

            //Up
            if (current.Row > 0 
                && validPosition.TryGetValue(Position.GetId(current.Row - 1, current.Column), out var pos))
                positions.Add(pos);

            //Down
            if (current.Row < board.Length - 1
                && validPosition.TryGetValue(Position.GetId(current.Row + 1, current.Column), out pos))
                positions.Add(pos); 

            //Left
            if (current.Column > 0
                && validPosition.TryGetValue(Position.GetId(current.Row, current.Column - 1), out pos))
                positions.Add(pos);

            //Right
            if (current.Column < board[0].Length - 1
                && validPosition.TryGetValue(Position.GetId(current.Row, current.Column + 1), out  pos))
                positions.Add(pos);

            return positions.ToArray();
        }
    }

    public class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public char Value { get; set; }

        public string Id => Position.GetId(Row, Column);

        public Position()
        { }

        public Position(char value, int row, int col)
        {
            Row = row;
            Column = col;
            Value = value;
        }

        public override bool Equals(object obj)
        {
            return obj is Position other
                && other.Row == Row
                && other.Column == Column;
        }

        public override int GetHashCode()
        {
            var acc = 3 * 5 + Row.GetHashCode();
            return acc * 7 + Column.GetHashCode();
        }

        public static string GetId(int row, int col) => $"{row},{col}";
    }
}
