using Project1.DataAcessLayer.Model;
using Project1.LogicalHandlerLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.UI
{
    class TeacherUI
    {
        TeacherHandler teacherHandler = new TeacherHandler();
        SubjectHandler subjectHandler = new SubjectHandler();
        public void Menu()
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool exit = true;
            while (exit)
            {
                Console.WriteLine("Quản lý giảng viên");
                Console.WriteLine("1.Thêm giảng viên mới");
                Console.WriteLine("2.Sửa thông tin giảng viên");
                Console.WriteLine("3.Xóa giảng viên");
                Console.WriteLine("4.Hiển thị tất cả giảng viên");
                Console.WriteLine("5.Hiển thị giảng viên");
                Console.WriteLine("6.Tìm kiếm giảng viên");
                Console.WriteLine("7.Trở lại trang chủ");
                Console.Write("Bạn chọn chế độ: ");
                int mode = int.Parse(Console.ReadLine());
                switch (mode)
                {
                    case 1:
                        AddNewTeacher();
                        break;
                    case 2:
                        UpdateTeacher();
                        break;
                    case 3:
                        DeleteTeacher();
                        break;
                    case 4:
                        ShowTeacher();
                        break;
                    case 5:
                        ShowTeacher2();
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

        public void AddNewTeacher()
        {
            bool exit = true;
            while (exit)
            {
                Teacher teacher = new Teacher(GetId(), GetName(), GetPosition(), GetSubjectId());
                teacherHandler.AddTeacher(teacher);
                Console.Write("bạn có muốn nhập tiếp?(y/n): ");
                string exitStr = Console.ReadLine();
                if (exitStr == "n")
                    exit = false;
            }
        }

        public void UpdateTeacher()
        {
            
            string id = GetId2();
            string name = GetName(true);
            string position = GetPosition(true);
            string subId = GetSubjectId(true);
            Teacher oldInfo = teacherHandler.GetTeacherInfor(id);
            Teacher newInfo = new Teacher();

            if (name != "")
                newInfo.Name = name;
            else newInfo.Name = oldInfo.Name;

            if (position != "")
                newInfo.Position = position;
            else newInfo.Position = oldInfo.Position;

            if (subId != "")
                newInfo.SubjectID = oldInfo.SubjectID;
            else newInfo.SubjectID = subId;

            newInfo.ID = id;

            teacherHandler.UpdateTeacher(id, newInfo);
        }

        public void DeleteTeacher()
        {
            string id = GetId2();
            teacherHandler.DeleteTeacher(id);
        }

        public void ShowTeacher()
        {
            List<Teacher> teachers = teacherHandler.GetTeacherList();
            foreach(var teacher in teachers)
            {
                Console.WriteLine(teacher.ID + "|" + teacher.Name + "|" + teacher.Position);
            }
        }

        public void ShowTeacher2()
        {
            Console.Write("Số lượng giảng viên muốn hiển thị: ");
            int length;
            int maxLength = teacherHandler.GetTeacherList().Count;
            while (true)
            {
                try
                {
                   length =  int.Parse(Console.ReadLine());
                    if (length > 0 && length <= maxLength)
                        break;
                    else
                        Console.WriteLine("số lượng giảng viên là số không âm và không quá giới hạn(+" + maxLength + "+)");
                }
                catch
                {
                    Console.WriteLine("Số lượng giảng viên là 1 số");
                }
            }
            List<Teacher> teachers = teacherHandler.GetTeacherList(length);
            foreach (var teacher in teachers)
            {
                Console.WriteLine(teacher.ID + "|" + teacher.Name + "|" + teacher.Position);
            }
        }

        public void Search()
        {
            Console.Write("Từ khóa: ");
            string input = Console.ReadLine();
            List<Teacher> teachers = teacherHandler.GetTeacherList();
            foreach(var teacher in teachers)
            {
                if (teacher.ID.Contains(input) || teacher.Name.Contains(input) || teacher.Position.Contains(input) || teacher.SubjectID.Contains(input))
                    Console.WriteLine(teacher.ID + "|" + teacher.Name + "|" + teacher.Position);
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
                else if (teacherHandler.GetTeacherIndex(id) >= 0)
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
                else if (teacherHandler.GetTeacherIndex(id)<0)
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

        public string GetPosition(bool acceptNull = false)
        {
            while (true)
            {
                Console.Write("Chức vụ: ");
                string position = Console.ReadLine();
                if (!teacherHandler.CheckPosition(position, acceptNull))
                    Console.WriteLine("Chức vụ gồm 9 kí tự trở lên(a-z)");
                else
                    return position;
            }
        }

        public string GetSubjectId(bool acceptNull = false)
        {
            while (true)
            {
                Console.Write("Mã bộ môn: ");
                string subId = Console.ReadLine();
                if (acceptNull && subId == "")
                    return subId;
                if (!subjectHandler.CheckIdSyntax(subId))
                    Console.WriteLine("mã bộ môn gồm 2 chữ số từ 0-9");
                else if (subjectHandler.GetSubIndex(subId) < 0)
                    Console.WriteLine("Bộ môn không tồn tại");
                else
                    return subId;
            }
        }
    }
}
