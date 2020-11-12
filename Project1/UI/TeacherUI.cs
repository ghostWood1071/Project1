using Project1.DataAcessLayer.Model;
using Project1.DataAcessLayer.Model.Interface;
using Project1.LogicalHandlerLayer;
using Project1.Source;
using Project1.UI.Component;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Project1.UI
{
    class TeacherUI: IUIable
    {
        //chưa tìm kiếm được theo tên chức vụ, tên bộ môn
        TeacherHandler teacherHandler = new TeacherHandler();
        SubjectHandler subjectHandler = new SubjectHandler();
        Table table = new Table(80);
        ICRUD<User> CRUD = new UserHandler();
        SubjectHandler handler = new SubjectHandler();

        private Teacher teacher;

        public TeacherUI()
        {

        }

        public TeacherUI(Teacher teacher)
        {
            this.teacher = teacher;
        }

        public void Menu()
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            bool exit = true;
            string[] menu =
            {
                "1.Thêm giảng viên mới",
                "2.Sửa thông tin giảng viên",
                "3.Xóa giảng viên",
                "4.Hiển thị tất cả giảng viên",
                "5.Tìm kiếm giảng viên",
                "6.Trở lại trang chủ"
            };
            MenuSelector menuSelector = new MenuSelector(menu,"Quản lý giảng viên");
            while (exit)
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
                        Console.Clear();
                        exit = false;
                        break;
                }
            }
        }

        public void Add()
        {
            bool exit = true;
            List<Teacher> teachers = this.teacher.Role == (int)UserPermission.HeadSection ? teacherHandler.GetList(this.teacher.SubjectID) : teacherHandler.GetList();
            while (exit)
            {
                Console.Clear();
                Console.CursorVisible = true;
                PrintTable(teachers);
                string id = GetId();
                string name = GetName();
                string subId = this.teacher.Role == (int)UserPermission.HeadSection ? this.teacher.SubjectID : GetSubjectId();
                int position = GetPosition();
                int role = teacherHandler.GetRole(position);
                Teacher teacher = new Teacher(id, name, position, subId, role);
                teacherHandler.Add(teacher);
                teachers.Add(teacher);
                PrintTable(teachers);
                Console.Write("bạn có muốn nhập tiếp?(y/n): ");
                string exitStr = Console.ReadLine();
                if (exitStr == "n")
                    exit = false;
            }
        }

        public void Update()
        {
            Console.CursorVisible = true;
            bool exit = false;
            List<Teacher> teachers = this.teacher.Role == (int)UserPermission.HeadSection ? teacherHandler.GetList(this.teacher.SubjectID) : teacherHandler.GetList();
            while (!exit)
            {   
                PrintTable(teachers);
                string id = GetId2();
                string name = GetName(true);
                string subId = this.teacher.Role == (int)UserPermission.HeadSection ? this.teacher.SubjectID : GetSubjectId(true);
                int position = GetPosition(true);
                

                Teacher oldInfo = teacherHandler.GetInfor(id);
                oldInfo.Account = oldInfo.ID;
                oldInfo.Password = CRUD.GetList()[CRUD.GetIndex(oldInfo.ID)].Password;

                Teacher newInfo = new Teacher();

                if (name != "")
                    newInfo.Name = name;
                else newInfo.Name = oldInfo.Name;

                if (position != -1)
                {
                    newInfo.Position = position;
                    newInfo.Role = teacherHandler.GetRole(position);
                }
                else
                {
                    newInfo.Position = oldInfo.Position;
                    newInfo.Role = oldInfo.Role;
                }

                if (subId != "")
                    newInfo.SubjectID = subId;
                else newInfo.SubjectID = oldInfo.SubjectID;

                newInfo.ID = id;
                newInfo.Account = id;
                newInfo.Password = oldInfo.Password;

                teacherHandler.Update(id, newInfo);
                teachers[teacherHandler.GetIndex(id, teachers)] = newInfo;
                PrintTable(teachers);
                Console.Write("bạn có muốn tiếp tục?: ");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.CursorVisible = false;
                    exit = true;
                }
            }
        }

        public void Delete()
        {
            Console.Clear();
            Console.CursorVisible = true;
            List<Teacher> teachers = this.teacher.Role == (int)UserPermission.HeadSection ? teacherHandler.GetList(this.teacher.SubjectID) : teacherHandler.GetList();
            bool exit = false;
            while (!exit)
            {
                PrintTable(teachers);
                string id = GetId2();
                teachers.RemoveAt(teacherHandler.GetIndex(id, teachers));
                teacherHandler.Delete(id);
                PrintTable(teachers);
                Console.Write("bạn có muốn tiếp tục?: ");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.CursorVisible = false;
                    exit = true;
                }
            }
            
        }

        public void Show()
        {
            Console.Clear();
            List<Teacher> teachers = this.teacher.Role == (int) UserPermission.HeadSection?teacherHandler.GetList(this.teacher.SubjectID): teacherHandler.GetList();
            PrintTable(teachers);
            Console.WriteLine("Nhấn esc để thoát");
            ConsoleKeyInfo keyInfo =  Console.ReadKey();
            if(keyInfo.Key == ConsoleKey.Escape)
                Console.Clear();
        }

        public void PrintTable(List<Teacher> teachers)
        {
            Console.Clear();
            List<Subject> subjects = handler.GetSubjects();
            
            table.PrintLine();
            table.PrintRow("ID", "Tên", "Chức vụ", "Bộ môn");
            table.PrintLine();
            foreach (var item in teachers)
            {
                table.PrintRow(
                    item.ID,
                    item.Name,
                    teacherHandler.GetPosition(item.Position),
                    subjects[subjectHandler.GetSubIndex(item.SubjectID)].Name);

            }
            table.PrintLine();
        }

        public void ShowTeacher2()
        {
        }

        public void Search()
        {
            bool exit = false;
            Table table = new Table(90);
            List<Subject> subjects = handler.GetSubjects();
            List<Teacher> teachers = this.teacher.Role == (int)UserPermission.HeadSection ? teacherHandler.GetList(this.teacher.SubjectID) : teacherHandler.GetList();
            while (!exit)
            {
                Console.Clear();
                Console.Write("Từ khóa: ");
                string input = Console.ReadLine();
                table.PrintLine();
                table.PrintRow("ID", "Tên", "Chức vụ", "Bộ môn");
                table.PrintLine();
                foreach (var teacher in teachers)
                {
                    if (teacher.ID.Contains(input) || teacher.Name.Contains(input) || input.Contains(teacher.Position.ToString()) || teacher.SubjectID.Contains(input))
                        table.PrintRow(
                        teacher.ID,
                        teacher.Name,
                        teacherHandler.GetPosition(teacher.Position),
                        subjects[subjectHandler.GetSubIndex(teacher.SubjectID)].Name);
                }
                table.PrintLine();
                Console.Write("bạn có muốn tiếp tục?: ");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.CursorVisible = false;
                    exit = true;
                }
            }
        }

        public string GetId()
        {
            // kiểm tra tồn tại + check cú pháp
            while (true)
            {
                Console.Write("Mã giáo viên: ");
                string id = Console.ReadLine();
                if (!teacherHandler.CheckID(id))
                    Console.WriteLine("Mã giáo viên gồm 3 chữ số 0-9");
                else if (teacherHandler.GetIndex(id) >= 0)
                    Console.WriteLine("Giảng viên đã tồn tại");
                else
                    return id;
            }
        }

        public string GetId2(bool acceptNull = false)
        {
            while (true)
            {
                Console.Write("Mã giáo viên: ");
                string id = Console.ReadLine();
                if (acceptNull && id == "")
                    return id;
                if (!teacherHandler.CheckID(id))
                    Console.WriteLine("Mã giáo viên gồm 3 chữ số 0-9");
                else if (teacherHandler.GetIndex(id)<0)
                    Console.WriteLine("Giảng viên không tồn tại");
                else
                    return id;
            }

        }

        public string GetName(bool accpetNull = false)
        {
            while (true)
            {
                Console.Write("Tên giảng viên: ");
                string name = Console.ReadLine();
                if (!teacherHandler.CheckName(name, accpetNull))
                    Console.WriteLine("Tên giảng viên gồm 10 kí tự trở lên(a-z)");
                else
                    return name;
            }
        }

        public int GetPosition(bool acceptNull = false)
        {
            Console.Clear();
            string[] menu =
            {
                "0.Trưởng Khoa",
                "1.Phó Trưởng khoa",
                "2.Trưởng bộ môn",
                "3.Phó trưởng bộ môn",
                "4.Giảng viên",
                "5.Giáo vụ khoa",
            };
            MenuSelector selector = new MenuSelector(menu, "Chức vụ: ");
            return selector.Selector();
        }

        public string GetSubjectId(bool acceptNull = false)
        {
            Console.Clear();
            List<Subject> subjects = subjectHandler.GetSubjects();
            string[] selectItem = new string[subjects.Count];
            for (int i = 0; i < subjects.Count; i++)
                selectItem[i] = subjects[i].Name;
            MenuSelector menuSelector = new MenuSelector(selectItem, "Bộ môn: ");
            return subjects[menuSelector.Selector()].ID;
        }

    }
}
