using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TeisterMask.DataProcessor.ImportDto
{
    public class EmployeeImportEmployeesDto    {
        [JsonProperty("Username"), MinLength(3), MaxLength(40), RegularExpression(@"[a-zA-Z0-9]{3,40}"), Required]
        public string Username { get; set; } 

        [JsonProperty("Email"), EmailAddress, Required]
        public string Email { get; set; } 

        [JsonProperty("Phone"), Phone, RegularExpression(@"\d{3}-\d{3}-\d{4}"), Required]
        public string Phone { get; set; } 

        [JsonProperty("Tasks")]
        public int[] Tasks { get; set; } 
    }
}