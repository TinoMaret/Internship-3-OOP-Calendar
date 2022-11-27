using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treci_DUMP_Domaći
{
    public class Eventi
    {
        public Guid Id { get; }
        public string EventName { get; set; }
        public string EventLocation { get; set; }
        public DateTime EventStart { get; set; }
        public DateTime EventEnd { get; set; }
        public string ParticipantEmails { get; private set; }
        public Eventi(string eventName, string eventLocation, DateTime eventStart, DateTime eventEnd, string participantEmails)
        {
            Id = Guid.NewGuid();
            EventName = eventName;
            EventLocation = eventLocation;
            EventStart = eventStart;
            EventEnd = eventEnd;
            ParticipantEmails = participantEmails;
        }

        public bool CheckIfOngoing()
        {
            if (EventStart < DateTime.Now && EventEnd > DateTime.Now)
                return true;

            else
                return false;
        }

        public bool CheckIfUpcoming()
        {
            if (EventStart > DateTime.Now)
                return true;
            else
                return false;
        }

        public bool CheckIfEnded()
        {
            if (EventEnd < DateTime.Now)
                return true;
            else
                return false;
        }

        public List<string> ListOfEmailsFromAnEvent()
        {
            List<string> EmailsFromAnEvent = new List<string>();
            String[] strlist = ParticipantEmails.Split(' ');
            foreach (string str in strlist)
            {
                EmailsFromAnEvent.Add(str);
            }
            return EmailsFromAnEvent;
        }

        public void RemoveParticipants(string a)
        {
            List<string> EmailsFromAnEvent = ListOfEmailsFromAnEvent();
            String[] strlist = a.Split(' ');
            foreach(string s in strlist)
            {
                EmailsFromAnEvent.Remove(s);
            }
            StringBuilder myStringBuilder = new StringBuilder();
            foreach(string e in EmailsFromAnEvent)
            {
                myStringBuilder.Append(e + "");
            }

            ParticipantEmails = myStringBuilder.ToString();
        }


    }
}
