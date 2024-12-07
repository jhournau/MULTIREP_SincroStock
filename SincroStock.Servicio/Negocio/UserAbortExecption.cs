using System;
using System.Runtime.Serialization;

namespace SincroStock.Servicio.Negocio
{
    [Serializable]
    internal class UserAbortExecption : Exception
    {
        public UserAbortExecption()
        {
        }

        public UserAbortExecption(string message) : base(message)
        {
        }

        public UserAbortExecption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserAbortExecption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}