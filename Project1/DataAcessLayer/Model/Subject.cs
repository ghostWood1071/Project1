using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project1.DataAcessLayer.Model
{
    class Subject // bộ môn
    {
        private string id;
        private string name;

        public Subject()
        {

        }

        public Subject(string id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
    }
}
