using System;
using System.Collections.Generic;
using System.Text;

namespace Arguments
{
    class MapArgumentMarshaler : IArgumentMarshaler
    {

        Dictionary<string,string> value = new Dictionary<string, string> ();
        public Dictionary<string, string> getDictionary()
        {
            return value;
        }

        public object getValue()
        {
            return value;
        }
        public void setValue(string value)
        {
            try
            {
                foreach (string s in value.Split(','))
                {
                    string[] pair = s.Split(':');
                    this.value.Add(pair[0], pair[1]);
                }
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgsException(ArgsException.ErrorCode.MALFORMED_MAP, value);
            }
        }

        public int setValue(string[] args, int position)
        {
            try
            {
                this.setValue(args[position + 1]);
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgsException(ArgsException.ErrorCode.MISSING_MAP);
            }
            return 1;
        }

    }
}
