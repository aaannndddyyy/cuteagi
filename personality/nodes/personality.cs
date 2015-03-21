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
using System.Xml;
using System.IO;
using System.Collections;
using System.Text;
using sluggish.utilities.xml;
using cuteagi.core;

namespace cuteagi.personalitymodel
{
    public class personality : nodeObjectPhysical
    {
        public bool isMale;
        
        #region "male and female names"
        
        public static bool isMaleName(String name)
        {
            bool is_male = false;
            
            return(is_male);
        }
        
        #endregion
        
        #region "birth and death"
        
        /// <summary>
        /// returns the date of birth of this person
        /// </summary>
        public DateTime GetDateOfBirth()
        {
            return(node_event.start_time);
        }

        /// <summary>
        /// set the date of birth of this person
        /// </summary>
        public void SetDateOfBirth(DateTime date)
        {
            node_event.start_time = date;
        }

        /// <summary>
        /// returns the date of death of this person
        /// </summary>
        public DateTime GetDateOfDeath()
        {
            return(node_event.end_time);
        }

        /// <summary>
        /// set the date of death of this person
        /// </summary>
        public void SetDateOfDeath(DateTime date)
        {
            node_event.end_time = date;
        }

        
        #endregion        

        #region "constructors"

        private void initPersonality()
        {
            SetNodeType("personality");
            Extroversion = new truthvalue();
            Extroversion.SetValue(50);
            Agreeableness = new truthvalue();
            Agreeableness.SetValue(50);
            Conscientiousness = new truthvalue();
            Conscientiousness.SetValue(50);
            Neuroticism = new truthvalue();
            Neuroticism.SetValue(50);
            Openness = new truthvalue();
            Openness.SetValue(50);
        }

        public personality()
        {
            initPersonality();
        }

        public personality(String Name, bool isMale)
        {
            initPersonality();            
            SetName(Name);
            this.isMale = isMale;
        }
        
        #endregion

        #region "physical features"

        public String EyeColour;
        public String HairColour;
        public String HairLength;
        public String FacialHair;
        public String BodyBuild;
        public String Height;
        #endregion



        #region "dimensions of personality"

        public truthvalue Extroversion;
        public truthvalue Agreeableness;
        public truthvalue Conscientiousness;
        public truthvalue Neuroticism;
        public truthvalue Openness;
        public String Sexuality = "Heterosexual";

        /// <summary>
        /// returns a description of the dimensions of personality for this person
        /// </summary>
        /// <returns></returns>
        public String DimensionsDescription()
        {
            String description = "This person is " +
                                 ExtroversionDescription() + ", " +
                                 AgreeablenessDescription() + ", " +
                                 ConscientiousnessDescription() + ", " +
                                 NeuroticismDescription() + ", " +
                                 OpennessDescription();
            return (description);
        }

        /// <summary>
        /// returns a description of the level of extroversion
        /// </summary>
        /// <returns></returns>
        private String ExtroversionDescription()
        {
            String description = "";

            if (Extroversion.GetValue() < 20)
            {
                description += "very quiet, doesn't like socialising";
            }
            else
            {
                if (Extroversion.GetValue() < 40)
                {
                    description += "somewhat quiet, goes out occasionally";
                }
                else
                {
                    if (Extroversion.GetValue() < 60)
                    {
                        description += "occasionally sociable";
                    }
                    else
                    {
                        if (Extroversion.GetValue() < 80)
                        {
                            description += "often sociable, likes to chat";
                        }
                        else
                        {
                            description += "highly sociable, very talkative, emotionally expressive";
                        }
                    }
                }
            }
            return (description);
        }

