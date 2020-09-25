using System;
using System.Data;
using Dapper;
using NodaTime;

namespace ServicesTests.NodatimeHandlers
{
    public class LocalDateTimeTypeHandler : SqlMapper.TypeHandler<LocalDateTime>
    {
        public override LocalDateTime Parse(object value)
        {
            if (value is DateTime)
            {
                return LocalDateTime.FromDateTime((DateTime)value);
            }

            throw new DataException($"Unable to convert {value} to LocalDateTime");
        }

        public override void SetValue(IDbDataParameter parameter, LocalDateTime value)
        {
            parameter.Value = value.ToDateTimeUnspecified();
        }
    }
}