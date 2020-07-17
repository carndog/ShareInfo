namespace DTO.Exceptions
{
    using System;

    public class PriceCreationException : Exception
    {
        public PriceCreationException()
        {
        }

        public PriceCreationException(string message)
            : base(message)
        {
        }

        public PriceCreationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}