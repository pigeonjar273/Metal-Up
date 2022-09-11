using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OOPRecordsModel
{
    public class StudentRepository
    {

        private List<Student> Students = new List<Student>();

        private const string fileName = @"C:\Users\micha\source\repos\pigeonjar273\Metal-Up\OOPRecords\OOPRecords.ConsoleUI\StudentsFile.json";

        public StudentRepository()
        {
            if (File.Exists(fileName))
            {
                Load();
            }
            else
            {
                var initialiser = new Initialiser();
                initialiser.Seed(this);
                SaveAll();
            }
        }


        public void Add(Student s)
        {
            Students.Add(s);
        }

        public IEnumerable<Student> AllStudents()
        {
            return Students;
        }

        public IEnumerable<Student> FindStudentByLastName(string lastName)
        {
            return from s in AllStudents()
                   where s.LastName.ToUpper().Contains(lastName.ToUpper())
                   select s;

        }

        public Student NewStudent(string firstName, string lastName, DateTime dob)
        {
            var s = new Student();
            s.FirstName = firstName;
            s.LastName = lastName;
            s.DateOfBirth = dob;
            Add(s);
            SaveAll();
            return s;
        }

        public void Load()
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string json = reader.ReadToEnd();
                Students = JsonSerializer.Deserialize<List<Student>>(json);
            }
        }

        public void SaveAll()
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(Students, options);
                writer.Write(json);
                writer.Flush();
            }

        }



    }
}
