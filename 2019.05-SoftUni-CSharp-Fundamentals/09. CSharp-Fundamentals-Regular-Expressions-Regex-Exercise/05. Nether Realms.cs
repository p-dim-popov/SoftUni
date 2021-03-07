using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Program
{
    class Program
    {
        static void Main()
        {
            var names = Console.ReadLine()
                .Split(new char[] { ',', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            var dict = new Dictionary<string, string[]>();
            foreach (var name in names)
            {
                int demonHealth = 0;
                var tempHealth = Regex.Matches(name, @"[^\d\+\-\*\/\.\s]")
                    .Select(x => demonHealth += char.Parse(x.Value))
                    .ToArray();
                decimal demonDamage = 0;
                var tempDamage = Regex.Matches(name, @"[\-\+]?[\d]+(?:[\.]*[\d]+|[\d]*)")
                    .Select(x => demonDamage += decimal.Parse(x.Value))
                    .ToArray();
                foreach(var ch in name.Where(c => c.Equals('*') || c.Equals('/')))
                {
                    if(ch.Equals('*'))
                    {
                        demonDamage *= 2;
                    }
                    if(ch.Equals('/'))
                    {
                        demonDamage /= 2;
                    }
                }
                dict[name] = new string[] {$"{demonHealth}", $"{demonDamage:f2}"};
            }
            dict = dict
                .OrderBy(x => x.Key)
                .ToDictionary(x => x.Key, y => y.Value);
            foreach(var kvp in dict)
            {
                Console.WriteLine($"{kvp.Key} - {kvp.Value[0]} health, {kvp.Value[1]} damage");
            }
        }
    }
}