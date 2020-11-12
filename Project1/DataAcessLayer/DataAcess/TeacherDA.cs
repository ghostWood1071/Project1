using Project1.DataAcessLayer.Model;
using Project1.DataAcessLayer.Model.Interface;
using Project1.Source;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Project1.DataAcessLayer.DataAcess
{
    class TeacherDA:ICRUD<Teacher>
    {
        private string fileName = StringSource.TEACHER_DTB_NAME;

        public List<Teacher> GetList()
        {
            List<Teacher> listTeacher = new List<Teacher>();
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            using(StreamReader reader = new StreamReader(fileName))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] infos = line.Split('|');
                    string teacherID = infos[0];
                    string teacherName = infos[1];
                    string position = infos[2];
                    string subjectId = infos[3];
                    listTeacher.Add(new Teacher(teacherID, teacherName, int.Parse(position), subjectId));
                    line = reader.ReadLine();
                }
                reader.Close();
                return listTeacher;
            }
        }

        public List<Teacher> GetList(int length)
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();

            List<Teacher> result = new List<Teacher>();
            using(StreamReader reader = new StreamReader(fileName))
            {
                string line;
                for(int i = 0; i<length; i++)
                {
                    line = reader.ReadLine();
                    if (line == null)
                        break;
                    string[] infos = line.Split('|');
                    result.Add(new Teacher(infos[0], infos[1], int.Parse(infos[2]), infos[3]));
                }
                return result;
            }
        }

        public void SaveAll(List<Teacher> teachersDatas)
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            using(StreamWriter writer = new StreamWriter(fileName))
            {
                foreach(var teacher in teachersDatas)
                    writer.WriteLine(teacher.ID + "|" + teacher.Name + "|" + teacher.Position + "|"+teacher.SubjectID);
                writer.Flush();
                writer.Close();
            }
        }

        public int GetIndex(string id)
        {
            List<Teacher> teachers = GetList();
            for(int i  = 0; i< teachers.Count(); i++)
                if (teachers[i].ID == id)
                    return i;
            return -1;
        }

        public void Add(Teacher teacher)
        {
            using(StreamWriter writer = new StreamWriter(fileName, true, Encoding.UTF8))
            {
                writer.WriteLine(teacher.ID+"|"+teacher.Name+"|"+teacher.Position+"|"+teacher.SubjectID);
                writer.Flush();
                writer.Close();
            }
        }

        public void Update(string id, Teacher newInfo)
        {
            List<Teacher> teachers = GetList();
            int index = GetIndex(id);
            teachers[index] = newInfo;
            SaveAll(teachers);
        }

        public void Delete(string id)
        {
            List<Teacher> teachers = GetList();
            int index = GetIndex(id);
            teachers.RemoveAt(index);
            SaveAll(teachers);
        }
    }
}
