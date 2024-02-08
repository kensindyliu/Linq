using System;
namespace LINQLab1
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        public Department()
        {

        }

        public Department(int departmentID, string departmentName)
        {
            this.DepartmentID = departmentID;
            this.DepartmentName = departmentName;
        }
    }
}

