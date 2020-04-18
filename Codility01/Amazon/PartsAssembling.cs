using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility01.Amazon
{
    public class PartsAssembling
    {
        public static int EstimateTime(int numberOfParts, int[] parts)
        {
            var assemblyCosts = 0;
            var assemblyParts = parts
                .OrderBy(part => part)
                .ToArray();

            while(assemblyParts.Length > 0)
            {
                var part = Assemble(assemblyParts);

                assemblyCosts += part.Cost;
                assemblyParts = part.NewParts;
            }

            return assemblyCosts;
        }

        protected static AssemblyStep Assemble(int[] orderedParts)
        {
            if (orderedParts == null)
                throw new Exception("Invalid parts array");

            else if (orderedParts.Length == 1) return new AssemblyStep
            {
                Cost = 0,
                NewParts = new int[0]
            };

            else
            {
                var cost = orderedParts[0] + orderedParts[1];
                var newParts = new List<int>(orderedParts.Skip(2))
                {
                    cost
                };

                return new AssemblyStep
                {
                    Cost = cost,
                    NewParts = newParts
                        .OrderBy(part => part)
                        .ToArray()
                };
            }
        }
    }

    public class AssemblyStep
    {
        public int Cost { get; set; }
        public int[] NewParts { get; set; }
    }
}
