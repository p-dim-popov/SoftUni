using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> data = Console
                .ReadLine()
                .Split(" ")
                .ToList();
            while (true)
            {
                string[] inputCommands = Console.ReadLine().Split().ToArray();
                string command = inputCommands[0];
                if (command == "3:1")
                    break;

                switch (command)
                {
                    case "merge":
                        int startIndex = int.Parse(inputCommands[1]);
                        int endIndex = int.Parse(inputCommands[2]);
                        Merge(startIndex, endIndex, data);
                        break;
                    case "divide":
                        int index = int.Parse(inputCommands[1]);
                        int partitions = int.Parse(inputCommands[2]);
                        data = Divide(index, partitions, data);
                        break;
                }

            }
            Console.WriteLine(String.Join(" ", data));
        }

        static void Merge(int start, int end, List<string> data)
        {
            List<string> temp = new List<string>();
            for (int i = 0; i < end - start + 1; i++)
            {
                if (start + i + 1 > data.Count)
                    break;
                temp.Add(data[start + i]);
            }
            data.Insert(start, String.Join("", temp));
            for (int i = 0; i < end - start + 1; i++)
            {
                if (start + i > data.Count)
                    break;
                data.RemoveAt(start + 1);
            }
        }
        static List<string> Divide(int index, int partitions, List<string> data)
        {
            char[] fraction = data[index].ToCharArray();
            List<string> temp = new List<string>();

            if (fraction.Length%partitions == 0)
            {
                int j = 0;
                for (int i = 0; i < partitions; i++)
                {
                    List<string> tempAdd = new List<string>();
                    for (; j < fraction.Length / partitions; j++)
                    {
                        tempAdd.Add(fraction[j].ToString());
                    }

                    data.Insert(index + i, String.Join("", tempAdd));
                    data.RemoveAt(index + i + 1);
                }
            }
            else if (fraction.Length%partitions > 1)
            {
                int i = 0;
                int j = 0;
                for (; i < partitions - 1; i++)
                {
                    List<string> tempAdd = new List<string>();
                    
                    for (; j < fraction.Length / partitions; j++)
                    {
                        tempAdd.Add(fraction[j].ToString());
                    }

                    data.Insert(index + i, String.Join("", tempAdd));
                    data.RemoveAt(index + i + 1);
                }

                List<string> lastTempAdd = new List<string>();

                for (; j < fraction.Length%partitions; j++)
                {
                    lastTempAdd.Add(fraction[j].ToString());
                }
                data.Insert(index + i, String.Join("", lastTempAdd));
                data.RemoveAt(index + i + 1);
            }
            else return data;

            return data;
        }
    }
}