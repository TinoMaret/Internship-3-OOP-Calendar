using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Treci_DUMP_Domaći
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Osobe> People = new List<Osobe>();
            List<Eventi> Events = new List<Eventi>();

            People.Add(new Osobe("Ante", "Antić", "aantic@ante.com"));
            People.Add(new Osobe("Mate", "Matić", "mmatic@mate.com"));
            People.Add(new Osobe("Šime", "Šimić", "ssimic@sime.com"));
            People.Add(new Osobe("Jure", "Jurić", "jjuric@jure.com"));
            People.Add(new Osobe("Ferdo", "Ferdić", "fferdic@ferdo.com"));
            People.Add(new Osobe("Toni", "Tonić", "ttonić@toni.com"));
            People.Add(new Osobe("Luka", "Lukić", "llukic@luka.com"));
            People.Add(new Osobe("Brane", "Branić", "bbranic@brane.com"));
            People.Add(new Osobe("Mile", "Milić", "mmilic@mile.com"));
            People.Add(new Osobe("Kruno", "Krunić", "kkrunic@kruno.com"));

            Events.Add(new Eventi("Stand-up", "Trogir", new DateTime(2022, 12, 05, 21, 00, 00), new DateTime(2022, 12, 05, 22, 00, 00),
                "aantic@ante.com ssimic@sime.com jjuric@jure.com ttonić@toni.com bbranic@brane.com"));
            Events.Add(new Eventi("Predavanje", "Solin", new DateTime(2022, 07, 10, 11, 15, 00), new DateTime(2022, 07, 10, 13, 00, 00),
                "mmatic@mate.com llukic@luka.com mmilic@mile.com fferdic@ferdo.com bbranic@brane.com"));
            Events.Add(new Eventi("Dump Days", "Split", new DateTime(2022, 05, 14, 08, 00, 00), new DateTime(2022, 05, 16, 22, 00, 00),
                "ssimic@sime.com jjuric@jure.com ttonić@toni.com mmilic@mile.com kkrunic@kruno.com"));
            Events.Add(new Eventi("Aukcija satova", "Dubrovnik", new DateTime(2022, 11, 27, 14, 00, 00), new DateTime(2023, 01, 12, 19, 00, 00),
                "llukic@luka.com jjuric@jure.com kkrunic@kruno.com ttonić@toni.com mmatic@mate.com"));
            Events.Add(new Eventi("Humanitarna Božićna utakmica", "Split", new DateTime(2022, 12, 25, 17, 00, 00), new DateTime(2022, 12, 25, 19, 00, 00),
                "aantic@ante.com ssimic@sime.com fferdic@ferdo.com mmilic@mile.com bbranic@brane.com"));

            foreach(var e in Events)
            {
                string[] strlist = e.ParticipantEmails.Split(' ');
                foreach(string s in strlist)
                {
                    foreach(Osobe P in People)
                    {
                        if(P.Email == s)
                        {
                            P.Attendance(e.Id, true);
                        }
                    }
                }
            }


            int UserInput;
            do
            {
                Console.WriteLine("1 - Aktivni elementi");
                Console.WriteLine("2 - Nadolazeći eventi");
                Console.WriteLine("3 - Eventi koji su završili");
                Console.WriteLine("4 - Kreiraj event");
                Console.WriteLine("5 - Izađi iz programa");

                UserInput = UserInputControl(1, 5);

                Console.Clear();
                switch (UserInput)
                {
                    case 1:
                        foreach (var e in Events)
                        {
                            if (e.CheckIfOngoing())
                            {
                                Console.WriteLine($"Id: {e.Id}");
                                Console.WriteLine($"Naziv: {e.EventName} - Lokacija: {e.EventLocation} - Ends in: {Math.Round((e.EventEnd - DateTime.Now).TotalHours, 1).ToString()}");
                                Console.WriteLine($"Popis suduonika:");
                                Console.WriteLine(ListOfAttendees(People, e));
                            }
                        }

                        Console.WriteLine("1 - Zabilježi neprisutnost");
                        Console.WriteLine("2 - Povratak na glavni menu");

                        UserInput = UserInputControl(1, 2);

                        switch (UserInput)
                        {
                            case 1:
                                Console.WriteLine("Unesi Id eventa");
                                string ID;
                                bool EventExists = false; ;
                                do
                                {
                                    ID = Console.ReadLine();
                                    EventExists = CheckId(Events, Guid.Parse(ID));
                                    if (!EventExists)
                                    {
                                        Console.WriteLine("Ne postoji Event s unesenim ID-om");
                                    }

                                } while (!EventExists);
                                Guid IDEvent = Guid.Parse(ID);
                                Console.WriteLine("Unesi email-ove osoba koje zelis uklonit");
                                string emails;
                                bool EmailsCheck = false;
                                do
                                {
                                    emails = Console.ReadLine();
                                    foreach(var e in Events)
                                    {
                                        if (e.Id == IDEvent)
                                        {
                                            List<string> EmailsFromThisEvent = e.ListOfEmailsFromAnEvent();
                                            string[] SeparatedInputEmails = emails.Split(' ');
                                            foreach (string sep in SeparatedInputEmails)
                                            {
                                                if (EmailsFromThisEvent.Contains(sep))
                                                {
                                                    EmailsCheck = true;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Unijeli ste email osobe koja nije bila pozvana na event");
                                                    EmailsCheck = false;
                                                    break;
                                                }
                                            }
                                        }
                                    }

                                } while (!EmailsCheck);
                                if (ChangeConformation())
                                {
                                    foreach (var e in Events)
                                    {
                                        if (e.Id == IDEvent)
                                        {
                                            List<string> EmailsFromThisEvent = e.ListOfEmailsFromAnEvent();
                                            string[] SeparatedInputEmails = emails.Split(' ');
                                            foreach (string sep in SeparatedInputEmails)
                                            {
                                                if (EmailsFromThisEvent.Contains(sep))
                                                {
                                                    foreach(Osobe P in People)
                                                    {
                                                        if (P.Email == sep)
                                                        {
                                                            P.Attendance(e.Id, false);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                continue;

                            case 2:
                                continue;
                        }
                        
                        continue;
                    case 2:
                        foreach (var e in Events)
                        {
                            if (e.CheckIfUpcoming())
                            {
                                Console.WriteLine($"Id: {e.Id}");
                                Console.WriteLine($"Naziv: {e.EventName} - Lokacija: {e.EventLocation} - Starts In: {(e.EventStart - DateTime.Now).TotalDays.ToString()} " +
                                    $"Duration: {Math.Round((e.EventEnd - e.EventStart).TotalHours, 1).ToString()}");
                                Console.WriteLine($"Popis suduonika:");
                                Console.WriteLine(ListOfAttendees(People, e));
                                Console.WriteLine("\n\n");
                            }
                        }

                        Console.WriteLine("1 - Izbriši event");
                        Console.WriteLine("2 - Ukloni osobe s eventa");
                        Console.WriteLine("3 - Povratak na glavni menu");

                        UserInput = UserInputControl(1, 3);

                        switch (UserInput)
                        {
                            case 1:
                                Console.WriteLine("Unesi Id eventa");
                                string ID;
                                bool EventExists = false; ;
                                do
                                {
                                    ID = Console.ReadLine();
                                    EventExists = CheckId(Events, Guid.Parse(ID));
                                    if (!EventExists)
                                    {
                                        Console.WriteLine("Ne postoji Event s unesenim ID-om");
                                    }

                                } while (!EventExists);
                                Guid IDEvent = Guid.Parse(ID);


                                if (ChangeConformation()){
                                    foreach (Eventi e in Events)
                                    {
                                        if (e.Id == IDEvent)
                                        {
                                            List<string> EmailsFromThisEvent = e.ListOfEmailsFromAnEvent();
                                            foreach (Osobe o in People)
                                            {
                                                if (EmailsFromThisEvent.Contains(o.Email)) {
                                                    o.AttendanceRemove(e.Id);
                                                }
                                            }
                                            Events.Remove(e);
                                        }
                                    }
                                }
                                else
                                    Console.WriteLine("Promjene nisu spremljene");


                                continue;

                            case 2:

                                Console.WriteLine("Unesi Id eventa");
                                string ID2;
                                bool EventExists2 = false; ;
                                do
                                {
                                    ID2 = Console.ReadLine();
                                    EventExists2 = CheckId(Events, Guid.Parse(ID2));
                                    if (!EventExists2)
                                    {
                                        Console.WriteLine("Ne postoji Event s unesenim ID-om");
                                    }

                                } while (!EventExists2);
                                Guid IDD = Guid.Parse(ID2);
                                string emails;
                                bool EmailsCheck = false;
                                do
                                {
                                    emails = Console.ReadLine();
                                    foreach (var e in Events)
                                    {
                                        if (e.Id == IDD)
                                        {
                                            List<string> EmailsFromThisEvent = e.ListOfEmailsFromAnEvent();
                                            string[] SeparatedInputEmails = emails.Split(' ');
                                            foreach (string sep in SeparatedInputEmails)
                                            {
                                                if (EmailsFromThisEvent.Contains(sep))
                                                {
                                                    EmailsCheck = true;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Unijeli ste email osobe koja nije bila pozvana na event");
                                                    EmailsCheck = false;
                                                    break;
                                                }
                                            }
                                        }
                                    }

                                } while (!EmailsCheck);

                                if (ChangeConformation())
                                {
                                    foreach (var e in Events)
                                    {
                                        if (e.Id == IDD)
                                        {
                                            List<string> EmailsFromThisEvent = e.ListOfEmailsFromAnEvent();
                                            foreach (Osobe o in People)
                                            {
                                                if (EmailsFromThisEvent.Contains(o.Email))
                                                {
                                                    o.AttendanceRemove(e.Id);
                                                }
                                            }
                                            e.RemoveParticipants(emails);
                                        }
                                    }
                                }
                                else
                                    Console.WriteLine("Promjene nisu spremljene");
                                continue;

                            case 3:
                                continue;
                        }

                        continue;

                    case 3:
                        foreach (var e in Events)
                        {
                            if (e.CheckIfEnded())
                            {
                                Console.WriteLine($"Id: {e.Id}");
                                Console.WriteLine($"Naziv: {e.EventName} - Lokacija: {e.EventLocation} - Ended Before: {(DateTime.Now - e.EventEnd).TotalDays.ToString()} " +
                                    $"Duration: {Math.Round((e.EventEnd - e.EventStart).TotalHours, 1).ToString()}");
                                Console.WriteLine("Popis suduonika:");
                                Console.WriteLine(DidAttend(People, e));
                                Console.WriteLine("Popis ne prisutnih sudionika");
                                Console.WriteLine(DidNotAttend(People, e));
                                Console.WriteLine("\n\n");
                            }
                        }
                        continue;
                    case 4:
                        Console.WriteLine("Unesi naziv eventa");
                        string Name = Console.ReadLine();
                        Console.WriteLine("Unesi lokaciju");
                        string Location = Console.ReadLine();
                        var cultureInfo = new CultureInfo("hr-HR");
                        bool CheckDate = false;
                        DateTime DateStart = new DateTime();
                        DateTime DateEnd = new DateTime();
                        do
                        {
                            Console.WriteLine("Unesi datum i vrijeme početka(DD.MM.YYYY. HH:MM)");
                            try
                            {
                                DateStart = DateTime.ParseExact(Console.ReadLine(), "g", cultureInfo);
                                if (DateStart < DateTime.Now)
                                {
                                    Console.WriteLine("Event ne može početi u prošlosti");
                                    CheckDate = false;
                                }
                                else
                                    CheckDate = true;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Neispravan unos datuma");
                                CheckDate=false;
                            }
                        } while (!CheckDate);
                        do
                        {
                            Console.WriteLine("Unesi datum i vrijeme kraja(DD.MM.YYYY. HH:MM)");
                            try
                            {
                                DateEnd = DateTime.ParseExact(Console.ReadLine(), "g", cultureInfo);
                                if (DateEnd < DateStart)
                                {
                                    Console.WriteLine("Event ne može početi u prošlosti");
                                    CheckDate = false;
                                }
                                else
                                    CheckDate = true;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Neispravan unos datuma");
                                CheckDate = false;
                            }
                        } while (!CheckDate) ;
                        bool EmailCheck = true;
                        string Emails;
                        do
                        {
                            Console.WriteLine("Unesi email osoba koje želiš pozvati na event (emailove odvoji razmakom)");
                            Emails = Console.ReadLine();
                            string[] SeparatedEmails = Emails.Split(' ');
                            foreach (string SeparatedEmail in SeparatedEmails)
                            {
                                if (!ListOfEmails(People).Contains(SeparatedEmail)){
                                    Console.WriteLine("Unijeli ste email od nepostojeće osobe");
                                    EmailCheck = false;
                                    break;
                                }
                            }
                        }while (!EmailCheck);
                        string[] Separated = Emails.Split(' ');
                        StringBuilder FinalInvitations = new StringBuilder();
                        foreach (string sep in Separated)
                        {
                            if (CheckForOverlap(Events, DateStart, DateEnd, sep))
                            {
                                Console.WriteLine($"Nije moguće dodati osobu s adresom: {sep} jer ima drugi event u tom terminu. ");
                            }
                            else
                            {
                                FinalInvitations.Append(sep + " ");
                            }

                        }
                        if (ChangeConformation())
                        {
                            Eventi Event = new Eventi(Name, Location, DateStart, DateEnd, FinalInvitations.ToString());
                            Events.Add(Event);
                        }
                        else
                            Console.WriteLine("Promjene nisu spremljene");

                        continue;
                }
            } while (UserInput != 5);
        }

        public static int UserInputControl(int a, int b)
        {
            int UserInput;
            bool InputCheck;
            do
            {
                InputCheck = int.TryParse(Console.ReadLine(), out UserInput);
                if (!InputCheck)
                {
                    Console.WriteLine("Neispravan unos");
                    UserInput = -1;
                }
                if (UserInput < a || UserInput > b)
                {
                    Console.WriteLine("Unos ne označava nijednu operaciju");
                }

            } while (UserInput < a || UserInput > b);

            return UserInput;
        }

        public static string ListOfAttendees(List<Osobe> People, Eventi AnEvent) {
            StringBuilder Attendees = new StringBuilder();
            String[] strlist = AnEvent.ParticipantEmails.Split(' ');
            foreach (String str in strlist)
            {
                foreach(Osobe osobe in People)
                {
                    if (str == osobe.Email)
                    {

                            Attendees.Append(osobe.Name + " " + osobe.Surname + " " + osobe.Email + ", ");
                    }
                }
            }
            return Attendees.ToString();
        }

        public static List<string> ListOfEmails(List<Osobe> People)
        {
            List<string> lista = new List<string>();
            foreach(var osobe in People)
            {
                lista.Add(osobe.Email);
            }
            return lista;
        }

        public static bool CheckForOverlap(List<Eventi> Events, DateTime Begining, DateTime End, string InvitedPersonEmail)
        {
            foreach(var e in Events)
            {
                if (e.ParticipantEmails.Contains(InvitedPersonEmail))
                {
                    if (e.EventStart < End)
                        return false;
                    if (e.EventEnd > Begining)
                        return false;
                }
            }
            return true;
        }

        public static bool ChangeConformation()
        {
            Console.WriteLine("Potvrdi promjene(D/N)");
            string unos = "";
            do
            {
                unos = Console.ReadLine();
                if (unos != "D" && unos != "d" && unos != "N" && unos != "n")
                {
                    Console.WriteLine("Unos ne oznacava ništa");
                }
            } while (unos != "D" && unos != "d" && unos != "N" && unos != "n");
            if (unos == "D" || unos == "d")
                return true;
            else
                return false;
        }

        public static bool CheckId(List<Eventi> Events, Guid ID)
        {
            List<Guid> ids = new List<Guid>();
            foreach(Eventi e in Events)
            {
                ids.Add(e.Id);
            }

            if (ids.Contains(ID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string DidAttend(List<Osobe> People, Eventi Event)
        {
            StringBuilder sb = new StringBuilder();
            foreach(Osobe o in People)
            {
                if (o.CheckIfAttended(Event.Id))
                {
                    sb.Append(o.Email + " ");
                }
            }

            return sb.ToString();
        }

        public static string DidNotAttend(List<Osobe> People, Eventi Event)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Osobe o in People)
            {
                if (!o.CheckIfAttended(Event.Id))
                {
                    sb.Append(o.Email + " ");
                }
            }

            return sb.ToString();
        }
    }
}
