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
    public class anecdote : node
    {
        public String Title;
        public String Description;
        public String Affect;
        public ArrayList locations = new ArrayList();
        public ArrayList personalities = new ArrayList();
        public ArrayList possessions = new ArrayList();

        #region "constructors"
        
        public anecdote()
        {
            SetNodeType("anecdote");
        }
        
        #endregion

        #region "types of anecdode"
        
        // possible anecdote types
        public String[] ANECDOTE_TYPES = { "", 
                                           "",
                                           "" };
        
        // the type of anecdote
        protected short anecdote_type;
        
        /// <summary>
        /// sets the anecdote type
        /// </summary>
        public void SetAnecdoteType(String type)
        {
            anecdote_type = 0;
            bool found = false;
            int i = 0;
            while ((i < ANECDOTE_TYPES.Length) && (!found))
            {
                if (ANECDOTE_TYPES[i] == type)
                {
                    anecdote_type = (short)i;
                    found = true;
                }
                i++;
            }
        }

        /// <summary>
        /// returns a description of the anecdote type
        /// </summary>
        public String GetAnecdoteType()
        {
            return(ANECDOTE_TYPES[anecdote_type]);
        }

        /// <summary>
        /// returns the anecdote type as an integer
        /// </summary>
        public int GetAnecdoteTypeInt32()
        {
            return((int)anecdote_type);
        }

        /// <summary>
        /// returns the anecdote type as an integer
        /// </summary>
        public int GetAnecdoteTypeInt32(String anecdote_type_description)
        {
            int result = -1;
            int i = 0;
            while ((i < ANECDOTE_TYPES.Length) && (result == -1))
            {
                if (ANECDOTE_TYPES[i] == anecdote_type_description) result = i;
                i++;
            }
            return(result);
        }
        
        #endregion


        #region "add components to the anecdote"

        /// <summary>
        /// add a person to this anecdote
        /// </summary>
        /// <param name="person">person</param>
        public void Add(personality person)
        {
            if (!personalities.Contains(person))
                personalities.Add(person);
        }

        /// <summary>
        /// add a location to this anecdote
        /// </summary>
        /// <param name="location">location</param>
        public void Add(personalityLocation location)
        {
            if (!locations.Contains(location))
                locations.Add(location);
        }

        /// <summary>
        /// add a possession to this anecdote
        /// </summary>
        /// <param name="p">possession</param>
        public void Add(possession p)
        {
            if (!possessions.Contains(p))
                possessions.Add(p);
        }
        
        #endregion
        
        #region "saving and loading"

        public override XmlElement getXML(XmlDocument doc)
        {
            XmlElement elem = doc.CreateElement("Anecdote");
            doc.DocumentElement.AppendChild(elem);
            xml.AddTextElement(doc, elem, "Title", Title);
            xml.AddTextElement(doc, elem, "Description", Description);
            xml.AddTextElement(doc, elem, "AnecdoteType", GetAnecdoteType());
            xml.AddTextElement(doc, elem, "Affect", Affect);
            
            if (locations.Count > 0)
            {
                XmlElement elemLocations = doc.CreateElement("AnecdoteLocations");
                elem.AppendChild(elemLocations);
            
                for (int i = 0; i < locations.Count; i++)
                    elemLocations.AppendChild(((location)locations[i]).getXMLIdentifier(doc));
            }

            if (personalities.Count > 0)
            {
                XmlElement elemPersonalities = doc.CreateElement("AnecdotePersonalities");
                elem.AppendChild(elemPersonalities);

                for (int i = 0; i < personalities.Count; i++)
                    elemPersonalities.AppendChild(((personality)personalities[i]).getXMLIdentifier(doc));
            }

            if (possessions.Count > 0)
            {
                XmlElement elemPossessions = doc.CreateElement("AnecdotePossessions");
                elem.AppendChild(elemPossessions);

                for (int i = 0; i < possessions.Count; i++)
                    elemPossessions.AppendChild(((possession)possessions[i]).getXMLIdentifier(doc));
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
            
            if (xnod.Name == "Title")
                Title = xnod.InnerText;
                
            if (xnod.Name == "Description")
                Description = xnod.InnerText;
                
            if (xnod.Name == "AnecdoteType")
                SetAnecdoteType(xnod.InnerText);
                
            if (xnod.Name == "Affect")
                Affect = xnod.InnerText;
                
            if (xnod.Name == "AnecdoteLocations")
            {
                locations.Clear();
                for (int i = 0; i < xnod.ChildNodes.Count; i++)
                {
                    location locn = new location();
                    locn.LoadFromXml((XmlNode)(xnod.ChildNodes[i]), level);
                    locations.Add(locn);
                }
            }

            if (xnod.Name == "AnecdotePersonalities")
            {
                personalities.Clear();
                for (int i = 0; i < xnod.ChildNodes.Count; i++)
                {
                    personality person = new personality();
                    person.LoadFromXml((XmlNode)(xnod.ChildNodes[i]), level);
                    personalities.Add(person);
                }
            }

            if (xnod.Name == "AnecdotePossessions")
            {
                possessions.Clear();
                for (int i = 0; i < xnod.ChildNodes.Count; i++)
                {
                    possession p = new possession();
                    p.LoadFromXml((XmlNode)(xnod.ChildNodes[i]), level);
                    possessions.Add(p);
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
