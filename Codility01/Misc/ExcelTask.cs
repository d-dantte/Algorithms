using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Codility01.Misc
{
    using InputData = KeyValuePair<string, string>;
    using OutputData = KeyValuePair<string, int>;

    public class ExcelTask
    {

        public static OutputData[] Sum(string csv, string[] urls)
        {
            var input = new List<InputData>();

            var finfo = new FileInfo(csv);
            string line = null;
            var isFirstLine = true;

            using (var reader = new StreamReader(finfo.OpenRead()))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (!isFirstLine)
                    {
                        var fields = line.Split(',');
                        input.Add(new InputData(fields[0].Unwrap("\""), fields[1]));
                    }

                    else isFirstLine = false;
                }
            }

            var patterns = urls
                .Select(url => new { Url = url, Pattern = url.Replace("*", ".*") })
                .Select(map => new { map.Url, map.Pattern, Regex = new Regex(map.Pattern) })
                .ToArray();

            var output = input.Aggregate(new Dictionary<string, int>(), (sums, inputMap) =>
            {
                var map = patterns.FirstOrDefault(patternMap => patternMap.Regex.IsMatch(inputMap.Key));
                if (map != null)
                {
                    if (sums.TryGetValue(map.Url, out var sum))
                        sum += int.Parse(inputMap.Value);

                    else sum = int.Parse(inputMap.Value);

                    sums[map.Url] = sum;
                }

                return sums;
            });

            return output.ToArray();
        }
    }
}
