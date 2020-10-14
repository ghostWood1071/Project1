using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.DataAcessLayer.Model
{
    class Major // chuyên ngành
    {
        private string id;
        private string name;
        private string subjectId;

        public Major()
        {

        }

        public Major(string id, string name, string subjectId)
        {
            this.id = id;
            this.name = name;
            this.subjectId = subjectId;
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

        public string SubjectID
        {
            get { return this.subjectId; }
            set { this.subjectId = value; }
        }
    }
}
