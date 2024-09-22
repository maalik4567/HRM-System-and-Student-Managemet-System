using HR_Managament_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Managament_System.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult ListEmployee()
        {
            var employees = EmployeeRepository.GetAllEmployees();
            return View(employees);
        }

        // GET: Create Employee
        public ActionResult CreateEmployee()
        {
            ViewBag.Departments = EmployeeRepository.GetDepartments();
            ViewBag.Positions = EmployeeRepository.GetPositions();
            ViewBag.EmployeeTypes = EmployeeRepository.GetEmployeeTypes();

            return View();
        }

        [HttpPost]
        public ActionResult CreateEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Capture the returned EmpId
                    int empId = EmployeeRepository.InsertEmployee(employee);
                    return RedirectToAction("Success", new { id = empId });
                }
                catch (Exception ex)
                {
                    // Use a logging framework in a real-world application
                    Console.WriteLine("Error inserting employee: " + ex.Message);
                    ModelState.AddModelError("", "An error occurred while saving the employee.");
                }
            }

            // Reload dropdown lists
            ViewBag.Departments = EmployeeRepository.GetDepartments();
            ViewBag.Positions = EmployeeRepository.GetPositions();
            ViewBag.EmployeeTypes = EmployeeRepository.GetEmployeeTypes();

            return View(employee);
        }

        //Empoyee Detail: 
        // GET: Employee/EmpDetail/5
        public ActionResult EmpDetail(int id)
        {
            // Fetch employee details from repository
            var employee = EmployeeRepository.GetEmployeeById(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        // GET: Employee/UpdateEmployee/5
        public ActionResult UpdateEmployee(int id)
        {
            var employee = EmployeeRepository.GetEmployeeById(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            // Prepare dropdown lists
            ViewBag.Departments = EmployeeRepository.GetDepartments();
            ViewBag.Positions = EmployeeRepository.GetPositions();
            ViewBag.EmployeeTypes = EmployeeRepository.GetEmployeeTypes();

            return View(employee);
        }

        // POST: Employee/UpdateEmployee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                // Call the repository method to update the employee record
                var result = EmployeeRepository.UpdateEmployee(employee);

                if (result)
                {
                    return RedirectToAction("ListEmployee"); // Redirect to list view after update
                }
                else
                {
                    ModelState.AddModelError("", "Unable to update employee. Please try again.");
                }
            }

            // Prepare dropdown lists
            ViewBag.Departments = EmployeeRepository.GetDepartments();
            ViewBag.Positions = EmployeeRepository.GetPositions();
            ViewBag.EmployeeTypes = EmployeeRepository.GetEmployeeTypes();

            return View(employee);
        }


        [HttpPost]
        public ActionResult DeleteEmployee(int id)
        {
            bool success = EmployeeRepository.DeleteEmployee(id);

            if (success)
            {
                // Redirect back to the employee list
                return RedirectToAction("ListEmployee");
            }

            ViewBag.ErrorMessage = "Unable to delete the employee. Please try again.";
            var employees = EmployeeRepository.GetAllEmployees();
            return View("ListEmployee", employees);
        }

        // GET: Success
        public ActionResult Success(int id)
        {
            var employee = EmployeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
    }
}