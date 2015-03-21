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
    public class skill : nodeTemporal
    {
        public truthvalue Strength;

        #region "constructors"

        private void initSkill()
        {
            SetNodeType("skill");
            Strength = new truthvalue();
        }
        
        public skill()
        {
            initSkill();            
        }

        public skill(String description)
        {
            initSkill();            
            SetDescription(description);
        }
        
        #endregion

        #region "types of skill"
        
        // possible skill types
        public String[] SKILL_TYPES = { "communication", "planning", "human relations", "organisational", "working", 
                                        "",
                                        "" };
        
        // the type of skill
        protected short skill_type;
        
        /// <summary>
        /// sets the skill type
        /// </summary>
        public void SetSkillType(String type)
        {
            skill_type = 0;
            bool found = false;
            int i = 0;
            while ((i < SKILL_TYPES.Length) && (!found))
            {
                if (SKILL_TYPES[i] == type)
                {
                    skill_type = (short)i;
                    found = true;
                }
                i++;
            }
        }

        /// <summary>
        /// returns a description of the skill type
        /// </summary>
        public String GetSkillType()
        {
            return(SKILL_TYPES[skill_type]);
        }

        /// <summary>
        /// returns the skill type as an integer
        /// </summary>
        public int GetSkillTypeInt32()
        {
            return((int)skill_type);
        }

        /// <summary>
        /// returns the skill type as an integer
        /// </summary>
        public int GetSkillTypeInt32(String skill_type_description)
        {
            int result = -1;
            int i = 0;
            while ((i < SKILL_TYPES.Length) && (result == -1))
            {
                if (SKILL_TYPES[i] == skill_type_description) result = i;
                i++;
            }
            return(result);
        }
        
        #endregion
        
        #region "description of the skill"
        
        public String GetDescription()
        {
            return(Name);
        }
        
        public void SetDescription(String desc)
        {
            Name = desc;
        }
        
        #endregion

        #region "saving and loading"

        public override XmlElement getXML(XmlDocument doc)
        {
            XmlElement elem = doc.CreateElement("Skill");
            doc.DocumentElement.AppendChild(elem);
            xml.AddTextElement(doc, elem, "SkillType", GetSkillType());
            xml.AddTextElement(doc, elem, "Description", GetDescription());
            xml.AddTextElement(doc, elem, "Strength", Strength.ToString());
            xml.AddTextElement(doc, elem, "AcquiredDate", GetAcquiredDate().ToString());
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
            
            if (xnod.Name == "SkillType")
                SetSkillType(xnod.InnerText);
                
            if (xnod.Name == "Description")
                SetDescription(xnod.InnerText);
                
            if (xnod.Name == "Strength")
                Strength = truthvalue.FromString(xnod.InnerText);

            if (xnod.Name == "AcquiredDate")
                SetAcquiredDate(DateTime.Parse(xnod.InnerText));
                
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
