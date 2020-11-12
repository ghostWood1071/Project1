using Project1.DataAcessLayer.Model;
using Project1.DataAcessLayer.Model.Interface;
using Project1.LogicalHandlerLayer;
using Project1.Source;
using Project1.UI.Component;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project1.UI
{
    class UserUI : IUIable
    {
        private User user;
        UserHandler userHandler = new UserHandler();
        AssignmentHandler assignmentHandler = new AssignmentHandler();
        TeacherHandler TeacherHandler = new TeacherHandler();
        TermsHandler termsHandler = new TermsHandler();

        public UserUI()
        {

        }

        public UserUI(User user)
        {
            this.user = user;
        }

        public void Menu()
        {
            bool exit = false;
            
                Console.Clear();
                switch (user.Role)
                {
                    case (int)UserPermission.Admin:
                    case (int)UserPermission.HeadSection:
                        ChangePassword();
                        Console.Clear();
                        break;
                    case (int)UserPermission.Normal:
                        PrintMenuSelector();
                        Console.Clear();
                        break;
                }
            
        }

        public void ReadAssignment()
        {
            List<Assignment> assignments = assignmentHandler.GetList(this.user.Account);
            List<Term> terms = termsHandler.GetListTerm();
            Teacher teacher = TeacherHandler.GetInfor(user.Account);

            Console.WriteLine("Giảng viên: ", teacher.Name);

            Table table = new Table(90);

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                table.PrintLine();
                table.PrintRow("Học phần", "Lớp", "Học kỳ", "Năm học");
                table.PrintLine();
                foreach (Assignment assignment in assignments)
                {
                    table.PrintRow(termsHandler.GetTerm(assignment.TermID, terms).Name, assignment.ClassID, assignment.Semester.ToString(), assignment.Year);
                }
                table.PrintLine();
                Console.WriteLine("Nhấn esc để thoát");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Escape)
                    exit = true;
            }
        }

        public void ChangePassword()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Mật khẩu cũ: ");
                string password = Console.ReadLine();
                if (password == user.Password)
                {
                    Console.WriteLine("Mật khẩu mới: ");
                    string newPass = Console.ReadLine();
                    user.Password = newPass;
                    userHandler.Update(user.Account, user);
                    exit = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Mật khẩu sai");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
            }

        }

        public void PrintMenuSelector()
        {
            bool exit = false;
            while (!exit)
            {
                string[] menu = { "Xem phân công", "Đổi mật khẩu", "thoát"};
                MenuSelector menuSelector = new MenuSelector(menu, "");
                switch (menuSelector.Selector())
                {
                    case 0:
                        ReadAssignment();
                        Console.Clear();
                        break;
                    case 1:
                        ChangePassword();
                        Console.Clear();
                        break;
                    case 2:
                        exit = true;
                        Console.Clear();
                        LoginUI loginUI = new LoginUI();
                        loginUI.Login();
                        break;
                }
            }
        }

        public void Add()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Search()
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