        /// <summary>
        /// returns a description of the level of Agreeableness
        /// </summary>
        /// <returns></returns>
        private String AgreeablenessDescription()
        {
            String description = "";

            if (Agreeableness.GetValue() < 20)
            {
                description += "unkind, doesn't show affection, uncooperative";
            }
            else
            {
                if (Agreeableness.GetValue() < 40)
                {
                    description += "somewhat dissagreeable, aloof";
                }
                else
                {
                    if (Agreeableness.GetValue() < 60)
                    {
                        description += "quite likeable";
                    }
                    else
                    {
                        if (Agreeableness.GetValue() < 80)
                        {
                            description += "very likeable, occasionally does things for others";
                        }
                        else
                        {
                            description += "highly likeable, very altruistic";
                        }
                    }
                }
            }
            return (description);
        }

        /// <summary>
        /// returns a description of the level of Conscientiousness
        /// </summary>
        /// <returns></returns>
        private String ConscientiousnessDescription()
        {
            String description = "";

            if (Conscientiousness.GetValue() < 20)
            {
                description += "forgetful, lacking willpower, never plans ahead, a creature of habit";
            }
            else
            {
                if (Conscientiousness.GetValue() < 40)
                {
                    description += "occasionally thoughtless, rarely makes plans";
                }
                else
                {
                    if (Conscientiousness.GetValue() < 60)
                    {
                        description += "moderately conscientious, usually sticks to their plans";
                    }
                    else
                    {
                        if (Conscientiousness.GetValue() < 80)
                        {
                            description += "very thoughtful, always puts consideration into their decisions";
                        }
                        else
                        {
                            description += "highly goal oriented, always appears to be in control of themselves, sticks to their plans";
                        }
                    }
                }
            }
            return (description);
        }

        /// <summary>
        /// returns a description of the level of Neuroticism
        /// </summary>
        /// <returns></returns>
        private String NeuroticismDescription()
        {
            String description = "";

            if (Neuroticism.GetValue() < 20)
            {
                description += "emotionally strong, even tempered";
            }
            else
            {
                if (Neuroticism.GetValue() < 40)
                {
                    description += "emotional outbursts occur rarely";
                }
                else
                {
                    if (Neuroticism.GetValue() < 60)
                    {
                        description += "sometimes irritable";
                    }
                    else
                    {
                        if (Neuroticism.GetValue() < 80)
                        {
                            description += "irritable, frequent emotional outbursts";
                        }
                        else
                        {
                            description += "emotionally unstable, always anxious, very moody";
                        }
                    }
                }
            }
            return (description);
        }

        /// <summary>
        /// returns a description of the level of openness
        /// </summary>
        /// <returns></returns>
        private String OpennessDescription()
        {
            String description = "";

            if (Openness.GetValue() < 20)
            {
                description += "has no hobbies, little imagination, is not creative";
            }
            else
            {
                if (Openness.GetValue() < 40)
                {
                    description += "has an interest in one subject indulged occasionally, is rarely creative, little imagination";
                }
                else
                {
                    if (Openness.GetValue() < 60)
                    {
                        description += "has a few interests, moderately imaginitive";
                    }
                    else
                    {
                        if (Openness.GetValue() < 80)
                        {
                            description += "somewhat creative, has an active imaginition";
                        }
                        else
                        {
                            description += "has numerous interests, highly imaginitive, highly creative";
                        }
                    }
                }
            }
            return (description);
        }

        #endregion

        #region "beliefs"

        public ArrayList beliefs = new ArrayList();

        public void RemoveBelief(String BeliefSystemType)
        {
            int index = 0;
            bool found = false;
            while ((index < beliefs.Count) && (!found))
            {
                belief b = (belief)beliefs[index];
                if (b.BeliefSystem == BeliefSystemType)
                {
                    found = true;
                    beliefs.Remove(b);
                }
                else
                    index++;
            }
        }

        public int beliefExists(String BeliefSystemType)
        {
            int index = 0;
            bool found = false;
            while ((index < beliefs.Count) && (!found))
            {
                belief b = (belief)beliefs[index];
                if (b.BeliefSystem == BeliefSystemType)
                    found = true;
                else
                    index++;
            }
            if (found)
                return (index);
            else
                return (-1);
        }

