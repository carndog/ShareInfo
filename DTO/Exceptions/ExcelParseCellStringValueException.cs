namespace DTO.Exceptions
{
    using System;

    public class ExcelParseCellStringValueException : Exception
    {
        public ExcelParseCellStringValueException()
        {
        }

        public ExcelParseCellStringValueException(string message)
            : base(message)
        {
        }

        public ExcelParseCellStringValueException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}