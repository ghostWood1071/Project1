using Project1.DataAcessLayer.DataAcess;
using Project1.DataAcessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.LogicalHandlerLayer
{
    class ClassHandler
    {
        ClassDA classDA = new ClassDA();

        public List<Class> GetClasses()
        {
            return classDA.GetClassList();
        }

        public List<Class> GetClasses(int length)
        {
            return classDA.GetClassList(length);
        }

        public void SaveAll(List<Class> classes)
        {
            classDA.SaveAll(classes);
        }

        public void Update(string id, Class newInfo)
        {
            classDA.Update(id, newInfo);
        }

        public void Delete(string id)
        {
            classDA.Delete(id);
        }

        public void Add(Class @class)
        {
            classDA.Add(@class);
        }

        public int GetIndex(string id)
        {
            return classDA.GetIndex(id);
        }


    }
}
