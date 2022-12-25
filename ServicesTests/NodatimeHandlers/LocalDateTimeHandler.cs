using Dapper;

namespace ServicesTests.NodatimeHandlers;

public class LocalDateTimeTypeHandler : SqlMapper.TypeHandler<LocalDateTime>
{
    public override LocalDateTime Parse(object value)
    {
        if (value is DateTime time)
        {
            return LocalDateTime.FromDateTime(time);
        }

        throw new DataException($"Unable to convert {value} to LocalDateTime");
    }

    public override void SetValue(IDbDataParameter parameter, LocalDateTime value)
    {
        parameter.Value = value.ToDateTimeUnspecified();
    }
}