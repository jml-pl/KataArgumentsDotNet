using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Arguments
{
    public class ArgsException : ApplicationException
    {

        public char errorArgumentId { get; set; } = '\0';
        public String errorParameter { get; set; }  = null;
        public ErrorCode errorCode { get; set; } = ErrorCode.OK;


        public ArgsException() { }
        public ArgsException(ErrorCode errorCode)
        {
            this.errorCode = errorCode;
        }

        public ArgsException(ErrorCode errorCode, String errorParameter)
        {
            this.errorCode = errorCode;
            this.errorParameter = errorParameter;
        }
        public ArgsException(ErrorCode errorCode, char errorArgumentId)
        {
            this.errorCode = errorCode;
            this.errorArgumentId = errorArgumentId;
        }


        public ArgsException(ErrorCode errorCode, char errorArgumentId, String errorParameter)
        {
            this.errorCode = errorCode;
            this.errorParameter = errorParameter;
            this.errorArgumentId = errorArgumentId;
        }


        public enum ErrorCode
        {
            OK, INVALID_ARGUMENT_FORMAT, UNEXPECTED_ARGUMENT, INVALID_ARGUMENT_NAME,
            MISSING_STRING,
            MISSING_INTEGER, INVALID_INTEGER,
            MISSING_DOUBLE, MALFORMED_MAP, MISSING_MAP, INVALID_DOUBLE
        }


    }
}
