namespace DTO.Exceptions
{
    using System;

    public class MissingMappedPropertyException : Exception
    {
        public MissingMappedPropertyException()
        {
        }

        public MissingMappedPropertyException(string message)
            : base(message)
        {
        }

        public MissingMappedPropertyException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}