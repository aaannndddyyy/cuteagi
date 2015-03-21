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
    public class relationship : linkTemporal
    {
        public bool hasEnded;
        public truthvalue affect;
        public truthvalue credibility;
        public truthvalue familiarity;        

        #region "constructors"

        private void initRelationship()
        {
            SetLinkType("relationship");
            affect = new truthvalue();
            affect.SetValue(0.5f);
            credibility = new truthvalue();
            credibility.SetValue(0.5f);
            familiarity = new truthvalue();
            familiarity.SetValue(0.5f);
        }

        public relationship() : base(null, null, null)
        {
            initRelationship();
        }

        public relationship(String type, 
                            personality thisPerson, 
                            personality relationshipPerson): 
                            base(relationshipPerson, thisPerson, null)
        {
            initRelationship();
        }
        
        #endregion
        
        #region "who is involved?"

        /// <summary>
        /// defines the people involved in the relationship
        /// </summary>        
        public void SetPeople(personality thisPerson, 
                              personality relationshipPerson)
        {
            atoms_incoming.Clear();
            AddToIncomingSet(relationshipPerson);
            
            atoms_outgoing.Clear();
            AddToOutgoingSet(thisPerson);
        }
        
        /// <summary>
        /// returns the person being related
        /// </summary>
        public personality GetRelationshipPerson()
        {
            if (atoms_incoming.Count() > 0)
                return((personality)atoms_incoming.Get(0));
            else 
                return(null);
        }

        /// <summary>
        /// returns the person to who the relationship belongs
        /// </summary>
        public personality GetThisPerson()
        {
            if (atoms_outgoing.Count() > 0)
                return((personality)atoms_outgoing.Get(0));
            else 
                return(null);
        }
        
        #endregion

        #region "types of relationship"
        
        // possible relationship types
        public String[] RELATIONSHIP_TYPES = { 
            "Ally",
            "Apprentice",
            "Aunt",
            "Babysitter",
            "Boss",
            "Boyfriend",
            "Brother",
            "Carer",
            "Celebrity",
            "Co-worker",
            "Daughter",
            "Doctor",
            "Employee",
            "Enemy",
            "Father",
            "Friend",
            "Girlfriend",
            "Grandfather",
            "Grandmother",
            "Great Grandfather",
            "Great Grandmother",
            "Husband",
            "Lover",
            "Mentor",
            "Mother",
            "Nephew",
            "Niece",
            "Nurse",
            "Opponent",
            "Partner",
            "Pet",
            "Politician",
            "Sister",
            "Son",
            "Step Brother",
            "Step Daughter",
            "Step Father",
            "Step Mother",
            "Step Sister",
            "Step Son",
            "Student",
            "Teacher",
            "Uncle",
            "Wife" };        
        
        // the type of relationship
        protected short relationship_type;
        
        /// <summary>
        /// sets the relationship type
        /// </summary>
        public void SetRelationshipType(String type)
        {
            relationship_type = 0;
            bool found = false;
            int i = 0;
            while ((i < RELATIONSHIP_TYPES.Length) && (!found))
            {
                if (RELATIONSHIP_TYPES[i] == type)
                {
                    relationship_type = (short)i;
                    found = true;
                }
                i++;
            }
        }

        /// <summary>
        /// returns a description of the relationship
        /// </summary>
        public String GetRelationshipType()
        {
            return(RELATIONSHIP_TYPES[relationship_type]);
        }

        /// <summary>
        /// returns the relationship type as an integer
        /// </summary>
        public int GetRelationshipTypeInt32()
        {
            return((int)relationship_type);
        }

        /// <summary>
        /// returns the relationship type as an integer
        /// </summary>
        public int GetRelationshipTypeInt32(String relationship_type_description)
        {
            int result = -1;
            int i = 0;
            while ((i < RELATIONSHIP_TYPES.Length) && (result == -1))
            {
                if (RELATIONSHIP_TYPES[i] == relationship_type_description) result = i;
                i++;
            }
            return(result);
        }
        
        #endregion


        #region "interpersonal properties"

        public ArrayList property = new ArrayList();

        /// <summary>
        /// return the index of an interpersonal property based upon its description
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        private int getInterpersonalPropertyIndex(String description)
        {
            int i = 0;
            bool found = false;
            while ((i < interpersonalProperty.Length) && (!found))
            {
                if (interpersonalProperty[i] == description)
                    found = true;
                else
                    i++;
            }
            if (found)
                return(i);
            else
                return(-1);
        }

        /// <summary>
        /// add an interpersonal property based upon its description
        /// </summary>
        /// <param name="description"></param>
        public void AddInterpersonalProperty(String description)
        {
            int index = getInterpersonalPropertyIndex(description);
            if (index > -1)
                if (!property.Contains(index))
                    property.Add(index);
        }

        /// <summary>
        /// remove an interpersonal property based upon its description
        /// </summary>
        /// <param name="description"></param>
        public void RemoveInterpersonalProperty(String description)
        {
            int index = getInterpersonalPropertyIndex(description);
            if (index > -1)
                property.Remove(index);
        }

        /// <summary>
        /// return a list of interpersonal properties which may be used to populate a dropdown list
        /// </summary>
        /// <returns></returns>
        public static String getInterpersonalPropertyList()
        {
            String list = "";
            for (int i = 0; i < interpersonalProperty.Length; i++)
            {
                list += interpersonalProperty[i];
                if (i < interpersonalProperty.Length - 1) list += "#";
            }
            return (list);
        }

        // list of interpersonal properties originally devised by Donald J. Kiesler
        public static String[] interpersonalProperty = {
            "is quick to take charge of the conversation or discussion, or to offer suggestions about what needs to be done",
            "is hesitant to express approval or acceptance of me",
            "is careful not to let his or her feelings show clearly; or speaks undemonstratively, with little variation in tone or manner",
            "finds it difficult to take the initiative; or looks to me for direction or focus; or shows a desire to do 'whatever you want'",
            "is receptive and cooperative to my requests, directions, appeals, or wishes; or is quick to assist or work together with me",
            "expresses pleasure in self; or comments on own accomplishments, awards, or successes",
            "scans carefully to detect any of my reactions, evaluations, or motives that might have a harmful intent",
            "shows little attention, interest, curiosity, or inquisitiveness about my personal life, affairs, feelings, or opinions",
            "waits for or follows my lead regarding topics or issues to discuss, directions or actions to pursue",
            "is quick to express approval or acceptance of me",
            "speaks or acts emotionally or melodramatically, or with much variation in tone or manner",
            "shows an intense task focus or desire to 'get down to business'; or suggests directions or objectives",
            "is quick to resist, not cooperate, or refuse to comply with my requests, directions, appeals, or wishes",
            "makes self-critical statements; or expresses low self-worth; or apologizes frequently",
            "gazes at me in an open, receptive, trusting, non-searching manner",
            "inquires into or expresses attention, interest, or curiosity about my personal life, affairs, feelings, or opinions",
            "dominates the flow of conversation, or changes topic, or interrupts and 'talks down'",
            "avoids at any cost showing affection, warmth, or approval",
            "endlessly prefaces or qualifies statements to the place where points being made get lost, or views or positions are unclear or ambiguous",
            "goes out of way to give me credit for my contributions, or to admire or praise me for my good ideas or suggestions",
            "inconveniences self or sacrifices to contribute, help, assist, or work cooperatively with me",
            "is cocky about own positions or decisions; or makes itabundantly clear s/he can do things by self; or avoids any hint that I can help",
            "expresses doubt, mistrust, or disbelief regarding my intentions or motives",
            "refrains at all costs from close visual or physical contact or direct body orientation with me",
            "finds it almost impossible to take the lead, or to initiate or change the topic of discussion",
            "constantly expresses approval, affection, or effusive warmth to me",
            "makes startling or 'loaded' comments; or takes liberties with facts to embellish stories",
            "works hard to avoid giving me credit for any contribution; or implies or claims that good ideas or suggestions were his/her own",
            "is openly antagonistic, oppositional, or obstructive to my statements, suggestions, or purposes",
            "is hesitant or embarrassed to express his or her opinions; or conducts self in an unsure, unconfident, or uneasy manner",
            "responds openly, candidly, or revealingly to the point of 'telling all'",
            "continually stands, sits, moves or leans toward me to be physically close",
            "expresses firm, strong personal preferences; or stands up for own opinions or positions",
            "acts in a stiff, formal, unfeeling, or evaluative manner",
            "finds it difficult to express his or her thoughts simply or without qualifications; or works hard to find precise words to express his or her thoughts",
            "is content, unquestioning, or approving about the focus or direction of a given topic of discussion or course of action; or is quick to follow my lead",
            "expresses appreciation, delight, or satisfaction about me, our situation, or our task",
            "prefers to rely on own resources to make decisions or solve problems",
            "claims that I misunderstand, misinterpret, or misjudge his/her intents or actions",
            "remains aloof, distant, remote, or stand-offish from me",
            "claims s/he doesn't have an opinion, preference, or position, or that 'it doesn't matter', whatever you want, 'I don't know', etc.",
            "acts in a relaxed, informal, warm, or nonjudgmental manner",
            "makes comments or replies that 'pop out' quickly and energetically",
            "questions or expresses reservation or disagreement about the focus or direction of the conversation or course of action",
            "grumbles, gripes, nags, or complains about me, our situation, or our task",
            "readily asks me for advice, help, or counsel",
            "communicates that I am sympathetic or fair in interpreting or judging his/her intents or actions",
            "is absorbed in, attentive to, or concentrates intensely on what I say or do",
            "states preferences, opinions, or positions in a dogmatic or unyielding manner",
            "has absolutely no room for sympathy, compromise, or mercy regarding my mistakes, weaknesses, or misconduct",
            "'talks around' or hedges on evaluations of me, events, or objects, or constantly minimizes expressions of his or her feelings",
            "makes statements that are deferentially, softly, or carefully presented as if s/he desperately wants to avoid any implication of disapproval, criticism, or disagreement",
            "seems always to agree with or accommodate me; or seems impossible to rile",
            "brags about achievements, successes, or good-fortune; or 'puts on airs' as if in complete control of his/her life",
            "expresses harsh judgment, 'never forgetting', or no forgiveness for my mistakes, weaknesses, or injurious actions",
            "seems constantly uncomfortable with me as if s/he wants to leave or be by self",
            "expresses own preferences hesitantly or weakly; or yields easily to my viewpoints; or backs down quickly when I question or disagree",
            "goes out of way to understand or be sympathetic towards me, or to find something about me to approve of, endorse, or support",
            "constantly overstates evaluations of me, events, or objects; or exaggerates expression of his/her feelings",
            "makes comments that avoid sharing credit with me for good happenings or joint accomplishments; or 'plays up' own contributions",
            "argumentatively challenges or refutes my statements or suggestions; or 'tells me off', 'lets me have it' when disagrees",
            "claims s/he is a constant failure, or is helpless, witless, or at the mercy of events and circumstances",
            "expresses unbending sympathy, understanding, or forgiveness for my hurtful or injurious actions",
            "finds it difficult to leave me; or goes out of way to secure more and more of my company",
            "seizes opportunities to instruct or explain things, or to give advice",
            "expresses stringent, exacting, rigorous standards or expectations of me",
            "delays giving clear answers or postpones decisions; or deliberates carefully before speaking or acting",
            "makes comments that give me credit for any good happenings or joint accomplishments; or points out my contributions while 'playing down' his or her own",
            "is attentive to, considerate or solicitous of my feelings, or sensitive to pressures or stresses in my life",
            "expresses his or her opinions with conviction and ease; or conducts self in a confident, assured, and unruffled manner",
            "in response to my inquiries or probings, acts evasively as if hiding important secrets",
            "is slow to respond or speak to me; or seems distracted by own thoughts",
            "is quick to agree with my opinions or to comply with my directions or preferences",
            "expresses lenient, soft-hearted, or compassionate standards or expectations of me",
            "makes hasty decisions; or jumps into new activities with little premeditation",
            "challenges or disputes my ideas or statements; or attempts to get the better of me or put me down",
            "ignores, overlooks, or is inconsiderate of my feelings; or disregards pressures or stresses in my life",
            "urgently solicits my advice, help, or counsel even for everyday troubles or difficulties",
            "shows trust in or reliance upon my good intentions or motives; or casts my behavior in the best possible light",
            "is careful to acknowledge and be responsive to my statements and actions",
            "overwhelms or 'steamrolls' me by his/her arguments, positions, preferences, or actions",
            "expresses severe, inflexible, or uncompromsing expectations for my conduct",
            "endlessly avoids or delays clear answers, decisions, actions, or commitment to positions",
            "makes flattering or glowing comments about me, our situation, or our joint task",
            "makes unconditionally supportive, encouraging, endorsing, comforting, or bolstering comments to me",
            "acts as if excessively 'full of self', or as feeling special or favored, or as cocksure of his/her future",
            "is bitterly accusatory, suspicious, or disbelieving of me",
            "seems totally unmoved, unaffected, or untouched by my comments or actions",
            "seems unable to assert what s/he wants, or to stand up to me, or to take any opposing position",
            "is unwaveringly tolerant, patient, or lenient in regard to his/her expectations for my conduct",
            "seems compelled to act out feelings with me, or impulsively to jump into new actions or activities",
            "makes critical, demeaning, snide, or derisive statements about me, our situation, or our joint task",
            "swears at me; or makes abusing, disparaging, damaging, or crude comments to me",
            "is constantly dissatisfied with self, guilty or depressed; or feels hopeless about the future",
            "shows blind faith or polyannish trust in me; or believes almost anything I say",
            "seems totally engrossed in me; or is constantly moved, affected, or responsive to my comments or actions" };

        #endregion

        #region "saving and loading"

        public override XmlElement getXML(XmlDocument doc)
        {
            XmlElement elem = doc.CreateElement("LinkRelationship");
            doc.DocumentElement.AppendChild(elem);
            xml.AddTextElement(doc, elem, "RelationshipType", GetRelationshipType());
            
            if ((atoms_incoming.Count() > 0) && (atoms_outgoing.Count() > 0))
            {
                XmlElement elemPeople = doc.CreateElement("PeopleRelationship");
                elem.AppendChild(elemPeople);
            
                personality relationshipPerson = (personality)atoms_incoming.Get(0);
                elemPeople.AppendChild(relationshipPerson.getXMLIdentifier(doc));

                personality thisPerson = (personality)atoms_outgoing.Get(0);
                elemPeople.AppendChild(thisPerson.getXMLIdentifier(doc));
            }
            
            xml.AddTextElement(doc, elem, "Started", GetStartedDate().ToString());
            xml.AddTextElement(doc, elem, "Ended", GetEndedDate().ToString());
            xml.AddTextElement(doc, elem, "Affect", affect.ToString());
            xml.AddTextElement(doc, elem, "Credibility", credibility.ToString());
            xml.AddTextElement(doc, elem, "Familiarity", familiarity.ToString());

            String props = "";
            for (int i = 0; i < property.Count; i++)
            {
                if (i > 0) props += ",";
                props += Convert.ToString((int)property[i]);
            }
            xml.AddTextElement(doc, elem, "InterpersonalProperties", props);

            return (elem);
        }
        
        /// <summary>
        /// parse an xml node
        /// </summary>
        /// <param name="xnod"></param>
        /// <param name="level"></param>
        public override void LoadFromXml(XmlNode xnod, int level)
        {
            LoadLinkFromXml(xnod, level);
        
            if (xnod.Name == "RelationshipType")
                SetRelationshipType(xnod.InnerText);
                
            if (xnod.Name == "PeopleRelationship")
            {
                for (int i = 0; i < xnod.ChildNodes.Count; i++)
                {                    
                    personality person = new personality();
                    person.LoadFromXml(xnod.ChildNodes[i], level);
                    if (i == 0)
                    {
                        atoms_incoming.Clear();
                        atoms_incoming.Add(person);
                    }
                    else
                    {
                        atoms_outgoing.Clear();
                        atoms_outgoing.Add(person);
                    }
                }
            }

            if (xnod.Name == "Started")
                SetStartedDate(DateTime.Parse(xnod.InnerText));

            if (xnod.Name == "Ended")
                SetEndedDate(DateTime.Parse(xnod.InnerText));

            if (xnod.Name == "Affect")
                affect = truthvalue.FromString(xnod.InnerText);
            
            if (xnod.Name == "Credibility")
                credibility = truthvalue.FromString(xnod.InnerText);

            if (xnod.Name == "Familiarity")
                familiarity = truthvalue.FromString(xnod.InnerText);            

            if (xnod.Name == "InterpersonalProperties")
            {
                property.Clear();
                String[] props = xnod.InnerText.Split(',');
                for (int i = 0; i < props.Length; i++)
                {
                    property.Add(Convert.ToInt32(props[i]));
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
