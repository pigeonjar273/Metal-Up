using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPRecordsModel
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public ICollection<Student> Tutees { get; set; } = new List<Student>();
        public override string ToString()
        {
            return $"{Title} {LastName} {JobTitle}";
        }




    }
}
