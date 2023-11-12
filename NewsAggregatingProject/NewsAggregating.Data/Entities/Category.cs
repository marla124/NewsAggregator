﻿namespace NewsAggregatingProject.Data.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<New> News { get; set; }
    }
}