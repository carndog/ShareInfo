namespace DTO.Exceptions
{
    using System;

    public class PriceStreamNotFoundException : Exception
    {
        public PriceStreamNotFoundException()
        {
        }

        public PriceStreamNotFoundException(string message)
            : base(message)
        {
        }

        public PriceStreamNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}