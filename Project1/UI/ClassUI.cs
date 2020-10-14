using Project1.DataAcessLayer.Model;
using Project1.LogicalHandlerLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Project1.UI
{
    class ClassUI
    {
        ClassHandler classHandler = new ClassHandler();
        SubjectUI subject = new SubjectUI();
        TeacherUI teacher = new TeacherUI();
        MajorUI major = new MajorUI();
        public void Menu()
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool exit = true;
            while (exit)
            {
                Console.WriteLine("Quản lý lớp");
                Console.WriteLine("1.Thêm lớp mới");
                Console.WriteLine("2.Sửa thông tin lớp");
                Console.WriteLine("3.Xóa lớp");
                Console.WriteLine("4.Hiển thị tất cả lớp");
                Console.WriteLine("5.Hiển thị lớp");
                Console.WriteLine("6.Tìm kiếm lớp");
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

        private void Show()
        {
            List<Class> classes = classHandler.GetClasses();
            int length;
            int maxlength = classes.Count;
            while (true)
            {
                try
                {
                    length = int.Parse(Console.ReadLine());
                    if (length <= maxlength)
                        break;
                    else
                        Console.WriteLine("số lượng lớp muốn hiển thị pahir nhỏ hơn(bằng) số lượng lớp hiện tại");
                }
                catch
                {
                    Console.WriteLine("số lượng lớp là 1 số");
                }
            }
           
            foreach (var c in classes)
                Console.WriteLine(c.ID + "|" + c.Population + "|" + c.SubjectID + "|" + c.MajorID + "|" + c.TeacherId + "|" + c.StartYear + "-" + c.EndYear);
        }

        public void Search()
        {
            Console.Write("Từ khóa: ");
            string input = Console.ReadLine();
            List<Class> classes = new List<Class>();
            foreach (var c in classes)
                if(c.ID.ToLower().Contains(input)||
                   c.Population.ToString().Contains(input)||
                   c.SubjectID.Contains(input)||
                   c.MajorID.Contains(input)||
                   c.TeacherId.Contains(input)||
                   c.StartYear.ToString().Contains(input)||
                   c.EndYear.ToString().Contains(input))
                Console.WriteLine(c.ID + "|" + c.Population + "|" + c.SubjectID + "|" + c.MajorID + "|" + c.TeacherId + "|" + c.StartYear + "-" + c.EndYear);
        }

        public void ShowAll()
        {
            List<Class> classes = classHandler.GetClasses();
            foreach (var c in classes)
                Console.WriteLine(c.ID + "|" + c.Population + "|" + c.SubjectID + "|" + c.MajorID + "|" + c.TeacherId + "|" + c.StartYear + "-" + c.EndYear);
        }

        public void Delete()
        {
            string classId = GetID2();
            classHandler.Delete(classId);
        }

        public void Update()
        {
            string classId = GetID2();
            Class oldInfo = classHandler.GetClasses()[classHandler.GetIndex(classId)];
            Class newInfo = new Class();
            int population = GetPopulation(true);
            string subId = subject.GetId2(true);
            string majorId = major.GetId2("Mã chuyên ngành",true);
            string teacherId = teacher.GetId2(true);

            if (population == 0)
                newInfo.Population = oldInfo.Population;
            else
                newInfo.Population = population;

            if (subId == "")
                newInfo.SubjectID = oldInfo.SubjectID;
            else
                newInfo.SubjectID = subId;

            if (majorId == "")
                newInfo.MajorID = oldInfo.MajorID;
            else
                newInfo.MajorID = majorId;

            if (teacherId == "")
                newInfo.TeacherId = oldInfo.TeacherId;
            else
                newInfo.TeacherId = teacherId;

            classHandler.Update(classId, newInfo);
        }

        public void Add()
        {
            bool exit = false;
            while (!exit)
            {
                string classId = GetID();
                int population = GetPopulation();
                string subId = subject.GetId2();
                string majorId = major.GetId2("Mã chuyên ngành");
                string teacherId = teacher.GetId2();

                classHandler.Add(new Class(classId, population, subId, majorId, teacherId));

                Console.Write("Bạn có muốn nhập tiếp không?(y/n): ");
                string exitStr = Console.ReadLine();
                if (exitStr == "n")
                    exit = true;
            }
            
            
        }

        public string GetID(bool acceptNull = false)
        {
            while (true)
            {
                Console.Write("Mã lớp: ");
                string id = Console.ReadLine();
                if (acceptNull && id == "")
                    return id;
                if (id.Length < 6)
                    Console.WriteLine("Mã lớp gồm 7 kí tự trở lên");
                else if (classHandler.GetIndex(id) >= 0)
                    Console.WriteLine("Lớp đã tồn tại");
                else
                    return id;
            }
        }

        public string GetID2(bool acceptNull = false)
        {
            while (true)
            {
                Console.Write("Mã lớp: ");
                string id = Console.ReadLine();
                if (acceptNull && id == "")
                    return id;
                if (id.Length < 6)
                    Console.WriteLine("Mã lớp gồm 7 kí tự trở lên");
                else if (classHandler.GetIndex(id) < 0)
                    Console.WriteLine("Lớp không tồn tại");
                else
                    return id;
            }
        }

        public int GetPopulation(bool acceptNull = false)
        {
            while (true)
            {
                Console.Write("Sĩ số lớp: ");
                try
                {
                    int population = int.Parse(Console.ReadLine());
                    if (population < 0)
                        Console.WriteLine("Sĩ số không âm");
                    else if (population < 10)
                        Console.WriteLine("Sĩ số 1 lớp >=10");
                    else
                        return population;
                }
                catch
                {
                    if (acceptNull)
                        return 0;
                    else
                        Console.WriteLine("Sĩ số là một số không phải kí tự");
                }
                
            }
        }
    }
}
