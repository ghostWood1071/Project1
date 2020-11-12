using Project1.DataAcessLayer.Model;
using Project1.DataAcessLayer.Model.Interface;
using Project1.Source;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.DataAcessLayer.DataAcess
{
    class AssignmentDA: ICRUD<Assignment>
    {
        private string fileName = StringSource.ASSIGNMENT_DB_NAME;

        public List<Assignment> GetList()
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            List<Assignment> assignments = new List<Assignment>();
            using(StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    string[] info = line.Split('|');
                    assignments.Add(new Assignment(info[0], info[1], info[2], info[3], int.Parse(info[4]), info[5]));
                }
                reader.Close();
                return assignments;
            }
        }

        public List<Assignment> GetList(int length)
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            List<Assignment> assignments = new List<Assignment>();
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                for(int i = 0; i<length; i++)
                {
                    line = reader.ReadLine();
                    string[] info = line.Split('|');
                    assignments.Add(new Assignment(info[0], info[1], info[2], info[3], int.Parse(info[4]), info[5]));
                }
                reader.Close();
                return assignments;
            }
        }

        public void SaveAll(List<Assignment> assignments)
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            using(StreamWriter writer = new StreamWriter(fileName))
            {
                foreach(var assign in assignments)
                    writer.WriteLine(assign.ID + "|" + assign.ClassID + "|" + assign.TeacherID+ "|" + assign.TermID+"|"+assign.Semester+"|"+assign.Year);
                writer.Flush();
                writer.Close();
            }
        }

        public void Add(Assignment assign)
        {
            using(StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine(assign.ID + "|" + assign.ClassID + "|" + assign.TeacherID + "|" + assign.TermID + "|" + assign.Semester + "|" + assign.Year);
                writer.Flush();
                writer.Close();
            }
        }
        public int GetIndex(string id)
        {
            List<Assignment> assignments = GetList();
            for(int i = 0; i<assignments.Count; i++)
                if (assignments[i].ID == id)
                    return i;
            return -1;
        }

        public void Update(string id, Assignment newInfo)
        {
            List<Assignment> assignments = GetList();
            assignments[GetIndex(id)] = newInfo;
            SaveAll(assignments);
        }

        public void Delete(string id)
        {
            List<Assignment> assignments = GetList();
            assignments.RemoveAt(GetIndex(id));
            SaveAll(assignments);
        }

       
    }
}
