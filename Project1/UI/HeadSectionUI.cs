using Project1.DataAcessLayer.Model;
using Project1.DataAcessLayer.Model.Interface;
using Project1.LogicalHandlerLayer;
using Project1.UI.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.UI
{
    class HeadSectionUI : IUIable
    {
        private User user;
        private ICRUD<Teacher> CRUD = new TeacherHandler();

        public HeadSectionUI(User user)
        {
            this.user = user;
        }

        public void Menu()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Clear();
            bool exit = false;
            string[] menu =
            {
                "1.Quản lý phân công",
                "2.Quản lý lớp",
                "3.Quản lý học phần",
                "4.Quản lý giảng viên",
                "5.Quản lý chuyên ngành",
                "5.Đổi mật khẩu",
                "6.Dang Xuat"
            };
            MenuSelector menuSelector = new MenuSelector(menu, "Quản lý giảng dạy cho trưởng bộ môn");
            while (!exit)
            {
                    int mode = menuSelector.Selector();
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
            Console.WriteLine("Ten Giang Vien: " + teacher.Name);
            Console.WriteLine("Chuc Vu: " + teacher.Position);
        }

        public IUIable GetUI(int mode)
        {
            TeacherHandler handler = new TeacherHandler();
            Teacher teacher =  handler.GetInfor(user.Account);
            teacher.Role = user.Role;
            teacher.Password = user.Password;
            switch (mode)
            {
                case 0: return new AssignmentUI(teacher);
                case 1: return new ClassUI(teacher);
                case 2: return new TermUI(teacher);
                case 3: return new TeacherUI(teacher);
                case 4: return new MajorUI(teacher);
                case 5: return new UserUI(teacher);
                default: return new LoginUI();
            }
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

        public void Update()
        {
            
        }
    }
}
