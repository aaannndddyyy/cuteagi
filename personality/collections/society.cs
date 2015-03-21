/*
    personality modeling framework
    Copyright (C) 2007 Bob Mottram
    fuzzgun@gmail.com

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Text;
using cuteagi.core;

namespace cuteagi.personalitymodel
{
    public class society : node
    {
        #region "locations"

        public ArrayList locations = new ArrayList();

        public int LocationExists(String Name, String Country)
        {
            int index = 0;
            bool found = false;
            while ((index < locations.Count) && (!found))
            {
                location loc = (location)locations[index];
                if ((loc.GetName() == Name) && (loc.Country == Country))
                    found = true;
                else
                    index++;
            }
            if (found)
                return (index);
            else
                return (-1);
        }

        public location AddLocation(String Name, String Country, String Type)
        {
            return (AddLocation(Name, Country, 0, 0, Type));
        }

        public location AddLocation(String Name, String Country,
                                float Latitude, float Longitude,
                                String Type)
        {
            int index = LocationExists(Name, Country);
            location loc = null;
            if (index == -1)
            {
                loc = new location();
                locations.Add(loc);
            }
            else
            {
                loc = (location)locations[index];
            }
            loc.SetName(Name);
            loc.Country = Country;
            loc.SetLatitude(Latitude);
            loc.SetLongitude(Longitude);
            loc.SetLocationType(Type);
            return (loc);
        }

        #endregion

        #region "people"

        public ArrayList personalities = new ArrayList();
        public personality current_personality = null;

        /// <summary>
        /// create a new personality
        /// </summary>
        /// <param name="name"></param>
        public personality CreatePersonality(String name)
        {
            personality p = null;

            if (personalityExists(name) == -1)
            {
                p = new personality();
                p.SetName(name);
                String[] namestr = name.Split(' ');
                if (male_names.Contains(namestr[0].ToLower()))
                    p.isMale = true;
                else
                    p.isMale = false;
                p.SetDateOfBirth(new DateTime(1980, 1, 1));

                personalities.Add(p);
            }
            return (p);
        }


        /// <summary>
        /// does the given name exist
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int personalityExists(String name)
        {
            bool found = false;
            int i = 0;
            while ((i < personalities.Count) && (!found))
            {
                personality p = (personality)personalities[i];
                if (p.GetName().ToLower() == name.ToLower())
                    found = true;
                else
                    i++;
            }
            if (found)
                return (i);
            else
                return (-1);
        }


        #endregion

        #region "names"

        protected ArrayList male_names = new ArrayList();

        /// <summary>
        /// load a list of male names
        /// </summary>
        /// <param name="filename"></param>
        private void LoadMaleNames(String filename)
        {
            bool filefound = true;
            String str;
            StreamReader oRead = null;

            male_names.Clear();
            try
            {
                oRead = File.OpenText(filename);
            }
            catch
            {
                filefound = false;
            }

            if (filefound)
            {
                do
                {
                    str = null;
                    try
                    {
                        str = oRead.ReadLine();
                        male_names.Add(str.ToLower());
                    }
                    catch
                    {
                    }
                }
                while (str != null);
                oRead.Close();
            }
        }

        #endregion

        #region "professions"

        public ArrayList professions = new ArrayList();

        private void addProfession(String name, int social_class,
                           int manual, int clerical, int leadership, int social_status)
        {
            addProfession(name, social_class, manual, clerical, leadership, social_status, 0, 0, 0, 0);
        }

        private void addProfession(String name, int social_class,
                                   int manual, int clerical, int leadership, int social_status,
                                   int religeous, int enforcement)
        {
            addProfession(name, social_class, manual, clerical, leadership, social_status, religeous, enforcement, 0, 0);
        }
        private void addProfession(String name, int social_class,
                                   int manual, int clerical, int leadership, int social_status,
                                   int religeous, int enforcement, int entertainment)
        {
            addProfession(name, social_class, manual, clerical, leadership, social_status, religeous, enforcement, entertainment, 0);
        }

        private void addProfession(String name, int social_class,
                                   int manual, int clerical, int leadership, int social_status,
                                   int religious, int enforcement, int entertainment, int teaching)
        {
            profession p = new profession();
            p.Name = name;
            p.Manual.SetValue(manual);
            p.Clerical.SetValue(clerical);
            p.Leadership.SetValue(leadership);
            p.SocialStatus.SetValue(social_status);
            p.Religious.SetValue(religious);
            p.Enforcement.SetValue(enforcement);
            p.Entertainment.SetValue(entertainment);
            p.Teaching.SetValue(teaching);
            p.SocialClass = social_class;
            professions.Add(p);
        }

        public String getProffessionsList()
        {
            String professions_list = "";
            for (int i = 0; i < professions.Count; i++)
            {
                professions_list += ((profession)professions[i]).Name;
                if (i < professions.Count - 1) professions_list += ",";
            }
            return (professions_list);
        }

        private void createProfessions()
        {
            addProfession("Abbess", profession.CLASS_UPPER, 0, 80, 80, 80, 100, 0);
            addProfession("Abbot", profession.CLASS_UPPER, 0, 80, 80, 80, 100, 0);
            addProfession("Accountant", profession.CLASS_UPPER_MIDDLE, 0, 100, 10, 50);
            addProfession("Acoustical Scientist", profession.CLASS_UPPER_MIDDLE, 40, 100, 10, 20);
            addProfession("Actor", profession.CLASS_UPPER_MIDDLE, 80, 0, 0, 80, 0, 0, 100);
            addProfession("Actress", profession.CLASS_UPPER_MIDDLE, 80, 0, 0, 80, 0, 0, 100);
            addProfession("Actuary", profession.CLASS_MIDDLE, 0, 0, 0, 0);
            addProfession("Administrator", profession.CLASS_UPPER_MIDDLE, 0, 100, 50, 50);
            addProfession("Advocate", profession.CLASS_UPPER_MIDDLE, 0, 100, 50, 50);
            addProfession("Aerospace engineer", profession.CLASS_HIGH_PROLE, 80, 20, 10, 40);
            addProfession("Agent", profession.CLASS_MIDDLE, 0, 100, 20, 20);
            addProfession("Agrarian", profession.CLASS_LOW_PROLE, 100, 0, 0, 10);
            addProfession("Agrologist", profession.CLASS_UPPER_MIDDLE, 50, 50, 10, 30, 0, 0, 50);
            addProfession("Agronomist", profession.CLASS_UPPER_MIDDLE, 50, 50, 10, 30);
            addProfession("Air traffic controller", profession.CLASS_MIDDLE, 20, 100, 10, 30);
            addProfession("Airman", profession.CLASS_HIGH_PROLE, 80, 80, 10, 60);
            addProfession("Alchemist", profession.CLASS_UPPER_MIDDLE, 20, 100, 0, 10, 10, 0);
            addProfession("Ambassador", profession.CLASS_UPPER, 0, 100, 100, 90);
            addProfession("Anesthesiologist", profession.CLASS_MIDDLE, 50, 50, 10, 60);
            addProfession("Analyst", profession.CLASS_MIDDLE, 0, 100, 10, 30);
            addProfession("Animal trainer", profession.CLASS_MIDDLE, 100, 0, 0, 10);
            addProfession("Anthropologist", profession.CLASS_MIDDLE, 50, 50, 20, 20);
            addProfession("Antique dealer", profession.CLASS_MIDDLE, 20, 40, 0, 10);
            addProfession("Arbiter", profession.CLASS_MIDDLE, 0, 100, 60, 60);
            addProfession("Arbitrator", profession.CLASS_MIDDLE, 0, 100, 60, 60);
            addProfession("Archbishop", profession.CLASS_UPPER, 0, 100, 100, 95);
            addProfession("Archer", profession.CLASS_LOW_PROLE, 100, 0, 0, 10);
            addProfession("Archaeologist", profession.CLASS_MIDDLE, 70, 40, 10, 50);
            addProfession("Architect", profession.CLASS_UPPER_MIDDLE, 0, 100, 50, 80);
            addProfession("Archivist", profession.CLASS_MIDDLE, 0, 100, 10, 10);
            addProfession("Armourer", profession.CLASS_MID_PROLE, 80, 10, 10, 30);
            addProfession("Art director", profession.CLASS_UPPER_MIDDLE, 10, 100, 80, 50, 0, 0, 80);
            addProfession("Art therapist", profession.CLASS_MIDDLE, 50, 10, 10, 10, 0, 0, 10);
            addProfession("Arts Manager", profession.CLASS_MIDDLE, 0, 100, 80, 30, 0, 0, 80);
            addProfession("Artist", profession.CLASS_MIDDLE, 80, 0, 10, 50, 0, 0, 100);
            addProfession("Assassin", profession.CLASS_BOTTOM, 100, 0, 0, 5);
            addProfession("Assessor", profession.CLASS_MIDDLE, 50, 50, 10, 20);
            addProfession("Astrologer", profession.CLASS_MIDDLE, 50, 5, 5, 10);
            addProfession("Astronaut", profession.CLASS_HIGH_PROLE, 100, 0, 10, 95);
            addProfession("Astronomer", profession.CLASS_MIDDLE, 50, 50, 0, 20);
            addProfession("Astrophysicist", profession.CLASS_MIDDLE, 20, 80, 10, 40);
            addProfession("Athlete", profession.CLASS_UPPER, 100, 0, 0, 80);
            addProfession("Attorney", profession.CLASS_MIDDLE, 0, 100, 80, 80);
            addProfession("Auditor", profession.CLASS_MIDDLE, 0, 100, 10, 70);
            addProfession("Au pair", profession.CLASS_MID_PROLE, 100, 0, 20, 20);
            addProfession("Author", profession.CLASS_MIDDLE, 0, 100, 0, 70);
            addProfession("Aviator", profession.CLASS_HIGH_PROLE, 100, 5, 0, 50);
            addProfession("Bacteriologist", profession.CLASS_MIDDLE, 50, 50, 10, 60);
            addProfession("Bailiff", profession.CLASS_HIGH_PROLE, 80, 20, 10, 5);
            addProfession("Baker", profession.CLASS_LOW_PROLE, 90, 0, 5, 20);
            addProfession("Balloonist", profession.CLASS_MID_PROLE, 80, 0, 0, 5);
            addProfession("Bandit", profession.CLASS_BOTTOM, 80, 0, 0, 1);
            addProfession("Banker", profession.CLASS_MIDDLE, 0, 100, 70, 70);
            addProfession("Barber", profession.CLASS_HIGH_PROLE, 100, 0, 0, 30);
            addProfession("Bard", profession.CLASS_HIGH_PROLE, 100, 0, 10, 30);
            addProfession("Bargeman", profession.CLASS_LOW_PROLE, 100, 0, 0, 10);
            addProfession("Barista", profession.CLASS_UPPER_MIDDLE, 0, 100, 70, 80);
            addProfession("Bartender", profession.CLASS_LOW_PROLE, 100, 0, 40, 10);
            addProfession("Barkeeper", profession.CLASS_LOW_PROLE, 100, 0, 40, 10);
            addProfession("Barman", profession.CLASS_LOW_PROLE, 100, 0, 40, 10);
            addProfession("Barmaid", profession.CLASS_LOW_PROLE, 100, 0, 40, 10);
            addProfession("Beautician", profession.CLASS_HIGH_PROLE, 100, 0, 10, 50);
            addProfession("Beekeeper", profession.CLASS_MID_PROLE, 100, 0, 0, 30);
            addProfession("Beggar", profession.CLASS_DESTITUTE, 100, 0, 0, 0);
            addProfession("Biologist", profession.CLASS_MIDDLE, 50, 80, 20, 60);
            addProfession("Biomedical Scientist", profession.CLASS_MIDDLE, 50, 80, 20, 60);
            addProfession("Healthcare Scientist", profession.CLASS_MIDDLE, 50, 80, 20, 60);
            addProfession("Bishop", profession.CLASS_UPPER_MIDDLE, 0, 100, 94, 90, 100, 0);
            addProfession("Blacksmith", profession.CLASS_HIGH_PROLE, 100, 0, 10, 15);
            addProfession("Boatbuilder", profession.CLASS_MID_PROLE, 100, 0, 0, 15);
            addProfession("Boatman", profession.CLASS_LOW_PROLE, 100, 0, 0, 15);
            addProfession("Boatswain", profession.CLASS_LOW_PROLE, 100, 0, 0, 15);
            addProfession("Boatwright", profession.CLASS_LOW_PROLE, 100, 0, 0, 15);
            addProfession("Bodyguard", profession.CLASS_MID_PROLE, 100, 0, 60, 30);
            addProfession("Bondbroker", profession.CLASS_MIDDLE, 0, 100, 30, 60);
            addProfession("Bookbinder", profession.CLASS_HIGH_PROLE, 50, 50, 10, 30);
            addProfession("Bookkeeper", profession.CLASS_MIDDLE, 0, 100, 40, 70);
            addProfession("Bookseller", profession.CLASS_MIDDLE, 20, 100, 10, 20);
            addProfession("Boxer", profession.CLASS_UPPER, 20, 100, 10, 20);
            addProfession("Brewer", profession.CLASS_MID_PROLE, 60, 10, 10, 70);
            addProfession("Bricker", profession.CLASS_LOW_PROLE, 100, 0, 0, 15);
            addProfession("Bricklayer", profession.CLASS_LOW_PROLE, 100, 0, 0, 15);
            addProfession("Broker", profession.CLASS_MIDDLE, 0, 100, 50, 40);
            addProfession("Builder", profession.CLASS_LOW_PROLE, 100, 0, 5, 10);
            addProfession("Bureaucrat", profession.CLASS_MIDDLE, 0, 100, 70, 60);
            addProfession("Burglar", profession.CLASS_BOTTOM, 100, 0, 0, 0);
            addProfession("Business analyst", profession.CLASS_MIDDLE, 0, 100, 50, 50);
            addProfession("Business owner", profession.CLASS_UPPER_MIDDLE, 20, 80, 80, 60);
            addProfession("Busker", profession.CLASS_DESTITUTE, 100, 0, 0, 5);
            addProfession("Butcher", profession.CLASS_MID_PROLE, 100, 0, 10, 15);
            addProfession("Butler", profession.CLASS_LOW_PROLE, 100, 20, 0, 10);
            addProfession("Cab driver", profession.CLASS_LOW_PROLE, 100, 0, 0, 15);
            addProfession("Cabinet-maker", profession.CLASS_MID_PROLE, 100, 0, 0, 15);
            addProfession("Calligrapher", profession.CLASS_HIGH_PROLE, 50, 50, 10, 20);
            addProfession("Cameraman", profession.CLASS_HIGH_PROLE, 70, 5, 0, 40);
            addProfession("Captain", profession.CLASS_UPPER_MIDDLE, 80, 10, 80, 70);
            addProfession("Cardinal", profession.CLASS_UPPER, 0, 100, 90, 90);
            addProfession("Cardiologist", profession.CLASS_UPPER_MIDDLE, 100, 20, 20, 80);
            addProfession("Carpenter", profession.CLASS_MID_PROLE, 100, 0, 0, 15);
            addProfession("Cartographer", profession.CLASS_MIDDLE, 30, 100, 0, 60);
            addProfession("Cartoonist", profession.CLASS_HIGH_PROLE, 50, 50, 0, 20);
            addProfession("Censor", profession.CLASS_MIDDLE, 0, 100, 60, 60);
            addProfession("Chamberlain", profession.CLASS_UPPER, 0, 100, 80, 80);
            addProfession("Chancellor", profession.CLASS_UPPER, 0, 100, 100, 95);
            addProfession("Chaplain", profession.CLASS_MIDDLE, 30, 80, 60, 65);
            addProfession("Cheesemaker", profession.CLASS_MID_PROLE, 100, 0, 0, 15);
            addProfession("Chemical engineer", profession.CLASS_HIGH_PROLE, 50, 50, 10, 60);
            addProfession("Chemist", profession.CLASS_HIGH_PROLE, 50, 50, 10, 60);
            addProfession("Chief of Police", profession.CLASS_UPPER_MIDDLE, 20, 100, 90, 90, 0, 100);
            addProfession("Chiropodist", profession.CLASS_HIGH_PROLE, 100, 0, 10, 20);
            addProfession("Chiropractor", profession.CLASS_MIDDLE, 50, 20, 20, 70);
            addProfession("Choreographer", profession.CLASS_MIDDLE, 80, 10, 70, 70, 0, 0, 80);
            addProfession("Civil servant", profession.CLASS_MIDDLE, 5, 100, 10, 60);
            addProfession("Civil engineer", profession.CLASS_HIGH_PROLE, 80, 20, 20, 60);
            addProfession("Clarinettist", profession.CLASS_MIDDLE, 100, 0, 0, 40, 0, 0, 50);
            addProfession("Cleaning staff", profession.CLASS_LOW_PROLE, 100, 0, 0, 5);
            addProfession("Clergyman", profession.CLASS_UPPER, 5, 100, 50, 50, 100, 0);
            addProfession("Clerk", profession.CLASS_MIDDLE, 0, 100, 10, 10);
            addProfession("Clockmaker", profession.CLASS_HIGH_PROLE, 80, 10, 10, 20);
            addProfession("Coach", profession.CLASS_MID_PROLE, 100, 5, 70, 70);
            addProfession("Coast guard", profession.CLASS_MID_PROLE, 100, 0, 20, 40);
            addProfession("Comedian", profession.CLASS_HIGH_PROLE, 100, 30, 0, 50, 0, 0, 100);
            addProfession("Composer", profession.CLASS_MIDDLE, 0, 100, 10, 80, 0, 0, 100);
            addProfession("Computer engineer", profession.CLASS_HIGH_PROLE, 0, 100, 10, 20);
            addProfession("Computer programmer", profession.CLASS_MIDDLE, 0, 100, 10, 20);
            addProfession("Conductor", profession.CLASS_HIGH_PROLE, 60, 10, 20, 10);
            addProfession("Constable", profession.CLASS_HIGH_PROLE, 50, 20, 70, 70, 0, 100);
            addProfession("Construction worker", profession.CLASS_LOW_PROLE, 100, 0, 5, 15);
            addProfession("Consultant", profession.CLASS_MIDDLE, 0, 100, 50, 70);
            addProfession("Cook", profession.CLASS_LOW_PROLE, 100, 0, 10, 50);
            addProfession("Coroner", profession.CLASS_MIDDLE, 40, 50, 10, 70);
            addProfession("executive officer", profession.CLASS_MIDDLE, 0, 100, 60, 50);
            addProfession("librarian", profession.CLASS_MIDDLE, 0, 100, 0, 20);
            addProfession("Correspondent", profession.CLASS_MIDDLE, 50, 70, 20, 50);
            addProfession("Cosmetologist", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Cosmonaut", profession.CLASS_HIGH_PROLE, 100, 0, 10, 90);
            addProfession("Courier", profession.CLASS_LOW_PROLE, 100, 20, 10, 15);
            addProfession("Cryptanalyst", profession.CLASS_MIDDLE, 0, 100, 0, 80);
            addProfession("Cryptographer", profession.CLASS_MIDDLE, 0, 100, 0, 80);
            addProfession("Cryptologist", profession.CLASS_MIDDLE, 0, 100, 0, 80);
            addProfession("Curator", profession.CLASS_MIDDLE, 20, 50, 50, 20);
            addProfession("Custodian", profession.CLASS_MIDDLE, 30, 50, 20, 20);
            addProfession("Customs officer", profession.CLASS_HIGH_PROLE, 70, 30, 60, 60);
            addProfession("Defence Secretary", profession.CLASS_UPPER, 100, 0, 0, 50);
            addProfession("Dancer", profession.CLASS_MID_PROLE, 100, 0, 0, 50);
            addProfession("Dentist", profession.CLASS_UPPER_MIDDLE, 100, 10, 80, 80);
            addProfession("Deputy", profession.CLASS_MIDDLE, 20, 80, 70, 70);
            addProfession("Deputy Prime Minister", profession.CLASS_UPPER, 20, 80, 70, 70);
            addProfession("Designer", profession.CLASS_MIDDLE, 0, 100, 60, 60);
            addProfession("Detective", profession.CLASS_HIGH_PROLE, 60, 50, 30, 50);
            addProfession("Dietician", profession.CLASS_MIDDLE, 100, 20, 20, 30);
            addProfession("Diplomat", profession.CLASS_UPPER_MIDDLE, 0, 100, 70, 80);
            addProfession("Director", profession.CLASS_UPPER_MIDDLE, 10, 100, 85, 90);
            addProfession("Disc jockey", profession.CLASS_HIGH_PROLE, 100, 0, 0, 15, 0, 0, 100);
            addProfession("DJ", profession.CLASS_HIGH_PROLE, 100, 0, 0, 15, 0, 0, 100);
            addProfession("Diver", profession.CLASS_MID_PROLE, 100, 0, 0, 40);
            addProfession("Docker", profession.CLASS_LOW_PROLE, 100, 0, 0, 10);
            addProfession("Doctor", profession.CLASS_UPPER_MIDDLE, 80, 40, 90, 90);
            addProfession("Doorman", profession.CLASS_LOW_PROLE, 80, 0, 50, 20);
            addProfession("Dramatist", profession.CLASS_HIGH_PROLE, 20, 100, 70, 70, 0, 0, 100);
            addProfession("Draper", profession.CLASS_MID_PROLE, 100, 0, 0, 10);
            addProfession("Dressmaker", profession.CLASS_HIGH_PROLE, 100, 0, 10, 10);
            addProfession("Drill instructor", profession.CLASS_HIGH_PROLE, 100, 0, 70, 50);
            addProfession("Drummer", profession.CLASS_LOW_PROLE, 100, 0, 0, 20, 0, 0, 100);
            addProfession("Dustman", profession.CLASS_LOW_PROLE, 100, 0, 0, 5);

            addProfession("Economist", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Editor", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Education Secretary", profession.CLASS_UPPER, 100, 0, 0, 50);
            addProfession("Educationalist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Electrical engineer", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Electrician", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Embalmer", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Embroiderer", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Embryologist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Emergency Manager", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Engine-driver", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Engine fitter", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Engineer", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Engraver", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Entertainer", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Entomologist", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Entrepreneur", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Environmental scientist", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Ergonomists", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Estate Agent", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Ethnologist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Ethologist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Etymologist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Evangelist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Examiner", profession.CLASS_MIDDLE, 80, 10, 10, 40, 0, 0, 0, 10);
            addProfession("Exchequer", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Executioner", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Executive", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Executor", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Exotic dancer", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Expert", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Explorer", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Executioner", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Exterminator", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Fabricshearer", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Factory worker", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Falconer", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Farmer", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Farrier", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Fashion designer", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("FBI Agent", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Fellmonger", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Feltmaker", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Ferryman", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Film director", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40, 0, 0, 100);
            addProfession("Film producer", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40, 0, 0, 100);
            addProfession("Financial Advisor", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Financial Analyst", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Financial Planner", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Financier", profession.CLASS_UPPER, 80, 10, 10, 40);
            addProfession("Fire officer", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Firefighter", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Fishmonger", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Fisherman", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Fitter", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Flautist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Flavorist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            //addProfession("Fletcher", profession.CLASS_MID_PROLE,80, 10, 10, 40);
            addProfession("Flight attendant", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Flight engineer", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Flight technician", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Flight instructor", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Floor manager", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Florist", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Flutist", profession.CLASS_MIDDLE, 80, 10, 10, 40, 0, 0, 100);
            addProfession("Footballer", profession.CLASS_UPPER, 80, 10, 10, 40);
            addProfession("Footman", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Forester", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Foreign Secretary", profession.CLASS_UPPER, 100, 0, 0, 50);
            addProfession("Forensic Scientist", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Fortune-teller", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Fraudster", profession.CLASS_BOTTOM, 80, 10, 10, 40);
            addProfession("Friar", profession.CLASS_MIDDLE, 80, 10, 10, 40, 100, 0);
            addProfession("Fruiterer", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Furbisher", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Game show host", profession.CLASS_UPPER, 80, 10, 10, 40, 0, 0, 100);
            addProfession("Garbage collector", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Garbler", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Gardener", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Gate-keeper", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Geisha", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Gemcutter", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Genealogist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            //addProfession("General", profession.CLASS_UPPER,80, 10, 10, 40);
            addProfession("General manager", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Geographer", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Geologist", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Geometer", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Geophysicist", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Gigolo", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Gladiator", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Glazier", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Goatherd", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Goldsmith", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Gondolier", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Governess", profession.CLASS_UPPER, 80, 10, 10, 40);
            addProfession("Government agent", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Governor", profession.CLASS_UPPER, 80, 10, 10, 40);
            addProfession("Grammarian", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Graphic artist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Gravedigger", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Grenadier", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Greengrocer", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Grinder", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Grocer", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Groom", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("manservant", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Guard", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Guitarist", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Gunsmith", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Gunstocker", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Guru", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40, 100, 0, 0, 100);
            addProfession("Gynecologist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Haberdasher", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Hairdesser", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Harnessmaker", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Hatter", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Headmaster", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Headmistress", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Heaumer", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Herbalist", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Herder", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Historian", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Home Secretary", profession.CLASS_UPPER, 100, 0, 0, 50);
            addProfession("Hornere", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Hosier", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Host", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Hostess", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Hotelier", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("House painter", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Housewife", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Hunter", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Hydraulic engineer", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Hypnotist", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Illuminator", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Illusionist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Illustrator", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Imam", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40, 100, 0, 0, 0);
            addProfession("Impersonator", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40, 0, 0, 100);
            addProfession("Importer", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Industrial designer", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Industrial engineer", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Industrialist", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Information Technologist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Inker", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Innkeeper", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Instrumentmaker", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Instructor", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Insurer", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Intelligence officer", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Interior designer", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Internist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Interpreter", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Interrogator", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Inventor", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Investment Analysts", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Investment Banker", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Investment Broker", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Invigilator", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Ironmonger", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Ironmaster", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Jailer", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Jeweler", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Jockey", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Joiner", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Journalist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Judge", profession.CLASS_UPPER, 80, 10, 10, 40);
            addProfession("Juggler", profession.CLASS_MID_PROLE, 80, 10, 10, 40, 0, 0, 100);
            addProfession("Jurist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("King", profession.CLASS_TOP, 80, 10, 10, 40);
            addProfession("Knifegrinder", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Knifesmith", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Knight", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Laboratory worker", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Ladeler", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Lady-in-waiting", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Landlord", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Landlady", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Lanternmaker", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Lauderer", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Law enforcement agent", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Lawyer", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Latener", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Leadworker", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Leader", profession.CLASS_UPPER, 80, 10, 10, 40);
            addProfession("Leatherer", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Lecturer", profession.CLASS_MIDDLE, 80, 10, 10, 40, 0, 0, 0, 100);
            addProfession("Lens grinder", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Librarian", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Librettist", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Lifeguard", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Lighthouse-keeper", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Light technician", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Linesman", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Linguist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Linkman", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Loan officer", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Locksmith", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Loriner", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Lord Chamberlain", profession.CLASS_UPPER, 80, 10, 10, 40);
            addProfession("Lorist", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Lumberjack", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Lyricist", profession.CLASS_MIDDLE, 80, 10, 10, 40, 0, 0, 100);
            addProfession("Macer", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Magician", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40, 0, 0, 100);
            addProfession("Magistrate", profession.CLASS_UPPER, 80, 10, 10, 40);
            addProfession("Mailman", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Makeup artist", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Management Analyst", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Management Consultant", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Manager", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Manicurist", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Manservant", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Manual therapist", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Manufacturer", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Marbler", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Marine", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Marketer", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Market gardener", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Marksman", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Marshal", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40, 0, 100);
            addProfession("Martial artist", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Mason", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Masseur", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Masseuse", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Master of hounds", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Matador", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Materials engineer", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Mathematician", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Matron", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Mayor", profession.CLASS_UPPER, 80, 10, 10, 40);
            addProfession("Mechanic", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Mechanical engineer", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Mechanician", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Mediator", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Medic", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Member of parlianemt", profession.CLASS_UPPER, 80, 10, 10, 40);
            addProfession("MP", profession.CLASS_UPPER, 80, 10, 10, 40);
            addProfession("Mercenary", profession.CLASS_BOTTOM, 80, 10, 10, 40);
            addProfession("Mercer", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Merchant", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Mesmerist", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Messenger", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Meteorologist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Microbiologist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Midwife", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Milkman", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Milkmaid", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Miller", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Miner", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Mintmaster", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Minister", profession.CLASS_UPPER, 80, 10, 10, 40);
            addProfession("Missionary", profession.CLASS_MID_PROLE, 80, 10, 10, 40, 100, 0);
            //addProfession("Model", profession.CLASS_UPPER,80, 10, 10, 40);
            addProfession("Molecatcher", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Moneychanger", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Moneylender", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Monk", profession.CLASS_MIDDLE, 80, 10, 10, 40, 100, 0);
            addProfession("Mortagager", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Mortician", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Music director", profession.CLASS_MIDDLE, 80, 10, 10, 40, 0, 0, 100);
            addProfession("Musician", profession.CLASS_MIDDLE, 80, 10, 10, 40, 0, 0, 100);
            addProfession("Maid", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Nanny", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Navigator", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Negotiator", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Ninja", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Numerologist", profession.CLASS_MIDDLE, 80, 10, 10, 40, 0, 0, 10);
            addProfession("Nun", profession.CLASS_MIDDLE, 80, 10, 10, 40, 100, 0);
            addProfession("Nurse", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Nursemaid", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Oboist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Obstetrician", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Occupational therapist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Odontologist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Oilpresser", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Operator", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Ophthalmologist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Optician", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Optometrist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Organist", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40, 0, 0, 100);
            addProfession("Organizer", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Ornithologist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Orthodontist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Orthopaedist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Otorhinolaryngologist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Painter", profession.CLASS_MIDDLE, 80, 10, 10, 40, 0, 0, 100);
            addProfession("Paleoseismologist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Paramedic", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Parchmenter", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Pargeter", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Park ranger", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Party leader", profession.CLASS_UPPER, 80, 10, 10, 40);
            addProfession("Pasteler", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Pastor", profession.CLASS_MIDDLE, 80, 10, 10, 40, 100, 0);
            addProfession("Patent clerk", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Pathologist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Pawnbroker", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Peddlar", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Pediatrician", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Percussionist", profession.CLASS_LOW_PROLE, 80, 10, 10, 40, 0, 0, 100);
            addProfession("Pewterer", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Pharmacist", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Philanthropist", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Philologist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Philosopher", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Photographer", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Physician", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Physicist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Physiognomist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Physiologist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Physiotherapist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Pianist", profession.CLASS_MID_PROLE, 80, 10, 10, 40, 0, 0, 100);
            addProfession("Piano tuner", profession.CLASS_MID_PROLE, 80, 10, 10, 40, 0, 0, 100);
            addProfession("Pickler", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Pickpocket", profession.CLASS_BOTTOM, 80, 10, 10, 40);
            addProfession("Pilot", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Pirate", profession.CLASS_BOTTOM, 80, 10, 10, 40);
            addProfession("Plasterer", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Playwright", profession.CLASS_MIDDLE, 80, 10, 10, 40, 0, 0, 100);
            addProfession("Plumber", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Poet", profession.CLASS_LOW_PROLE, 80, 10, 10, 40, 0, 0, 100);
            addProfession("Police", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Police officer", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Police inspector", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Politician", profession.CLASS_UPPER, 80, 10, 10, 40);
            addProfession("Political scientist", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Poll-taker", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Pope", profession.CLASS_UPPER, 80, 10, 10, 40, 100, 0);
            addProfession("Porcilinist", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Poter", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Poulterer", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Presenter", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("President", profession.CLASS_TOP, 80, 10, 10, 40);
            addProfession("Press officer", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Priest", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40, 100, 0);
            addProfession("Prime minister", profession.CLASS_UPPER, 80, 10, 10, 40);
            addProfession("Prince", profession.CLASS_TOP, 80, 10, 10, 40, 0, 0, 0, 100);
            addProfession("Princess", profession.CLASS_TOP, 80, 10, 10, 40, 0, 0, 0, 100);
            addProfession("Principal", profession.CLASS_MIDDLE, 80, 10, 10, 40, 0, 0, 0, 100);
            addProfession("Private detective", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Proctologist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Procurator", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Proctor", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Professor", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40, 0, 0, 0, 100);
            addProfession("Professional athlete", profession.CLASS_UPPER, 80, 10, 10, 40);
            addProfession("Programmer", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Prostitute", profession.CLASS_BOTTOM, 80, 10, 10, 40);
            addProfession("Psychologist", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Public relations officer", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Publisher", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Quartermaster", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Queen", profession.CLASS_UPPER, 80, 10, 10, 40);
            addProfession("Rabbi", profession.CLASS_MIDDLE, 80, 10, 10, 40, 100, 0, 0, 50);
            addProfession("Race driver", profession.CLASS_UPPER, 80, 10, 10, 40);
            addProfession("Radiologist", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Radiographer", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Real estate agent", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Real estate broker", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Real estate investor", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Real estate developer", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Receptionist", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Record Producer", profession.CLASS_MIDDLE, 80, 10, 10, 40, 0, 0, 100);
            addProfession("Recording engineer", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40, 0, 0, 50);
            addProfession("Rector", profession.CLASS_MIDDLE, 80, 10, 10, 40, 100, 0);
            addProfession("Referee", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Refuse collector", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Registrar", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Remedial Teacher", profession.CLASS_MIDDLE, 80, 10, 10, 40, 0, 0, 0, 100);
            addProfession("Reporter", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Researcher", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Rivener", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Roofer", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Sailmaker", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Sailor", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Salesperson", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Salesman", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Saleswoman", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Saxophonist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Scabbardmaker", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Scale mechanic", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Scientist", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Scout", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Screenwriter", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Scribe", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Scrivener", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Sculptor", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Scythesmith", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Seamstress", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Secretary general", profession.CLASS_UPPER, 80, 10, 10, 40);
            addProfession("Secretary", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Security guard", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Senator", profession.CLASS_UPPER, 80, 10, 10, 40);
            addProfession("Servant", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Sexologist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Sheepshearer", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Shepherd", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Shaman", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Sheriff", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Shingler", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Shipwright", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Shoemaker", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Shop assistant", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Shrimper", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Signalman", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Silversmith", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Singer", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Slave", profession.CLASS_BOTTOM, 80, 10, 10, 40);
            addProfession("Slaver", profession.CLASS_BOTTOM, 80, 10, 10, 40);
            addProfession("Smelter", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Smith", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Sniper", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Social worker", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Software engineer", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Soldier", profession.CLASS_LOW_PROLE, 80, 10, 10, 40, 0, 50);
            addProfession("Solicitor", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Sound technician", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Special agent", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Speech therapist", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Spin doctor", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Spy", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Stage designer", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Steersman", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Steward", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Stewardess", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Stock-breeder", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Stockbroker", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Stonecutter", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Street artist", profession.CLASS_LOW_PROLE, 80, 10, 10, 40, 0, 0, 100);
            addProfession("Street musician", profession.CLASS_LOW_PROLE, 80, 10, 10, 40, 0, 0, 100);
            addProfession("Street vendor", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Stringer", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Stuntman", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40, 0, 0, 100);
            addProfession("Surgeon", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Surveyor", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Student", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Swineherd", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Swimmer", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Swimming Coach", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Switchboard operator", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Swordsmith", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("System administrator", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Systems designer", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Systems analyst", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Taikonaut", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Tailor", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Tallowchandler", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Tanner", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Tattooer", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Taxidermist", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Taxi-driver", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Tea lady", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Teacher", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40, 0, 0, 0, 100);
            addProfession("Technical engineer", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Technician", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Telegraphist", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Telephone operator", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Tentsman", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Tennis player", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Test developer", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Test pilot", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Theatre director", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Theologian", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40, 100, 0);
            addProfession("Therapist", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Thief", profession.CLASS_BOTTOM, 80, 10, 10, 40);
            addProfession("Thimbler", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Tiler", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Tinsmith", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Torturer", profession.CLASS_BOTTOM, 80, 10, 10, 40);
            addProfession("Trader", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Tradesman", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Transit planner", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Translator", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Treasurer", profession.CLASS_UPPER, 80, 10, 10, 40);
            addProfession("Troubador", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Tutor", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Urban planner", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Undertaker", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Unemployed", profession.CLASS_DESTITUTE, 80, 10, 10, 40);
            addProfession("Upholsterer", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Usher", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Valet", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Ventriloquist", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40, 0, 0, 100);
            addProfession("Verger", profession.CLASS_MIDDLE, 80, 10, 10, 40, 100, 0);
            addProfession("Veterinarian", profession.CLASS_UPPER_MIDDLE, 80, 10, 10, 40);
            addProfession("Vicar", profession.CLASS_MIDDLE, 80, 10, 10, 40, 100, 0);
            addProfession("Violinist", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Waiter", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Waitress", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Watchman", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Weaponsmith", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Weaver", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Webmaster", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Welder", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Window-dresser", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Wine connoisseur", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Wiredrawer", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Wiremonger", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Woodcarver", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Wood-cutter", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Woodturner", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Wooler", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Winemaker", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Wireless operator", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Writer", profession.CLASS_MIDDLE, 80, 10, 10, 40);
            addProfession("Xylophonist", profession.CLASS_HIGH_PROLE, 80, 10, 10, 40);
            addProfession("Yodeler", profession.CLASS_LOW_PROLE, 80, 10, 10, 40);
            addProfession("Zookeeper", profession.CLASS_MID_PROLE, 80, 10, 10, 40);
            addProfession("Zoologist", profession.CLASS_HIGH_PROLE, 80, 20, 10, 40);
        }

        #endregion

        #region "events"

        public ArrayList events = new ArrayList();

        /// <summary>
        /// remove an event
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Date"></param>
        public void RemoveEvent(String Type, DateTime Date)
        {
            int index = eventExists(Type, Date);
            if (index > -1)
                events.RemoveAt(index);
        }

        /// <summary>
        /// create a new event
        /// </summary>
        public societyevent CreateEvent(String Type, DateTime Date, String Description,
                                        String LocationName, String LocationCountry,
                                        String people)
        {
            return (CreateEvent(Type, Date, Description, LocationName, LocationCountry, "", "", people));
        }


        /// <summary>
        /// create a new event
        /// </summary>
        public societyevent CreateEvent(String Type, DateTime Date, String Description,
                                        String LocationName, String LocationCountry,
                                        String LocationName2, String LocationCountry2, 
                                        String people)
        {
            societyevent e = null;

            int index = eventExists(Type, Date);
            if (index == -1)
            {
                e = new societyevent();
                events.Add(e);
            }
            else e = (societyevent)events[index];

            index = LocationExists(LocationName, LocationCountry);
            if (index == -1)
            {
                e.locations.Add(AddLocation(LocationName, LocationCountry, ""));
            }
            else
            {
                e.locations.Add((location)locations[index]);
            }

            if ((LocationName2 != "") || (LocationCountry2 != ""))
            {
                index = LocationExists(LocationName2, LocationCountry2);
                if (index == -1)
                {
                    e.locations.Add(AddLocation(LocationName2, LocationCountry2, ""));
                }
                else
                {
                    e.locations.Add((location)locations[index]);
                }
            }

            if (people != "")
            {
                String[] person = people.Split(',');
                for (int i = 0; i < person.Length; i++)
                {
                    person[i] = person[i].Trim();
                    index = personalityExists(person[i]);
                    if (index > -1)
                        if (!e.people.Contains((personality)personalities[index]))
                            e.people.Add((personality)personalities[index]);
                }
            }

            e.Type = Type;
            e.SetStartTime(Date);
            e.Description = Description;
            return (e);
        }


        /// <summary>
        /// does the given event exist
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int eventExists(String Type, DateTime Date)
        {
            bool found = false;
            int i = 0;
            while ((i < events.Count) && (!found))
            {
                societyevent e = (societyevent)events[i];
                DateTime startTime = e.GetStartTime();
                if ((e.Type == Type) && (Date.Year == startTime.Year) && (Date.Month == startTime.Month) && (Date.Day == startTime.Day))
                    found = true;
                else
                    i++;
            }
            if (found)
                return (i);
            else
                return (-1);
        }
        

        #endregion

        #region "saving and loading"

        public XmlDocument getXML()
        {
            // Create the document.
            XmlDocument doc = new XmlDocument();

            // Insert the xml processing instruction and the root node
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "ISO-8859-1", null);
            doc.PrependChild(dec);

            XmlElement nodeSociety = doc.CreateElement("Society");
            doc.AppendChild(nodeSociety);

            if (locations.Count > 0)
            {
	            XmlElement nodeGeography = doc.CreateElement("Geography");
	            nodeSociety.AppendChild(nodeGeography);

	            // locations
	            for (int i = 0; i < locations.Count; i++)
	            {
	                location loc = (location)locations[i];
	                XmlElement elem = loc.getXML(doc);
	                nodeGeography.AppendChild(elem);
	            }
            }

            if (personalities.Count > 0)
            {
	            XmlElement nodePopulation = doc.CreateElement("Population");
	            nodeSociety.AppendChild(nodePopulation);

	            // people
	            for (int i = 0; i < personalities.Count; i++)
	            {
	                personality p = (personality)personalities[i];
	                XmlElement elem = p.getXML(doc);
	                nodePopulation.AppendChild(elem);
	            }
            }

            if (events.Count > 0)
            {
	            XmlElement nodeEvents = doc.CreateElement("Events");
	            nodeSociety.AppendChild(nodeEvents);

	            // events
	            for (int i = 0; i < events.Count; i++)
	            {
	                societyevent e = (societyevent)events[i];
	                XmlElement elem = e.getXML(doc);
	                nodeEvents.AppendChild(elem);
	            }
            }
            
            return (doc);
        }

        /// <summary>
        /// parse an xml node
        /// </summary>
        /// <param name="xnod"></param>
        /// <param name="level"></param>
        public override void LoadFromXml(XmlNode xnod, int level)
        {
            LoadNodeFromXml(xnod, level);
            
            if (xnod.Name == "Geography")
            {
                locations.Clear();
                for (int i = 0; i < xnod.ChildNodes.Count; i++)
                {                            
                    location locn = new location();
                    locn.LoadFromXml((XmlNode)xnod.ChildNodes[i], level);
                    locations.Add(locn);
                }
            }

            if (xnod.Name == "Population")
            {
                personalities.Clear();
                for (int i = 0; i < xnod.ChildNodes.Count; i++)
                {
                    personality p = new personality();
                    p.LoadFromXml((XmlNode)xnod.ChildNodes[i], level);
                    personalities.Add(p);
                }
            }

            if (xnod.Name == "Events")
            {
                events.Clear();
                for (int i = 0; i < xnod.ChildNodes.Count; i++)
                {                            
                    societyevent ev = new societyevent();
                    ev.LoadFromXml((XmlNode)xnod.ChildNodes[i], level);
                    events.Add(ev);
                }
            }

            // call recursively on all children of the current node
            if (xnod.HasChildNodes)
            {
                XmlNode xnodWorking = xnod.FirstChild;
                while (xnodWorking != null)
                {
                    LoadFromXml(xnodWorking, level + 1);
                    xnodWorking = xnodWorking.NextSibling;
                }
            }
        }


        private String getXMLString()
        {
            string strXML = "";

            XmlDocument doc = getXML();
            if (doc != null)
            {
                StringWriter writer = new StringWriter();
                doc.Save(writer);
                strXML = writer.ToString();
            }

            doc = null;
            return (strXML);
        }

        public void Save(String filename)
        {
            StreamWriter oWrite = null;
            bool allowWrite = true;

            try
            {
                oWrite = File.CreateText(filename);
            }
            catch
            {
                allowWrite = false;
            }

            if (allowWrite)
            {
                oWrite.Write(getXMLString());
                oWrite.Close();
            }
        }



        #endregion

        public society()
        {
            createProfessions();
            LoadMaleNames("male-names.txt");
        }
    }
}
