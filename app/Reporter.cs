using System;
using System.Linq;
using System.Collections.Generic;
using CoreSchool.Entities;

namespace CoreSchool.App
{
    public class Reporter
    {
        Dictionary<KeyDictionaty, IEnumerable<SchoolBaseObject>> _dictionary;
        public Reporter(Dictionary<KeyDictionaty, IEnumerable<SchoolBaseObject>> dicObsEsc)
        {
            if (dicObsEsc == null)
                throw new ArgumentNullException(nameof(dicObsEsc));

            _dictionary = dicObsEsc;
        }

        public IEnumerable<Test> GetTestsList()
        {
            if (_dictionary.TryGetValue(KeyDictionaty.Test, out IEnumerable<SchoolBaseObject> list))
            {
                return list.Cast<Test>();
            }
            {
                return new List<Test>();
            }
        }

        public IEnumerable<string> GetSubjectsList()
        {
            return GetSubjectsList(out var dummy);
        }

        public IEnumerable<string> GetSubjectsList(
            out IEnumerable<Test> testsList)
        {
            testsList = GetTestsList();

            return (from Test ev in testsList
                    select ev.Subject.Name).Distinct(); ;
        }

        public Dictionary<string, IEnumerable<Test>> GetDictionaryTestsForSubjects()
        {
            var dictionaryResponse = new Dictionary<string, IEnumerable<Test>>();

            var subjectsList = GetSubjectsList(out var testsList);

            foreach (var subject in subjectsList)
            {
                var subjectTest = from eval in testsList
                                where eval.Subject.Name == subject
                                select eval;

                dictionaryResponse.Add(subject, subjectTest);
            }

            return dictionaryResponse;
        }

        public Dictionary<string, IEnumerable<object>> GetAverageStudentForSubject()
        {
            var answer = new Dictionary<string, IEnumerable<object>>();
            var dictionaryTestsForSubjects = GetDictionaryTestsForSubjects();

            foreach (var subjectWithTests in dictionaryTestsForSubjects)
            {
                var promStudent = from eval in subjectWithTests.Value
                                 group eval by new
                                 {
                                     eval.Student.UniqueId,
                                     eval.Student.Name
                                 }
                            into studentTestGroup
                                 select new StudentsAverage
                                 {
                                     studentId = studentTestGroup.Key.UniqueId,
                                     studentName = studentTestGroup.Key.Name,
                                     average = studentTestGroup.Average(test => test.Note)
                                 };

                answer.Add(subjectWithTests.Key, promStudent);
            }

            return answer;
        }
    }
}