using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Project1.DataAcessLayer.Model
{
    class Class
    {
        private string id;
        private int population;
        private string subjectId;
        private string majorId;
        private string teacherId;

        public Class()
        {

        }

        public Class(string id, int population, string subject, string major, string teacherId)
        {
            this.id = id;
            this.population = population;
            this.subjectId = subject;
            this.majorId = major;
            this.teacherId = teacherId;
        }

        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public int Population
        {
            get { return this.population; }
            set { this.population = value; }
        }

        public string SubjectID
        {
            get { return this.subjectId; }
            set { this.subjectId = value;  }
        }

        public string MajorID
        {
            get { return this.majorId; }
            set { this.majorId = value; }
        }

        public string TeacherId
        {
            get { return this.teacherId; }
            set { this.teacherId = value; }
        }

        public int StartYear
        {
            get { return int.Parse(this.id.Substring(3, 2)); }
        }

        public int EndYear
        {
            get { return this.StartYear + 4;}
        }
        
    }
}
