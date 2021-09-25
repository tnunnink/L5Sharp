using System;

namespace LogixHelper.Exceptions
{
    public class MemberNotFoundException : Exception
    {
        public MemberNotFoundException()
        {
        }
        
        public MemberNotFoundException(string message)
            : base(message)
        {
        }
        
        public MemberNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}