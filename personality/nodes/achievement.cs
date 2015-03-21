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
    public class achievement : nodeTemporal
    {
        public String Category;
        public String Description;
        public int Value;

        #region "constructors"
        
        public achievement()
        {
            SetNodeType("achievement");
        }
        
        #endregion

        #region "types of achievement"
        
        // possible achievement types
        public String[] ACHIEVEMENT_TYPES = { "", 
                                              "",
                                              "" };
        
        // the type of achievement
        protected short achievement_type;
        
        /// <summary>
        /// sets the achievement type
        /// </summary>
        public void SetAchievementType(String type)
        {
            achievement_type = 0;
            bool found = false;
            int i = 0;
            while ((i < ACHIEVEMENT_TYPES.Length) && (!found))
            {
                if (ACHIEVEMENT_TYPES[i] == type)
                {
                    achievement_type = (short)i;
                    found = true;
                }
                i++;
            }
        }

        /// <summary>
        /// returns a description of the achievement type
        /// </summary>
        public String GetAchievementType()
        {
            return(ACHIEVEMENT_TYPES[achievement_type]);
        }

        /// <summary>
        /// returns the achievement type as an integer
        /// </summary>
        public int GetAchievementTypeInt32()
        {
            return((int)achievement_type);
        }

        /// <summary>
        /// returns the achievement type as an integer
        /// </summary>
        public int GetAchievementTypeInt32(String achievement_type_description)
        {
            int result = -1;
            int i = 0;
            while ((i < ACHIEVEMENT_TYPES.Length) && (result == -1))
            {
                if (ACHIEVEMENT_TYPES[i] == achievement_type_description) result = i;
                i++;
            }
            return(result);
        }
        
        #endregion

        #region "saving and loading"

        public override XmlElement getXML(XmlDocument doc)
        {
            XmlElement elem = doc.CreateElement("Achievement");
            doc.DocumentElement.AppendChild(elem);
            xml.AddTextElement(doc, elem, "AchievementType", GetAchievementType());
            xml.AddTextElement(doc, elem, "Category", Category);
            xml.AddTextElement(doc, elem, "Description", Description);
            xml.AddTextElement(doc, elem, "AcquiredDate", GetAcquiredDate().ToString());
            xml.AddTextElement(doc, elem, "Value", Convert.ToString(Value));
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
            
            if (xnod.Name == "AchievementType")
                SetAchievementType(xnod.InnerText);
                
            if (xnod.Name == "Category")
                Category = xnod.InnerText;
                
            if (xnod.Name == "Description")
                Description = xnod.InnerText;
                
            if (xnod.Name == "AcquiredDate")
                SetAcquiredDate(DateTime.Parse(xnod.InnerText));

            if (xnod.Name == "Value")
                Value = Convert.ToInt32(xnod.InnerText);

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
