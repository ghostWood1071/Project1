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
    class LoginHandler : IUserable<User>
    {
        ICRUD<User> cRUD = new UserHandler();
        public User Login(string account, string password)
        {
            List<User> users = cRUD.GetList();
            int index = cRUD.GetIndex(account);
            if(index >= 0)
            {
                User user = users[index];
                if (user.Password == password)
                {
                    return user;
                }    
                else return null;
            }
            return null;
        }

        public static IUIable GetUI(User user)
        {
            switch (user.Role)
            {
                case (int)UserPermission.Admin:
                    return new AdminUI(user);
                case (int)UserPermission.HeadSection:
                    return new HeadSectionUI(user);
                default:
                    return new UserUI(user);
            }
        }

        public void Logout()
        {

        }
    }
}
