using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClassLibrary
{
    public class Tools
    {
        public int ToolID { get; set; }
        public string ToolName { get; set; }
        public string ToolURL { get; set; }
        public string ImageBase64 { get; set; }
    }
    public class Passcode: Tools
    {
        public int PasscodeID { get; set; }
        public int CredentialID { get; set; }
        public int ClientId { get; set; }
        public string PasscodeValue { get; set; }
        public string Email { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? TotalMinutes { get; set; }
        public int? UsedMinutes { get; set; }
        public bool IsActive { get; set; }
    }
    public class Credentials: Tools
    {
        public int CredentialID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ExtensionAuth : Passcode
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUsedTime
    {
        public int ToolID { get; set; }
        public int? UsedMinutes { get; set; }
        public string PasscodeValue { get; set; }
    }
}
