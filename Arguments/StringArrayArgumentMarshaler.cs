using System;
using System.Collections.Generic;
using System.Text;

namespace Arguments
{
    class StringArrayArgumentMarshaler : IArgumentMarshaler
    {

        List<string> value = new List<string>();

        public object getValue()
        {
            return value.ToArray();
        }

        public string[] getStringArray()
        {
            return value.ToArray();
        }

        public void setValue(string value)
        {
            this.value.Add(value);
        }
        public int setValue(string[] args, int position)
        {
            try
            {
                this.setValue(args[position + 1]);
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgsException(ArgsException.ErrorCode.MISSING_STRING);
            }
            return 1;
        }
    }
}
