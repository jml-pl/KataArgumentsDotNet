using System;
using System.Collections.Generic;
using System.Text;

namespace Arguments
{
   public class Args
    {

        Dictionary<char, IArgumentMarshaler> marshalers = new Dictionary<char, IArgumentMarshaler>();
        HashSet<char> argsFound = new HashSet<char>();
        public int currentArgument { get; set; }

        public Args(string schema, string[] args)
        {
            parseSchema(schema);
            parseArgs(args);
        }
        void parseSchema(string schema)
        {
            foreach (string s in schema.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!char.IsLetter(s[0]))
                    throw new ArgsException(ArgsException.ErrorCode.INVALID_ARGUMENT_NAME, s[0]);
                try
                {
                    marshalers.Add(s[0], ArgumentMarshalerFactory.Build(s.Substring(1)));
                }
                catch (IndexOutOfRangeException)
                {
                    throw new ArgsException(ArgsException.ErrorCode.INVALID_ARGUMENT_NAME, s[0]);
                }
                catch (ArgsException e)
                {
                    throw new ArgsException(e.errorCode, s[0], e.errorParameter);
                }
            }
        }
        
        void parseArgs(string[] args)
        {
            for (currentArgument = 0; currentArgument < args.Length; currentArgument++)
            {
                string flag = args[currentArgument];
                try
                {
                    if (!isFlag(flag))
                        break;
                    if (isMultiFlag(flag))
                        parseMultiFlag(flag.Substring(1));
                    else
                    {
                        currentArgument += parseFlag(flag[1], args, currentArgument);
                    }
                }
                catch (KeyNotFoundException)
                {
                    throw new ArgsException(ArgsException.ErrorCode.UNEXPECTED_ARGUMENT, flag[1]);
                }
                catch (ArgsException e)
                {
                    throw new ArgsException(e.errorCode, flag[1], e.errorParameter);
                }
            }
        }

        public bool isFlag(string flag)
        {
            return (flag[0] == '-');
        }

        public bool isMultiFlag(string flag)
        {
            return (flag.Length  > 2);
        }

        public void parseMultiFlag(string flags)
        {
            foreach (char flag in flags)
            {
                parseFlag(flag);
            }
        }

        public int parseFlag(char flag)
        {
            return parseFlag(flag, null, 0);
        }

        public int parseFlag(char flag, string[] args, int position)
        {
            argsFound.Add(flag);
            return marshalers[flag].setValue(args, position);
        }


        public bool has(char arg)
        {
            return argsFound.Contains(arg);
        }
        public object getValue(char arg)
        {
            return marshalers[arg].getValue();
        }

        public bool getBool(char arg)
        {
            return (bool) getValue(arg);
        }
        public string getString(char arg)
        {
            return (string) getValue(arg);
        }

        public int getInt(char arg)
        {
            return (int)getValue(arg);
        }

        public double getDouble(char arg)
        {
            return (double) getValue(arg);
        }

        public string[] getStringArray(char arg)
        {
            return (string[]) getValue(arg);
        }

        public Dictionary<string,string> getMap (char arg)
        {
            return (Dictionary<string, string>) getValue(arg);
        }

    }
}
