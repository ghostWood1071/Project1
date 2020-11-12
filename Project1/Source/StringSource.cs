using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Source
{
    class StringSource
    {
        public static string TEACHER_DTB_NAME = "teacher.txt";
        public static string ROOM_DB_NAME = "room.txt";
        public static string SUBJECT_DB_NAME = "subject.txt";
        public static string MAJOR_DB_NAME = "major.txt";
        public static string CLASS_DB_NAME = "class.txt";
        public static string TERM_DB_NAME = "term.txt";
        public static string ASSIGNMENT_DB_NAME = "assignment.txt";
        public static string DEFAULT_PASSWORD = "0123";
    }

    enum UserPermission{
        Admin  = 0,
        HeadSection = 1,
        Normal = 2
    }

    enum TeacherPosition
    {
        Dean,
        DeanAssist,
        HeadSection,
        HeadSectionAsist,
        Teacher,
        Ministry
    }

    class NumberValue
    {
        public static float StandardTime = 250.0f;
        public static int CreditNumPerSubject = 20;
    }

}
