using Project1.DataAcessLayer.Model;
using Project1.LogicalHandlerLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project1.UI
{
    class RoomUI
    {
        
        RoomHandler handler = new RoomHandler();
        public void Menu()
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Quản lý phòng học");
                Console.WriteLine("1.Thêm phòng học mới");
                Console.WriteLine("2.Sửa thông tin phòng học");
                Console.WriteLine("3.Xóa phòng học");
                Console.WriteLine("4.Tìm kiếm phòng học");
                Console.WriteLine("5.Hiển thị tất cả phòng học");
                Console.WriteLine("6.Hiển thị thông tin phòng học");
                Console.WriteLine("7.Quay lại trang chủ");
                Console.Write("Bạn chọn chế độ: ");
                try
                {
                    int mode = int.Parse(Console.ReadLine());
                    if(mode<=0)
                        Console.WriteLine("Chế độ là các số từ 1-7");
                    switch (mode)
                    {
                        case 1:
                            AddRoom();
                            break;
                        case 2:
                            UpdateRoom();
                            break;
                        case 3:
                            DeleteRoom();
                            break;
                        case 4:
                            SearchRoom();
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

        public void AddRoom()
        {
            bool exit = false;
            while (!exit)
            {
                handler.AddRoom(new Room(GetId(), GetName(), GetCapacity()));
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
                Console.Write("Mã phòng: ");
                string id = Console.ReadLine();
                if (!handler.CheckID(id))
                    Console.WriteLine("Mã phòng gồm 3 kí tự là các số từ 0-9");
                else if (handler.GetRoomIndex(id) >= 0)
                    Console.WriteLine("Phòng đã tồn tại");
                else
                    return id;
            }
        }

        public string GetId2(bool acceptNull = false)
        {
            while (true)
            {
                Console.Write("Mã phòng: ");
                string id = Console.ReadLine();
                if (acceptNull && id == "")
                    return id;
                if (!handler.CheckID(id))
                    Console.WriteLine("Mã phòng gồm 3 kí tự là các số từ 0-9");
                else if (handler.GetRoomIndex(id) < 0)
                    Console.WriteLine("Phòng không tồn tại");
                else
                    return id;
            }
        }

        public string GetName(bool acceptNull = false)
        {
            while (true)
            {
                Console.Write("Tên phòng: ");
                string name = Console.ReadLine();
                if (acceptNull && name == "")
                    return name;
                if (handler.CheckName(name, acceptNull))
                    return name;
                else Console.WriteLine("Tên gồm 9 kí tự trở lên");
            }
        }
        
        public int GetCapacity(bool acceptNull = false) 
        {
            while (true)
            {
                try
                {
                    Console.Write("Sức chứa: ");
                    int capacity = int.Parse(Console.ReadLine());
                    if (!handler.CheckCapacity(capacity, acceptNull))
                        Console.WriteLine("Sức chứa lớn hơn hoặc bằng 20 người");
                    else
                        return capacity;
                }
                catch
                {
                    Console.WriteLine("Sức chứa là số");
                }
            }
        }

        public void UpdateRoom()
        {
            string id = GetId2();
            string name = GetName(true);
            int capacity = GetCapacity(true);
            Room newInfo = new Room();
            Room oldInfo = handler.GetListRoom()[handler.GetRoomIndex(id)];

            newInfo.ID = id;

            if (name == "")
                newInfo.Name = oldInfo.Name;
            else
                newInfo.Name = name;

            if (capacity == 0)
                newInfo.Capacity = oldInfo.Capacity;
            else
                newInfo.Capacity = capacity;

            handler.UpdateRoom(id, newInfo);
        }

        public void DeleteRoom()
        {
            string id = GetId2();
            handler.DeleteRoom(id);
        }

        public void SearchRoom()
        {
            List<Room> rooms = handler.GetListRoom();
            string searcher = Console.ReadLine();
            foreach(var room in rooms)
            {
                if(room.ID.Contains(searcher) || room.Name.Contains(searcher) || room.Capacity.ToString().Contains(searcher))
                    Console.WriteLine(room.ID + "|" + room.Name + "|" + room.Capacity);
            }
        }

        public void ShowAll()
        {
            List<Room> rooms = handler.GetListRoom();
            foreach(var room in rooms)
                Console.WriteLine(room.ID + "|" + room.Name + "|" + room.Capacity);
        }

        public void Show()
        {
            int length;
            int maxLength = handler.GetListRoom().Count;
            while (true)
            {
                Console.Write("Số lượng phòng muốn hiển thị");
                try
                {
                    length = int.Parse(Console.ReadLine());
                    if (length > 0 && length <=maxLength)
                        break;
                    else
                        Console.WriteLine("Số lượng phòng là số không âm");
                }
                catch
                {
                    Console.WriteLine("Số lượng phòng là 1 số");
                }
            }
            List<Room> rooms = handler.GetListRoom(length);
            foreach(var room in rooms)
                Console.WriteLine(room.ID + "|" + room.Name + "|" + room.Capacity);
        }
           
    }
}
