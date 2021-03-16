using System;
using System.Collections.Generic;
using System.Text;

namespace Arguments
{
    interface IArgumentMarshaler
    {

        public bool hasValue()
        {
            return true;
        }

        public void setValue() {}

        public void setValue(string value) {}

        public int setValue(string[] args, int position);

        public bool getBool() {  
            //TODO exception????
            ; return false; 
        }

        public object getValue();

    }
}
