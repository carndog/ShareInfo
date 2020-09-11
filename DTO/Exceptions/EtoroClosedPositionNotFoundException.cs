namespace DTO.Exceptions
{
    using System;

    public class EtoroClosedPositionNotFoundException : Exception
    {
        public EtoroClosedPositionNotFoundException()
        {
        }

        public EtoroClosedPositionNotFoundException(string message)
            : base(message)
        {
        }

        public EtoroClosedPositionNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}