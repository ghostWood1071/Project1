using Project1.LogicalHandlerLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.DataAcessLayer.Model.Interface
{
    interface IUserable<T>
    {
        T Login(string account, string password);
        void Logout();
    }
}
