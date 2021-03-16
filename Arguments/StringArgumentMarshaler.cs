using System;
using System.Collections.Generic;
using System.Text;

namespace Arguments
{
    class StringArgumentMarshaler : IArgumentMarshaler
    {
        string value = "";

        public object getValue()
        {
            return value;
        }

        public string getString()
        {
            return value;
        }

        public void setValue(string value)
        {
            this.value = value;
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
