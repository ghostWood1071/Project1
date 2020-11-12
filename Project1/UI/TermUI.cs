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
    class TermUI : IUIable
    {
        TermsHandler handler = new TermsHandler();
        SubjectHandler subjectHandler = new SubjectHandler();

        private Teacher teacher;

        public TermUI()
        {

        }

        public TermUI(Teacher teacher)
        {
            this.teacher = teacher;
        }

        public void Menu()
        {
            Console.Clear();
            string[] menu = {
                "1.Thêm học phần mới",
                "2.Sửa thông tin học phần",
                "3.Xóa học phần",
                "4.Tìm kiếm học phần",
                "5.Hiển thị tất cả học phần",
                "6.Quay lại trang chủ"
            };
            MenuSelector menuSelector = new MenuSelector(menu, "Quản lý học phần");
            Console.OutputEncoding = Encoding.UTF8;
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
                        Search();
                        Console.Clear();
                        break;
                    case 4:
                        ShowAll();
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
            Console.Clear();
            Console.CursorVisible = true;
            List<Term> terms = this.teacher.Role == (int)UserPermission.HeadSection ? handler.GetList(this.teacher.SubjectID) : handler.GetListTerm();

            bool exit = false;
            while (!exit)
            {
                PrintTable(terms);
                string id = GetId();
                string name = GetName();
                int creditNum = GetCreditNum();
                string subId = this.teacher.Role == (int)UserPermission.HeadSection ? this.teacher.SubjectID : GetSubId();
                Term term = new Term(id,name,creditNum,subId);
                handler.AddTerm(term);
                terms.Add(term);
                PrintTable(terms);
                Console.Write("Bạn muốn nhập tiếp không?(y/n): ");
                string e = Console.ReadLine();
                if (e == "n")
                    exit = true;
            }
            Console.CursorVisible = false;
        } //checked

        public string GetId()
        {
            while (true)
            {
                Console.Write("Mã học phần: ");
                string id = Console.ReadLine();
                if (!handler.CheckId(id))
                    Console.WriteLine("Mã học phần gồm 3 kí tự là các số từ 0-9");
                else if (handler.GetTermIndex(id) >= 0)
                    Console.WriteLine("Học phần đã tồn tại");
                else
                    return id;
            }
        }

        public string GetId2(bool acceptNull = false)
        {
            while (true)
            {
                Console.Write("Mã học phần: ");
                string id = Console.ReadLine();
                if (acceptNull && id == "")
                    return id;
                if (!handler.CheckId(id))
                    Console.WriteLine("Mã học phần gồm 3 kí tự là các số từ 0-9");

                else if (handler.GetTermIndex(id) < 0)
                    Console.WriteLine("Học phần không tồn tại");
                else
                    return id;
            }
        }

        public string GetSubId()
        {
            Console.Clear();
            List<Subject> subjects = subjectHandler.GetSubjects();
            string[] selectItem = new string[subjects.Count];
            for (int i = 0; i < subjects.Count; i++)
                selectItem[i] = subjects[i].Name;
            MenuSelector selector = new MenuSelector(selectItem, "Bộ môn: ");
            return subjects[selector.Selector()].ID;
        }

        public string GetName(bool acceptNull = false)
        {
            while (true)
            {
                Console.Write("Tên học phần: ");
                string name = Console.ReadLine();
                if (acceptNull && name == "")
                    return name;
                if (name.Length <= 10)
                    Console.WriteLine("Tên học phần gồm 10 kí tự trở lên");
                else
                    return name;
            }
        }

        public int GetCreditNum(bool acceptNull = false)
        {
            while (true)
            {
                try
                {
                    Console.Write("Số tín chỉ: ");
                    int creditNum = int.Parse(Console.ReadLine());
                    if (!handler.CheckCredit(creditNum))
                        Console.WriteLine("Số tín chỉ >=1 và <=4");
                    else
                        return creditNum;
                }
                catch
                {
                    if (acceptNull)
                        return 0;
                    Console.WriteLine("Số tín chỉ là số");
                }
            }
        }

        public void Update()
        {
            bool exit = false;
            List<Term> terms = this.teacher.Role == (int)UserPermission.HeadSection ? handler.GetList(this.teacher.SubjectID) : handler.GetListTerm();
            while (!exit)
            {
                Console.CursorVisible = true;
                Console.Clear();
                PrintTable(terms);
                string id = GetId2();
                string name = GetName(true);
                int creditNum = GetCreditNum(true);
                string subId = this.teacher.Role == (int)UserPermission.HeadSection ? this.teacher.SubjectID : GetSubId();
                Term newInfo = new Term();
                Term oldInfo = handler.GetTerm(id, terms);

                newInfo.ID = id;

                if (name == "")
                    newInfo.Name = oldInfo.Name;
                else
                    newInfo.Name = name;

                if (creditNum == 0)
                    newInfo.CreditNum = oldInfo.CreditNum;
                else
                    newInfo.CreditNum = creditNum;

                if (subId == "")
                    newInfo.SubjectId = oldInfo.SubjectId;
                else
                    newInfo.SubjectId = subId;

                handler.UpdateTerm(id, newInfo);
                terms[handler.GetIndex(oldInfo.ID, terms)] = newInfo;

                Console.Clear();
                PrintTable(terms);
                Console.Write("Bạn có muốn sửa tiếp không?(nhấn esc để thoát)");
                ConsoleKeyInfo info = Console.ReadKey();
                if (info.Key == ConsoleKey.Escape)
                    exit = true;
            }
        } //checked

        public void Delete()
        {
            AssignmentHandler assignmentHandler = new AssignmentHandler();
            bool exit = false;
            while (!exit)
            {
                List<Term> terms = this.teacher.Role == (int)UserPermission.HeadSection ? handler.GetList(this.teacher.SubjectID) : handler.GetListTerm();
                PrintTable(terms);
                string id = GetId2();
                handler.DeleteTerm(id);
                terms.RemoveAt(handler.GetIndex(id, terms));
                assignmentHandler.Delete(assignment => assignment.TermID == id);
                PrintTable(terms);
                Console.Clear();
                PrintTable(terms);
                Console.Write("Bạn có muốn sửa tiếp không?(nhấn esc để thoát)");
                ConsoleKeyInfo info = Console.ReadKey();
                if (info.Key == ConsoleKey.Escape)
                    exit = true;
            }
        }  //checked

        public void Search()
        {

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.CursorVisible = true;
                List<Term> rooms = this.teacher.Role == (int)UserPermission.HeadSection ? handler.GetList(this.teacher.SubjectID) : handler.GetListTerm();
                List<Subject> subjects = subjectHandler.GetSubjects();
                Console.Write("Từ khóa: ");
                string searcher = Console.ReadLine();
                Table table = new Table(100);
                table.PrintLine();
                table.PrintRow("ID", "Ten HP", "so tin chi", "bo mon");
                table.PrintLine();
                for (int i = 0; i < rooms.Count; i++)
                {
                    if (rooms[i].ID.Contains(searcher) ||
                        rooms[i].Name.Contains(searcher) ||
                        rooms[i].SubjectId.Contains(searcher) ||
                        rooms[i].CreditNum.ToString().Contains(searcher))

                        table.PrintRow(
                        rooms[i].ID,
                        rooms[i].Name,
                        rooms[i].CreditNum.ToString(),
                        subjects[subjectHandler.GetSubIndex(rooms[i].SubjectId)].Name
                    );
                }
                table.PrintLine();
                Console.Write("Nhấn esc để thoát");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Escape)
                    exit = true;
                Console.CursorVisible = false;
            }
        } // checked

        public void ShowAll()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                List<Term> rooms = this.teacher.Role == (int)UserPermission.HeadSection ? handler.GetList(this.teacher.SubjectID) : handler.GetListTerm();
                PrintTable(rooms);
                Console.WriteLine("bấm enter để thoát");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Enter)
                    exit = true;
            }
        } // checked

        public void Show()
        {
        } // checked

        public void PrintTable(List<Term> terms)
        {
            Console.Clear();
            List<Subject> subjects = subjectHandler.GetSubjects();
            Table table = new Table(100);
            table.PrintLine();
            table.PrintRow("ID", "Ten HP", "so tin chi", "bo mon");
            table.PrintLine();
            foreach (var room in terms)
                table.PrintRow(
                    room.ID,
                    room.Name,
                    room.CreditNum.ToString(),
                    subjects[subjectHandler.GetSubIndex(room.SubjectId)].Name
                );
            table.PrintLine();
        }

    }
}
