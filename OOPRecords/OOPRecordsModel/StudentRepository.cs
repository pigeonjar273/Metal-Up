using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPRecordsModel
{
    public class StudentRepository
    {

        private List<Student> Students = new List<Student>();

        public StudentRepository()
        {
            var initialiser = new Initialiser();
            initialiser.Seed(this);
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
            return s;
        }


    }
}
