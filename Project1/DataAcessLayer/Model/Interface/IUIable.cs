using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.DataAcessLayer.Model.Interface
{
    interface IUIable
    {
        void Menu();
        void Add();
        void Update();
        void Delete();
        void Search();
        void Show();
    }
}
