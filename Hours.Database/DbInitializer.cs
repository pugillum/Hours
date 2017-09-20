using Hours.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hours.Database
{
    public static class DbInitializer
    {
        public static void Initialize(HoursContext context)
        {
            context.Database.EnsureCreated();

            // Look for any projects.
            if (context.Projects.Any())
            {
                return;   // DB has been seeded
            }

            //create projects
            var project1 = new Project
            {
                Id = Guid.NewGuid(),
                Name = "Project 1",
                CreatedDate = DateTime.Now
            };

            var project2 = new Project
            {
                Id = Guid.NewGuid(),
                Name = "Project 2",
                CreatedDate = DateTime.Now
            };

            var projects = new Project[]{ project1, project2 };

            //create employees

            var employee1 = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "User",
                LastName = "1",
                UserName = "User1",
                CreatedDate = DateTime.Now,
                Email = "user1@users.com"
            };

            var employee2 = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "User",
                LastName = "2",
                UserName = "User2",
                CreatedDate = DateTime.Now,
                Email = "user2@users.com"
            };

            var employee3 = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "User",
                LastName = "3",
                UserName = "User3",
                CreatedDate = DateTime.Now,
                Email = "user3@users.com"
            };

            var employees = new Employee[] { employee1, employee2, employee3 };

            var project1Employee1 = new ProjectEmployee
            {
                Employee = employee1,
                EmployeeId = employee1.Id,
                Project = project1,
                ProjectId = project1.Id
            };

            var project1Employee2 = new ProjectEmployee
            {
                Employee = employee2,
                EmployeeId = employee2.Id,
                Project = project1,
                ProjectId = project1.Id
            };

            var project2Employee1 = new ProjectEmployee
            {
                Employee = employee1,
                EmployeeId = employee1.Id,
                Project = project2,
                ProjectId = project2.Id
            };

            var project2Employee3 = new ProjectEmployee
            {
                Employee = employee3,
                EmployeeId = employee3.Id,
                Project = project2,
                ProjectId = project2.Id
            };

            var projectEmployees = new ProjectEmployee[] { project1Employee1, project1Employee2,
                project2Employee1, project2Employee3 };

            //add link objects
            project1.ProjectEmployees.Add(project1Employee1);
            project1.ProjectEmployees.Add(project1Employee2);
            project2.ProjectEmployees.Add(project2Employee1);
            project2.ProjectEmployees.Add(project2Employee3);

            employee1.ProjectEmployees.Add(project1Employee1);
            employee1.ProjectEmployees.Add(project2Employee1);
            employee2.ProjectEmployees.Add(project1Employee2);
            employee3.ProjectEmployees.Add(project2Employee3);

            foreach (Project p in projects)
            {
                context.Projects.Add(p);
            }

            foreach (Employee e in employees)
            {
                context.Employees.Add(e);
            }

            foreach (ProjectEmployee pe in projectEmployees)
            {
                context.ProjectEmployees.Add(pe);
            }

            context.SaveChanges();

            //employee 1 does some tasks on project 1 and project 2
            var user1Task1 = new Task
            {
                Id = Guid.NewGuid(),
                Employee = employee1,
                EmployeeId = employee1.Id,
                Project = project1,
                ProjectId = project1.Id,
                StartTime = DateTime.Parse("2017/01/01 09:00:00"),
                EndTime = DateTime.Parse("2017/01/01 10:00:00")
            };

            var user1Task2 = new Task
            {
                Id = Guid.NewGuid(),
                Employee = employee1,
                EmployeeId = employee1.Id,
                Project = project1,
                ProjectId = project1.Id,
                StartTime = DateTime.Parse("2017/01/01 10:00:00"),
                EndTime = DateTime.Parse("2017/01/01 11:00:00")
            };

            var user1Task3 = new Task
            {
                Id = Guid.NewGuid(),
                Employee = employee1,
                EmployeeId = employee1.Id,
                Project = project2,
                ProjectId = project2.Id,
                StartTime = DateTime.Parse("2017/01/01 11:00:00"),
                EndTime = DateTime.Parse("2017/01/01 16:00:00")
            };

            // employee 2 works on project 1
            var user2Task1 = new Task
            {
                Id = Guid.NewGuid(),
                Employee = employee2,
                EmployeeId = employee2.Id,
                Project = project1,
                ProjectId = project1.Id,
                StartTime = DateTime.Parse("2017/01/01 11:00:00"),
                EndTime = DateTime.Parse("2017/01/01 16:00:00")
            };

            // employee 3 works on project 2
            var user3Task1 = new Task
            {
                Id = Guid.NewGuid(),
                Employee = employee3,
                EmployeeId = employee3.Id,
                Project = project2,
                ProjectId = project2.Id,
                StartTime = DateTime.Parse("2017/01/01 09:00:00"),
                EndTime = DateTime.Parse("2017/01/01 16:00:00")
            };

            context.Tasks.Add(user1Task1);
            context.Tasks.Add(user1Task2);
            context.Tasks.Add(user1Task3);
            context.Tasks.Add(user2Task1);
            context.Tasks.Add(user3Task1);

            context.SaveChanges();
        }

    }
}
