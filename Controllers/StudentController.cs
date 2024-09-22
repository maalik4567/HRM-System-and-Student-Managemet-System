using HR_Managamenr_System.Models;
using HR_Managament_System.Models;
using HR_Managemenr_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Managament_System.Controllers
{
    public class StudentController : Controller
    {

        [HttpGet]
        public ActionResult StudentIndex()
        {

            ViewBag.Courses = StudentRepository.GetCourses();
            ViewBag.Classes = StudentRepository.GetClasses();
            List<Student> students = StudentRepository.GetAllStudents().ToList();
            return View(students);
        }

        [HttpPost]
        public ActionResult StudentIndex(Student student, int[] SelectedCourses)
        {
            if (ModelState.IsValid)
            {
                // Convert array of selected courses to comma-separated string
                string courseIds = string.Join(",", SelectedCourses);

                // Insert student into the database
                int studentId = StudentRepository.InsertStudent(student, courseIds);

                // Optionally, redirect to another action or view
                return RedirectToAction("StudentIndex"); // Assumes you have a StudentList action
            }

            // If validation fails, return the same view with validation errors
            ViewBag.Courses = StudentRepository.GetCourses();
            ViewBag.Classes = StudentRepository.GetClasses();
            return View(student);
        }


        // GET: Student/Edit/5
        public ActionResult EditStudent(int id)
        {
            var student = StudentRepository.GetStudentById(id);

            if (student == null)
            {
                return HttpNotFound(); // Return 404 if student not found
            }

            ViewBag.Courses = StudentRepository.GetCourses();
            ViewBag.Classes = StudentRepository.GetClasses();

            var selectedCourses = string.IsNullOrEmpty(student.MarkedCourseIds)
                ? new int[0]
                : student.MarkedCourseIds.Split(',')
                                   .Select(int.Parse)
                                   .ToArray();

            var availableCourses = string.IsNullOrEmpty(student.AvailableCoursesIds)
                ? new int[0]
                : student.AvailableCoursesIds.Split(',')
                                   .Select(int.Parse)
                                   .ToArray();

            var viewModel = new StudentViewModel
            {
                Student = student,
                Courses = ViewBag.Courses as List<SelectListItem>,
                MarkedCourses = selectedCourses,
                AvailableCourses = availableCourses
            };

            return View(viewModel); // Pass the StudentViewModel to the view
        }


        [HttpPost]
        public ActionResult EditStudent(StudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Check the SelectedCourses to ensure they're integers
                

                string courseIds = string.Join(",", viewModel.MarkedCourses);

                bool isUpdated = StudentRepository.UpdateStudent(
                    viewModel.Student.Id,
                    viewModel.Student.Name,
                    viewModel.Student.ContactNumber,
                    viewModel.Student.ClassId,
                    courseIds
                );

                if (isUpdated)
                {
                    return RedirectToAction("StudentIndex");
                }
                else
                {
                    ModelState.AddModelError("", "Update failed. Please try again.");
                }
            }

            // Repopulate ViewBag if the model state is invalid
            ViewBag.Courses = StudentRepository.GetCourses();
            ViewBag.Classes = StudentRepository.GetClasses();

            return View(viewModel);
        }





        [HttpPost]
        public ActionResult DeleteStudent(int id)
        {
            bool success = StudentRepository.DeleteStudent(id);

            if (success)
            {
                // Redirect back to the student list
                return RedirectToAction("StudentIndex"); // Assuming "StudentIndex" is the view showing the student list
            }

            ViewBag.ErrorMessage = "Unable to delete the student. Please try again.";
            var students = StudentRepository.GetAllStudents(); // Get updated student list
            return View("StudentIndex", students); // Return the view with the updated list and error message
        }



    }
}