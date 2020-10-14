using Project1.DataAcessLayer.DataAcess;
using Project1.DataAcessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.LogicalHandlerLayer
{
    class RoomHandler
    {
        RoomDA roomDA = new RoomDA();

        public void AddRoom(Room room)
        {
            roomDA.AddRoom(room);
        }

        public void UpdateRoom(string id, Room newInfo)
        {
            roomDA.UpdateRoom(id, newInfo);
        }

        public void DeleteRoom(string id)
        {
            roomDA.DeleteRoom(id);
        }

        public List<Room> GetListRoom()
        {
            return roomDA.GetListRoom();
        }

        public List<Room> GetListRoom(int length)
        {
            return roomDA.GetListRoom(length);
        }

        public int GetRoomIndex(string id)
        {
            List<Room> rooms = GetListRoom();
            for (int i = 0; i < rooms.Count; i++)
                if (id == rooms[i].ID)
                    return i;
            return -1;
        }

        public Room GetRoomInfo(string id)
        {
            List<Room> rooms = new List<Room>();
            return rooms[GetRoomIndex(id)];
        }

        public bool CheckID(string id)
        {
            try
            {
                int ID = int.Parse(id);
                if (ID >= 100 && ID <= 600)
                    return true;
                return false;
            } catch
            {
                return false;
            }
        }

        public bool CheckName(string name, bool acceptNull = false)
        {
            if (acceptNull && (name.Length == 0 || name.Length >= 9))
                return true;
            else if(!acceptNull && name.Length >= 9)
                 return true;
            return false;
        }

        public bool CheckCapacity(int capacity, bool acceptNull = false)
        {
            if (acceptNull)
                return true;
            else if (capacity >= 20)
                return true;
            return false;
        }
    }
}