        public belief AddBelief(int Strength, String BeliefSystemType, 
                                DateTime AcquiredDate, int RelinquishedAge)
        {
            belief b = null;
            int index = beliefExists(BeliefSystemType);
            if (index == -1)
            {
                b = new belief();
                beliefs.Add(b);
            }
            else
            {
                b = (belief)beliefs[index];
            }
            b.Strength.SetValue(Strength);
            b.BeliefSystem = BeliefSystemType;
            b.SetAcquiredDate(AcquiredDate);
            b.RelinquishedAge = RelinquishedAge;

            return (b);
        }

        #endregion

        #region "roles"

        public ArrayList roles = new ArrayList();

        public void RemoveRole(String profession, DateTime StartedDate)
        {
            int index = 0;
            bool found = false;
            while ((index < roles.Count) && (!found))
            {
                role r = (role)roles[index];
                DateTime start = r.GetAcquiredDate();
                if ((r.Profession.Name == profession) &&
                    (start.Year == StartedDate.Year) &&
                    (start.Month == StartedDate.Month) &&
                    (start.Day == StartedDate.Day))
                {
                    found = true;
                    roles.Remove(r);
                }
                else
                    index++;
            }
        }

        public int roleExists(String profession, DateTime StartedDate)
        {
            int index = 0;
            bool found = false;
            while ((index < roles.Count) && (!found))
            {
                role r = (role)roles[index];
                DateTime start = r.GetAcquiredDate();
                if ((r.Profession.Name == profession) &&
                    (start.Year == StartedDate.Year) &&
                    (start.Month == StartedDate.Month) &&
                    (start.Day == StartedDate.Day))
                    found = true;
                else
                    index++;
            }
            if (found)
                return (index);
            else
                return (-1);
        }

        public role AddRole(society soc, String Profession, String Description,
                            DateTime StartedDate,
                            String Affect,
                            String Locn, String Country, String skills_list)
        {
            role r = null;
            int index = roleExists(Profession, StartedDate);
            if (index == -1)
            {
                r = new role();
                roles.Add(r);
            }
            else
            {
                r = (role)roles[index];
            }
            r.Profession = new profession(Profession);
            r.Description = Description;
            r.Affect = Affect;
            r.SetAcquiredDate(StartedDate);
            
            if (skills_list != "")
            {
                String[] skills_str = skills_list.Split(',');
                for (int i = 0; i < skills_str.Length; i++)
                {
                    String s = skills_str[i];
                    r.Skills.Add(new skill(s.Trim()));
                }
            }

            int locn_index = soc.LocationExists(Locn, Country);
            if (locn_index > -1)
                r.Locn = (location)soc.locations[locn_index];
            else 
                r.Locn = soc.AddLocation(Locn, Country, "");

            return (r);
        }


        #endregion

        #region "possessions"

        protected ArrayList possessions = new ArrayList();

        public void RemovePossession(String Name)
        {
            Name = Name.ToLower();
            int index = 0;
            bool found = false;
            while ((index < possessions.Count) && (!found))
            {
                possession p = (possession)possessions[index];
                if (p.GetName() == Name)
                {
                    found = true;
                    possessions.Remove(p);
                }
                else
                    index++;
            }
        }


        public int possessionExists(String Name)
        {
            Name = Name.ToLower();
            int index = 0;
            bool found = false;
            while ((index < possessions.Count) && (!found))
            {
                possession p = (possession)possessions[index];
                if (p.GetName() == Name)
                    found = true;
                else
                    index++;
            }
            if (found)
                return (index);
            else
                return (-1);
        }

        public possession AddPossession(String Name, String Type,
                                        DateTime AcquiredDate,
                                        DateTime RelinquishedDate,
                                        bool NotOwned,
                                        personality AcquiredFrom,
                                        String Affect, int value)
        {
            int index = possessionExists(Name);
            possession p = null;
            if (index == -1)
            {
                p = new possession();
                possessions.Add(p);
            }
            else
            {
                p = (possession)possessions[index];
            }
            p.BelongsTo = this;
            p.SetName(Name.ToLower().Trim());
            p.SetPossessionType(Type);
            p.SetAcquiredDate(AcquiredDate);
            p.SetRelinquishedDate(RelinquishedDate);
            p.notOwned = NotOwned;
            p.AcquiredFrom = AcquiredFrom;
            p.Value = value;
            return (p);
        }

