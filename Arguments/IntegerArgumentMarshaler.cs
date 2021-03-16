using System;
using System.Collections.Generic;
using System.Text;

namespace Arguments
{
 
    class IntegerArgumentMarshaler : IArgumentMarshaler
    {
        int value = 0;
        public int getInt()
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
                this.value = Int32.Parse(value);
            }
            catch (FormatException)
            {
                throw new ArgsException(ArgsException.ErrorCode.INVALID_INTEGER, value);
            }
        }

        public int setValue(string[] args, int position)
        {
            try
            {
                this.setValue(args[position + 1]);
            } catch (IndexOutOfRangeException)
            {
                throw new ArgsException(ArgsException.ErrorCode.MISSING_INTEGER);
            }
            return 1;
        }


    }
}
