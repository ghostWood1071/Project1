using Project1.DataAcessLayer.Model;
using Project1.DataAcessLayer.Model.Interface;
using Project1.LogicalHandlerLayer;
using Project1.UI.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Timers;
using System.Threading;

namespace Project1.UI
{
    class LoginUI:IUIable
    {
        IUserable<User> userable = new LoginHandler();

        public void Login()
        {
            Console.Clear();
            Console.CursorVisible = true;
            Console.OutputEncoding = Encoding.UTF8;
            while (true)
            {
                MessageBox messageBox = new MessageBox(30, 10, 10, "Đăng nhập");
                messageBox.Draw();

                Console.CursorLeft = messageBox.PaddingLeft + 3;
                Console.CursorTop = (messageBox.Height + messageBox.PaddingTop) - 7;
                Console.Write("Tài khoản: ");

                Console.CursorLeft = messageBox.PaddingLeft + 3;
                Console.CursorTop += 3;
                Console.Write("Mật Khẩu: ");

                Console.CursorTop -= 3;
                Console.CursorLeft = messageBox.PaddingLeft + 3 + "Tài khoản: ".Length;
                string account = Console.ReadLine();

                Console.CursorTop += 2;
                Console.CursorLeft = messageBox.PaddingLeft + 3 + "Mật khẩu: ".Length;
                string password = Console.ReadLine();
                User user = userable.Login(account, password);
                if (user != null)
                {
                    IUIable UI = LoginHandler.GetUI(user);
                    UI.Menu();
                    break;
                }
                else
                {
                    Console.Clear();
                    MessageBox box = new MessageBox(50, 5, Console.CursorTop + 5, "Lỗi đăng nhập");
                    box.Draw();
                    Console.CursorLeft = box.PaddingLeft + 3;
                    Console.CursorTop = Console.CursorTop - 3;
                    Console.WriteLine("Mật khẩu hoặc tài khoản không đúng");
                    Thread.Sleep(1000);
                    Console.Clear();
                    Login();
                    
                }
            }
            
        }

        public void Logout()
        {
            Login();
        }

        public void Menu()
        {
            throw new NotImplementedException();
        }

        public void Search()
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Add()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }
    }
}
