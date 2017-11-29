using System;
using System.Runtime.Serialization;

namespace G1ANT.Language.Ui.Exceptions
{
    [Serializable]
    public class WindowNotFoundException : Exception
    {
        public WindowNotFoundException()
        {
        }

        public WindowNotFoundException(string message) : base(message)
        {
        }

        public WindowNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WindowNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}