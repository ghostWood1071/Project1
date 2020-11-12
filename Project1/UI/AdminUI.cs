using Project1.DataAcessLayer.Model;
using Project1.DataAcessLayer.Model.Interface;
using Project1.LogicalHandlerLayer;
using Project1.Source;
using Project1.UI.Component;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.UI
{
    class AdminUI : IUIable
    {

        private User user;
        ICRUD<Teacher> CRUD = new TeacherHandler();

        public AdminUI(User user)
        {
            this.user = user;
        }

        public void Menu()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Clear();
            bool exit = false;
            string[] menu = {
                "1.Quản lý giảng viên",
                "2.Thay đổi thông tin đăng nhập",
                "3.Quản lý lớp học",
                "4.Quản lý bộ môn",
                "5.Quản lý chuyên ngành",
                "6.Quản lý học phần",
                "7.Đăng xuất"
            }; 

            while (!exit)
            {

                MenuSelector selector = new MenuSelector(menu, "Quản lý giảng dạy cho Admin");
                int mode = selector.Selector();
                IUIable UI = GetUI(mode);
                if (UI is LoginUI)
                {
                        LoginUI loginUI = new LoginUI();
                        loginUI.Logout();
                }
                else UI.Menu();
            }
        }

        public void Show()
        {
            List<Teacher> teachers = CRUD.GetList();
            Teacher teacher = teachers[CRUD.GetIndex(user.Account)];
            Console.WriteLine("Giang vien: "+teacher.Name);
            Console.WriteLine("Chuc vu: ");
        }

        public IUIable GetUI(int mode)
        {
            TeacherHandler handler = new TeacherHandler();
            Teacher teacher = handler.GetInfor(user.Account);
            teacher.Role = (int)UserPermission.Admin;
            teacher.Password = user.Password;
            switch (mode)
            {
                case 0: return new TeacherUI(teacher);
                case 1: return new UserUI(teacher);
                case 2: return new ClassUI(teacher);
                case 3: return new SubjectUI();
                case 4: return new MajorUI();
                case 5: return new TermUI(teacher);
                default: return new LoginUI();
            }
        }

        public void Update()
        {
            
        }

        public void Add()
        {

        }

        public void Delete()
        {

        }

        public void Search()
        {

        }
    }
}
