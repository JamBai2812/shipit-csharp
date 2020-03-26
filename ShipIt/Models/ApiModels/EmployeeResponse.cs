using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipIt.Models.ApiModels
{
    public class EmployeeResponse : Response
    {
        public List<Employee> Employees { get; set; }
        public EmployeeResponse(Employee employee)
        {
            Employees = new List<Employee>() {employee};
            Success = true;
        }
        public EmployeeResponse(List<Employee> employees)
        {
            Employees = employees;
            Success = true;
        }

        public EmployeeResponse() { }
    }
}