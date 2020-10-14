using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.DataAcessLayer.Model
{
    class Teacher
    {
        private string id;
        private string name;
        private string subjectId;
        private string position; //chức vụ

        public Teacher()
        {

        }

        public Teacher(string id, string name, string position, string subjectId)
        {
            this.id = id;
            this.name = name;
            this.position = position;
            this.subjectId = subjectId;
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

        public string Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

    }
        
}
