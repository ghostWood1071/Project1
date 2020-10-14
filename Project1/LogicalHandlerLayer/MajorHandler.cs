using Project1.DataAcessLayer.DataAcess;
using Project1.DataAcessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.LogicalHandlerLayer
{
    class MajorHandler
    {
        MajorDA majorDA = new MajorDA();

        SubjectHandler handler = new SubjectHandler();

        public List<Major> GetMajors()
        {
            return majorDA.GetListMajor();
        }

        public void SaveAll(List<Major> majors)
        {
            majorDA.SaveAllData(majors);
        }

        public void AddMajor(Major major)
        {
            majorDA.AddMajor(major);
        }

        public void UpdateMajor(string id, Major newInfo)
        {
            majorDA.UpdateMajor(id, newInfo);
        }

        public void DeleteMajor(string id)
        {
            majorDA.DeleteMajor(id);
        }

        public int GetMajorIndex(string id)
        {
            return majorDA.GetMajorIndex(id);
        }

        public bool CheckId(string id)
        {
            return handler.CheckIdSyntax(id);
        }

        public bool CheckName(string name)
        {
            return handler.CheckName(name);
        }

    }
}
