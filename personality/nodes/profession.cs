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
using System.Text;
using sluggish.utilities.xml;
using cuteagi.core;

namespace cuteagi.personalitymodel
{
    public class profession : node
    {
        // social classes, taken from: Class, A Guide Through the American Status System (1983)
        // See http://en.wikipedia.org/wiki/Paul_Fussell
        public const int CLASS_TOP = 8;          // the super-rich, heirs to huge fortunes
        public const int CLASS_UPPER = 7;        // rich celebrities and people who can afford full-time domestic staff
        public const int CLASS_UPPER_MIDDLE = 6; // self-made well-educated professionals
        public const int CLASS_MIDDLE = 5;       // office workers
        public const int CLASS_HIGH_PROLE = 4;   // skilled blue-collar workers
        public const int CLASS_MID_PROLE = 3;    // workers in factories and the service industry
        public const int CLASS_LOW_PROLE = 2;    // manual laborers
        public const int CLASS_DESTITUTE = 1;    // the unemployed or homeless
        public const int CLASS_BOTTOM = 0;       // those incarcerated in prisons and institutions
    
        public String Name;
        public int SocialClass = 0;

        //skill levels for this profession
        public truthvalue Manual;
        public truthvalue Clerical;
        public truthvalue Leadership;
        public truthvalue SocialStatus;
        public truthvalue Religious;
        public truthvalue Enforcement;
        public truthvalue Entertainment;
        public truthvalue Teaching;
        
        #region "constructors"
        
        private void init()
        {
            SetNodeType("profession");
            Manual = new truthvalue();
            Clerical = new truthvalue();
            Leadership = new truthvalue();
            SocialStatus = new truthvalue();
            Religious = new truthvalue();
            Enforcement = new truthvalue();
            Entertainment = new truthvalue();
            Teaching = new truthvalue();
        }
        
        public profession()
        {
            init();
        }

        public profession(String name)
        {
            init();
            Name = name;
        }
        
        #endregion
        
        #region "saving and loading"

        public override XmlElement getXMLIdentifier(XmlDocument doc)
        {
            XmlElement elem = doc.CreateElement("Profession");
            doc.DocumentElement.AppendChild(elem);

            xml.AddTextElement(doc, elem, "Name", Name);
            return(elem);
        }

        public override XmlElement getXML(XmlDocument doc)
        {
            XmlElement elem = doc.CreateElement("Profession");
            doc.DocumentElement.AppendChild(elem);

            xml.AddTextElement(doc, elem, "Name", Name);
            xml.AddTextElement(doc, elem, "Class", SocialClass.ToString());

            XmlElement elemDimensions = doc.CreateElement("Dimensions");
            elem.AppendChild(elemDimensions);

            xml.AddTextElement(doc, elemDimensions, "Manual", Manual.ToString());
            xml.AddTextElement(doc, elemDimensions, "Clerical", Clerical.ToString());
            xml.AddTextElement(doc, elemDimensions, "Leadership", Leadership.ToString());
            xml.AddTextElement(doc, elemDimensions, "SocialStstus", SocialStatus.ToString());
            xml.AddTextElement(doc, elemDimensions, "Religious", Religious.ToString());
            xml.AddTextElement(doc, elemDimensions, "Enforcement", Enforcement.ToString());
            xml.AddTextElement(doc, elemDimensions, "Entertainment", Entertainment.ToString());
            xml.AddTextElement(doc, elemDimensions, "Teaching", Teaching.ToString());
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
                
            if (xnod.Name == "SocialClass")
                SocialClass = Convert.ToInt32(xnod.InnerText);
                
            if (xnod.Name == "Dimensions")
            {
            }
                
            if (xnod.Name == "Manual")
            {
                Manual = truthvalue.FromString(xnod.InnerText);
            }

            if (xnod.Name == "Clerical")
            {
                Clerical = truthvalue.FromString(xnod.InnerText);
            }

            if (xnod.Name == "Leadership")
            {
                Leadership = truthvalue.FromString(xnod.InnerText);
            }

            if (xnod.Name == "SocialStatus")
            {
                SocialStatus = truthvalue.FromString(xnod.InnerText);
            }

            if (xnod.Name == "Religious")
            {
                Religious = truthvalue.FromString(xnod.InnerText);
            }

            if (xnod.Name == "Enforcement")
            {
                Enforcement = truthvalue.FromString(xnod.InnerText);
            }

            if (xnod.Name == "Entertainment")
            {
                Entertainment = truthvalue.FromString(xnod.InnerText);
            }

            if (xnod.Name == "Teaching")
            {
                Teaching = truthvalue.FromString(xnod.InnerText);
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
