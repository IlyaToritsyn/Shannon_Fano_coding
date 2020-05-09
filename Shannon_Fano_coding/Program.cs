﻿using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите строку:");

        string str = Console.ReadLine();

        Console.WriteLine();

        string a = ShennonFano(str);

        Console.WriteLine(a + "\n");
        Console.WriteLine("Нажмите любую клавишу, чтобы закрыть программу.");
        Console.ReadKey();
    }

    private static string ShennonFano(string s)
    {
        Dictionary<char, float> prob = new Dictionary<char, float>();

        Dictionary<char, string> code = new Dictionary<char, string>();

        string diffChars = GetDifferentChars();
        string answer = string.Empty;

        foreach (char c in diffChars)
        {
            code.Add(c, string.Empty);
        }

        foreach (char c in diffChars)
        {
            prob.Add(c, GetProbability(c));
        }

        prob = prob.OrderByDescending(c => c.Value).ToDictionary(c => c.Key, c => c.Value);

        GetCodes(prob);

        foreach (KeyValuePair<char, string> k in code)
        {
            Console.WriteLine(k.Key + "-" + k.Value);
        }

        Console.WriteLine();

        foreach (char c in s)
        {
            answer += code[c];
        }

        return answer;

        float GetProbability(char c)
        {
            return s.Count(n => n == c) / (float)s.Length;
        }

        string GetDifferentChars()
        {
            string r = string.Empty;

            foreach (char c in s)
            {
                if (!r.Contains(c))
                {
                    r += c;
                }
            }

            return r;
        }

        int GetMinDifference(Dictionary<char, float> p)
        {
            if (p.Count == 2)
            {
                return 1;
            }

            int k;
            int count = p.Count;

            float min = float.MaxValue;

            for (k = 1; k < count; k++)
            {
                if (Math.Abs(p.Take(k).ToDictionary(c => c.Key, c => c.Value).Values.Sum() - p.Skip(k).ToDictionary(c => c.Key, c => c.Value).Values.Sum()) < min)
                {
                    min = Math.Abs(p.Take(k).ToDictionary(c => c.Key, c => c.Value).Values.Sum() - p.Skip(k).ToDictionary(c => c.Key, c => c.Value).Values.Sum());
                }

                else
                {
                    break;
                }
            }

            return k - 1;
        }

        void GetCodes(Dictionary<char, float> v)
        {
            int count = GetMinDifference(v);

            Dictionary<char, float> d1 = v.Take(count).ToDictionary(c => c.Key, c => c.Value);
            Dictionary<char, float> d2 = v.Skip(count).ToDictionary(c => c.Key, c => c.Value);

            foreach (KeyValuePair<char, float> b in d1)
            {
                code[b.Key] += '1';
            }

            foreach (KeyValuePair<char, float> b in d2)
            {
                code[b.Key] += '0';
            }

            if (d1.Count > 1)
            {
                GetCodes(d1);
            }

            if (d2.Count > 1)
            {
                GetCodes(d2);
            }
        }
    }
}