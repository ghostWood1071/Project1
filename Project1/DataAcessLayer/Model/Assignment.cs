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

        public Assignment()
        {

        }

        public Assignment(string id, string classId, string teacherId, string termId)
        {
            this.id = id;
            this.classId = classId;
            this.teacherId = teacherId;
            this.termId = termId;
        }

        public Assignment(string classId, string teacherId, string termId)
        {
            this.classId = classId;
            this.teacherId = teacherId;
            this.termId = termId;
            this.id = CreateID();
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

        private string CreateID()
        {
            return this.id = this.classId.Substring(3) + this.teacherId + this.TermID;
        }
    }
}
