namespace DTO.Exceptions
{
    using System;

    public class IncorrectHeaderException : Exception
    {
        public IncorrectHeaderException()
        {
        }

        public IncorrectHeaderException(string message)
            : base(message)
        {
        }

        public IncorrectHeaderException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}