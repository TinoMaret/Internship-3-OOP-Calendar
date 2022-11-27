using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treci_DUMP_Domaći
{
    public class Osobe
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }

        private Dictionary<Guid, bool> _attendance = new Dictionary<Guid, bool>();

        public Osobe(string name, string surname, string email)
        {
            Name = name;
            Surname = surname;
            Email = email;
        }

        public void Attendance(Guid ID, bool Attended)
        {
            if (_attendance.ContainsKey(ID))
                _attendance[ID] = Attended;
            else
                _attendance.Add(ID, Attended);
        }

        public void AttendanceRemove(Guid ID)
        {
            _attendance.Remove(ID);
        }

        public bool CheckIfAttended(Guid ID)
        {
            return _attendance[ID];
        }
    }
}
