using System.Runtime.Intrinsics.Arm;

namespace LINQLab1;

class Program
{
    static List<Employee> Employees = new();
    static List<Department> Departments = new();
    static List<Project> Projects = new();
    static void Main(string[] args)
    {
        Initialization();  //For test and also from the assignment requirments

        while (true)
        {
            Console.Clear();
            Console.Write("Menu\n1.Group employees by their departments." +
                "\n2.Calculate the average salary for each department." +
                "\n3.Find the department with the highest total salary." +
                "\n4.Group by the projects and get who are involved in." +
                "\n5.Calculate the total number of projects in each department." +
                "\n6.List an employee Info(department and project are involed."+
                "\n7.List the employees who saraly above 50000." +
                "\n8.Exit");
            string strInput = Console.ReadLine();
            switch (strInput)
            {
                case "1":
                    GroupEmployeeByDepartments();
                    break;
                case "2":
                    AverageDepartmentSaraly();
                    break;
                case "3":
                    HighestPaidDepartment();
                    break;
                case "4":
                    projectsWhoInvolved();
                    break;
                case "5":
                    ProjectDepartmentsAmount();
                    break;
                case "6":
                    ListEmployeeInfo();
                    break;
                case "7":
                    EmployeesSaralyAbove50000();
                    break;
                case "8":
                    Console.WriteLine("\nBye...Press any key to exit.");
                    Console.ReadKey();
                    return;
            }
            Console.ReadKey();
        }

    }

    static void Initialization()
    {
        //add 11 employees
        Employees.Add(new Employee(1, "John1", "Doe1", 50001, 1));
        Employees.Add(new Employee(2, "Jane2", "Smith2", 70002, 2));
        Employees.Add(new Employee(3, "Jane3", "Smith3", 80003, 3));
        Employees.Add(new Employee(4, "Jane4", "Smith4", 90004, 1));
        Employees.Add(new Employee(5, "Jane5", "Smith5", 10005, 2));
        Employees.Add(new Employee(6, "Jane6", "Smith6", 20006, 3));
        Employees.Add(new Employee(7, "Jane7", "Smith7", 30007, 1));
        Employees.Add(new Employee(8, "Jane8", "Smith8", 40008, 2));
        Employees.Add(new Employee(9, "Jane9", "Smith9", 50009, 3));
        Employees.Add(new Employee(10, "Jane10", "Smith10", 100010, 1));
        Employees.Add(new Employee(11, "Jane11", "Smith11", 110011, 2));
        //add 3 departments
        Departments.Add(new Department(1, "HR"));
        Departments.Add(new Department(2, "Finance"));
        Departments.Add(new Department(3, "Develop"));
        //add 5 projects
        Projects.Add(new Project(1, "Project 1", 1));
        Projects.Add(new Project(2, "Project 2", 2));
        Projects.Add(new Project(3, "Project 3", 3));
        Projects.Add(new Project(4, "Project 4", 1));
        Projects.Add(new Project(4, "Project 5", 2));
        Projects.Add(new Project(5, "Project 5", 1));
        Projects.Add(new Project(5, "Project 5", 2));
        Projects.Add(new Project(5, "Project 5", 3));
    }

    //Group employees by their departments.
    static void GroupEmployeeByDepartments()
    {
        Console.Clear();
        var groupEmployee = Employees.GroupBy(em => em.DepartmentID);
        foreach (var group in groupEmployee)
        {
            Console.WriteLine($"group: {group.Key}");
            foreach (Employee emg in group)
            {
                Console.WriteLine(emg.ToString());
            }
        }
    }

    //Calculate the average salary for each department.
    static void AverageDepartmentSaraly()
    {
        Console.Clear();
        var groupEmployee = Employees.GroupBy(em => em.DepartmentID);
        foreach (var group in groupEmployee)
        {
            double averageSaraly = group.Average(g => g.Salary);
            Console.WriteLine($"average saraly of group: {group.Key} is {averageSaraly}");
        }
    }

    //Find the department with the highest total salary.
    static void HighestPaidDepartment()
    {
        Console.Clear();
        var departmentWithHighestTotalSalaryID = Employees
            .GroupBy(emp => emp.DepartmentID)
            .OrderByDescending(group => group.Sum(emp => emp.Salary))
            .Select(group => group.Key)
            .FirstOrDefault();

        Console.WriteLine($"Department of the group with the highest total salary: ID {departmentWithHighestTotalSalaryID}\n");

    }

    //Group by the projects and get who are involved in
    static void projectsWhoInvolved()
    {
        Console.Clear(); 
        var queryEm = from employee in Employees
                      join department in Departments on employee.DepartmentID equals department.DepartmentID
                      join project in Projects on department.DepartmentID equals project.DepartmentID
                      group new
                      {
                          project.ProjectID,
                          project.ProjectName,
                          department.DepartmentID,
                          department.DepartmentName,
                          employee.EmployeeID,
                          employee.FirstName,
                          employee.LastName,
                      } by project.ProjectID;

        foreach (var group in queryEm)
        {
            Console.WriteLine($"Project: {group.Key}");
            foreach (var result in group)
            {
                Console.WriteLine($"ProjectName: {result.ProjectName} Involved department:{result.DepartmentName}" +
                    $" EmployeeID:{result.EmployeeID} Name:{result.FirstName} {result.LastName}");
            }
        }
    }

    //Calculate the total number of projects in each department.
    static void ProjectDepartmentsAmount()
    {
        Console.Clear();
        var departmentsInvolvedInProjects = Projects
            .GroupBy(project => project.ProjectID)
            .Select(group => new
            {
                ProjectID = group.Key,
                DepartmentCount = group.Select(project => project.DepartmentID).Distinct().Count()
            });

        foreach (var project in departmentsInvolvedInProjects)
        {
            Console.WriteLine($"Project ID: {project.ProjectID}, Departments Involved: {project.DepartmentCount}");
        }
    }

    //show an employee's info, department belong and project involved
    static void ListEmployeeInfo()
    {
        Console.Clear();
        Console.Write("Please input the first name of the employee for details:");
        string strFirstName = Console.ReadLine();
        var query2 = from em2 in Employees
                     where em2.FirstName == strFirstName
                     join dep2 in Departments on em2.DepartmentID equals dep2.DepartmentID
                     join proj2 in Projects on dep2.DepartmentID equals proj2.DepartmentID
                     group new
                     {
                         em2.EmployeeID,
                         firstname = em2.FirstName,
                         em2.LastName,
                         em2.DepartmentID,
                         dep2.DepartmentName,
                     } by proj2.ProjectName;

        string projNames = "";
        string deparmentName = "";
        string lastName = "";
        foreach(var proj in query2)
        {
            projNames += " " + proj.Key+",";
            deparmentName = proj.First().DepartmentName;
            lastName = proj.First().LastName;
        }
        projNames = projNames.TrimEnd(',');
        Console.WriteLine($"The employee:{strFirstName} {lastName} is belong to department:{deparmentName}.\n" +
            $"His department is invovled in {query2.Count()} projects:{projNames}");
    }

    static void EmployeesSaralyAbove50000()
    {
        Console.Clear();
        //var query = Employees.FindAll(em => em.Salary > 50000);
        var query2 = Employees.Where(em => em.Salary > 50000);
        Console.WriteLine("Employees who's saraly is above 50000");
        foreach(Employee emp in query2)
        {
            Console.WriteLine($"EmployeeID: {emp.EmployeeID} Name: {emp.FirstName} {emp.LastName} Saraly: {emp.Salary}");
        }
    }

}

