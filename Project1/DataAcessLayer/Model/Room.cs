using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.DataAcessLayer.Model
{
    class Room
    {
        private string id;
        private string name;
        private int capacity;

        public Room()
        {

        }

        public Room(string id, string name, int capacity)
        {
            this.id = id;
            this.name = name;
            this.capacity = capacity;
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

        public int Capacity
        {
            get { return this.capacity; }
            set { this.capacity = value; }
        }
    }
}
