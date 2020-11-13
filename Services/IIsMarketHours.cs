﻿using NodaTime;

 namespace Services
{
    public interface IIsMarketHours
    {
        bool Get(LocalDateTime datetime);
    }
}