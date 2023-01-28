using System;
using System.Collections.Generic;
using System.Linq;
using CoreSchool.App;
using CoreSchool.Entities;
using CoreSchool.Useful;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += EventAction;
            AppDomain.CurrentDomain.ProcessExit += (o, s) => Printer.Beep(2000, 1000, 1);

            var engine = new SchoolEngine();
            engine.Initialize();
            Printer.WriteTitle("WELCOME TO THE SCHOOL");

            var reporter = new Reporter(engine.GetDiccionarioObjetos());
            var testsList = reporter.GetTestsList();
            var subjectList = reporter.GetSubjectsList();
            var testForSubjectList = reporter.GetDictionaryTestsForSubjects();
            var averageForSubjectList = reporter.GetAverageStudentForSubject();

            Printer.WriteTitle("Capturing a Console Evaluation.");
            var newTest = new Test();
            string name, notastring;
            float note;

            WriteLine("Enter the name of the evaluation.");
            Printer.PressEnter();
            name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Printer.WriteTitle("The name value cannot be empty.");
                WriteLine("Exiting the program.");
            }
            else
            {
                newTest.Name = name.ToLower();
                WriteLine("The name of the evaluation has been entered correctly.");
            }


            WriteLine("Enter the evaluation score.");
            Printer.PressEnter();
            notastring = Console.ReadLine();


            try
            {
                newTest.Note = float.Parse(notastring);
                if (newTest.Note < 0 || newTest.Note > 5)
                {
                    throw new ArgumentOutOfRangeException("The grade must be between 0 and 5.");
                }
                WriteLine("The evaluation score has been entered correctly.");
                return;
            }
            catch (ArgumentOutOfRangeException arge)
            {
                Printer.WriteTitle(arge.Message);
                WriteLine("Exiting the program.");
            }
            finally
            {
                Printer.WriteTitle("FINALLY");
                Printer.Beep(2500, 500, 3);

            }
            // catch(Exception)
            // {
            //     Printer.WriteTitle("The note value is not a valid number.");
            //     WriteLine("Exiting the program");
        }

        private static void EventAction(object sender, EventArgs e)
        {
            Printer.WriteTitle("LEAVING");
            Printer.Beep(3000, 1000, 3);
            Printer.WriteTitle("EXITED");
        }

        private static void PrintSchoolCourses(School school)
        {

            Printer.WriteTitle("School Courses");


            if (school?.Courses != null)
            {
                foreach (var courses in school.Courses)
                {
                    WriteLine($"Name {courses.Name}, Id  {courses.UniqueId}");
                }
            }
        }
    }
}
