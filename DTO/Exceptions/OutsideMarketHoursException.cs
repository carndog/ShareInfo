namespace DTO.Exceptions
{
    using System;

    public class OutsideMarketHoursException : Exception
    {
        public OutsideMarketHoursException()
        {
        }

        public OutsideMarketHoursException(string message)
            : base(message)
        {
        }

        public OutsideMarketHoursException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}