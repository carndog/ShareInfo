namespace DTO.Exceptions
{
    using System;

    public class PriceNotFoundException : Exception
    {
        public PriceNotFoundException()
        {
        }

        public PriceNotFoundException(string message)
            : base(message)
        {
        }

        public PriceNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}