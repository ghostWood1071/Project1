using Project1.DataAcessLayer.DataAcess;
using Project1.DataAcessLayer.Model;
using Project1.DataAcessLayer.Model.Interface;
using Project1.Source;
using Project1.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.LogicalHandlerLayer
{
    class AssignmentHandler: ICRUD<Assignment>
    {
        public delegate bool DeleteCondition(Assignment assignment);

        AssignmentDA DA = new AssignmentDA();

        TermsHandler termHandler = new TermsHandler();

        public List<Assignment> GetList()
        {
            return DA.GetList();
        }

        public List<Assignment> GetList(int length)
        {
            return DA.GetList(length);
        }

        public List<Assignment> GetList(List<Class> classes, List<Term> terms)
        {
            List<Assignment> result = new List<Assignment>();
            foreach(var t in terms)
            {
                foreach(var c in classes)
                {
                    result.Add(new Assignment(c.ID, null, t.ID, 0));
                }
            }
            return result;
        }

        public List<Assignment> GetList(string teacherId)
        {
            List<Assignment> assignments = GetList();
            List<Assignment> result = new List<Assignment>();
            foreach(Assignment assignment in assignments)
            {
                if (assignment.TeacherID == teacherId)
                    result.Add(assignment);
            }
            return result;
        }

        public void SaveAll(List<Assignment> assignments)
        {
            DA.SaveAll(assignments);
        }

        public int GetIndex(string id)
        {
            return DA.GetIndex(id);
        }

        public void Add(Assignment assignment)
        {
            DA.Add(assignment);
        }

        public void Update(string id, Assignment newInfo)
        {
            DA.Update(id, newInfo);
        }

        public void Delete(string id)
        {
            DA.Delete(id);
        }

        public void Delete(DeleteCondition deleteCondition)
        {
            List<Assignment> assignments = GetList();
            for(int i = 0; i<assignments.Count; i++)
            {
                if (deleteCondition(assignments[i]))
                    assignments.RemoveAt(i);
            }
            SaveAll(assignments);
        }

        public Assignment GetAssignment(string id)
        {
            int index = GetIndex(id);
            if (index >= 0)
                return GetList()[index];
            return null;
        }

        public void Combine(List<Assignment> assignments) 
        {
            int thisYear = DateTime.Now.Year;
            List<Assignment> list = GetList();
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Assignment assignment = assignments[GetIndex(assignments, list[i].ClassID.Substring(3) + list[i].TermID)];
                    if (thisYear > int.Parse((list[i].Year.Split('-')[0])))
                        assignments.Remove(assignment);
                    else
                    {
                        assignment.TeacherID = list[i].TeacherID;
                        assignment.Semester = list[i].Semester;
                        assignment.Year = list[i].Year;
                    }
                }
            }
            catch
            {
                
            }
        }

        private int GetIndex(List<Assignment> assignments, string id)
        {
            for(int i = 0; i<assignments.Count; i++)
            {
                if (assignments[i].ID == id)
                    return i;
            }
            return -1;
        }

        public int GetTeacherIndex(List<Teacher> teachers, string id)
        {
            for(int i = 0; i<teachers.Count; i++)
            {
                if (teachers[i].ID == id)
                    return i;
            }
            return -1;
        }

        public float GetStandardTime(int position)
        {
            if (position == (int)TeacherPosition.Dean || position == (int)TeacherPosition.DeanAssist)
            {
                return NumberValue.StandardTime - NumberValue.StandardTime * (30.0f / 100);

            } else if (position == (int)TeacherPosition.HeadSection || position == (int)TeacherPosition.HeadSectionAsist){
                return NumberValue.StandardTime - NumberValue.StandardTime * (20.0f / 100);
            } else
                return NumberValue.StandardTime;
        }

        public int GetWorkTime(List<Assignment> assignments, string teacherId)
        {
            int workTime = 0;
            foreach(var assginment in assignments)
            {
                if(assginment.TeacherID == teacherId)
                {
                    workTime += termHandler.GetTerm(assginment.TermID).CreditNum * NumberValue.CreditNumPerSubject;
                }
            }
            return workTime;
        }
    }
}
