using Project1.DataAcessLayer.Model;
using Project1.LogicalHandlerLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.UI
{
    class TermUI
    {
        TermsHandler handler = new TermsHandler();
        public void Menu()
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Quản lý học phần");
                Console.WriteLine("1.Thêm học phần mới");
                Console.WriteLine("2.Sửa thông tin học phần");
                Console.WriteLine("3.Xóa học phần");
                Console.WriteLine("4.Tìm kiếm học phần");
                Console.WriteLine("5.Hiển thị tất cả học phần");
                Console.WriteLine("6.Hiển thị thông tin học phần");
                Console.WriteLine("7.Quay lại trang chủ");
                Console.Write("Bạn chọn chế độ: ");
                try
                {
                    int mode = int.Parse(Console.ReadLine());
                    if (mode <= 0)
                        Console.WriteLine("Chế độ là các số từ 1-7");
                    switch (mode)
                    {
                        case 1:
                            AddTerm();
                            break;
                        case 2:
                            UpdateTerm();
                            break;
                        case 3:
                            DeleteTerm();
                            break;
                        case 4:
                            SearchTerm();
                            break;
                        case 5:
                            ShowAll();
                            break;
                        case 6:
                            Show();
                            break;
                        case 7:
                            exit = true;
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Chế độ là các số từ 1-7");
                }
            }
        }

        public void AddTerm()
        {
            bool exit = false;
            while (!exit)
            {
                handler.AddTerm(new Term(GetId(), GetName(), GetCreditNum()));
                Console.Write("Bạn muốn nhập tiếp không?(y/n): ");
                string e = Console.ReadLine();
                if (e == "n")
                    exit = true;
            }
        }

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

        public void UpdateTerm()
        {
            string id = GetId2();
            string name = GetName(true);
            int creditNum = GetCreditNum(true);
            Term newInfo = new Term();
            Term oldInfo = handler.GetListTerm()[handler.GetTermIndex(id)];

            newInfo.ID = id;

            if (name == "")
                newInfo.Name = oldInfo.Name;
            else
                newInfo.Name = name;

            if (creditNum == 0)
                newInfo.CreditNum = oldInfo.CreditNum;
            else
                newInfo.CreditNum = creditNum;

            handler.UpdateTerm(id, newInfo);
        }

        public void DeleteTerm()
        {
            string id = GetId2();
            handler.DeleteTerm(id);
        }

        public void SearchTerm()
        {
            List<Term> rooms = handler.GetListTerm();
            string searcher = Console.ReadLine();
            foreach (var room in rooms)
            {
                if (room.ID.Contains(searcher) || room.Name.Contains(searcher) || room.CreditNum.ToString().Contains(searcher))
                    Console.WriteLine(room.ID + "|" + room.Name + "|" + room.CreditNum);
            }
        }

        public void ShowAll()
        {
            List<Term> rooms = handler.GetListTerm();
            foreach (var room in rooms)
                Console.WriteLine(room.ID + "|" + room.Name + "|" + room.CreditNum);
        }

        public void Show()
        {
            int length;
            int maxLength = handler.GetListTerm().Count;
            while (true)
            {
                Console.Write("Số lượng học phần muốn hiển thị");
                try
                {
                    length = int.Parse(Console.ReadLine());
                    if (length > 0 && length <= maxLength)
                        break;
                    else
                        Console.WriteLine("Số lượng học phần là số không âm");
                }
                catch
                {
                    Console.WriteLine("Số lượng học phần là 1 số");
                }
            }
            List<Term> rooms = handler.GetListTerm(length);
            foreach (var room in rooms)
                Console.WriteLine(room.ID + "|" + room.Name + "|" + room.CreditNum);
        }
    }
}
