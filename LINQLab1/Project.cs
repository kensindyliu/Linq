using System;
namespace LINQLab1
{
	public class Project
	{
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public int DepartmentID { get; set; }

        public Project()
        {

        }

        public Project(int projectID, string projectName, int departmentID)
        {
            this.ProjectID = projectID;
            this.ProjectName = projectName;
            this.DepartmentID = departmentID;
        }
    }
}

