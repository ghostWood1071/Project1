using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.DataAcessLayer.Model
{
    class Term // học phần
    {
        private string id;
        private string name;
        private int creditsNum;
        private string subjectId;
   

        public Term()
        {

        }



        public Term(string id, string name, int creditsNum, string subjectId)
        {
            this.id = id;
            this.name = name;
            this.creditsNum = creditsNum;
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

        public int CreditNum
        {
            get { return this.creditsNum; }
            set { this.creditsNum = value; }
        }

        public string SubjectId { get => subjectId; set => subjectId = value; }

    }
}
