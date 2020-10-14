using Project1.DataAcessLayer.Model;
using Project1.LogicalHandlerLayer;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.UI
{
    class AssignmentUI
    {
        AssignmentHandler handler = new AssignmentHandler();
        TeacherUI teacher = new TeacherUI();
        TermUI term = new TermUI();
        ClassUI classUI = new ClassUI();
        public void Menu()
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool exit = true;
            while (exit)
            {
                Console.WriteLine("Quản lý phân công giảng dạy");
                Console.WriteLine("1.Thêm phân công mới");
                Console.WriteLine("2.Sửa thông tin phân công");
                Console.WriteLine("3.Xóa phân công");
                Console.WriteLine("4.Hiển thị tất cả phân công");
                Console.WriteLine("5.Hiển thị phân công");
                Console.WriteLine("6.Tìm kiếm phân công");
                Console.WriteLine("7.Trở lại trang chủ");
                Console.Write("Bạn chọn chế độ: ");
                int mode = int.Parse(Console.ReadLine());
                switch (mode)
                {
                    case 1:
                        Add();
                        break;
                    case 2:
                        Update();
                        break;
                    case 3:
                        Delete();
                        break;
                    case 4:
                        ShowAll();
                        break;
                    case 5:
                        Show();
                        break;
                    case 6:
                        Search();
                        break;
                    case 7:
                        exit = false;
                        break;
                }
            }
        }

        private void Search()
        {
            throw new NotImplementedException();
        }

        private void Show()
        {
            int length;
            int maxLength = handler.GetAssignments().Count;
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
            List<Assignment> assignments = handler.GetAssignments(length);
            foreach (var assign in assignments)
                Console.WriteLine(assign.ID + "|" + assign.ClassID + "|" + assign.TeacherID + "|" + assign.TermID);
        }

        private void ShowAll()
        {
            List<Assignment> assignments = handler.GetAssignments();
            foreach(var assign in assignments)
                Console.WriteLine(assign.ID + "|" + assign.ClassID + "|" + assign.TeacherID + "|" + assign.TermID);
        }

        private void Delete()
        {
            string id = GetId();
            handler.Delete(id);
        }

        private void Update()
        {
            string id = GetId();
            string classId = classUI.GetID2(true);
            string teacherId = teacher.GetId2(true);
            string termId = term.GetId2(true);

            Assignment old = handler.GetAssignments()[handler.GetIndex(id)];

            if (classId == "")
                classId = old.ClassID;
            if (teacherId == "")
                teacherId = old.TeacherID;
            if (termId == "")
                termId = old.TermID;

            Assignment newInfo = new Assignment(classId,teacherId,termId);
            handler.Update(id, newInfo);
        }

        private void Add()
        {
            bool exit = false;
            while (!exit)
            {
                string classId = classUI.GetID2();
                string teacherId = teacher.GetId2();
                string termId = term.GetId2();
                handler.Add(new Assignment(classId, teacherId, termId));

                Console.Write("Bạn muốn nhập tiếp không?(y/n) ");
                string exitStr = Console.ReadLine();
                if (exitStr == "n")
                    exit = true;
            }
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
