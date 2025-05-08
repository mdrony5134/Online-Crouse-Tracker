using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineCourseTracker
{
    // 🔹 Feature 1: User Registration & Login ---> Mahita
    public class User
    {
        public string Username { get; private set; }
        private string Password;

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public bool Authenticate(string inputPassword)
        {
            return Password == inputPassword;
        }
    }

    // 🔹 Feature 2: Course Creation by Admin ---> Alomgir + Joy
    public class Admin : User
    {
        public Admin(string username, string password) : base(username, password) { }

        public Course CreateCourse(string title, string description)
        {
            return new Course(title, description);
        }

        // 🆕 Admin Dashboard: View all students and their course progress
        public void ViewAllStudentProgress(List<Student> students)
        {
            Console.WriteLine("\n📋 All Student Progress:");
            if (students.Count == 0)
            {
                Console.WriteLine("No registered students yet.");
                return;
            }

            for (int i = 0; i < students.Count; i++)
            {
                Student student = students[i];
                Console.WriteLine($"\n👤 Student: {student.Username}");
                if (student.EnrolledCourses.Count == 0)
                {
                    Console.WriteLine("  No courses enrolled.");
                }
                else
                {
                    for (int j = 0; j < student.EnrolledCourses.Count; j++)
                    {
                        Course course = student.EnrolledCourses[j];
                        double progress = student.GetProgress(course);
                        Console.WriteLine($"  - {course.Title}: {progress}%");
                    }
                }
            }
        }
    }

    // 🔹 Feature 2 Continued: Course Structure
    public class Course
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public Course(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }

    // 🔹 Feature 4: Interface for Progress Tracking
    public interface IProgress
    {
        void UpdateProgress(double percentage);
        double GetProgress();
    }

    public class CourseProgress : IProgress
    {
        private double progress;

        public void UpdateProgress(double percentage)
        {
            progress = Math.Clamp(progress + percentage, 0, 100);
        }

        public double GetProgress()
        {
            return progress;
        }
    }

    // 🔹 Feature 3 & 4: Student Class for Enrollment and Tracking ---> Joy + Alomgir
    public class Student : User
    {
        public List<Course> EnrolledCourses { get; private set; }
        private Dictionary<Course, CourseProgress> ProgressMap;

        public Student(string username, string password) : base(username, password)
        {
            EnrolledCourses = new List<Course>();
            ProgressMap = new Dictionary<Course, CourseProgress>();
        }

        public void Enroll(Course course)
        {
            if (!EnrolledCourses.Contains(course))
            {
                EnrolledCourses.Add(course);
                ProgressMap[course] = new CourseProgress();
                Console.WriteLine($"✅ Enrolled in: {course.Title}");
            }
            else
            {
                Console.WriteLine("⚠️ Already enrolled in this course.");
            }
        }

        public void UpdateCourseProgress(Course course, double percent)
        {
            if (ProgressMap.ContainsKey(course))
            {
                ProgressMap[course].UpdateProgress(percent);
                Console.WriteLine($"📈 Progress for '{course.Title}': {ProgressMap[course].GetProgress()}%");
            }
        }

        public double GetProgress(Course course)
        {
            return ProgressMap.ContainsKey(course) ? ProgressMap[course].GetProgress() : 0;
        }

        // 🔹 Feature 5: Student Dashboard ---> Aunto
        public void ShowDashboard()
        {
            Console.WriteLine($"\n📊 Your Dashboard - {Username}");
            if (EnrolledCourses.Count == 0)
            {
                Console.WriteLine("You have not enrolled in any courses.");
                return;
            }

            for (int i = 0; i < EnrolledCourses.Count; i++)
            {
                Course course = EnrolledCourses[i];
                Console.WriteLine($"- {course.Title}: {ProgressMap[course].GetProgress()}%");
            }
        }
    }

    // 🔹 Course Catalog
    public class CourseCatalog
    {
        private List<Course> courses = new List<Course>();

        public void AddCourse(Course course)
        {
            courses.Add(course);
        }

        public List<Course> GetAllCourses()
        {
            return courses;
        }
    }

    // 🔹 Main Program Logic
    class Program
    {
        static void Main(string[] args)
        {
            Admin admin = new Admin("admin", "1234");
            CourseCatalog catalog = new CourseCatalog();
            List<Student> students = new List<Student>();
            Student loggedInStudent = null;

            // Add predefined courses
            catalog.AddCourse(admin.CreateCourse("C# Basics", "Introduction to C#"));
            catalog.AddCourse(admin.CreateCourse("OOP in C#", "Learn OOP concepts"));
            catalog.AddCourse(admin.CreateCourse("ASP.NET Core", "Web development with .NET"));

            while (true)
            {
                Console.WriteLine("\n🎓 Welcome to Online Course Tracker");
                Console.WriteLine("1. Register as Student");
                Console.WriteLine("2. Login as Student");
                Console.WriteLine("3. Admin Dashboard");
                Console.WriteLine("4. Exit");
                Console.Write("Choose option: ");
                int mainOption = int.Parse(Console.ReadLine());

                if (mainOption == 1)
                {
                    Console.Write("Enter username: ");
                    string user = Console.ReadLine();
                    Console.Write("Enter password: ");
                    string pass = Console.ReadLine();
                    students.Add(new Student(user, pass));
                    Console.WriteLine("✅ Registration successful!");
                }

                else if (mainOption == 2)
                {
                    Console.Write("Enter username: ");
                    string user = Console.ReadLine();
                    Console.Write("Enter password: ");
                    string pass = Console.ReadLine();

                    loggedInStudent = null;
                    for (int i = 0; i < students.Count; i++)
                    {
                        if (students[i].Username == user && students[i].Authenticate(pass))
                        {
                            loggedInStudent = students[i];
                            break;
                        }
                    }

                    if (loggedInStudent != null)
                    {
                        Console.WriteLine($"✅ Welcome, {loggedInStudent.Username}!");

                        while (true)
                        {
                            Console.WriteLine("\n--- Student Dashboard ---");
                            Console.WriteLine("1. View All Courses");
                            Console.WriteLine("2. Enroll in Course");
                            Console.WriteLine("3. Update Progress");
                            Console.WriteLine("4. My Dashboard");
                            Console.WriteLine("5. Logout");
                            Console.Write("Choose: ");
                            int studentOption = int.Parse(Console.ReadLine());

                            if (studentOption == 1)
                            {
                                List<Course> allCourses = catalog.GetAllCourses();
                                Console.WriteLine("\n📚 Available Courses:");
                                for (int i = 0; i < allCourses.Count; i++)
                                {
                                    Console.WriteLine($"- {allCourses[i].Title}: {allCourses[i].Description}");
                                }
                            }
                            else if (studentOption == 2)
                            {
                                Console.Write("Enter course title: ");
                                string title = Console.ReadLine();
                                List<Course> allCourses = catalog.GetAllCourses();
                                Course found = null;
                                for (int i = 0; i < allCourses.Count; i++)
                                {
                                    if (allCourses[i].Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                                    {
                                        found = allCourses[i];
                                        break;
                                    }
                                }

                                if (found != null)
                                {
                                    loggedInStudent.Enroll(found);
                                }
                                else
                                {
                                    Console.WriteLine("❌ Course not found.");
                                }
                            }
                            else if (studentOption == 3)
                            {
                                Console.Write("Enter course title: ");
                                string title = Console.ReadLine();
                                Course target = null;
                                for (int i = 0; i < loggedInStudent.EnrolledCourses.Count; i++)
                                {
                                    if (loggedInStudent.EnrolledCourses[i].Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                                    {
                                        target = loggedInStudent.EnrolledCourses[i];
                                        break;
                                    }
                                }

                                if (target != null)
                                {
                                    Console.Write("Add progress (e.g. 20): ");
                                    double percent = double.Parse(Console.ReadLine());
                                    loggedInStudent.UpdateCourseProgress(target, percent);
                                }
                                else
                                {
                                    Console.WriteLine("❌ Not enrolled in this course.");
                                }
                            }
                            else if (studentOption == 4)
                            {
                                loggedInStudent.ShowDashboard();
                            }
                            else if (studentOption == 5)
                            {
                                Console.WriteLine("👋 Logged out.");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("❌ Invalid option.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("❌ Login failed.");
                    }
                }

                else if (mainOption == 3)
                {
                    Console.Write("Enter admin username: ");
                    string user = Console.ReadLine();
                    Console.Write("Enter admin password: ");
                    string pass = Console.ReadLine();

                    if (admin.Username == user && admin.Authenticate(pass))
                    {
                        admin.ViewAllStudentProgress(students);
                    }
                    else
                    {
                        Console.WriteLine("❌ Invalid admin credentials.");
                    }
                }

                else if (mainOption == 4)
                {
                    Console.WriteLine("👋 Goodbye!");
                    break;
                }

                else
                {
                    Console.WriteLine("❌ Invalid selection.");
                }
            }
        }
    }
}
