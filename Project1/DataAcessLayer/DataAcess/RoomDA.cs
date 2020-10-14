using Project1.DataAcessLayer.Model;
using Project1.Source;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.DataAcessLayer.DataAcess
{
    class RoomDA
    {
        string fileName = StringSource.ROOM_DB_NAME;

        public List<Room> GetListRoom()
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            List<Room> rooms = new List<Room>();
            using(StreamReader reader = new StreamReader(fileName))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] info = line.Split('|');
                    rooms.Add(new Room(info[0], info[1], int.Parse(info[2])));
                    line = reader.ReadLine();
                }
                reader.Close();
                return rooms;
            }
        }

        public List<Room> GetListRoom(int length)
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            List<Room> rooms = new List<Room>();
            using (StreamReader reader = new StreamReader(fileName))
            {
                for(int i = 0; i<length; i++)
                {
                    string line = reader.ReadLine();
                    string[] info = line.Split('|');
                    rooms.Add(new Room(info[0], info[1], int.Parse(info[2])));
                }
                reader.Close();
                return rooms;
            }
        }

        public void SaveAllData(List<Room> rooms)
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach(var room in rooms)
                    writer.WriteLine(room.ID + "|" + room.Name + "|" + room.Capacity);
                writer.Flush();
                writer.Close();
            }
        }

        public void AddRoom(Room room)
        {
            using (StreamWriter writer = new StreamWriter(fileName, true, Encoding.UTF8))
            {
                writer.WriteLine(room.ID + "|" + room.Name + "|" + room.Capacity);
                writer.Flush();
                writer.Close();
            }
        }

        public int GetRoomIndex(string id)
        {
            List<Room> rooms = GetListRoom();
            for(int i = 0; i<rooms.Count; i++)
            {
                if (rooms[i].ID == id)
                    return i;
            }
            return -1;
        }

        public void UpdateRoom(string id, Room newInfo)
        {
            int index = GetRoomIndex(id);
            List<Room> rooms = GetListRoom();
            rooms[index] = newInfo;
            SaveAllData(rooms);
        }

        public void DeleteRoom(string id)
        {
            int index = GetRoomIndex(id);
            List<Room> rooms = GetListRoom();
            rooms.RemoveAt(index);
            SaveAllData(rooms);
        }


    }
}