        #endregion

        #region "locations"

        public ArrayList locations = new ArrayList();

        public personalityLocation AddLocation(society soc,
                                String Name, String Country,
                                float Latitude, float longitude,
                                String Type, String Affect,
                                DateTime Date)
        {
            location locn = soc.AddLocation(Name, Country, Type);
            personalityLocation plocn = new personalityLocation();
            plocn.Locn = locn;
            plocn.Date = Date;
            plocn.Affect = Affect;
            locations.Add(plocn);
            return (plocn);
        }

        #endregion

        #region "relationships"

        public ArrayList relationships = new ArrayList();

        /// <summary>
        /// remove a relationship
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="person"></param>
        public void RemoveRelationship(String Type, personality person)
        {
            bool found = false;
            int i = 0;
            while ((i < relationships.Count) && (!found))
            {
                relationship r = (relationship)relationships[i];
                if ((r.GetRelationshipPerson() == person) && (Type == r.GetRelationshipType()))
                {
                    relationships.Remove(r);
                    found = true;
                }
                else
                    i++;
            }
        }

        /// <summary>
        /// does this relationship already exist?
        /// </summary>
        /// <param name="RelationshipType"></param>
        /// <param name="person"></param>
        /// <returns></returns>
        public int RelationshipExists(String RelationshipType, personality person)
        {
            bool found = false;
            int i = 0;
            while ((i < relationships.Count) && (!found))
            {
                relationship r = (relationship)relationships[i];
                if ((r.GetRelationshipPerson() == person) && (RelationshipType == r.GetRelationshipType()))
                    found = true;
                else
                    i++;
            }
            if (found)
                return (i);
            else
                return (-1);
        }

        /// <summary>
        /// add a relationship
        /// </summary>
        /// <param name="Type">the type of relationship, such as mother or father</param>
        /// <param name="person">person object</param>
        public void AddRelationship(String Type, personality person, DateTime started, DateTime ended, bool hasEnded, 
                                    float affect, float credibility, float familiarity)
        {
            int index = RelationshipExists(Type, person);
            relationship r = null;
            if (index == -1)
            {
                r = new relationship(Type, person, this);
                relationships.Add(r);
            }
            else
            {
                r = (relationship)relationships[index];
            }
            r.SetStartedDate(started);
            r.SetEndedDate(ended);
            r.hasEnded = hasEnded;
            r.affect = new truthvalue(affect);
            r.credibility = new truthvalue(credibility);
            r.familiarity = new truthvalue(familiarity);
        }

        public void AddInterpersonalProperty(String Type, personality person, String interpersonalDescription)
        {
            int index = RelationshipExists(Type, person);
            if (index > -1)
            {
                relationship r = (relationship)relationships[index];
                r.AddInterpersonalProperty(interpersonalDescription);
            }
        }

        public void RemoveInterpersonalProperty(String Type, personality person, String interpersonalDescription)
        {
            int index = RelationshipExists(Type, person);
            if (index > -1)
            {
                relationship r = (relationship)relationships[index];
                r.RemoveInterpersonalProperty(interpersonalDescription);
            }
        }


        #endregion

        #region "skills"

        public static String[] communicationSkill = {
            "Speaking effectively","Writing concisely","Listening attentively",
            "Expressing ideas","Facilitating group discussion","Providing appropriate feedback",
            "Negotiating","Perceiving nonverbal messages","Persuading","Reporting information",
            "Describing feelings","Interviewing","Editing" };

