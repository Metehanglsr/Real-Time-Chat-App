using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeChatApp.Application.DTOs
{
    public class UserDto
    {
        public string ConnectionId { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}
