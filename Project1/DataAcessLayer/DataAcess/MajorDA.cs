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
    class MajorDA
    {
        string fileName = StringSource.MAJOR_DB_NAME;

        public List<Major> GetListMajor()
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            List<Major> majors = new List<Major>();
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] info = line.Split('|');
                    majors.Add(new Major(info[0], info[1], info[2]));
                    line = reader.ReadLine();
                }
                reader.Close();
                return majors;
            }
        }

        public List<Major> GetListRoom(int length)
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            List<Major> majors = new List<Major>();
            using (StreamReader reader = new StreamReader(fileName))
            {
                for (int i = 0; i < length; i++)
                {
                    string line = reader.ReadLine();
                    string[] info = line.Split('|');
                    majors.Add(new Major(info[0], info[1], info[2]));
                }
                reader.Close();
                return majors;
            }
        }

        public void SaveAllData(List<Major> majors)
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var Major in majors)
                    writer.WriteLine(Major.ID + "|" + Major.Name + "|" + Major.SubjectID);
                writer.Flush();
                writer.Close();
            }
        }

        public void AddMajor(Major Major)
        {
            using (StreamWriter writer = new StreamWriter(fileName, true, Encoding.UTF8))
            {
                writer.WriteLine(Major.ID + "|" + Major.Name + "|" + Major.SubjectID);
                writer.Flush();
                writer.Close();
            }
        }

        public int GetMajorIndex(string id)
        {
            List<Major> majors = GetListMajor();
            for (int i = 0; i < majors.Count; i++)
            {
                if (majors[i].ID == id)
                    return i;
            }
            return -1;
        }

        public void UpdateMajor(string id, Major newInfo)
        {
            int index = GetMajorIndex(id);
            List<Major> majors = GetListMajor();
            majors[index] = newInfo;
            SaveAllData(majors);
        }

        public void DeleteMajor(string id)
        {
            int index = GetMajorIndex(id);
            List<Major> majors = GetListMajor();
            majors.RemoveAt(index);
            SaveAllData(majors);
        }
    }
}
