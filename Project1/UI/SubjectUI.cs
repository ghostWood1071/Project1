using Project1.DataAcessLayer.Model;
using Project1.DataAcessLayer.Model.Interface;
using Project1.LogicalHandlerLayer;
using Project1.UI.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
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
                        case 0:
                            Add();
                            Console.Clear();
                            break;
                        case 1:
                            Update();
                            Console.Clear();
                            break;
                        case 2:
                            Delete();
                            Console.Clear();
                            break;
                        case 3:
                            Show();
                            Console.Clear();
                            break;
                        case 4:
                            Search();
                            Console.Clear();
                            break;
                        case 5:
                            exit = true;
                            Console.Clear();
                            break;
                    }
                
            }
        }

        public void Add()
        {
            List<Subject> subjects = handler.GetSubjects();
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.CursorVisible = true;
                PrintTable(subjects);
                Subject subject = new Subject(GetId(), GetName());
                handler.AddSubject(subject);
                subjects.Add(subject);
                Console.Clear();
                PrintTable(subjects);
                Console.Write("Bạn có muốn nhập tiếp không?(esc để thoát)");
                ConsoleKeyInfo exitStr = Console.ReadKey();
                if (exitStr.Key == ConsoleKey.Escape)
                {
                    Console.CursorVisible = false;
                    exit = true;
                }
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
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.CursorVisible = true;
                List<Subject> subjects = handler.GetSubjects();
                PrintTable(subjects);
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
                subjects[handler.GetSubIndex(id, subjects)] = newInfo;
                Console.Clear();
                PrintTable(subjects);
                Console.Write("Bạn có muốn nhập tiếp không?(esc để thoát)");
                ConsoleKeyInfo exitStr = Console.ReadKey();
                if (exitStr.Key == ConsoleKey.Escape)
                {
                    Console.CursorVisible = false;
                    exit = true;
                }
            }

        }

        public void Delete()
        {
            bool exit = false;
            List<Subject> subjects = handler.GetSubjects();
            while (!exit)
            {
                Console.Clear();
                Console.CursorVisible = true;
                PrintTable(subjects);
                string id = GetId2();
                handler.DeleteSubject(id);
                subjects.RemoveAt(handler.GetSubIndex(id, subjects));
                Console.Clear();
                PrintTable(subjects);
                Console.Write("Bạn có muốn nhập tiếp không?(esc để thoát)");
                ConsoleKeyInfo exitStr = Console.ReadKey();
                if (exitStr.Key == ConsoleKey.Escape)
                {
                    Console.CursorVisible = false;
                    exit = true;
                }

            }
        }

        public void Search()
        {
            bool exit = false;
            List<Subject> subjects = handler.GetSubjects();
            while (!exit)
            {
                Console.Clear();
                Console.CursorVisible = true;
                Console.Write("Từ khóa: ");
                string input = Console.ReadLine();
                List<Subject> result = new List<Subject>();
                foreach (var sub in subjects)
                    if (sub.ID.ToLower().Contains(input) || sub.Name.ToLower().Contains(input))
                        result.Add(sub);
                Console.Clear();
                PrintTable(subjects);
                Console.Write("Bạn có muốn nhập tiếp không?(esc để thoát)");
                ConsoleKeyInfo exitStr = Console.ReadKey();
                if (exitStr.Key == ConsoleKey.Escape)
                {
                    Console.CursorVisible = false;
                    exit = true;
                }
            }
        }

        public void Show()
        {
            List<Subject> subjects = handler.GetSubjects();
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                PrintTable(subjects);
                Console.Write("Bạn có muốn nhập tiếp không?(esc để thoát)");
                ConsoleKeyInfo exitStr = Console.ReadKey();
                if (exitStr.Key == ConsoleKey.Escape)
                {
                    Console.CursorVisible = false;
                    exit = true;
                }
            }
        }

        public void PrintTable(List<Subject> subjecs)
        {
            Console.Clear();
            Table table = new Table(90);
            table.PrintLine();
            table.PrintRow("ID","Tên bộ môn");
            table.PrintLine();
            foreach(var sub in subjecs)
            {
                table.PrintRow(sub.ID, sub.Name);
            }
            table.PrintLine();
        }
    }
}
