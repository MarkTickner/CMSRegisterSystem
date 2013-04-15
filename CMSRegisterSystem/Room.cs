using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSRegisterSystem
{
    public class Room
    {
        private string roomID;
        public string RoomID
        {
            get { return roomID; }
        }

        private string roomName;
        public string RoomName
        {
            get { return roomName; }
        }

        public Room(string roomID, string roomName)
        {
            this.roomID = roomID;
            this.roomName = roomName;
        }
    }
}