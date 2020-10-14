using Project1.DataAcessLayer.Model;
using Project1.Source;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.DataAcessLayer.DataAcess
{
    class ClassDA
    {
        private string fileName = StringSource.CLASS_DB_NAME;
        public List<Class> GetClassList()
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            List<Class> classes = new List<Class>();
            using(StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    string[] info = line.Split('|');
                    classes.Add(new Class(info[0], int.Parse(info[1]), info[2], info[3], info[4]));
                }
                reader.Close();
                return classes;
            }
        }

        public List<Class> GetClassList(int length)
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            List<Class> classes = new List<Class>();
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                for(int i = 0; i<length; i++)
                {
                    line = reader.ReadLine();
                    string[] info = line.Split('|');
                    classes.Add(new Class(info[0], int.Parse(info[1]), info[2], info[3], info[4]));
                }
                reader.Close();
                return classes;
            }
        }

        public void SaveAll(List<Class> classes)
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach(Class @class in classes)
                    writer.WriteLine(@class.ID + "|" + @class.Population + "|" + @class.SubjectID + "|" + @class.MajorID + "|" + @class.TeacherId);
                writer.Flush();
                writer.Close();
            }
        }

        public void Add(Class @class)
        {
            using(StreamWriter writer = new StreamWriter(fileName,true))
            {
                writer.WriteLine(@class.ID + "|" + @class.Population + "|" + @class.SubjectID + "|" + @class.MajorID + "|" + @class.TeacherId);
                writer.Flush();
                writer.Close();
            }
        }

        public int GetIndex(string id)
        {
            List<Class> classes = GetClassList();
            for (int i = 0; i < classes.Count; i++)
                if (classes[i].ID == id)
                    return i;
            return -1;
        }

        public void Update(string id, Class newIfo)
        {
            List<Class> classes = GetClassList();
            classes[GetIndex(id)] = newIfo;
            SaveAll(classes);
        }

        public void Delete(string id)
        {
            List<Class> classes = GetClassList();
            classes.RemoveAt(GetIndex(id));
            SaveAll(classes);
        }
    }
}
