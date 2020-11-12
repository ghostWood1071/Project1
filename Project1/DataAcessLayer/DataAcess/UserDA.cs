using Project1.DataAcessLayer.Model;
using Project1.DataAcessLayer.Model.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project1.DataAcessLayer.DataAcess
{
    class UserDA : ICRUD<User>
    {
        private string fileName = "user.txt";
        public void Add(User Object)
        {
            StreamWriter writer = new StreamWriter(fileName, true);
            writer.WriteLine(Object.Account+"|"+Object.Password+"|"+Object.Role);
            writer.Flush();
            writer.Close();
        }

        public void Delete(string id)
        {
            List<User> users = GetList();
            users.RemoveAt(GetIndex(id));
            SaveAll(users);

        }

        public int GetIndex(string id)
        {
            List<User> users = GetList();
            for(int i = 0; i<users.Count; i++)
            {
                if (users[i].Account == id)
                    return i;
            }
            return -1;
        }

        public List<User> GetList()
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            List<User> result = new List<User>();
            StreamReader reader = new StreamReader(fileName);
            string line = reader.ReadLine();
            while (line != null)
            {
                string[] infos = line.Split('|');
                result.Add(new User(infos[0], infos[1], int.Parse(infos[2])));
                line = reader.ReadLine();
            }
            reader.Close();
            return result;
        }

        public List<User> GetList(int length)
        {
            List<User> result = new List<User>();
            StreamReader reader = new StreamReader(fileName);
            for(int i = 0; i<length; i++)
            {
                string[] infos = reader.ReadLine().Split('|');
                result.Add(new User(infos[0], infos[1], int.Parse(infos[2])));
            }
            reader.Close();
            return result;
        }

        public void SaveAll(List<User> list)
        {
            StreamWriter writer = new StreamWriter(fileName);
            foreach(var item in list)
                writer.WriteLine(item.Account + "|" + item.Password + "|" + item.Role);
            writer.Flush();
            writer.Close();
        }

        public void Update(string id, User newInfo)
        {
            List<User> users = GetList();
            users[GetIndex(id)] = newInfo;
            SaveAll(users);
        }
    }
}
