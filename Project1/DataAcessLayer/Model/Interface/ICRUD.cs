using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.DataAcessLayer.Model.Interface
{
    interface ICRUD<T>
    {
        List<T> GetList();
        List<T> GetList(int length);
        void Add(T Object);
        void Delete(string id);
        void Update(string id, T newInfo);
        int GetIndex(string id);
        void SaveAll(List<T> list);
    }
}
