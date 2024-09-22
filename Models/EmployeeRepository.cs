using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using System.Web.Mvc;

namespace HR_Managament_System.Models
{
    public class EmployeeRepository
    {
        private static readonly string strConnectionString = "Data Source=DESKTOP-P7354T7\\SQLEXPRESS;Initial Catalog=HRSystem;Integrated Security=True";


        public static IEnumerable<SelectListItem> GetCourses()
        {
            using (var connection = new SqlConnection(strConnectionString))
            {
                var sql = "SELECT CourseId, CourseName FROM Course WHERE IsActive = 1";
                var courses = connection.Query(sql)
                    .Select(c => new SelectListItem
                    {
                        Value = c.CourseId.ToString(),
                        Text = c.CourseName
                    }).ToList();
                return courses;
            }
        }





        public static IEnumerable<SelectListItem> GetDepartments()
        {
            using (var connection = new SqlConnection(strConnectionString))
            {
                var departments = connection.Query("SELECT Id, Name FROM Department");
                return departments.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                });
            }
        }

        public static IEnumerable<SelectListItem> GetPositions()
        {
            using (var connection = new SqlConnection(strConnectionString))
            {
                var positions = connection.Query("SELECT Id, Title FROM Position");
                return positions.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Title
                });
            }
        }

        public static IEnumerable<SelectListItem> GetEmployeeTypes()
        {
            using (var connection = new SqlConnection(strConnectionString))
            {
                var employeeTypes = connection.Query("SELECT Id, Type FROM EmployeeType");
                return employeeTypes.Select(et => new SelectListItem
                {
                    Value = et.Id.ToString(),
                    Text = et.Type
                });
            }
        }


        //CHECK ---HERE 
        public static SelectListItem GetDepartmentById(int id)
        {
            using (var connection = new SqlConnection(strConnectionString))
            {
                var department = connection.QueryFirstOrDefault("SELECT Id, Name FROM Department WHERE Id = @Id", new { Id = id });
                return department != null ? new SelectListItem { Value = department.Id.ToString(), Text = department.Name } : null;
            }
        }

        public static SelectListItem GetPositionById(int id)
        {
            using (var connection = new SqlConnection(strConnectionString))
            {
                var position = connection.QueryFirstOrDefault("SELECT Id, Title FROM Position WHERE Id = @Id", new { Id = id });
                return position != null ? new SelectListItem { Value = position.Id.ToString(), Text = position.Title } : null;
            }
        }

        public static SelectListItem GetEmployeeTypeById(int id)
        {
            using (var connection = new SqlConnection(strConnectionString))
            {
                var employeeType = connection.QueryFirstOrDefault("SELECT Id, Type FROM EmployeeType WHERE Id = @Id", new { Id = id });
                return employeeType != null ? new SelectListItem { Value = employeeType.Id.ToString(), Text = employeeType.Type } : null;
            }
        }


        //GET LIST OF EMPLOYEES
        public static IEnumerable<Employee> GetAllEmployees()
        {
            using (var connection = new SqlConnection(strConnectionString))
            {
                return connection.Query<Employee>("GetAllEmployees", commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        //BY ID 
        public static Employee GetEmployeeById(int empId)
        {
            using (var connection = new SqlConnection(strConnectionString))
            {
                return connection.QuerySingleOrDefault<Employee>(
                    "GetEmployeeById",
                    new { EmpId = empId },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }


        public static int InsertEmployee(Employee employee)
        {
            using (var connection = new SqlConnection(strConnectionString))
            {
                var query = "InsertEmployee"; // Stored procedure name

                var parameters = new
                {
                    FullName = employee.FullName,
                    Email = employee.Email,
                    DepartmentId = employee.DepartmentID,
                    PositionId = employee.PositionID,
                    HireDate = employee.HireDate,
                    DateOfBirth = employee.DateOfBirth,
                    EmployeeTypeId = employee.EmpTypeID,
                    Gender = employee.Gender,
                    Salary = employee.Salary,
                    CNIC = employee.CNIC,
                    PhoneNumber = employee.PhoneNumber
                };

                var empId = connection.QuerySingle<int>("InsertEmployee", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return empId;
            }
        }
        public static bool UpdateEmployee(Employee employee)
        {
            using (var connection = new SqlConnection(strConnectionString))
            {
                var parameters = new
                {
                    EmpId = employee.EmpId,
                    FullName = employee.FullName,
                    Email = employee.Email,
                    DepartmentId = employee.DepartmentID,  // Ensure these match your model
                    PositionId = employee.PositionID,
                    HireDate = employee.HireDate,
                    DateOfBirth = employee.DateOfBirth,
                    EmployeeTypeId = employee.EmpTypeID,
                    Gender = employee.Gender,
                    Salary = employee.Salary,
                    CNIC = employee.CNIC,
                    PhoneNumber = employee.PhoneNumber
                };

                var result = connection.Execute("UpdateEmployee", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result > 0; // Return true if update was successful
            }
        }



        //DELETE EMPLOYEE
        public static bool DeleteEmployee(int empId)
        {
            using (var connection = new SqlConnection(strConnectionString))
            {
                // Update the parameter name to match the stored procedure's expectation
                var parameters = new { EmployeeId = empId }; // Change EmpId to EmployeeId
                var result = connection.Execute("DeleteEmployeeById", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return result > 0; // Return true if delete was successful
            }
        }

        



    }
}