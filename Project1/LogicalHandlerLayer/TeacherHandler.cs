using Project1.DataAcessLayer.DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project1.DataAcessLayer.Model;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace Project1.LogicalHandlerLayer
{
    class TeacherHandler
    {
        TeacherDA teacherDA = new TeacherDA();
        SubjectHandler handler = new SubjectHandler();
        public List<Teacher> GetTeacherList()
        {
            return teacherDA.GetTeacherList();
        }

        public List<Teacher> GetTeacherList(int length)
        {
            return teacherDA.GetTeacherList(length);
        }

        public void SaveAllData(List<Teacher> teachers)
        {
            teacherDA.SaveAllData(teachers);
        }

        public void AddTeacher(Teacher teacher)
        {
            teacherDA.AddTeacher(teacher);
        } 

        public void UpdateTeacher(string id, Teacher newInfo)
        {
            teacherDA.UpdateTeacher(id, newInfo);
        }

        public void DeleteTeacher(string id)
        {
            teacherDA.DeleteTeacher(id);
        }

        public int GetTeacherIndex(string id)
        {
            return teacherDA.GetTeacherIndex(id);
        }

        public Teacher GetTeacherInfor(string id)
        {
            int index = GetTeacherIndex(id);
            return GetTeacherList()[index];
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
    }
}
