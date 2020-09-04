namespace DTO.Exceptions
{
    using System;

    public class CannotWritePropertyException : Exception
    {
        public CannotWritePropertyException()
        {
        }

        public CannotWritePropertyException(string message)
            : base(message)
        {
        }

        public CannotWritePropertyException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}