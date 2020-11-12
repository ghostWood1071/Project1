﻿using Project1.DataAcessLayer.Model;
using Project1.DataAcessLayer.Model.Interface;
using Project1.LogicalHandlerLayer;
using Project1.UI.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Project1.UI
{
    class SubjectUI:IUIable
    {
        SubjectHandler handler = new SubjectHandler();
        public void Menu()
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            string[] menu =
            {
               "1.Thêm bộ môn mới",
               "2.Sửa thông tin bộ môn",
                "3.Xóa bộ môn",
                "4.Hiển thị danh sách bộ môn",
                "5.Tìm kiếm bộ môn",
                "6.Trở lại trang chủ",
            };
            MenuSelector menuSelector = new MenuSelector(menu, "Quản lý bộ môn");
            bool exit = false;
            while (!exit)
            {
                int mode = menuSelector.Selector();
                    switch (mode)
                    {
                        case 1:
                            Add();
                            Console.Clear();
                            break;
                        case 2:
                            Update();
                            Console.Clear();
                            break;
                        case 3:
                            Delete();
                            Console.Clear();
                            break;
                        case 4:
                            Show();
                            Console.Clear();
                            break;
                        case 5:
                            Search();
                            Console.Clear();
                            break;
                        case 6:
                            exit = true;
                            Console.Clear();
                            break;
                    }
                
            }
        }

        public void Add()
        {
            bool exit = false;
            while (!exit)
            {
                handler.AddSubject(new Subject(GetId(), GetName()));
                Console.Write("Bạn có muốn nhập tiếp không?(y/n): ");
                string exitStr = Console.ReadLine();
                if (exitStr == "n")
                    exit = true;
            }
        }

        public string GetId(bool acceptNull = false)
        {
            while (true)
            {
                Console.Write("Mã bộ môn: ");
                string id = Console.ReadLine();
                if (acceptNull && id == "")
                    return id;
                if (!handler.CheckIdSyntax(id))
                    Console.WriteLine("Mã gồm 2 kí tự là số 0-9");
                else if (handler.GetSubIndex(id) >= 0)
                    Console.WriteLine("Đã tồn tại");
                else
                    return id;
            }
        }

        public string GetId2(bool acceptNull = false)
        {
            while (true)
            {
                Console.Write("Mã bộ môn: ");
                string id = Console.ReadLine();
                if (acceptNull && id == "")
                    return id;
                if (!handler.CheckIdSyntax(id))
                    Console.WriteLine("Mã gồm 2 kí tự là số 0-9");
                else if (handler.GetSubIndex(id) < 0)
                    Console.WriteLine("Bộ môn không tồn tại");
                else
                    return id;
            }
        }

        public string GetName(bool acceptNull = false)
        {
            while (true)
            {
                Console.Write("Tên bộ môn: ");
                string name = Console.ReadLine();
                if (acceptNull && name == "")
                    return name;
                if (!handler.CheckName(name))
                    Console.WriteLine("Tên gồm 10 kí tự trở lên");
                else
                    return name;
            }
        }

        public void Update()
        {
            List<Subject> subjects = handler.GetSubjects();
            string id = GetId2();
            string name = GetName(true);
            int index = handler.GetSubIndex(id);
            Subject newInfo = new Subject();
            Subject oldInfo = subjects[index];

            newInfo.ID = id;

            if (name == "")
                newInfo.Name = oldInfo.Name;
            else
                newInfo.Name = name;

            handler.UpdateSubject(id, newInfo);
        }

        public void Delete()
        {
            string id = GetId2();
            handler.DeleteSubject(id);
        }

        public void Search()
        {
            Console.Write("Từ khóa: ");
            string input = Console.ReadLine();
            List<Subject> subjects = handler.GetSubjects();
            foreach (var sub in subjects)
                if (sub.ID.ToLower().Contains(input) || sub.Name.ToLower().Contains(input))
                    Console.WriteLine(sub.ID + "|" + sub.Name);
        }

        public void Show()
        {
            List<Subject> subjects = handler.GetSubjects();
            foreach (var sub in subjects)
                    Console.WriteLine(sub.ID + "|" + sub.Name);
        }

        public void PrintTable(List<Subject> subjecs)
        {

        }
    }
}