        public static String[] planningSkill = {
            "Forecasting", "Predicting", "Creating ideas","Identifying problems",
            "Imagining alternatives","Identifying resources","Gathering information",
            "Solving problems","Setting goals","Extracting important information",
            "Defining needs","Analyzing","Developing evaluation strategies" };

        public static String[] humanRelationsSkill = {
            "Developing rapport","Being Sensitive","Listening","Conveying feelings",
            "Providing support for others","Motivating","Sharing credit","Counseling",
            "Cooperating","Delegating with respect","Representing others","Perceiving feelings and situations",
            "Asserting", "Deceiving" };

        public static String[] organisationalSkill = {
            "Initiating new ideas","Handling details","Coordinating tasks","Managing groups",
            "Delegating responsibility","Teaching","Coaching","Counseling","Promoting change",
            "Selling ideas or products","Decision making with others","Managing conflict",
            "Inspiring confidence in others" };

        public static String[] workingSkill = {
            "Implementing decisions","Cooperating","Enforcing policies","Being punctual",
            "Managing time","Attending to detail","Meeting goals","Enlisting help",
            "Accepting responsibility", "Setting and meeting deadlines","Organizing",
            "Making decisions" };

        /// <summary>
        /// returns a complete list of skills
        /// </summary>
        /// <returns></returns>
        public static String getGeneralSkillsList()
        {
            String SkillsList = "";

            for (int i = 0; i < communicationSkill.Length; i++)
                SkillsList += communicationSkill[i] + ",";
            for (int i = 0; i < planningSkill.Length; i++)
                SkillsList += planningSkill[i] + ",";
            for (int i = 0; i < humanRelationsSkill.Length; i++)
                SkillsList += humanRelationsSkill[i] + ",";
            for (int i = 0; i < organisationalSkill.Length; i++)
                SkillsList += organisationalSkill[i] + ",";
            for (int i = 0; i < workingSkill.Length; i++)
            {
                SkillsList += workingSkill[i];
                if (i < workingSkill.Length - 1) SkillsList += ",";
            }
            return (SkillsList);
        }

        public ArrayList generalskills = new ArrayList();

        public void RemoveGeneralSkill(String Description)
        {
            Description = Description.ToLower();
            int index = 0;
            bool found = false;
            while ((index < generalskills.Count) && (!found))
            {
                skill s = (skill)generalskills[index];
                if (s.GetDescription().ToLower() == Description)
                {
                    found = true;
                    generalskills.Remove(s);
                }
                else
                    index++;
            }
        }


        public int generalSkillExists(String Description)
        {
            Description = Description.ToLower();
            int index = 0;
            bool found = false;
            while ((index < generalskills.Count) && (!found))
            {
                skill s = (skill)generalskills[index];
                if (s.GetDescription().ToLower() == Description)
                    found = true;
                else
                    index++;
            }
            if (found)
                return (index);
            else
                return (-1);
        }

        private String getGeneralSkillType(String Description)
        {
            String Type = "";

            int i = 0;
            while ((i < communicationSkill.Length) && (Type == ""))
            {
                if (communicationSkill[i] == Description)
                    Type = "communication";
                else
                    i++;
            }

            i = 0;
            while ((i < planningSkill.Length) && (Type == ""))
            {
                if (planningSkill[i] == Description)
                    Type = "planning";
                else
                    i++;
            }

            i = 0;
            while ((i < humanRelationsSkill.Length) && (Type == ""))
            {
                if (humanRelationsSkill[i] == Description)
                    Type = "human relations";
                else
                    i++;
            }

            i = 0;
            while ((i < organisationalSkill.Length) && (Type == ""))
            {
                if (organisationalSkill[i] == Description)
                    Type = "organisational";
                else
                    i++;
            }

            i = 0;
            while ((i < workingSkill.Length) && (Type == ""))
            {
                if (workingSkill[i] == Description)
                    Type = "working";
                else
                    i++;
            }

            return (Type);
        }

