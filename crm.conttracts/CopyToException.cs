using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace System.Data
{
    [Serializable]
    internal class CopyToException : Exception
    {
        public CopyToException()
        {
        }

        public CopyToException(string message) : base(message)
        {
        }

        public CopyToException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CopyToException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
