using System;
using System.Collections.Generic;
using System.Linq;
using CoreSchool.Entities;
using CoreSchool.Useful;

namespace CoreSchool.App
{
    public sealed class SchoolEngine
    {
        public School School { get; set; }

        public SchoolEngine()
        {

        }

        public void Initialize()
        {
            School = new School("North Well", 2012, SchoolTypes.Elementary,
            city: "Akron", country: "USA"
            );

            UploadCourses();
            UploadSubjects();
            UploadEvaluations();

        }

        public void PrintDictionary(Dictionary<KeyDictionaty, IEnumerable<SchoolBaseObject>> dic, bool imprEval = false)
        {
            foreach (var objdic in dic)
            {
                Printer.WriteTitle(objdic.Key.ToString());

                foreach (var val in objdic.Value)
                {
                    switch (objdic.Key)
                    {
                        case KeyDictionaty.Test:
                            if (imprEval)
                                Console.WriteLine(val);
                            break;
                        case KeyDictionaty.School:
                            Console.WriteLine("School: " + val);
                            break;
                        case KeyDictionaty.Student:
                            Console.WriteLine("Student: " + val.Name);
                            break;
                        case KeyDictionaty.Course:
                            var curtmp = val as Course;
                            if (curtmp != null)
                            {
                                int count = curtmp.Students.Count;
                                Console.WriteLine("Course: " + val.Name + "Students amount: " + count);
                            }
                            break;
                        default:
                            Console.WriteLine(val);
                            break;
                    }
                }
            }
        }

        public Dictionary<KeyDictionaty, IEnumerable<SchoolBaseObject>> GetDiccionarioObjetos()
        {
            var dictionary = new Dictionary<KeyDictionaty, IEnumerable<SchoolBaseObject>>();

            //dictionary.Add(KeyDictionaty.School, new[] { School });
            dictionary.Add(KeyDictionaty.Course, School.Courses.Cast<SchoolBaseObject>());

            var list_tmp = new List<Test>();
            var list_tmpas = new List<Subject>();
            var list_tmpal = new List<Student>();

            foreach (var cur in School.Courses)
            {
                list_tmpas.AddRange(cur.Subjects);
                list_tmpal.AddRange(cur.Students);

                foreach (var alum in cur.Students)
                {
                    list_tmp.AddRange(alum.Tests);
                }

            }
            dictionary.Add(KeyDictionaty.Test, list_tmp.Cast<SchoolBaseObject>());
            dictionary.Add(KeyDictionaty.Subject, list_tmpas.Cast<SchoolBaseObject>());
            dictionary.Add(KeyDictionaty.Student, list_tmpal.Cast<SchoolBaseObject>());
            return dictionary;
        }

        public IReadOnlyList<SchoolBaseObject> GetSchoolObjects(
            bool bringTests = true,
            bool bringStudents = true,
            bool bringsSubjects = true,
            bool bringCourses = true
            )
        {
            return GetSchoolObjects(out int dummy, out dummy, out dummy, out dummy);
        }

        public IReadOnlyList<SchoolBaseObject> GetSchoolObjects(
            out int countTests,
            bool bringTests = true,
            bool bringStudents = true,
            bool bringsSubjects = true,
            bool bringCourses = true
           )
        {
            return GetSchoolObjects(out countTests, out int dummy, out dummy, out dummy);
        }

        public IReadOnlyList<SchoolBaseObject> GetSchoolObjects(
            out int countTests, out int conteoCursos,
            bool bringTests = true,
            bool bringStudents = true,
            bool bringsSubjects = true,
            bool bringCourses = true
                        )
        {

            return GetSchoolObjects(out countTests, out conteoCursos, out int dummy, out dummy);
        }

        public IReadOnlyList<SchoolBaseObject> GetSchoolObjects(
                        out int countTests,
                        out int countCourses,
                        out int countSubjects,
                        bool bringTests = true,
                        bool bringStudents = true,
                        bool bringsSubjects = true,
                        bool bringCourses = true
             )
        {

            return GetSchoolObjects(out countTests, out countCourses, out countSubjects, out int dummy);
        }

        public IReadOnlyList<SchoolBaseObject> GetSchoolObjects(
            out int countTests,
            out int countCourses,
            out int countSubjects,
            out int countStudents,
            bool bringTests = true,
            bool bringStudents = true,
            bool bringsSubjects = true,
            bool bringCourses = true
            )
        {
            countStudents = countSubjects = countTests = 0;

            var list_Obj = new List<SchoolBaseObject>();
            list_Obj.Add(School);

            if (bringCourses)
                list_Obj.AddRange(School.Courses);

            countCourses = School.Courses.Count;
            foreach (var course in School.Courses)
            {
                countSubjects += course.Subjects.Count;
                countStudents += course.Students.Count;

                if (bringsSubjects)
                    list_Obj.AddRange(course.Subjects);

                if (bringStudents)
                    list_Obj.AddRange(course.Students);

                if (bringTests)
                {
                    foreach (var student in course.Students)
                    {

                        list_Obj.AddRange(student.Tests);
                        countTests += student.Tests.Count;
                    }
                }
            }

            return list_Obj.AsReadOnly();
        }

        #region Loading Methods
        private void UploadEvaluations()
        {
            var rnd = new Random();
            foreach (var course in School.Courses)
            {
                foreach (var subject in course.Subjects)
                {
                    foreach (var student in course.Students)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            var ev = new Test
                            {
                                Subject = subject,
                                Name = $"{subject.Name} Ev#{i + 1}",
                                Note = MathF.Round(
                                    5 * (float)rnd.NextDouble()
                                    , 2),
                                Student = student
                            };
                            student.Tests.Add(ev);
                        }
                    }
                }
            }

        }
        private void UploadSubjects()
        {
            foreach (var course in School.Courses)
            {
                var list_Subject = new List<Subject>(){
                            new Subject{Name="Mathematics"} ,
                            new Subject{Name="Physical Education"},
                            new Subject{Name="Spanish"},
                            new Subject{Name="Natural Sciences"}
                };
                course.Subjects = list_Subject;
            }
        }

        private List<Student> GenerateStudentsAtRandom(int amount)
        {
            string[] firstName = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] lastName = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] secondName = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var list_Student = from n1 in firstName
                               from n2 in secondName
                               from n3 in lastName
                               select new Student { Name = $"{n1} {n2} {n3}" };

            return list_Student.OrderBy((al) => al.UniqueId).Take(amount).ToList();
        }

        private void UploadCourses()
        {
            School.Courses = new List<Course>(){
                        new Course(){ Name = "101", Journey = JourneyTypes.Morning },
                        new Course() {Name = "201", Journey = JourneyTypes.Morning},
                        new Course{Name = "301", Journey = JourneyTypes.Morning},
                        new Course(){ Name = "401", Journey = JourneyTypes.Afternoon },
                        new Course() {Name = "501", Journey = JourneyTypes.Afternoon},
            };

            Random rnd = new Random();
            foreach (var c in School.Courses)
            {
                int randomAmount = rnd.Next(5, 20);
                c.Students = GenerateStudentsAtRandom(randomAmount);
            }
        }
    }
    #endregion
}