using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Project1.DataAcessLayer.Model
{
    class Schedule
    {
        private DateTime time;
        private string assignmentId;
        private string roomID;
        private int population;

        public Schedule()
        {

        }

        public Schedule(DateTime time, string assignmentId, string roomID, int population)
        {
            this.time = time;
            this.roomID = roomID;
            this.assignmentId = assignmentId;
            this.population = population;
        }

        public DateTime Time
        {
            get { return this.time; }
            set { this.time = value; }
        }

        public string AssignmentID
        {
            get { return this.assignmentId; }
            set { this.assignmentId = value; }
        }

        public string RoomID
        {
            get { return this.roomID; }
            set { this.roomID = value; }
        }

        public int Population
        {
            get { return this.population; }
            set { this.population = value; }
        }
    }
}
