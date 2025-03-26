using JoinsUsingLINQ;

List<Student> students = new List<Student>
{
    new Student{Name = "Ram", StudentID = 1 },
    new Student{Name = "Ramu", StudentID = 2 },
    new Student{Name = "Krish", StudentID = 3 },
    new Student{Name = "Ramya", StudentID = 4 }
};

List<Course> courses =  
[
    new Course{CourseID = 100, Name = "Maths", StudentID = 2 },
    new Course{CourseID = 101,Name = "Physics", StudentID = 3 },
    new Course{CourseID = 103, Name = "Chemistry", StudentID = 6 },
    new Course{CourseID = 104, Name = "Biology", StudentID = 8 },
    new Course{CourseID = 105, Name = "Zoology", StudentID = 9}
];

var query = from student in students
           join course in courses on student.StudentID equals course.StudentID
           select new {student, course};                                           // selecting both objects directly

foreach (var result in query)
    Console.WriteLine($"{result.student.Name} is pursuing {result.course.Name}");

Console.WriteLine();

var query1 = from student in students
             join course in courses on student.StudentID equals course.StudentID
             select new { StudentName = student.Name, NameOfCourse = course.Name };  // selecting only required data by creating anonymous type properties

foreach (var result in query1)
    Console.WriteLine($"{result.StudentName} is pursuing {result.NameOfCourse}");

Console.WriteLine();

//Inner join
//An inner join returns only the matching rows from both collections based on a specified condition.

Console.WriteLine("Inner join:");
var innerJoinQuery = from student in students
                     join course in courses on student.StudentID equals course.StudentID
                     select new
                     {
                         student.Name,
                         CourseName = course.Name
                     };
foreach (var result in innerJoinQuery)
    Console.WriteLine($"{result.Name} is pursuing {result.CourseName}");

Console.WriteLine();

//Left join or Left Outer join:
/*  A left outer join returns all rows from the left collection (first collection in the query)
 *  and the matching rows from the right collection (second collection in the query).
 *  If there's no matching row in the right collection, it returns null values for the right collection.
 */
//Example:We want to find all students and their enrolled courses, including students with no courses.

Console.WriteLine("Left join:");
var leftOuterJoinQuery = from student in students
                         join course in courses on student.StudentID equals course.StudentID into courseGroup
                         from course in courseGroup.DefaultIfEmpty()
                         select new
                         {
                             student.Name,
                             CourseName = course != null ? course.Name : "No Course Enrolled"
                         };

foreach (var result in leftOuterJoinQuery)
    Console.WriteLine($"{result.Name} - {result.CourseName}");

Console.WriteLine();

//Right Outer join or Right join:
/*  A right outer join returns all rows from the right collection (second collection in the query) 
 *  and the matching rows from the left collection (first collection in the query). 
 *  If there's no matching row in the left collection, it returns null values for the left collection.
Example:
We want to find all courses and the students enrolled in each course, including courses with no students.
 */
Console.WriteLine("Right join:");
var rightOuterJoinQuery = from course in courses
                          join student in students on course.StudentID equals student.StudentID into studentGroup
                          from student in studentGroup.DefaultIfEmpty()
                          select new
                          {
                              StudentName = student != null? student.Name:"No students enrolled",
                              CourseName = course.Name
                          };


foreach (var result in rightOuterJoinQuery)
    Console.WriteLine($"{result.StudentName} - {result.CourseName}");
Console.WriteLine();

//Full Outer Join (or Full Join) - Not Directly Supported in LINQ:
/*Example (Simulated Full Outer Join):
We want to combine all students and their enrolled courses, as well as all courses and the students enrolled in each course.
 */

Console.WriteLine("Full join:");
var fullOuterJoinQuery = (from student in students
                          join course in courses on student.StudentID equals course.StudentID into courseGroup
                          from course in courseGroup.DefaultIfEmpty()
                          select new
                          {
                              StudentName = student.Name,
                              CourseName = course != null ? course.Name : "No Course Enrolled"
                          })
                         .Union
                         (from course in courses
                          join student in students on course.StudentID equals student.StudentID into studentGroup
                          from student in studentGroup.DefaultIfEmpty()
                          select new
                          {
                              StudentName = student != null ? student.Name : "No students enrolled",
                              CourseName = course.Name
                          });

foreach (var result in fullOuterJoinQuery)
    Console.WriteLine($"{result.StudentName} - {result.CourseName}");
Console.WriteLine();