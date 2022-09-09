namespace OOPRecordsModel
{
    public class Student
    {
        public int FirstName { get; set; }
        public int LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int Age()
        {
            var today = DateTime.Today;
            int ageToday = today.Year - DateOfBirth.Year;

            if (today.Month < DateOfBirth.Month || (today.Month == DateOfBirth.Month && today.Day < DateOfBirth.Day))
            {
                ageToday--;
            }

            return ageToday;    

        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, Age {Age()}";
        }
    }
}