using Dapper;

namespace ServicesTests.NodatimeHandlers;

public class LocalDateTypeHandler : SqlMapper.TypeHandler<LocalDate>
{
    public override LocalDate Parse(object value)
    {
        if (value is DateTime)
        {
            return LocalDate.FromDateTime((DateTime)value);
        }

        throw new DataException($"Unable to convert {value} to LocalDate");
    }

    public override void SetValue(IDbDataParameter parameter, LocalDate value)
    {
        parameter.Value = value.ToDateTimeUnspecified();
    }
}