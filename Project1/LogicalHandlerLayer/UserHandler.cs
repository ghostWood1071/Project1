using Project1.DataAcessLayer.DataAcess;
using Project1.DataAcessLayer.Model;
using Project1.DataAcessLayer.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.LogicalHandlerLayer
{
    class UserHandler : ICRUD<User>
    {

        UserDA userDA = new UserDA();

        public void Add(User Object)
        {
            userDA.Add(Object);
        }

        public void Delete(string id)
        {
            userDA.Delete(id);
        }

        public int GetIndex(string id)
        {
            return userDA.GetIndex(id);
        }

        public List<User> GetList()
        {
            return userDA.GetList();
        }

        public List<User> GetList(int length)
        {
            return userDA.GetList(length);
        }

        public void SaveAll(List<User> list)
        {
            userDA.SaveAll(list);
        }

        public void Update(string id, User newInfo)
        {
            userDA.Update(id, newInfo);
        }
    }
}
