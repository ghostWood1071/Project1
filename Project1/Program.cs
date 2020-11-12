using Project1.DataAcessLayer.DataAcess;
using Project1.DataAcessLayer.Model;
using Project1.LogicalHandlerLayer;
using Project1.UI;
using Project1.UI.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WindowHeight = Console.LargestWindowHeight;
            //Console.WindowWidth = Console.LargestWindowWidth;
            //TeacherUI teacherUI = new TeacherUI();
            //teacherUI.Menu();
            //RoomUI roomUI = new RoomUI();
            //roomUI.Menu();
            //SubjectUI subjectUI = new SubjectUI();
            //subjectUI.Menu();
            //MajorUI majorUI = new MajorUI();
            //majorUI.Menu();
            //ClassUI classUI = new ClassUI();
            //classUI.Menu();
            //TermUI termUI = new TermUI();
            //termUI.Menu();
            //ClassUI classUI = new ClassUI();
            //classUI.Menu();
            //string classType = "hello";

            LoginUI loginUI = new LoginUI();
            loginUI.Login();
            //AssignmentUI assignmentUI = new AssignmentUI();
            //assignmentUI.Menu();

            //TermUI termUI = new TermUI();
            //termUI.Menu();
            //Assignment assignment = new Assignment("101191A","001", "001");

            //AssignmentHandler handler = new AssignmentHandler();
            //handler.Update("191A002001", assignment);
        }
    }
}
