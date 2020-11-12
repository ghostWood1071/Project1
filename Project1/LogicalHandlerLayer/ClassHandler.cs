using Project1.DataAcessLayer.DataAcess;
using Project1.DataAcessLayer.Model;
using System;
using System.Collections.Generic;

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

        public int GetIndex(string id, List<Class> classes)
        {
            for(int i = 0; i<classes.Count; i++)
            {
                if (classes[i].ID == id)
                    return i;
            }
            return -1;
        }

        public List<Class> GetList(string subjectID)
        {
            List<Class> classes = GetClasses();
            List<Class> result = new List<Class>();
            foreach(var cl in classes)
            {
                if (cl.SubjectID == subjectID)
                    result.Add(cl);
            }
            return result;

        }

        public Class GetClass(string id)
        {
            List<Class> classes = GetClasses();
            return classes[GetIndex(id)];
        }

        public Class GetClass(string id, List<Class> classes)
        {
            foreach(Class @class in classes)
            {
                if (id == @class.ID)
                    return @class;
            }
            return null;
        }


    }
}
