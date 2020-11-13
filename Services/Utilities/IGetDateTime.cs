using NodaTime;

namespace Services.Utilities
{
    public interface IGetDateTime
    {
        LocalDateTime Get();
    }
}