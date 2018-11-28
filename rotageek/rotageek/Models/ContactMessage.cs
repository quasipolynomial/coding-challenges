using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rotageek.Models
{
    public class ContactMessage
    {
        public Guid Id { get; set; }

        public string Message { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