        public skill AddGeneralSkill(String Description, int Strength, DateTime AcquiredDate)
        {
            int index = generalSkillExists(Description);
            skill s = null;
            if (index == -1)
            {
                s = new skill();
                generalskills.Add(s);
            }
            else
            {
                s = (skill)generalskills[index];
            }
            s.SetSkillType(getGeneralSkillType(Description));
            s.SetDescription(Description);
            s.Strength = new truthvalue(Strength);
            s.SetAcquiredDate(AcquiredDate);
            return (s);
        }


        #endregion

        #region "achievements"

        public ArrayList achievements = new ArrayList();

        public void RemoveAchievement(String Type, String Category, int AchievedAge)
        {
        /*
            Type = Type.ToLower();
            Category = Category.ToLower();
            int index = 0;
            bool found = false;
            while ((index < achievements.Count) && (!found))
            {
                achievement a = (achievement)achievements[index];
                if ((a.GetAchievementType().ToLower() == GetAchievementType()) && (a.Category.ToLower() == Category))
                {
                    found = true;
                    achievements.Remove(a);
                }
                else
                    index++;
            }
        */
        }


        public int achievementExists(String Type, String Category, DateTime AchievedDate)
        {
        /*
            Type = Type.ToLower();
            Category = Category.ToLower();
            int index = 0;
            bool found = false;
            while ((index < achievements.Count) && (!found))
            {
                achievement a = (achievement)achievements[index];
                if ((a.Type.ToLower() == Type) && (a.Category.ToLower() == Category))
                    found = true;
                else
                    index++;
            }
            if (found)
                return (index);
            else
                return (-1);
        */
            return(-1);
        }

        public achievement AddAchievement(String Type, String Category, DateTime AchievedDate,
                                          int Value, String Description)
        {
            int index = achievementExists(Type, Category, AchievedDate);
            achievement a = null;
            if (index == -1)
            {
                a = new achievement();
                achievements.Add(a);
            }
            else
            {
                a = (achievement)achievements[index];
            }
            a.SetAchievementType(Type);
            a.Category = Category;
            a.SetAcquiredDate(AchievedDate);
            a.Description = Description;
            a.Value = Value;
            return (a);
        }


        #endregion

        #region "opinions"

        public ArrayList opinions = new ArrayList();

        public void RemoveOpinion(String Type, societyevent ev)
        {
            int index = 0;
            bool found = false;
            while ((index < opinions.Count) && (!found))
            {
                opinion op = (opinion)opinions[index];
                if ((op.GetOpinionType() == Type) && (op.Event == ev))
                {
                    found = true;
                    opinions.Remove(op);
                }
                else
                    index++;
            }
        }


        public int opinionExists(String Type, societyevent ev)
        {
            int index = 0;
            bool found = false;
            while ((index < opinions.Count) && (!found))
            {
                opinion op = (opinion)opinions[index];
                if ((op.GetOpinionType() == Type) && (op.Event == ev))
                    found = true;
                else
                    index++;
            }
            if (found)
                return (index);
            else
                return (-1);
        }

        public opinion AddOpinion(String Type, societyevent Event, String Reason)
        {
            int index = opinionExists(Type, Event);
            opinion op = null;
            if (index == -1)
            {
                op = new opinion();
                opinions.Add(op);
            }
            else
            {
                op = (opinion)opinions[index];
            }
            op.SetOpinionType(Type);
            op.Event = Event;
            op.Reason = Reason;
            return (op);
        }


        #endregion


        #region "saving and loading"

        public override XmlElement getXMLIdentifier(XmlDocument doc)
        {
            XmlElement elem = doc.CreateElement("Person");
            doc.DocumentElement.AppendChild(elem);
            xml.AddTextElement(doc, elem, "Name", GetName());
            xml.AddTextElement(doc, elem, "Male", isMale.ToString());
            xml.AddTextElement(doc, elem, "DateOfBirth", GetDateOfBirth().ToString());
            return (elem);
        }


