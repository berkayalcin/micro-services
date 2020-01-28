using System;

namespace MicroServices.Common.Exceptions
{
    public class MicroServicesException:Exception
    {
        public string Code { get; }

        public MicroServicesException()
        {
            
        }

        public MicroServicesException(string code)
        {
            Code = code;
        }
        public MicroServicesException(string message,params object[] args):this(string.Empty,message,args)
        {

        }
        public MicroServicesException(string code,string message, params object[] args) : this(null,code, message, args)
        {

        }

        public MicroServicesException(Exception innerException,string message,params object[] args):this(innerException,string.Empty,message,args)
        {
            
        }

        public MicroServicesException(Exception innerException,string code, string message, params object[] args) :base(string.Format(message,args),innerException)
        {
            Code = code;
        }
    }
}