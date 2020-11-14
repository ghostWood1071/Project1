using Project1.DataAcessLayer.DataAcess;
using Project1.DataAcessLayer.Model;
using Project1.DataAcessLayer.Model.Interface;
using Project1.LogicalHandlerLayer;
using Project1.Source;
using Project1.UI.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.UI
{
    class MajorUI: IUIable
    {
        MajorHandler majorHandler = new MajorHandler();
        SubjectHandler subjectHandler = new SubjectHandler();
        SubjectUI subject = new SubjectUI();

        private Teacher teacher;

        public MajorUI()
        {

        }

        public MajorUI(Teacher teacher)
        {
            this.teacher = teacher;
        } 

        public void Menu()
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool exit = false;
            string[] menu = { "1.Thêm chuyên ngành mới",
                "2.Sửa thông tin chuyên ngành",
                "3.Xóa chuyên ngành",
                "4.Hiển thị danh sách chuyên ngành",
                "5.Tìm kiếm chuyên ngành",
                "6.Trở lại trang chủ"
            };
            MenuSelector menuSelector = new MenuSelector(menu, "Quản lý chuyên ngành"); 
            while (!exit)
            {
                Console.Clear();
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

        public void Search()
        {
            bool exit = false;
            List<Major> majors = majorHandler.GetMajors();
            List<Subject> subjects = subjectHandler.GetSubjects();
            while (!exit)
            {
                Console.Clear();
                Console.CursorVisible = true;
                List<Major> result = new List<Major>();
                Console.Write("Từ khóa: ");
                string input = Console.ReadLine();
                input = input.ToLower();
                foreach (var major in majors)
                {
                    if (major.ID.Contains(input) ||
                       major.Name.ToLower().Contains(input) ||
                       major.SubjectID.Contains(input))
                    {
                        result.Add(major);
                    }
                }
                Console.Clear();
                PrintTable(result);

                Console.Write("Bạn có muốn nhập tiếp không(nhấn esc để thoát)?");
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
            bool exit = false;
            List<Major> majors = this.teacher.Role == (int)UserPermission.HeadSection ? majorHandler.GetMajors(this.teacher.SubjectID) : majorHandler.GetMajors();
            while (!exit) 
            {
                Console.Clear();
                PrintTable(majors);
                Console.Write("Bạn có muốn nhập tiếp không(nhấn esc để thoát)?");
                ConsoleKeyInfo exitStr = Console.ReadKey();
                if (exitStr.Key == ConsoleKey.Escape)
                    exit = true;
            }
            
            
        }

        public void Delete()
        {
            bool exit = false;
            List<Major> majors = this.teacher.Role == (int)UserPermission.HeadSection ? majorHandler.GetMajors(this.teacher.SubjectID) : majorHandler.GetMajors();
            while (!exit)
            {
                Console.Clear();
                Console.CursorVisible = true;
                PrintTable(majors);
                string id = GetId2("Mã chuyên ngành");
                majorHandler.DeleteMajor(id);
                majors.RemoveAt(majorHandler.GetIndex(id, majors));
                Console.Clear();
                PrintTable(majors);
                Console.Write("Bạn có muốn nhập tiếp không(nhấn esc để thoát)?");
                ConsoleKeyInfo exitStr = Console.ReadKey();
                if (exitStr.Key == ConsoleKey.Escape)
                {
                    Console.CursorVisible = false;
                    exit = true;
                }
            }
           
        }

        public void Update()
        {
            List<Major> majors = this.teacher.Role == (int)UserPermission.HeadSection ? majorHandler.GetMajors(this.teacher.SubjectID) : majorHandler.GetMajors();
            bool exit = false;
            while (!exit)
            {
                Console.Clear();

                PrintTable(majors);
                Console.CursorVisible = true;
                string id = GetId2("Mã chuyên ngành");
                string name = GetName(true);
                string subId = this.teacher.Role == (int)UserPermission.HeadSection ? this.teacher.SubjectID : GetSubId();

                Major newInfo = new Major();
                Major oldInfo = majorHandler.GetMajors()[majorHandler.GetMajorIndex(id)];

                newInfo.ID = oldInfo.ID;

                if (name == "")
                    newInfo.Name = oldInfo.Name;
                else
                    newInfo.Name = name;

                if (subId == "")
                    newInfo.SubjectID = oldInfo.SubjectID;
                else
                    newInfo.SubjectID = subId;

                majorHandler.UpdateMajor(id, newInfo);
                majors[majorHandler.GetIndex(id, majors)] = newInfo;
                Console.Clear();
                PrintTable(majors);

                Console.Write("Bạn có muốn nhập tiếp không(nhấn esc để thoát)?");
                ConsoleKeyInfo exitStr = Console.ReadKey();
                if (exitStr.Key == ConsoleKey.Escape)
                {
                    Console.CursorVisible = false;
                    exit = true;
                }
            }
        }

        public void Add()
        {
            List<Major> majors = this.teacher.Role ==(int) UserPermission.HeadSection? majorHandler.GetMajors(this.teacher.SubjectID) :majorHandler.GetMajors();
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.CursorVisible = true;
                PrintTable(majors);
                string id = GetId("Mã chuyên ngành");
                string name = GetName();
                string subId = this.teacher.Role == (int)UserPermission.HeadSection ? this.teacher.SubjectID : GetSubId();
                majorHandler.AddMajor(new Major(id, name, subId));
                majors.Add(new Major(id, name, subId));
                Console.Clear();
                PrintTable(majors);
                Console.Write("Bạn có muốn nhập tiếp không(nhấn esc để thoát)?");
                ConsoleKeyInfo exitStr = Console.ReadKey();
                if (exitStr.Key == ConsoleKey.Escape)
                {
                    Console.CursorVisible = false;
                    exit = true;
                }
            }
        }

        public string GetId(string title, bool acceptNull = false)
        {
            while (true)
            {
                Console.Write(title+": ");
                string id = Console.ReadLine();
                if (acceptNull && id == "")
                    return id;
                else if (!majorHandler.CheckId(id))
                    Console.WriteLine(title+" gồm 2 chữ số từ 0-9");
                else if (majorHandler.GetMajorIndex(id) >= 0)
                    Console.WriteLine(title+" đã tồn tại");
                else
                    return id;
            }
        }

        public string GetId2(string title, bool acceptNull = false)
        {
            while (true)
            {
                Console.Write(title+": ");
                string id = Console.ReadLine();
                if (acceptNull && id == "")
                    return id;
                else if (!majorHandler.CheckId(id))
                    Console.WriteLine(title+" gồm 2 chữ số từ 0-9");
                else if (majorHandler.GetMajorIndex(id) < 0)
                    Console.WriteLine(title+" không tồn tại");
                else
                    return id;
            }
        }

        public string GetName(bool acceptMull = false)
        {
            while (true)
            {
                Console.Write("Tên chuyên ngành: ");
                string name = Console.ReadLine();
                if (acceptMull && name == "")
                    return name;
                if (!majorHandler.CheckName(name))
                    Console.WriteLine("Tên chuyên nhành gồm 10 kí tự trở lên");
                else
                    return name;
            }

        }

        public string GetSubId(bool acceptNull = false)
        {
            Console.Clear();
            List<Subject> subjects = subjectHandler.GetSubjects();
            string[] selectItem = new string[subjects.Count];
            for (int i = 0; i < subjects.Count; i++)
                selectItem[i] = subjects[i].Name;
            MenuSelector menuSelector = new MenuSelector(selectItem, "Bộ môn");
            return subjects[menuSelector.Selector()].ID;
        } // checked

        public void PrintTable(List<Major> majors )
        {
            List<Subject> subjects = subjectHandler.GetSubjects();
            Table table = new Table(90);
            table.PrintLine();
            table.PrintRow("Id", "Chuyên nghành", "Bộ môn");
            table.PrintLine();
            foreach(var major in majors)
            {
                Subject subject = subjectHandler.GetSubject(major.SubjectID, subjects);
                table.PrintRow(major.ID, major.Name, subject.Name);
            }
            table.PrintLine();
        } //checked

    }
}
