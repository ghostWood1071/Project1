using Project1.DataAcessLayer.DataAcess;
using System;
using System.Collections.Generic;
using Project1.DataAcessLayer.Model;
using System.Text.RegularExpressions;
using Project1.DataAcessLayer.Model.Interface;
using Project1.Source;

namespace Project1.LogicalHandlerLayer
{
    class TeacherHandler: ICRUD<Teacher>
    {
        TeacherDA teacherDA = new TeacherDA();
        ICRUD<User> UserHandler = new UserHandler();
        public List<Teacher> GetList()
        {
            return teacherDA.GetList();
        }

        public List<Teacher> GetList(int length)
        {
            return teacherDA.GetList(length);
        }

        public void SaveAll(List<Teacher> teachers)
        {
            teacherDA.SaveAll(teachers);
        }

        public void Add(Teacher teacher)
        {
            teacher.Password = StringSource.DEFAULT_PASSWORD;
            teacherDA.Add(teacher);
            UserHandler.Add(teacher);
        } 

        public void Update(string id, Teacher newInfo)
        {
            teacherDA.Update(id, newInfo);
            UserHandler.Update(id, newInfo);
        }

        public void Delete(string id)
        {
            teacherDA.Delete(id);
            UserHandler.Delete(id);
        }

        public int GetIndex(string id)
        {
            return teacherDA.GetIndex(id);
        }

        public int GetIndex(string id, List<Teacher> teachers)
        {
            for(int i = 0; i<teachers.Count; i++)
            {
                if (teachers[i].ID == id)
                    return i;
            }
            return -1;
        }

        public Teacher GetInfor(string id)
        {
            int index = GetIndex(id);
            return GetList()[index];
        }

        public Teacher GetInfo(string id, List<Teacher> teachers)
        {
            foreach (var teacher in teachers)
                if (id == teacher.ID)
                    return teacher;
            return null;
        }

        public bool CheckID(string id)
        {
            if(id.Length == 3)
            {
                for(int i = 0; i<3; i++)
                    if (!Regex.IsMatch(id[i].ToString(), "[0-9]"))
                        return false;
                return true;
            }
            return false;
        }

        public bool CheckName(string name, bool acceptNull = false)
        {
            if (acceptNull)
            {
                if (CheckStringWithRegex(name, "[a-zA-Z ]") || name == "")
                    return true;
            }
            else
            {
                if (name.Length >= 10 && CheckStringWithRegex(name, "[a-zA-Z ]"))
                    return true;
            }
            return false;
        }

        public bool CheckPosition(string position, bool acceptNull = false)
        {
            if (acceptNull)
            {
                if (CheckStringWithRegex(position, "[a-zA-Z ]") || position == "")
                    return true;
            }
            else
            {
                if (position.Length >= 9 && CheckStringWithRegex(position, "[a-zA-Z ]"))
                    return true;
            }
            return false;
        }

        private bool CheckStringWithRegex(string input, string pattern)
        {
            for (int i = 0; i < input.Length; i++)
                if (!Regex.IsMatch(input[i].ToString(), pattern))
                    return false;
            return true;
        }

        public string GetPosition(int pos)
        {
            string[] positions = {"Trưởng khoa" , "Phó Trưởng khoa", "Trưởng bộ môn", "Phó Trưởng bộ môn", "giảng viên", "giáo vụ khoa"};
            return positions[pos];
        }

        public int GetRole(int position)
        {
            switch (position)
            {
                case 0:
                case 1:
                case 5:
                    return (int) UserPermission.Admin;
                case 2:
                    return (int)UserPermission.HeadSection;
                case 3:
                case 4:
                    return (int)UserPermission.Normal;
                default:
                    return (int)UserPermission.Normal;
            }
        }

        public List<Teacher> GetList(string subjectID)
        {
            List<Teacher> result = new List<Teacher>();
            List<Teacher> root = GetList();
            foreach(var teacher in root)
            {
                if (teacher.SubjectID == subjectID)
                    result.Add(teacher);
            }
            return result;
        }
    }
}