        public override XmlElement getXML(XmlDocument doc)
        {
            XmlElement elem = doc.CreateElement("Person");
            doc.DocumentElement.AppendChild(elem);
            xml.AddTextElement(doc, elem, "Name", GetName());
            xml.AddTextElement(doc, elem, "Male", isMale.ToString());
            xml.AddTextElement(doc, elem, "DateOfBirth", GetDateOfBirth().ToString());

            XmlElement elemPhysical = doc.CreateElement("PhysicalAttributes");
            elem.AppendChild(elemPhysical);
            
            xml.AddTextElement(doc, elemPhysical, "EyeColour", EyeColour);
            xml.AddTextElement(doc, elemPhysical, "HairColour", HairColour);
            xml.AddTextElement(doc, elemPhysical, "HairLength", HairLength);
            xml.AddTextElement(doc, elemPhysical, "FacialHair", FacialHair);
            xml.AddTextElement(doc, elemPhysical, "BodyBuild", BodyBuild);
            xml.AddTextElement(doc, elemPhysical, "Height", Height);

            XmlElement elemPersonality = doc.CreateElement("PersonalityAttributes");
            elem.AppendChild(elemPersonality);

            xml.AddTextElement(doc, elemPersonality, "Extroversion", Convert.ToString(Extroversion));
            xml.AddTextElement(doc, elemPersonality, "Agreeableness", Convert.ToString(Agreeableness));
            xml.AddTextElement(doc, elemPersonality, "Conscientiousness", Convert.ToString(Conscientiousness));
            xml.AddTextElement(doc, elemPersonality, "Neuroticism", Convert.ToString(Neuroticism));
            xml.AddTextElement(doc, elemPersonality, "Openness", Convert.ToString(Openness));
            xml.AddTextElement(doc, elemPersonality, "Sexuality", Sexuality);

            if (beliefs.Count > 0)
            {
                XmlElement elemBeliefs = doc.CreateElement("Beliefs");
                elem.AppendChild(elemBeliefs);
                for (int i = 0; i < beliefs.Count; i++)
                    elemBeliefs.AppendChild(((belief)beliefs[i]).getXML(doc));
            }

            if (roles.Count > 0)
            {
                XmlElement elemRoles = doc.CreateElement("Roles");
                elem.AppendChild(elemRoles);
                for (int i = 0; i < roles.Count; i++)
                {
                    role r = (role)roles[i];
                    XmlElement elemRole = r.getXML(doc);
                    elemRoles.AppendChild(elemRole);
                }
            }

            if (possessions.Count > 0)
            {
                XmlElement elemPossessions = doc.CreateElement("Possessions");
                elem.AppendChild(elemPossessions);
                for (int i = 0; i < possessions.Count; i++)
                {
                    possession p = (possession)possessions[i];
                    XmlElement elemPossession = p.getXML(doc);
                    elemPossessions.AppendChild(elemPossession);
                }
            }

            if (relationships.Count > 0)
            {
                XmlElement elemRelationships = doc.CreateElement("Relationships");
                elem.AppendChild(elemRelationships);
                for (int i = 0; i < relationships.Count; i++)
                {
                    relationship rel = (relationship)relationships[i];
                    XmlElement elemRelationship = rel.getXML(doc);
                    elemRelationships.AppendChild(elemRelationship);
                }
            }

            if (generalskills.Count > 0)
            {
                XmlElement elemGeneralSkills = doc.CreateElement("GeneralSkills");
                elem.AppendChild(elemGeneralSkills);
                for (int i = 0; i < generalskills.Count; i++)
                {
                    skill s = (skill)generalskills[i];
                    XmlElement elemSkill = s.getXML(doc);
                    elemGeneralSkills.AppendChild(elemSkill);
                }
            }

            if (opinions.Count > 0)
            {
                XmlElement elemOpinions = doc.CreateElement("Opinions");
                elem.AppendChild(elemOpinions);
                for (int i = 0; i < opinions.Count; i++)
                {
                    opinion op = (opinion)opinions[i];
                    XmlElement elemOpinion = op.getXML(doc);
                    elemOpinions.AppendChild(elemOpinion);
                }
            }

            return (elem);
        }

