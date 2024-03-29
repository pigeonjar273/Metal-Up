﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace OOPRecordsModel
{
    public class Initializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            var students = context.Students;
            var alg = NewStudent(students, "Alie", "Algol", "19/02/2004");
            var frt = NewStudent(students, "Forrest", "Fortran", "22/09/2003");
            var jav = NewStudent(students, "James", "Java", "24/03/2004");
            var cee = NewStudent(students, "Celia", "Cee-Sharp", "12/09/2003");
            var vee = NewStudent(students, "Veronica", "Vee-Bee", "05/09/2003");
            var sim = NewStudent(students, "Simon", "Simula", "31/07/2003");
            var typ = NewStudent(students, "Tilly", "TypeScript", "14/01/2003");
            var pyt = NewStudent(students, "Petra", "Python", "17/06/2003");
            var has = NewStudent(students, "Harry", "Haskell", "08/04/2003");
            var cob = NewStudent(students, "Corinie", "Cobol", "28/02/2003");

            var teachers = context.Teachers;
            var dec = NewTeacher(teachers, "Mr.", "Deckerd");
            var tyr = NewTeacher(teachers, "Dr.", "Tyrell");
            var maj = NewTeacher(teachers, "Maj.", "Major");
            var dou = NewTeacher(teachers, "Mrs.", "Doubtfire");
            var doo = NewTeacher(teachers, "Dr.", "Doolittle");
            var str = NewTeacher(teachers, "Dr.", "Strangelove");
            var iss = NewTeacher(teachers, "Ms.", "Issippi");
            var and = NewTeacher(teachers, "Ms.", "Andrist");
            var jek = NewTeacher(teachers, "Dr.", "Jekyll");
            var hyd = NewTeacher(teachers, "Mr.", "Hyde");
            var rob = NewTeacher(teachers, "Mrs.", "Robinson");
            var wor = NewTeacher(teachers, "Mrs.", "Worthington");
            var hu = NewTeacher(teachers, "Dr.", "Hu");
            var ove = NewTeacher(teachers, "Cpt.", "Over");

            alg.Tutor = dec;
            frt.Tutor =  tyr;
            jav.Tutor = maj;

        }

        private Student NewStudent(DbSet<Student> students, string firtsName, string lastName, string dob)
        {
            var s = new Student();
            s.FirstName = firtsName;
            s.LastName = lastName;
            s.DateOfBirth = Convert.ToDateTime(dob);
            students.Add(s);
            return s;
        }

        private Teacher NewTeacher(DbSet<Teacher> teachers, string title, string lastName)
        {
            var t = new Teacher();
            t.Title = title;
            t.LastName = lastName;
            teachers.Add(t);
            return t;
        }



    }
}
