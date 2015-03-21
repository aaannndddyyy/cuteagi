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
using System.Collections;
using System.Text;
using sluggish.utilities.xml;
using cuteagi.core;

namespace cuteagi.personalitymodel
{
    /// <summary>
    /// stores information about a personal opinion
    /// </summary>
    public class opinion : node
    {
        // an event associated with this opinion, which may involve people, places or possessions
        public societyevent Event;
 
        // strength of opinion
        public truthvalue Strength;
 
        // a reason associated with this opinion
        public String Reason;
        
        #region "constructors"
        
        public opinion()
        {
            SetNodeType("opinion");
            Strength = new truthvalue();
        }
        
        #endregion
        
        #region "types of opinion"
        
        // possible opinion types
        public String[] OPINION_TYPES = { "", 
                                          "",
                                          "" };
        
        // the type of opinion
        protected short opinion_type;
        
        /// <summary>
        /// sets the opinion type
        /// </summary>
        public void SetOpinionType(String type)
        {
            opinion_type = 0;
            bool found = false;
            int i = 0;
            while ((i < OPINION_TYPES.Length) && (!found))
            {
                if (OPINION_TYPES[i] == type)
                {
                    opinion_type = (short)i;
                    found = true;
                }
                i++;
            }
        }

        /// <summary>
        /// returns a description of the opinion type
        /// </summary>
        public String GetOpinionType()
        {
            return(OPINION_TYPES[opinion_type]);
        }

        /// <summary>
        /// returns the opinion type as an integer
        /// </summary>
        public int GetOpinionTypeInt32()
        {
            return((int)opinion_type);
        }

        /// <summary>
        /// returns the opinion type as an integer
        /// </summary>
        public int GetOpinionTypeInt32(String opinion_type_description)
        {
            int result = -1;
            int i = 0;
            while ((i < OPINION_TYPES.Length) && (result == -1))
            {
                if (OPINION_TYPES[i] == opinion_type_description) result = i;
                i++;
            }
            return(result);
        }
        
        #endregion
        

        #region "saving and loading"

        public override XmlElement getXML(XmlDocument doc)
        {
            XmlElement elem = doc.CreateElement("Opinion");
            doc.DocumentElement.AppendChild(elem);
            xml.AddTextElement(doc, elem, "OpinionType", GetOpinionType());
            
            XmlElement elemEvent = doc.CreateElement("OpinionEvent");
            elem.AppendChild(elemEvent);
            
            elemEvent.AppendChild(Event.getXML(doc));
            
            xml.AddTextElement(doc, elem, "Strength", Strength.ToString());
            xml.AddTextElement(doc, elem, "Reason", Reason);
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
            
            if (xnod.Name == "OpinionType")
                SetOpinionType(xnod.InnerText);
                
            if (xnod.Name == "OpinionEvent")
            {
                Event = new societyevent();
                Event.LoadFromXml(xnod, level + 1);
            }
                
            if (xnod.Name == "Strength")
                Strength = truthvalue.FromString(xnod.InnerText);

            if (xnod.Name == "Reason")
                Reason = xnod.InnerText;
                
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
