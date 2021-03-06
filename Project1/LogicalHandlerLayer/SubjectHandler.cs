﻿using Project1.DataAcessLayer.DataAcess;
using Project1.DataAcessLayer.Model;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Project1.LogicalHandlerLayer
{
    class SubjectHandler
    {
        SubjectDA subjectDA = new SubjectDA();

        public List<Subject> GetSubjects()
        {
            return subjectDA.GetSubjectList();
        }

        public List<Subject> GetSubjects(int length)
        {
            return subjectDA.GetSubjectList(length);
        }

        public void SaveAll(List<Subject> subjects)
        {
            subjectDA.SaveAllData(subjects);
        }

        public void AddSubject(Subject subject)
        {
            subjectDA.AddSubject(subject);
        }

        public void UpdateSubject(string id, Subject newInfo)
        {
            subjectDA.UpdateSubject(id, newInfo);
        }

        public void DeleteSubject(string id)
        {
            subjectDA.DeleteSubject(id);
        }

        public int GetSubIndex(string id)
        {
            return subjectDA.GetSubIndex(id);
        }

        public int GetSubIndex(string id, List<Subject> subjects)
        {
            for(int i = 0; i<subjects.Count; i++)
            {
                if (subjects[i].ID == id)
                    return i;
            }
            return -1;
        }

        public bool CheckIdSyntax(string id)
        {
            if (id.Length == 2)
            {
                for (int i = 0; i < 2; i++)
                    if (!Regex.IsMatch(id[i].ToString(), "[0-9]"))
                        return false;
                return true;
            }
            return false;
        }

        public bool CheckName(string name)
        {
            if (name.Length >= 10)
            {
                for(int i = 0; i<name.Length; i++)
                    if (!Regex.IsMatch(name[i].ToString(), "[0-9A-Za-z ]"))
                        return false;
                    return true;
            }
            return false;
        }

        public Subject GetSubject(string id, List<Subject> subjects)
        {
            foreach(var sub in subjects)
            {
                if (sub.ID == id)
                    return sub;
            }
            return null;
        }

    }
}
