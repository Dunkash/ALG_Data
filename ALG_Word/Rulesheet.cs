using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace ALG_Word
{

    public class Pair<T, K>
    {
        public T First { get; set; }
        public K Second { get; set; }
    }

    class Rulesheet
    {
        public string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        public int IterationCap = 1000;
        public string[] alphabet;
        public Dictionary<string, Pair<string, bool>> Rules;

        public Rulesheet(Dictionary<string, Pair<string, bool>> r)
        {
            Rules = r;
            if (Rules.Keys.Contains("E"))
            {
                var temp = Rules["E"];
                Rules.Remove("E");
                Rules["\b"] = temp;
            }

        }

        public Rulesheet(string file)
        {
            Rules = new Dictionary<string, Pair<string, bool>>();

            var lines = File.ReadAllLines(file);
            alphabet = lines[0].Split(';');
            for (int i=1; i<lines.Length;i++)
            {
                var temp = lines[i].Split('>');
                string from = temp[0];
                bool stop = (temp[1].First() == '.');
                string to;
                if (stop)
                    to = temp[1].Substring(1);
                else
                    to = temp[1];
                Rules.Add(from,new Pair<string, bool> {First=to,Second=stop});
            }
            if (Rules.Keys.Contains("E"))
            {
                var temp = Rules["E"];
                Rules.Remove("E");
                Rules[""] = temp;
            }
        }
        public string Apply(string s)
        {
            Console.Write(s);
            int iteration = 0;
            start: while (true)
            {
                foreach (var rule in Rules)
                {
                    if (s.IndexOf(rule.Key)!=-1)
                    {
                        if (rule.Key == "")
                            s = rule.Value.First + s;
                        else if (rule.Value.First != "E")
                            s = ReplaceFirst(s, rule.Key, rule.Value.First);
                        else s = ReplaceFirst(s, rule.Key, "");

                        if (s != "") Console.Write("=>" + s);
                        else Console.Write("=>" + "E");
                            
                        if (rule.Value.Second)
                        {
                            Console.Write(";");
                            return s;
                        }
                        iteration++;
                        if (iteration > IterationCap)
                        { 
                            Console.Write(";\nOperations cap reached. Ending execution.");
                            return "";
                        }

                        goto start;
                    }
                }
                Console.Write(";");
                return s;
            }
        }
    }
}
