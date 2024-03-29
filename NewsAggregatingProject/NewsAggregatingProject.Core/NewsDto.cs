﻿
namespace NewsAggregatingProject.Core
{
    public class NewsDto
    {
        public Guid Id { get; set; }

        public string? Description { get; set; }
        public string Title { get; set; }

        public string? ContentNew { get; set; }
        public DateTime Date { get; set; }
        public float? Rate { get; set; }
        public string SourceUrl { get; set; }
        public Guid SourceId { get; set; }
    }
}
