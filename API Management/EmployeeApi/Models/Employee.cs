using Newtonsoft.Json;
using System;

namespace EmployeeApi.Models
{
    public class Employee
    {
        [JsonProperty("employee_id")]
        public int EmployeeId { get; set; }

        [JsonProperty("first_name")]
        public required string FirstName { get; set; }

        [JsonProperty("last_name")]
        public required string LastName { get; set; }

        [JsonProperty("salary")]
        public decimal Salary { get; set; }

        [JsonProperty("hire_date")]
        public DateTime HireDate { get; set; }

        [JsonProperty("department")]
        public required string Department { get; set; }

        [JsonProperty("employee_status")]
        public required string EmployeeStatus { get; set; }
    }
}
