using System;
using System.Collections.Generic;

namespace Arguments
{
    public class Program
    {
        public static int Main(string[] args)
        {
            //-l - p 8080 - d / usr / logs

            //-l -p 1 -d s
            //string schmat = "-l -p 1 -d s";
            string schmat = "-p 2-10 -d s-100";
            string domylsne = "-p 777 -d super";
            string[] schmatTablica = schmat.Split(' ', StringSplitOptions.None);


            Dictionary<string, string> flagsSchema = CheckArgs(schmatTablica);
            Dictionary<string, string> flags = CheckArgs(args);

            foreach (string flag in flags.Keys)
            {
                if (!flagsSchema.ContainsKey(flag))
                {

                    Console.WriteLine("Not expected flag: " + flag);
                    Console.WriteLine("Usage: " + schmat);
                    return -1;
                } else
                {
                    if (flagsSchema[flag] != null)
                    {
                        //typ do sprawdzenia
                        if (flags[flag] != null)
                        {
                            //wymaga i jest wartosc dla flagi sprawdzmay typ
                            if (flagsSchema[flag] == "1")
                            {
                                int i;
                                if (!(int.TryParse(flags[flag], out i))){
                                    Console.WriteLine("Numeric argument expected for flag: " + flag);
                                    Console.WriteLine("Usage: " + schmat);
                                }
                            }
                        }
                        else
                        {
                            //nie podan
                            //moze jest odmylne
                            Console.WriteLine("Expected argument not provided for flag: " + flag);
                            Console.WriteLine("Usage: " + schmat);
                        }

                    } else
                    {
                        //flags schema null
                        if (flags[flag] != null)
                        {
                            Console.WriteLine("Not expected argument for flag: " + flag);
                            Console.WriteLine("Usage: " + schmat);
                        }

                    }
                }


            }

            return 0;
        }

        public static Dictionary<string, string> CheckArgs(string[] args)
        {

            Dictionary<string, string> flags = new Dictionary<string, string>();

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith("-") && args[i].Length == 2 && Char.IsLetter(args[i][1]))
                {

                    if (i + 1 < args.Length)
                    {
                        //to mmay kolejny 
                        if (!(args[i + 1].StartsWith("-") && args[i + 1].Length == 2 && Char.IsLetter(args[i + 1][1])))
                        {
                            //czyli nastepny nie jest flaga
                            flags.Add(args[i], args[i + 1]);
                            i++;
                        }
                        else
                        {
                            flags.Add(args[i], null);
                        }

                    }

                }
            }
            return flags;

        }

    }
}
