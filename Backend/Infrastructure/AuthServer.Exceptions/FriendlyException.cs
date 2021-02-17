using System;
using System.ComponentModel;
using System.Reflection;

namespace AuthServer.Exceptions
{
    public class FriendlyException : Exception
    {
        public ErrorCode Code { get; private set; }

        public FriendlyException(ErrorCode code) => Code = code;

        public FriendlyException(ErrorCode code, Exception innerException) : base(null, innerException) => Code = code;

        public override string Message
        {
            get
            {
                string value = Code.ToString();
                FieldInfo field = Code.GetType().GetField(value);
                DescriptionAttribute descriptionAttribute = (DescriptionAttribute)(field.GetCustomAttributes(typeof(DescriptionAttribute), false)[0]);
                return descriptionAttribute.Description;
            }
        }
    }
}
