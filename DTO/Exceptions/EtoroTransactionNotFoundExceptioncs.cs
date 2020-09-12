namespace DTO.Exceptions
{
    using System;

    public class EtoroTransactionNotFoundException : Exception
    {
        public EtoroTransactionNotFoundException()
        {
        }

        public EtoroTransactionNotFoundException(string message)
            : base(message)
        {
        }

        public EtoroTransactionNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}