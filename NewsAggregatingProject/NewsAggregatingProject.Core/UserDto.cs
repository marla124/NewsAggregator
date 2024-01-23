using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregatingProject.Core
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email {  get; set; }
        public string Password { get; set; }
        public Guid UserStatusId { get; set; }
    }
}
