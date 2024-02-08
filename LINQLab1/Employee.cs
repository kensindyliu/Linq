using System;
namespace LINQLab1
{
	public class Employee
	{
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Salary { get; set; }
        public int DepartmentID { get; set; }

        public Employee()
        {

        }

        public Employee(int employeeID, string firstName, string lastName, double salary, int departmentID)
        {
            this.EmployeeID = employeeID;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Salary = salary;
            this.DepartmentID = departmentID;
        }

        public override string ToString()
        {
            return string.Format($"EmployeeID: {EmployeeID} FirstName:{FirstName} LastName:{LastName} Salary:{Salary} DepartmentID:{DepartmentID}");
        }
    }
}

