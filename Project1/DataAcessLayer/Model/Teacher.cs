using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.DataAcessLayer.Model
{
    class Teacher: User
    {
        private string id;
        private string name;
        private string subjectId;
        private int position; //chức vụ

        public Teacher()
        {
            
        }

        public Teacher(string id, string name, int position, string subjectId)
        {
            this.id = id;
            this.name = name;
            this.position = position;
            this.subjectId = subjectId;
            this.account = id;
        }

        public Teacher(string id, string name, int position, string subjectId, int role)
        {
            this.id = id;
            this.name = name;
            this.position = position;
            this.subjectId = subjectId;
            this.account = id;
            this.role = role;
        }

        public Teacher(string id, int role)
        {
            this.id = id;
            this.role = role;
            this.position = role;
        }

        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string SubjectID
        {
            get { return this.subjectId; }
            set { this.subjectId = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public int Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

    }
        
}
