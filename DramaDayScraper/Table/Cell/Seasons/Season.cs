﻿using DramaDayScraper.Table.Cell.MediaVersions;

namespace DramaDayScraper.Table.Cell.Seasons
{
    public class Season
    {
        public int? SeasonNumber { get; set; }

        public ICollection<MediaVersion> MediaVersions { get; set; } = new List<MediaVersion>();
    }
}
