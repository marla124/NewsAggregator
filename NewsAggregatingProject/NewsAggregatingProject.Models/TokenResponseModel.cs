using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregatingProject.Models
{
    public class TokenResponseModel
    {
        public string JwtToken {  get; set; }
        public Guid RefreshToken { get; set; }
    }
}
