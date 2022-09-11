using MetalUp;
using OOPRecordsModel;
using System;

namespace OOPRecords.ConsoleUI
{
    class Program
    {
        static void Main()
        {
            var students = new StudentRepository();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Main Menu");
                Console.WriteLine("1. Create Student");
                Console.WriteLine("2. Find Student");
                Console.WriteLine("3. All Students");
                Console.WriteLine("4. Quit");
                int selection = ConsolePlus.ReadInteger("Select option: ", 1, 4);
                Console.Clear();
                switch (selection)
                {
                    case 1: CreateStudent(students); break;
                    case 2: FindStudent(students); break;
                    case 3: AllStudents(students); break;
                    case 4: Quit(); break;
                }
                Console.WriteLine("Press any key to continue ...");
                Console.ReadKey();
            }
        }
        private static void AllStudents(StudentRepository students)
        {
            ConsolePlus.WriteList(students.AllStudents(), "\n");
        }
        private static void FindStudent(StudentRepository students)
        {
            Console.WriteLine("Find Student");
            string match = ConsolePlus.ReadString("Last name or part of last name: ", 1);
            ConsolePlus.WriteList(students.FindStudentByLastName(match), "\n");
        }
        private static void CreateStudent(StudentRepository students)
        {
            Console.WriteLine("Create Student");
            string firstName = ConsolePlus.ReadString("First Name: ", 1);
            string lastName = ConsolePlus.ReadString("Last Name: ", 1);
            DateTime dob = ConsolePlus.ReadDate("Date Of Birth: ", -10000, -1000);
            students.NewStudent(firstName, lastName, dob);
        }

        private static void Quit()
        {
            Console.Write("Shutting down program...");
            Console.Read();
            Environment.Exit(0);
        }
    }
}