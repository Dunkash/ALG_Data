using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;

namespace ALG_Word
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input path to the txt file, containing rules");
            Console.WriteLine("Write EXIT any time to exit application");
            var input = "";
            while (!File.Exists(input))
            {
                input = Console.ReadLine();
                if (input == "EXIT")
                    Environment.Exit(0);
                if (!File.Exists(input))
                    Console.WriteLine("Directory doesn't exist");
            }
            var rules = new Rulesheet(input);
            
            Console.WriteLine("\nAlphabet of this task:");
            Console.Write("{");
            for (var i=0;i<rules.alphabet.Length;i++)
            {
                Console.Write(rules.alphabet[i]);
                Console.Write(";");
            }
            Console.Write("}\n");
            Console.Write("\n");

            Console.WriteLine("Rules of this task:");
            foreach(var i in rules.Rules)
            {
                Console.Write(i.Key + "=>");
                if (i.Value.Second)
                    Console.Write(".");
                Console.Write(i.Value.First+";\n");
            }
            Console.Write("\n");

            while (input!="EXIT")
            {
                here: Console.WriteLine("Write word to apply substitutions. Write \"EXIT\" to end session. Write \"CAP\" to set iteration cap ");
                input = Console.ReadLine();
                Console.WriteLine();
                if (input=="CAP")
                {
                    Console.WriteLine("Input maximum number of iterations");
                    var cap = Console.ReadLine();
                    int newcap=0;
                    if (int.TryParse(cap, out newcap))
                        rules.IterationCap = newcap;
                    else
                        Console.WriteLine("Incorrect input");
                    goto here;
                }
                foreach (var i in input)
                {
                    if (!rules.alphabet.ToList().Contains(Char.ToString(i)))
                    { 
                        Console.WriteLine("Word contains symbols, not supported by alphabet \n");
                        goto here;
                    }

                }
                rules.Apply(input);
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}

/*
 var rules = new Dictionary<string, Pair<string, bool>>
            {
                {"a",new Pair<string,bool>{First="bb",Second = false } },
                {"b",new Pair<string,bool>{First="E",Second = false }}
            };
*/

