using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClassLibrary
{
    public class LoginCredentials
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime UpdateOn { get; set; }
        public string? UpdateBy { get; set; }

    }
}
