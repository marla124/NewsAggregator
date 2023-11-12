﻿namespace NewsAggregatingProject.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public Guid Id_status { get; set; }
        public UserStatus UserStatus { get; set; }
        public List<Comment> Comments { get; set; }
    }
}