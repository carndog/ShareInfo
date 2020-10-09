namespace DTO.Exceptions
{
    using System;

    public class PeriodPriceNotFoundException : Exception
    {
        public PeriodPriceNotFoundException()
        {
        }

        public PeriodPriceNotFoundException(string message)
            : base(message)
        {
        }

        public PeriodPriceNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}