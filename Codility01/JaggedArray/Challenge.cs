/*
You are a developer for a university. Your current project is to develop a system for students to 
find courses they share with friends. The university has a system for querying courses students are 
enrolled in, returned as a list of (ID, course) pairs.

Write a function that takes in a list of (student ID number, course name) pairs and returns, 
for every pair of students, a list of all courses they share.

Sample Input:

student_course_pairs_1 = [
  ["58", "Linear Algebra"],
  ["94", "Art History"],
  ["94", "Operating Systems"],
  ["17", "Software Design"],
  ["58", "Mechanics"],
  ["58", "Economics"],
  ["17", "Linear Algebra"],
  ["17", "Political Science"],
  ["94", "Economics"],
  ["25", "Economics"],
  ["58", "Software Design"],
]

Sample Output (pseudocode, in any order):

find_pairs(student_course_pairs_1) =>
{
  [58, 17]: ["Software Design", "Linear Algebra"]
  [58, 94]: ["Economics"]
  [58, 25]: ["Economics"]
  [94, 25]: ["Economics"]
  [17, 94]: []
  [17, 25]: []
}

Additional test cases:

Sample Input:

student_course_pairs_2 = [
  ["42", "Software Design"],
  ["0", "Advanced Mechanics"],
  ["9", "Art History"],
]

Sample output:

find_pairs(student_course_pairs_2) =>
{
  [0, 42]: []
  [0, 9]: []
  [9, 42]: []
}

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codility01.JaggedArray
{
    using Kvp = KeyValuePair<int[], string[]>;

    class Challenge
    {        
        public static void Output(List<Kvp> pairs)
        {
            foreach(var pair in pairs)
            {
                var courses = string.Join(
                    ", ",
                    pair.Value.Select(s => $"\"{s}\""));

                Console.WriteLine($"[{pair.Key[0]}, {pair.Key[1]}]: [{courses}]");
            }

            Console.WriteLine("\n\n\n");
        }

        public static List<Kvp>  FindPairs(string[][] studentCourses)
        {

            var studentMap = studentCourses
                .Select(course => new KeyValuePair<string, string>(course[0], course[1]))
                .GroupBy(kvp => kvp.Key)
                .ToDictionary(group => group.Key, group => group.Select(kvp => kvp.Value).ToList());

            //find pairs
            var result = new List<Kvp>();
            var students = studentMap.Keys.ToArray();
            for(int cnt = 0; cnt < students.Length; cnt++)
            {
                for(int cntt = cnt+1; cntt < students.Length; cntt++)
                {
                    var studentPair = new[] 
                    { 
                        int.Parse(students[cnt]), 
                        int.Parse(students[cntt])
                    };

                    var commonCourses = studentMap[students[cnt]]
                        .Intersect(studentMap[students[cntt]])
                        .ToArray();

                    result.Add(new Kvp(studentPair, commonCourses));
                }
            }

            return result;
        }
    }
}
