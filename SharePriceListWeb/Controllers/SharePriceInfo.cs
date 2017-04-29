﻿using System;

namespace SharePriceListWeb.Controllers
{
    public class SharePriceInfo
    {
        public string ShareIndex { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }

        public decimal? Price { get; set; }

        public decimal? Change { get; set; }

        public decimal? ChangePercentage { get; set; }
    }
}