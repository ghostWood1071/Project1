using Project1.DataAcessLayer.DataAcess;
using Project1.DataAcessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.LogicalHandlerLayer
{
    class AssignmentHandler
    {
        AssignmentDA DA = new AssignmentDA();
        public List<Assignment> GetAssignments()
        {
            return DA.GetAssignments();
        }

        public List<Assignment> GetAssignments(int length)
        {
            return DA.GetAssignments(length);
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
    }
}
