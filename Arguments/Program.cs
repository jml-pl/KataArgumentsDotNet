using System;
using System.Collections.Generic;

namespace Arguments
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Args arg = new Args("l,p#,d*", args);
                bool logging = arg.getBool('l');
                int port = arg.getInt('p');
                String directory = arg.getString('d');
                executeApplication(logging, port, directory);
            }
            catch (ArgsException e)
            {
                Console.WriteLine("Argument error: {0}\n", e.errorCode);
            }


        }

        private static void executeApplication(bool logging, int port, String directory)
        {
            Console.WriteLine("logging is {0}, port:{1}, directory:{2}\n", logging, port, directory);
        }

    }
}
