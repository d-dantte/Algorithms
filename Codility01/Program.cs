using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility01
{
    class Program
    {
        static void xMain(string[] args)
        {

            var now = DateTimeOffset.Now;
            Console.WriteLine(Solution2A(10, 2000));
            Console.WriteLine(Solution2A(6000, 700000));
            Console.WriteLine($"Solution 2 Done in: {DateTimeOffset.Now - now}\n\n\n");

            now = DateTimeOffset.Now;
            Console.WriteLine(Solution2B(10, 2000));
            Console.WriteLine(Solution2B(6000, 700000));
            Console.WriteLine($"Solution 3 Done in: {DateTimeOffset.Now - now}\n\n\n");

            now = DateTimeOffset.Now;
            Console.WriteLine(Solution2A(10, 2000));
            Console.WriteLine(Solution2A(6000, 700000));
            Console.WriteLine($"Solution 2 Done in: {DateTimeOffset.Now - now}\n\n\n");

            now = DateTimeOffset.Now;
            Console.WriteLine(Solution2B(10, 2000));
            Console.WriteLine(Solution2B(6000, 700000));
            Console.WriteLine($"Solution 3 Done in: {DateTimeOffset.Now - now}\n\n\n");

            now = DateTimeOffset.Now;
            Console.WriteLine(Solution2A(10, 2000));
            Console.WriteLine(Solution2A(6000, 700000));
            Console.WriteLine($"Solution 2 Done in: {DateTimeOffset.Now - now}\n\n\n");

            now = DateTimeOffset.Now;
            Console.WriteLine(Solution2B(10, 2000));
            Console.WriteLine(Solution2B(6000, 700000));
            Console.WriteLine($"Solution 3 Done in: {DateTimeOffset.Now - now}\n\n\n");

            now = DateTimeOffset.Now;
            Console.WriteLine(Solution2A(10, 2000));
            Console.WriteLine(Solution2A(6000, 700000));
            Console.WriteLine($"Solution 2 Done in: {DateTimeOffset.Now - now}\n\n\n");

            now = DateTimeOffset.Now;
            Console.WriteLine(Solution2B(10, 2000));
            Console.WriteLine(Solution2B(6000, 700000));
            Console.WriteLine($"Solution 3 Done in: {DateTimeOffset.Now - now}\n\n\n");

            now = DateTimeOffset.Now;
            Console.WriteLine(Solution2A(10, 2000));
            Console.WriteLine(Solution2A(6000, 700000));
            Console.WriteLine($"Solution 2 Done in: {DateTimeOffset.Now - now}\n\n\n");

            now = DateTimeOffset.Now;
            Console.WriteLine(Solution2B(10, 2000));
            Console.WriteLine(Solution2B(6000, 700000));
            Console.WriteLine($"Solution 3 Done in: {DateTimeOffset.Now - now}\n\n\n");

            now = DateTimeOffset.Now;
            Console.WriteLine(Solution2A(10, 2000));
            Console.WriteLine(Solution2A(6000, 700000));
            Console.WriteLine($"Solution 2 Done in: {DateTimeOffset.Now - now}\n\n\n");

            now = DateTimeOffset.Now;
            Console.WriteLine(Solution2B(10, 2000));
            Console.WriteLine(Solution2B(6000, 700000));
            Console.WriteLine($"Solution 3 Done in: {DateTimeOffset.Now - now}\n\n\n");

            now = DateTimeOffset.Now;
            Console.WriteLine(Solution2A(10, 2000));
            Console.WriteLine(Solution2A(6000, 700000));
            Console.WriteLine($"Solution 2 Done in: {DateTimeOffset.Now - now}\n\n\n");

            now = DateTimeOffset.Now;
            Console.WriteLine(Solution2B(10, 2000));
            Console.WriteLine(Solution2B(6000, 700000));
            Console.WriteLine($"Solution 3 Done in: {DateTimeOffset.Now - now}\n\n\n");

            now = DateTimeOffset.Now;
            Console.WriteLine(Solution2A(10, 2000));
            Console.WriteLine(Solution2A(6000, 700000));
            Console.WriteLine($"Solution 2 Done in: {DateTimeOffset.Now - now}\n\n\n");

            now = DateTimeOffset.Now;
            Console.WriteLine(Solution2B(10, 2000));
            Console.WriteLine(Solution2B(6000, 700000));
            Console.WriteLine($"Solution 3 Done in: {DateTimeOffset.Now - now}\n\n\n");

            now = DateTimeOffset.Now;
            Console.WriteLine(Solution2A(10, 2000));
            Console.WriteLine(Solution2A(6000, 700000));
            Console.WriteLine($"Solution 2 Done in: {DateTimeOffset.Now - now}\n\n\n");

            now = DateTimeOffset.Now;
            Console.WriteLine(Solution2B(10, 2000));
            Console.WriteLine(Solution2B(6000, 700000));
            Console.WriteLine($"Solution 3 Done in: {DateTimeOffset.Now - now}\n\n\n");

            now = DateTimeOffset.Now;
            Console.WriteLine(Solution2A(10, 2000));
            Console.WriteLine(Solution2A(6000, 700000));
            Console.WriteLine($"Solution 2 Done in: {DateTimeOffset.Now - now}\n\n\n");

            now = DateTimeOffset.Now;
            Console.WriteLine(Solution2B(10, 2000));
            Console.WriteLine(Solution2B(6000, 700000));
            Console.WriteLine($"Solution 3 Done in: {DateTimeOffset.Now - now}\n\n\n");

            now = DateTimeOffset.Now;
            Console.WriteLine(Solution2A(10, 2000));
            Console.WriteLine(Solution2A(6000, 700000));
            Console.WriteLine($"Solution 2 Done in: {DateTimeOffset.Now - now}\n\n\n");

            now = DateTimeOffset.Now;
            Console.WriteLine(Solution2B(10, 2000));
            Console.WriteLine(Solution2B(6000, 700000));
            Console.WriteLine($"Solution 3 Done in: {DateTimeOffset.Now - now}\n\n\n");

            now = DateTimeOffset.Now;
            Console.WriteLine(Solution2A(10, 2000));
            Console.WriteLine(Solution2A(6000, 700000));
            Console.WriteLine($"Solution 2 Done in: {DateTimeOffset.Now - now}\n\n\n");

            now = DateTimeOffset.Now;
            Console.WriteLine(Solution2B(10, 2000));
            Console.WriteLine(Solution2B(6000, 700000));
            Console.WriteLine($"Solution 3 Done in: {DateTimeOffset.Now - now}\n\n\n");

            Console.ReadKey();
        }


        static void xxMain(string[] args)
        {
            Console.Write("Enter path:");
            var path = Console.ReadLine();

            RenameFilesGrandeur(path);
        }

        static void xxxMain(string[] args)
        {
            var challenge = new LeftmostSubstring.Challenge();

            int A = 53;
            int B = 1953786;
            var result = challenge.Solution(A, B);
            Console.WriteLine($"A [{A}], B [{B}], result [{result}]");

            A = 57;
            B = 153786;
            result = challenge.Solution(A, B);
            Console.WriteLine($"A [{A}], B [{B}], result [{result}]");

             A = 78;
             B = 195378678;
            result = challenge.Solution(A, B);
            Console.WriteLine($"A [{A}], B [{B}], result [{result}]");


            Console.ReadKey();
        }

        static void JaggedArrayMain(string[] args)
        {
            string[][] studentCoursePairs1 =
            {
              new string[] {"58", "Linear Algebra"},
              new string[] {"94", "Art History"},
              new string[] {"17", "Software Design"},
              new string[] {"58", "Mechanics"},
              new string[] {"58", "Economics"},
              new string[] {"17", "Linear Algebra"},
              new string[] {"17", "Political Science"},
              new string[] {"94", "Economics"},
              new string[] {"25", "Economics"},
              new string[] {"58", "Software Design"}
            };

            var pairs = JaggedArray.Challenge.FindPairs(studentCoursePairs1);
            JaggedArray.Challenge.Output(pairs);

            string[][] studentCoursePairs2 =
            {
              new string[] {"42", "Software Design"},
              new string[] {"0", "Advanced Mechanics"},
              new string[] {"9", "Art History"},
            };

            pairs = JaggedArray.Challenge.FindPairs(studentCoursePairs2);
            JaggedArray.Challenge.Output(pairs);


            Console.ReadKey();
        }


        static void MiscInvoker()
        {
            var input = new[]
            {
                "/api/v2/content/documentgroups/*",
                "/api/v1/content/groups/*",
                "/api/v1/currentcustomer/",
                "/api/v1/currentcustomer/*",
                "/api/v1/pagecontent",
                "/api/v1/gamecollections",
                "/api/v1/game-providers",
                "/api/v1/games/?",
                "/api/v1/games/[^/]+",
            };

            var csv = "C:/Betsson/Tasks/OBGAPI-Support-Week45/3months";


            var output = Misc.ExcelTask.Sum($"{csv}.csv", input);

            var finfo = new FileInfo($"{csv}-output.csv");
            using(var writer = new StreamWriter(finfo.OpenWrite()))
            {
                output
                    .ToList()
                    .ForEach(map => writer.WriteLine($"{map.Key}, {map.Value}"));

                writer.Flush();
            }


            Console.WriteLine("Done");
        }


        static void LargestMatrixMain(string[] args)
        {
            var array1 = new List<List<int>>
            {
                new List<int>{ 0, 1, 1 },
                new List<int>{ 1, 1, 0},
                new List<int>{ 1, 0, 1}
            };

            var array2 = new List<List<int>>
            {
                new List<int>{ 1, 1, 1, 1, 1},
                new List<int>{ 1, 1, 1, 0, 0},
                new List<int>{ 1, 1, 1, 0, 0},
                new List<int>{ 1, 1, 1, 0, 0},
                new List<int>{ 1, 1, 1, 1, 1}
            };

            var result = LargestMatrix.Challenge.largestMatrix(array1);
            Console.WriteLine($"Largest matrix: {result}");

            result = LargestMatrix.Challenge.largestMatrix(array2);
            Console.WriteLine($"Largest matrix: {result}");
        }

        static void PoisonIntervalMain(string[] args)
        {
            var a = new List<int> { 1, 2 };
            var p = new List<int> { 3, 4 };
            var n = 4;
            var result = PoisonInterval.Challenge.bioHazard(n, a, p);
            Console.WriteLine($"Largest interval for n({n}): {result}");

            a = new List<int> { 1, 2 };
            p = new List<int> { 3, 5 };
            n = 5;
            result = PoisonInterval.Challenge.bioHazard(n, a, p);
            Console.WriteLine($"Largest interval for n({n}): {result}");

            a = new List<int> { 2, 3, 4, 3 };
            p = new List<int> { 8, 5, 6, 4 };
            n = 8;
            result = PoisonInterval.Challenge.bioHazard(n, a, p);
            Console.WriteLine($"Largest interval for n({n}): {result}");

            a = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8};
            p = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8};
            n = 8;
            result = PoisonInterval.Challenge.bioHazard(n, a, p);
            Console.WriteLine($"Largest interval for n({n}): {result}");

            a = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            p = new List<int> { 2, 3, 4, 5, 6, 7, 8, 1 };
            n = 8;
            result = PoisonInterval.Challenge.bioHazard(n, a, p);
            Console.WriteLine($"Largest interval for n({n}): {result}");

            a = new List<int> { 1 };
            p = new List<int> { 1 };
            n = 8;
            result = PoisonInterval.Challenge.bioHazard(n, a, p);
            Console.WriteLine($"Largest interval for n({n}): {result}");

            a = new List<int> { 8 };
            p = new List<int> { 1 };
            n = 8;
            result = PoisonInterval.Challenge.bioHazard(n, a, p);
            Console.WriteLine($"Largest interval for n({n}): {result}");

            a = new List<int> { 1 };
            p = new List<int> { 1 };
            n = 1;
            result = PoisonInterval.Challenge.bioHazard(n, a, p);
            Console.WriteLine($"Largest interval for n({n}): {result}");


            #region long values
            var values = @"50
47
8
24
31
45
24
41
38
28
41
4
10
11
50
17
48
13
30
30
18
44
36
29
45
2
9
19
25
31
23
50
28
29
49
36
16
14
26
16
30
18
6
2
39
6
3
17
39
47
50
9
23
29
10
16
4
30
13
20
8
34
29
36
27
18
50
22
23
37
46
42
8
4
45
41
47
4
17
25
9
34
32
14
15
37
20
45
2
46
5
49
24
33
17
38
45";
            #endregion
            PoisonInterval.Challenge.Read(values, out n, out a, out p);
            result = PoisonInterval.Challenge.bioHazard(n, a, p);
            Console.WriteLine($"Largest interval for n({n}): {result}");

            #region long values
            values = @"510
129
116
357
436
474
188
38
351
292
428
196
459
127
260
442
122
81
335
190
76
240
206
487
473
209
352
129
228
40
462
455
446
421
14
341
166
256
297
88
85
271
423
178
252
226
210
470
254
418
453
397
275
207
25
346
429
394
327
127
452
145
268
312
413
468
159
64
395
91
501
418
71
157
39
497
118
75
381
209
286
386
73
404
178
330
94
34
506
50
171
493
284
131
112
104
251
459
147
95
440
185
473
72
110
281
284
332
429
150
392
265
309
352
321
472
294
352
391
495
334
6
64
258
91
376
212
457
324
186
303
129
69
343
332
330
454
396
123
469
452
266
126
328
65
307
203
285
246
398
494
231
256
64
357
460
295
379
382
64
472
30
300
80
275
478
238
441
45
259
321
497
335
495
479
339
250
484
168
444
166
504
281
331
141
227
196
228
249
78
39
220
324
221
194
322
370
388
49
425
506
321
376
368
181
446
414
414
175
178
160
384
128
470
328
76
364
139
429
97
15
484
358
31
161
471
184
204
11
411
213
260
150
121
104
1
51
400
247
481
387
505
247
505
279
381
412
66
89
510
111
303
299
509
235
98
181
261
334
111
204";
            #endregion
            PoisonInterval.Challenge.Read(values, out n, out a, out p);
            result = PoisonInterval.Challenge.bioHazard(n, a, p);
            Console.WriteLine($"Largest interval for n({n}): {result}");
        }

        static void StockFinderMain(string[] args)
        {
            var first = "1-January-2000";
            var last = "22-February-2000";
            var day = "Monday";
            StockQuery.Challenge.openAndClosePrices(first, last, day);

            //first = "";
            //last = "";
            //day = "";
            //StockQuery.Challenge.openAndClosePrices(first, last, day);
        }

        static void DijkstraMain(string[] args)
        {
            //values
            var testValues = new List<List<int>>
            {
                new List<int>{ 1, 2, 5 },
                new List<int>{ 1, 3, 2 },
                new List<int>{ 3, 4, 1 },
                new List<int>{ 1, 4, 6 },
                new List<int>{ 3, 5, 5 },
                new List<int>{ 10, 11, 2 }
            };

            //build edges
            var edges = testValues
                .Select(list => new ShortestPath.WeightedEdge(
                    start: list[0],
                    end: list[1],
                    weight: list[2]))
                .ToList();

            new[] {2, 3, 4, 10}
                .Select(vertex => ShortestPath.Challenge.DijkistraShortestDistance(edges, 1, vertex))
                .ToList()
                .ForEach(distance => Console.WriteLine(distance));
        }
        

        static void IntParserMain(string[] args)
        {
            var result = IntParser.Challenge.MyAtoi("-2147483649");
            Console.WriteLine(result);
            result = IntParser.Challenge.MyAtoi(int.MaxValue.ToString());
            Console.WriteLine(result);
            result = IntParser.Challenge.MyAtoi("0000000");
            Console.WriteLine(result);
            result = IntParser.Challenge.MyAtoi("");
            Console.WriteLine(result);
            result = IntParser.Challenge.MyAtoi("  0000000000012345678");
            Console.WriteLine(result);

        }


        static void DictionaryMain(string[] args)
        {
            var sentence = "every non sense exists".Replace(" ", "");
            var dictionary = new List<string>
            {
                "every",
                "nonsense",
                "non",
                "exists",
            };

            WordDictionary.Challenge.FindString(sentence, dictionary);
        }

        static void ServerUpdateMain(string[] args)
        {

            var x = new ServerUpdate.Challenge();
            var minDays = x.minimumDays(5, 5, new int[5, 5]
            {
                { 1, 0, 0, 0, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 0, 0, 1, 0 },
                { 0, 0, 0, 0, 1 }
            });

            Console.WriteLine("Minimum days to update servers: " + minDays);
        }

        static void LongestPalindromeMain(string[] args)
        {
            var palindrome = LongestPalindrome.Challenge.LongestPalindrome("babad");
            Console.WriteLine(palindrome);

            palindrome = LongestPalindrome.Challenge.LongestPalindrome("abb");
            Console.WriteLine(palindrome);
        }

        static void MergeKListsMain(string[] args)
        {
            var input = MergeSortedLists.Challenge.ToLists(new int [][]
            {
                new []{ 1, 4, 5 },
                new []{ 1, 3, 4},
                new []{ 2, 6}
            });
            var output = MergeSortedLists.Challenge.MergeKLists(input);
            Console.WriteLine(output.Output());

            input = MergeSortedLists.Challenge.ToLists(new int[][]
            {
                new []{ 1, 2, 2 },
                new []{ 1, 1, 2}
            });
            output = MergeSortedLists.Challenge.MergeKLists(input);
            Console.WriteLine(output.Output());
        }

        static void SumTargetMain(string[] args)
        {
            var output = SumTarget.Challenge.CombinationSum(
                new[] { 2, 3, 6, 7 },
                7);

            output?
                .ToList()
                .ForEach(list => Console.WriteLine(string.Join(", ", list.ToArray())));
            Console.WriteLine("\n\n");


            output = SumTarget.Challenge.CombinationSum(
                 new[] { 2, 3, 5 },
                 8);

            output?
                .ToList()
                .ForEach(list => Console.WriteLine(string.Join(", ", list.ToArray())));
            Console.WriteLine("\n\n");


            output = SumTarget.Challenge.CombinationSum(
                 new[] { 2, 3, 5 },
                 7);

            output?
                .ToList()
                .ForEach(list => Console.WriteLine(string.Join(", ", list.ToArray())));
            Console.WriteLine("\n\n");


            output = SumTarget.Challenge.CombinationSum(
                 new[] { 1, 2 },
                 1);

            output?
                .ToList()
                .ForEach(list => Console.WriteLine(string.Join(", ", list.ToArray())));
            Console.WriteLine("\n\n");


            output = SumTarget.Challenge.CombinationSum(
                 new[] { 7, 3, 2 },
                 18);

            output?
                .ToList()
                .ForEach(list => Console.WriteLine(string.Join(", ", list.ToArray())));
            Console.WriteLine("\n\n");


            output = SumTarget.Challenge.CombinationSum(
                 new[] { 2, 3, 6, 7 },
                 7);

            output?
                .ToList()
                .ForEach(list => Console.WriteLine(string.Join(", ", list.ToArray())));
            Console.WriteLine("\n\n");


            output = SumTarget.Challenge.CombinationSum(
                 new[] { 4, 5, 2 },
                 16);

            output?
                .ToList()
                .ForEach(list => Console.WriteLine(string.Join(", ", list.ToArray())));
            Console.WriteLine("\n\n");
        }

        static void PermutationMain(string[] args)
        {
            var test = new[] { 1, 2, 3 };

            var output = Permutation.Challenge.Permutation(test);
            output?
                .ToList()
                .ForEach(list => Console.WriteLine(string.Join(", ", list.ToArray())));
            Console.WriteLine("\n");

            test = new[] { 1, 1, 2 };

            output = Permutation.Challenge.Permutation(test);
            output?
                .ToList()
                .ForEach(list => Console.WriteLine(string.Join(", ", list.ToArray())));
            Console.WriteLine("\n");

            test = new[] { 1, 2, 3, 4, 5 };

            output = Permutation.Challenge.Permutation(test);
            output?
                .ToList()
                .ForEach(list => Console.WriteLine(string.Join(", ", list.ToArray())));
            Console.WriteLine("\n");
        }

        static void WordFindMain(string[] args)
        {
            var found = WordFInder.Challenge.Exist(new[] { new[] { 'a', 'b' } }, "ba");

            Console.WriteLine(found);
        }

        static void ConnectedNetworkMain(string[] args)
        {
            var output = ConnectedEdges.Challenge.CriticalConnections(0, new List<IList<int>>
            {
                new List<int>{ 0, 1 } as IList<int>,
                new List<int>{ 1, 2 } as IList<int>,
                new List<int>{ 2, 0 } as IList<int>,
                new List<int>{ 1, 3 } as IList<int>,
                new List<int>{ 3, 4 } as IList<int>,
                new List<int>{ 4, 5 } as IList<int>,
                new List<int>{ 5, 3 } as IList<int>,
            });
            output?
                .ToList()
                .ForEach(list => Console.WriteLine(string.Join(", ", list.ToArray())));
            Console.WriteLine("\n");
        }

        static void RotateMatrix(string[] args)
        {
            var matrix = new[]
            {
                new[] {1, 2, 3, 4 },
                new[] { 5, 6, 7, 8},
                new[] {9, 10, 11, 12},
                new[] { 13, 14, 15, 16}
            };

            Console.WriteLine(MatrixRotation.Challenge.ToString(matrix));

            var x = MatrixRotation.Challenge.RotateMatrixAntiClockwise(matrix);

            Console.WriteLine(MatrixRotation.Challenge.ToString(x));
        }

        static void WordLadder(string[] args)
        {

            var result = Codility01.WordLadder.Challenge.LadderLength("hit", "cog", new List<string>
            {
                "hot",
                "dot",
                "lot",
                "dog",
                "log",
                "cog"
            });

            Console.WriteLine(result);
        }


		static void Main(string[] args)
        {
            WordLadder(args);

            Console.ReadKey();
        }


        static void RenameFilesGrandeur(string root)
        {
            var rootDir = new DirectoryInfo(root);

            if (!rootDir.Exists)
                throw new Exception("Directory does not exist: " + rootDir.FullName);

            rootDir
                .GetFiles("*.jpg", SearchOption.AllDirectories)
                .Where(finfo => finfo.Name.Contains("_full") || finfo.Name.Contains("_thumb"))
                .ToList()
                .ForEach(finfo =>
                {
                    File.Move(
                        finfo.FullName,
                        finfo.FullName.Replace("_full", "-full").Replace("_thumb", "-thumb"));
                });
        }


        /// <summary>
        /// This is the naive way of solving the question
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        static int Solution2A(int A, int B)
        {
            var small = A;
            var large = B;

            if (small == large)
                return IntegerSquarRootCount(small);

            else
            {
                var integerRootCount = 0;
                for(int value = small; value <= large; value++)
                {
                    var temp = IntegerSquarRootCount(value);
                    if (temp > integerRootCount)
                        integerRootCount = temp;
                }

                return integerRootCount;
            }
        }

        /// <summary>
        /// This is the actual solution submitted on codility, along with the functions it depends on.
        /// This one is more efficient than 2A
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        static int Solution2B(int A, int B)
        {
            var ps = FindPerfectSquare(A, B);
            if (ps == null)
                return 0;

            else
            {
                int[] oddSumInfo = LargestOddSum(ps[1]);
                
                var integerRootCount = 0;
                for (int odds = oddSumInfo[0], oddSum = oddSumInfo[1];
                    oddSum <= B; 
                    oddSum += (odds += 2))
                {
                    var temp = IntegerSquarRootCount(oddSum);
                    if (temp > integerRootCount)
                        integerRootCount = temp;
                }

                return integerRootCount;
            }
        }

        static int[] FindPerfectSquare(int value, int limit)
        {
            for(int v = value; v <= limit; v++)
            {
                var d = Math.Sqrt(v);
                if (IsInteger(d))
                    return new[] { v, (int)d };
            }

            return null;
        }

        static int[] LargestOddSum(int squareRoot)
        {
            var odd = squareRoot + (squareRoot - 1);

            var sum = 0;
            for(int cnt = odd; cnt > 0; cnt -= 2)
            {
                sum += cnt;
            }

            return new[] { odd, sum };
        }

        static bool IsInteger(double d) => Math.Floor(d) == d;

        static int IntegerSquarRootCount(int i)
        {
            int count = 0;
            double v = i;
            while(IsInteger(v = Math.Sqrt(v)))
            {
                count++;
            }

            return count;
        }
    }
}
