using System;
using System.Collections.Generic;
using System.Text;

namespace Arguments
{
    class BooleanArgumentMarshaler : IArgumentMarshaler
    {
        bool isPresent = false;

        public bool getBool()
        {
            return isPresent;
        }

        public object getValue()
        {
            return isPresent;
        }

        public void setValue()
        {
            isPresent = true;
        }

        public bool hasValue()
        {
            return false;
        }

        public int setValue(string[] args, int position)
        {
            this.setValue();
            return 0;
        }
    }

}
