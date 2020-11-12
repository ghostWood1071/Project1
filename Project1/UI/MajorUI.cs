using Project1.DataAcessLayer.DataAcess;
using Project1.DataAcessLayer.Model;
using Project1.DataAcessLayer.Model.Interface;
using Project1.LogicalHandlerLayer;
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
        public void Menu()
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Quản lý chuyên ngành");
                Console.WriteLine("1.Thêm chuyên ngành mới");
                Console.WriteLine("2.Sửa thông tin chuyên ngành");
                Console.WriteLine("3.Xóa chuyên ngành");
                Console.WriteLine("4.Hiển thị danh sách chuyên ngành");
                Console.WriteLine("5.Tìm kiếm chuyên ngành");
                Console.WriteLine("6.Trở lại trang chủ");
                Console.WriteLine("Bạn chọn chế độ: ");
                try
                {
                    int mode = int.Parse(Console.ReadLine());
                    if (mode <= 0)
                        Console.WriteLine("Bạn chọn chế độ từ 0-6");
                    else
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
                                Show();
                                break;
                            case 5:
                                Search();
                                break;
                            case 6:
                                exit = true;
                                break;
                        }
                }
                catch
                {
                    Console.WriteLine("Bạn chọn chế độ từ 0-6");
                }
            }
        }

        public void Search()
        {
            List<Major> majors = majorHandler.GetMajors();
            List<Subject> subjects = subjectHandler.GetSubjects();
            Console.Write("Từ khóa: ");
            string input = Console.ReadLine();
            input = input.ToLower();
            foreach(var major in majors)
            {
                if(major.ID.Contains(input) || 
                   major.Name.ToLower().Contains(input) || 
                   major.SubjectID.Contains(input) ||
                   subjects[subjectHandler.GetSubIndex(major.SubjectID)].Name.Contains(input))
                {
                    Console.WriteLine(major.ID + "|" + major.Name + "|" + major.SubjectID);
                }
            }
        }

        public void Show()
        {
            List<Major> majors = majorHandler.GetMajors();
            foreach (var major in majors)
                Console.WriteLine(major.ID + "|" + major.Name + "|" + major.SubjectID);
        }

        public void Delete()
        {
            string id = GetId2("Mã chuyên ngành");
            majorHandler.DeleteMajor(id);
        }

        public void Update()
        {
            string id = GetId2("Mã chuyên ngành");
            string name = GetName(true);
            string subId = subject.GetId2(true);

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
        }

        public void Add()
        {
            bool exit = false;
            while (!exit)
            {
                string id = GetId("Mã chuyên ngành");
                string name = GetName();
                string subId = subject.GetId2();
                majorHandler.AddMajor(new Major(id, name, subId));
                Console.Write("Bạn có muốn nhập tiếp không(y/n)?");
                string exitStr = Console.ReadLine();
                if (exitStr == "n")
                    exit = true;
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
            while (true)
            {
                Console.Write("Mã bộ môn: ");
                string subId = Console.ReadLine();
                if (acceptNull && subId == "")
                    return subId;
                else if (!subjectHandler.CheckIdSyntax(subId))
                    Console.WriteLine("Mã bộ môn gồm 2 chữ số 0-9");
                else if (subjectHandler.GetSubIndex(subId) < 0)
                    Console.WriteLine("Mã bộ môn không tồn tại");
                else
                    return subId;
            }
        }

    }
}
