using Microsoft.AspNetCore.Mvc;
namespace NewsAggregatingProject.MVC7.Models
{
    public class NewsModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
