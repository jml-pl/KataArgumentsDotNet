using System;
using System.Collections.Generic;
using System.Text;

namespace Arguments
{
    class ArgumentMarshalerFactory
    {
        public static IArgumentMarshaler Build(string typeCode)
        {

            switch (typeCode)
            {
                case "":
                    return new BooleanArgumentMarshaler();
                case "*":
                    return new StringArgumentMarshaler();
                case "#":
                    return new IntegerArgumentMarshaler();
                case "##":
                    return new DoubleArgumentMarshaler();
                case "[*]":
                    return new StringArrayArgumentMarshaler();
                case "&":
                    return new MapArgumentMarshaler();
                default:
                    throw new ArgsException(ArgsException.ErrorCode.INVALID_ARGUMENT_FORMAT);

            }
        }

    }
}
