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
    class SubjectDA
    {
        string fileName = StringSource.SUBJECT_DB_NAME;

        public List<Subject> GetSubjectList()
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            List<Subject> subjects = new List<Subject>();
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] info = line.Split('|');
                    subjects.Add(new Subject(info[0], info[1]));
                    line = reader.ReadLine();
                }
                reader.Close();
                return subjects;
            }
        }

        public List<Subject> GetSubjectList(int length)
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            List<Subject> subjects = new List<Subject>();
            using (StreamReader reader = new StreamReader(fileName))
            {
                
                for(int i =0; i<length; i++)
                {
                    string line = reader.ReadLine();
                    string[] info = line.Split('|');
                    subjects.Add(new Subject(info[0], info[1]));
                }
                reader.Close();
                return subjects;
            }
        }

        public void SaveAllData(List<Subject> subjects)
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            using(StreamWriter writer = new StreamWriter(fileName))
            {
                foreach(var sub in subjects)
                    writer.WriteLine(sub.ID + "|" + sub.Name);
                writer.Flush();
                writer.Close();
            }
        }

        public int GetSubIndex(string id)
        {
            List<Subject> subjects = GetSubjectList();
            for (int i = 0; i < subjects.Count; i++)
                if (subjects[i].ID == id)
                    return i;
            return -1;
        }

        public void AddSubject(Subject subject)
        {
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine(subject.ID + "|" + subject.Name);
                writer.Flush();
                writer.Close();
            }
        }

        public void UpdateSubject(string id, Subject newInfo)
        {
            int index = GetSubIndex(id);
            List<Subject> subjects = GetSubjectList();
            subjects[index] = newInfo;
            SaveAllData(subjects);
        }

        public void DeleteSubject(string id)
        {
            int index = GetSubIndex(id);
            List<Subject> subjects = GetSubjectList();
            subjects.RemoveAt(index);
            SaveAllData(subjects);
        }
    }
}
