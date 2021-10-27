using System;
using System.Runtime.Serialization;

namespace KlingerSystem.Core.DomainObjects
{
    public class DomainException : Exception
    {
        public DomainException()
        {
        }

        public DomainException(string message) : base(message)
        {
        }

        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
