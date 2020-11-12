using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project1.DataAcessLayer.Model
{
    class Assignment // kế hoạch phân công
    {
        private string id;
        private string classId;
        private string teacherId;
        private string termId;
        private int semester;
        private string year;

        public Assignment()
        {

        }

        public Assignment(string id, string classId, string teacherId, string termId, int semester, string year)
        {
            this.id = id;
            this.classId = classId;
            this.teacherId = teacherId;
            this.termId = termId;
            this.year = year;
            this.semester = semester;
        }

        public Assignment(string classId, string teacherId, string termId, int semester)
        {
            this.classId = classId;
            this.teacherId = teacherId;
            this.termId = termId;
            this.id = CreateID(); 
            this.year = DateTime.Now.Year + "-" + (DateTime.Now.Year + 1);
            this.semester = semester;
        }

        public string ID
        {
            get { return this.id; }
        }

        public string ClassID
        {
            get { return this.classId; }
            set 
            { 
                this.classId = value;
                this.id = CreateID();
            }
        }

        public string TeacherID
        {
            get { return this.teacherId; }
            set 
            { 
                this.teacherId = value;
                this.id = CreateID();
            }
        }

        public string TermID
        {
            get { return this.termId; }
            set { 
                this.termId = value;
                this.id = CreateID();
            }
        }

        public int Semester { get => semester; set => semester = value; }
        public string Year { get => year; set => year = value; }

        private string CreateID()
        {
            return this.id = this.classId.Substring(3) + this.teacherId + this.TermID;
        }
    }
}
