namespace DTO.Exceptions
{
    using System;

    public class IncorrectHeaderCountException : Exception
    {
        public IncorrectHeaderCountException()
        {
        }

        public IncorrectHeaderCountException(string message)
            : base(message)
        {
        }

        public IncorrectHeaderCountException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}