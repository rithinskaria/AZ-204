using Microsoft.AspNetCore.Mvc;
using EmployeeApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "employees.json");

        // Helper method to read the JSON file
        private List<Employee> GetEmployeesFromFile()
        {
            var jsonData = System.IO.File.ReadAllText(_filePath);
            var employees = JsonConvert.DeserializeObject<List<Employee>>(jsonData);
            return employees;
        }

        // Helper method to write back to the JSON file
        private void WriteEmployeesToFile(List<Employee> employees)
        {
            var jsonData = JsonConvert.SerializeObject(employees, Formatting.Indented);
            System.IO.File.WriteAllText(_filePath, jsonData);
        }

        // GET: api/employee
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            var employees = GetEmployeesFromFile();
            return Ok(employees);
        }

        // GET: api/employee/{id}
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            var employees = GetEmployeesFromFile();
            var employee = employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        // POST: api/employee
        [HttpPost]
        public ActionResult<Employee> PostEmployee(Employee newEmployee)
        {
            var employees = GetEmployeesFromFile();
            employees.Add(newEmployee);

            // Write updated list back to file
            WriteEmployeesToFile(employees);

            return CreatedAtAction(nameof(GetEmployee), new { id = newEmployee.EmployeeId }, newEmployee);
        }

        // PUT: api/employee/{id}
        [HttpPut("{id}")]
        public IActionResult PutEmployee(int id, Employee updatedEmployee)
        {
            var employees = GetEmployeesFromFile();
            var existingEmployee = employees.FirstOrDefault(e => e.EmployeeId == id);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            // Update employee data
            existingEmployee.FirstName = updatedEmployee.FirstName;
            existingEmployee.LastName = updatedEmployee.LastName;
            existingEmployee.Salary = updatedEmployee.Salary;
            existingEmployee.HireDate = updatedEmployee.HireDate;
            existingEmployee.Department = updatedEmployee.Department;
            existingEmployee.EmployeeStatus = updatedEmployee.EmployeeStatus;

            // Write updated list back to file
            WriteEmployeesToFile(employees);

            return NoContent();
        }

        // DELETE: api/employee/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employees = GetEmployeesFromFile();
            var employee = employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            // Remove the employee
            employees.Remove(employee);

            // Write updated list back to file
            WriteEmployeesToFile(employees);

            return NoContent();
        }
    }
}