        /// <summary>
        /// parse an xml node
        /// </summary>
        /// <param name="xnod"></param>
        /// <param name="level"></param>
        public override void LoadFromXml(XmlNode xnod, int level)
        {
            LoadNodeFromXml(xnod, level);
            
            if (xnod.Name == "Name")
                Name = xnod.InnerText;
                
            if (xnod.Name == "isMale")
                isMale = Convert.ToBoolean(xnod.InnerText);
                
            if (xnod.Name == "DateOfBirth")
                SetDateOfBirth(DateTime.Parse(xnod.InnerText));
                
            if (xnod.Name == "PhysicalAttributes")
            {
            }

            if (xnod.Name == "EyeColour")
                EyeColour = xnod.InnerText;

            if (xnod.Name == "HairColour")
                HairColour = xnod.InnerText;

            if (xnod.Name == "HairLength")
                HairLength = xnod.InnerText;

            if (xnod.Name == "FacialHair")
                FacialHair = xnod.InnerText;

            if (xnod.Name == "BodyBuild")
                BodyBuild = xnod.InnerText;
                
            if (xnod.Name == "Height")
                Height = xnod.InnerText;

            if (xnod.Name == "PersonalityAttributes")
            {
            }
                
            if (xnod.Name == "Extroversion")
                Extroversion = truthvalue.FromString(xnod.InnerText);

            if (xnod.Name == "Agreeableness")
                Agreeableness = truthvalue.FromString(xnod.InnerText);

            if (xnod.Name == "Conscientiousness")
                Conscientiousness = truthvalue.FromString(xnod.InnerText);

            if (xnod.Name == "Neuroticism")
                Neuroticism = truthvalue.FromString(xnod.InnerText);

            if (xnod.Name == "Openness")
                Openness = truthvalue.FromString(xnod.InnerText);

            if (xnod.Name == "Sexuality")
                Sexuality = xnod.InnerText;

            if (xnod.Name == "Beliefs")
            {
                beliefs.Clear();
                for (int i = 0; i < xnod.ChildNodes.Count; i++)
                {
                    belief b = new belief();
                    b.LoadFromXml((XmlNode)(xnod.ChildNodes[i]), level);
                    beliefs.Add(b);
                }
            }

            if (xnod.Name == "Roles")
            {
                roles.Clear();
                for (int i = 0; i < xnod.ChildNodes.Count; i++)
                {
                    role r = new role();
                    r.LoadFromXml((XmlNode)(xnod.ChildNodes[i]), level);
                    roles.Add(r);
                }
            }

            if (xnod.Name == "Possessions")
            {
                possessions.Clear();
                for (int i = 0; i < xnod.ChildNodes.Count; i++)
                {
                    possession p = new possession();
                    p.LoadFromXml((XmlNode)(xnod.ChildNodes[i]), level);
                    possessions.Add(p);
                }
            }

            if (xnod.Name == "Relationships")
            {
                relationships.Clear();
                for (int i = 0; i < xnod.ChildNodes.Count; i++)
                {
                    relationship r = new relationship();
                    r.LoadFromXml((XmlNode)(xnod.ChildNodes[i]), level);
                    relationships.Add(r);
                }
            }

            if (xnod.Name == "GeneralSkills")
            {
                generalskills.Clear();
                for (int i = 0; i < xnod.ChildNodes.Count; i++)
                {
                    skill s = new skill();
                    s.LoadFromXml((XmlNode)(xnod.ChildNodes[i]), level);
                    generalskills.Add(s);
                }
            }

            if (xnod.Name == "Opinions")
            {
                opinions.Clear();
                for (int i = 0; i < xnod.ChildNodes.Count; i++)
                {
                    opinion o = new opinion();
                    o.LoadFromXml((XmlNode)(xnod.ChildNodes[i]), level);
                    opinions.Add(o);
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

        #endregion    
    }
}
