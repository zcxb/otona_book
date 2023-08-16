using System;
using System.Runtime.Serialization;

namespace OtonaBookApi.Common
{
    public class BizException : ApplicationException
    {
        public string SubCode { get; private set; }
        public string SubMessage { get; private set; }

        //public BizException() : base() { }

        //public BizException(string message) : base(message) { }

        //public BizException(string message, Exception innerException) : base(message, innerException) { }

        //public BizException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public BizException(string subCode, string subMessage) : base()
        {
            SubCode = subCode;
            SubMessage = subMessage;
        }
    }
}

