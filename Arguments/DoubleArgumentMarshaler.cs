using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Arguments
{
    class DoubleArgumentMarshaler : IArgumentMarshaler
    {
        double value = 0;
        public double getDouble()
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
                this.value = Double.Parse(value, CultureInfo.InvariantCulture);
            } catch (FormatException)
            {
                throw new ArgsException(ArgsException.ErrorCode.INVALID_DOUBLE, value);
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
                throw new ArgsException(ArgsException.ErrorCode.MISSING_DOUBLE);
            }
            return 1;
        }
    }
}
