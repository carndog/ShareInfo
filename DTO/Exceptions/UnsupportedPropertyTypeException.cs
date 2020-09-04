namespace DTO.Exceptions
{
    using System;

    public class UnsupportedPropertyTypeException : Exception
    {
        public UnsupportedPropertyTypeException()
        {
        }

        public UnsupportedPropertyTypeException(string message)
            : base(message)
        {
        }

        public UnsupportedPropertyTypeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}