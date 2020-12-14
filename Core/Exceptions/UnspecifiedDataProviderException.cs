using System;
using System.Runtime.Serialization;

namespace Kaizen.Core.Exceptions
{
    [Serializable]
    public class UnspecifiedDataProviderException : Exception
    {
        public UnspecifiedDataProviderException() : base("The Data Provider entry in appsettings.json is empty or the one specified has not been found!")
        {
        }

        protected UnspecifiedDataProviderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
