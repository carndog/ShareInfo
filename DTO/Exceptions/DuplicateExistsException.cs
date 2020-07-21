namespace DTO.Exceptions
{
    using System;

    public class DuplicateExistsException : Exception
    {
        public DuplicateExistsException()
        {
        }

        public DuplicateExistsException(string message)
            : base(message)
        {
        }

        public DuplicateExistsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}