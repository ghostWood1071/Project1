using Project1.DataAcessLayer.Model;
using Project1.DataAcessLayer.Model.Interface;
using Project1.LogicalHandlerLayer;
using Project1.UI.Component;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace Project1.UI
{
    class AssignmentUI : IUIable
    {
        AssignmentHandler handler = new AssignmentHandler();

        TeacherHandler teacherHandler = new TeacherHandler();

        ClassHandler classHandler = new ClassHandler();

        TermsHandler termHandler = new TermsHandler();

        private Teacher user;

        public AssignmentUI(Teacher user)
        {
            this.user = user;
        }

        public AssignmentUI()
        {

        }

        public void Menu()
        {
            Console.Clear();
            bool exit = false;
            while (!exit)
            {
                string[] menu = { "Thêm, Xóa, Sửa phân công", "Thống kê", "Thoát" };
                MenuSelector menuSelector = new MenuSelector(menu, "Quản lí phân công");
                switch (menuSelector.Selector())
                {
                    case 0:
                        CRUD();
                        break;
                    case 1:
                        Statistic();
                        break;
                    case 2:
                        Console.Clear();
                        exit = true;
                        break;
                }
            }
        }

        public void Statistic()
        {
            Console.Clear();
            bool exit = false;
            while (!exit) {
                string subId = user.SubjectID;
                List<Assignment> assignments = handler.GetList();
                List<Teacher> teachers = teacherHandler.GetList(subId);
                Table table = new Table(90);
                table.PrintLine();
                table.PrintRow("ID", "Tên", "Số giờ dạy", "Số giờ vượt", "Số giờ chuẩn");
                table.PrintLine();
                foreach (var teacher in teachers)
                {
                    int workTime = handler.GetWorkTime(assignments, teacher.ID);
                    float standardTime = handler.GetStandardTime(teacher.Position);
                    float exceedTime = (float)workTime - standardTime;
                    table.PrintRow(teacher.ID, teacher.Name, workTime.ToString(), exceedTime.ToString(), standardTime.ToString());
                }
                table.PrintLine();
                Console.WriteLine("nhấn esc để thoát");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    exit = true;
                }
            }
        }

        public void CRUD(int pos, List<Assignment> assignments , List<Teacher> teachers)
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            bool exit = true;
            while (exit)
            {
                string[] menu =
                {
                    "1.Sửa thông tin",
                    "2.Xóa phân công"
                };
                MenuSelector menuSelector = new MenuSelector(menu,"");
                int mode = menuSelector.Selector();
                switch (mode)
                {
                    case 0:
                        Update(pos, assignments[pos].ID, assignments, teachers);
                        exit = false;
                        break;
                    case 1:
                        Delete(pos, assignments);
                        exit = false;
                        break;
                }
            }
        }

        public void CRUD()
        {

            Console.Clear();
            List<Class> classes = classHandler.GetList(user.SubjectID);
            List<Term> terms = termHandler.GetList(user.SubjectID);
            List<Teacher> teachers = teacherHandler.GetList(user.SubjectID);
            List<Assignment> assignments = handler.GetList(classes, terms);
            handler.Combine(assignments);

            int left = Console.CursorLeft;
            bool exit = false;
            int position = 0;
            PrintSelectorTable(assignments, position);
            while (!exit)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (position > 0)
                        {
                            position--;
                            PrintSelectorTable(assignments, position);
                        }
                        Console.CursorLeft = left;
                        break;
                    case ConsoleKey.DownArrow:
                        if (position < assignments.Count - 1)
                        {
                            position++;
                            PrintSelectorTable(assignments, position);
                        }
                        Console.CursorLeft = left;
                        break;
                    case ConsoleKey.Enter:
                        if (assignments[position].TeacherID == null)
                        {
                            Add(position, assignments, teachers);
                            PrintSelectorTable(assignments,position);  
                        }
                        else
                        {
                            CRUD(position, assignments, teachers);
                            PrintSelectorTable(assignments, position);
                        }
                        break;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        exit = true;
                        break;
                }
            }
        }

        public void PrintSelectorTable(List<Assignment> assignments, int position)
        {
            Console.Clear();
            Table table = new Table(100);
            table.PrintLine();
            table.PrintRow("Mã HP", "Tên HP", "Số tín chỉ", "Mã lớp", "Mã giảng viên","Học kỳ","Năm học");
            table.PrintLine();
            for (int i = 0; i < assignments.Count; i++)
            {
                Term term = termHandler.GetTerm(assignments[i].TermID);
                Class cl = classHandler.GetClass(assignments[i].ClassID);
                string teacherID = assignments[i].TeacherID == null ? " " : assignments[i].TeacherID;
                if (i == position)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.Blue;
                    table.PrintRow(term.ID, term.Name, term.CreditNum.ToString(), cl.ID, teacherID,assignments[i].Semester.ToString(),assignments[i].Year);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else table.PrintRow(term.ID, term.Name, term.CreditNum.ToString(), cl.ID, teacherID, assignments[i].Semester.ToString(), assignments[i].Year);

            }
            table.PrintLine();
        }

        public void PrintTeacherSelector(List<Teacher> teachers, int position)
        {
            Console.Clear();
            Table table = new Table(90);
            table.PrintLine();
            table.PrintRow("ID", "Tên");
            table.PrintLine();
            for(int i = 0; i<teachers.Count; i++)
            {
                if (i == position)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.Blue;
                    table.PrintRow(teachers[i].ID, teachers[i].Name);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else table.PrintRow(teachers[i].ID, teachers[i].Name);
            }
            table.PrintLine();
        }

        public void PrintSemesterSelector(Assignment assignment)
        {
            Console.Clear();
            string[] menu = { "1", "2" };
            MenuSelector menuSelector = new MenuSelector(menu, "Học kỳ");
            switch (menuSelector.Selector())
            {
                case 0:
                    assignment.Semester = 1;
                    break;
                case 1:
                    assignment.Semester = 2;
                    break;
            }
        }

        public void Add(int position, List<Assignment> assignments, List<Teacher> teachers)
        {
            int pos = 0;
            PrintTeacherSelector(teachers, pos);
            bool exit = false;
            while (!exit)
            {
                ConsoleKeyInfo consoleKey = Console.ReadKey();
                Console.Clear();
                switch (consoleKey.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (pos < teachers.Count)
                        {
                            pos++;
                            PrintTeacherSelector(teachers, pos);
                        }
                        else Console.CursorLeft = 0;
                        break;
                    case ConsoleKey.UpArrow:
                        if (pos > 0)
                        {
                            pos--;
                            PrintTeacherSelector(teachers, pos);
                        }
                        else Console.CursorLeft = 0;
                        break;
                    case ConsoleKey.Enter:
                        assignments[position].TeacherID = teachers[pos].ID;
                        PrintSemesterSelector(assignments[position]);
                        handler.Add(assignments[position]);
                        exit = true;
                        break;
                }
            }
        }

        public void Update(int position, string oldId, List<Assignment> assignments, List<Teacher> teachers)
        {
            Console.CursorVisible = true;
            Console.WriteLine("giảng viên cũ: " + assignments[position].TeacherID);
            //string tcherId = DrawInputForm(assignments, position, teachers, 1); // ==> nhấn enter và lấy được teacherID sau khi nhập
            //assignments[position].TeacherID = tcherId;
            //Assignment newInfo = assignments[position];
            //handler.Update(oldId, newInfo);
            //PrintSelectorTable(assignments, position);
            int pos = 0;
            PrintTeacherSelector(teachers, pos);
            bool exit = false;
            while (!exit)
            {
                ConsoleKeyInfo consoleKey = Console.ReadKey();
                Console.Clear();
                switch (consoleKey.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (pos < teachers.Count)
                        {
                            pos++;
                            PrintTeacherSelector(teachers, pos);
                        }
                        else Console.CursorLeft = 0;
                        break;
                    case ConsoleKey.UpArrow:
                        if (pos > 0)
                        {
                            pos--;
                            PrintTeacherSelector(teachers, pos);
                        }
                        else Console.CursorLeft = 0;
                        break;
                    case ConsoleKey.Enter:
                        assignments[position].TeacherID = teachers[pos].ID;
                        PrintSemesterSelector(assignments[position]);
                        handler.Update(oldId, assignments[position]);
                        exit = true;
                        break;
                }
            }
        }

        public void Delete(int position, List<Assignment> assignments)
        {
            Console.Clear();
            Console.WriteLine();
            string[] menu = { "có", "không" };
            MenuSelector menuSelector = new MenuSelector(menu, "Bạn có chắc muốn xóa phân công này?");
            int select = menuSelector.Selector();
            switch (select)
            {
                case 0:
                    Console.Clear();
                    handler.Delete(assignments[position].ID);
                    assignments[position].TeacherID = null;
                    assignments[position].Semester = 0;
                    Console.WriteLine("Xóa thành công");
                    break;
                case 1:
                    Console.Clear();
                    PrintSelectorTable(assignments, position);
                    break;
            }
        }

        public void Add()
        {

        }

        public void Search()
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            int length;
            int maxLength = handler.GetList().Count;
            while (true)
            {
                Console.Write("Số lượng phân công muốn hiển thị");
                try
                {
                    length = int.Parse(Console.ReadLine());
                    if (length > 0 && length <= maxLength)
                        break;
                    else
                        Console.WriteLine("Số lượng phân công là số không âm");
                }
                catch
                {
                    Console.WriteLine("Số lượng phân công là 1 số");
                }
            }
            List<Assignment> assignments = handler.GetList(length);
            foreach (var assign in assignments)
                Console.WriteLine(assign.ID + "|" + assign.ClassID + "|" + assign.TeacherID + "|" + assign.TermID);
        }

        public void ShowAll()
        {
            List<Assignment> assignments = handler.GetList();
            foreach (var assign in assignments)
                Console.WriteLine(assign.ID + "|" + assign.ClassID + "|" + assign.TeacherID + "|" + assign.TermID);
        }

        public void Delete()
        {
            string id = GetId();
            handler.Delete(id);
        }

        public void Update()
        {
        }

        public string GetId()
        {
            while (true)
            {
                Console.Write("Mã phân công: ");
                string id = Console.ReadLine();
                if (id.Length < 9)
                    Console.WriteLine("Mã phân công gồm 10 kí tự");
                else if (handler.GetIndex(id) < 0)
                    Console.WriteLine("Phân công không tồn tại");
                else
                    return id;
            }
        }

    }
}