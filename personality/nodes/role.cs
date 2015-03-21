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
    public class role : nodeTemporal
    {
        public profession Profession;
        public String Description;
        public String Affect;
        public location Locn;
        public ArrayList Skills;

        #region "construcors"
        
        public role()
        {
            SetNodeType("role");
            Skills = new ArrayList();
            Locn = new location();
            Profession = new profession();
        }
        
        #endregion

        #region "saving and loading"

        public override XmlElement getXML(XmlDocument doc)
        {
            XmlElement elem = doc.CreateElement("Role");
            doc.DocumentElement.AppendChild(elem);
            
            elem.AppendChild(Profession.getXMLIdentifier(doc));
            
            xml.AddTextElement(doc, elem, "Description", Description);
            xml.AddTextElement(doc, elem, "Started", GetAcquiredDate().ToString());
            xml.AddTextElement(doc, elem, "Affect", Affect);
            
            if (Skills.Count > 0)
            {
                XmlElement elemSkills = doc.CreateElement("Skills");
                elem.AppendChild(elemSkills);
                
                for (int i = 0; i < Skills.Count; i++)
                {
                    skill s = (skill)Skills[i];
                    elemSkills.AppendChild(s.getXML(doc));
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
            
            if (xnod.Name == "Description")
                Description = xnod.InnerText;
                
            if (xnod.Name == "Started")
                SetAcquiredDate(DateTime.Parse(xnod.InnerText));

            if (xnod.Name == "Affect")
                Affect = xnod.InnerText;

            if (xnod.Name == "Location")
            {
                Locn = new location();
                Locn.LoadFromXml(xnod, level);
            }

            if (xnod.Name == "Skills")
            {
                Skills.Clear();
                for (int i = 0; i < xnod.ChildNodes.Count; i++)
                {
                    skill s = new skill();
                    s.LoadFromXml((XmlNode)xnod.ChildNodes[i], level);
                    Skills.Add(s);
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
