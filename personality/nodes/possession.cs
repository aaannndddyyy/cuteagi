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
using System.Text;
using sluggish.utilities.xml;
using cuteagi.core;

namespace cuteagi.personalitymodel
{
    /// <summary>
    /// stores information about a personal possession
    /// </summary>
    public class possession : nodeSpatioTemporal
    {
        public personality BelongsTo;
        public bool notOwned;
        public personality AcquiredFrom;
        public int Value;      // how valued is this possession by its owner

        #region "constructors"
        
        public possession()
        {
            SetNodeType("possession");
        }
        
        #endregion

        #region "types of possession"
        
        // possible possession types
        public String[] POSSESSION_TYPES = { "property", 
                                             "utensil",
                                             "furniture",
                                             "literature",
                                             "electrical",
                                             "art",
                                             "transport",
                                             "clothing",
                                             "music",
                                             "food", "drink" };
        
        // the type of possession
        protected short possession_type;
        
        /// <summary>
        /// sets the possession type
        /// </summary>
        public void SetPossessionType(String type)
        {
            possession_type = 0;
            bool found = false;
            int i = 0;
            while ((i < POSSESSION_TYPES.Length) && (!found))
            {
                if (POSSESSION_TYPES[i] == type)
                {
                    possession_type = (short)i;
                    found = true;
                }
                i++;
            }
        }

        /// <summary>
        /// returns a description of the possession type
        /// </summary>
        public String GetPossessionType()
        {
            return(POSSESSION_TYPES[possession_type]);
        }

        /// <summary>
        /// returns the possession type as an integer
        /// </summary>
        public int GetPossessionTypeInt32()
        {
            return((int)possession_type);
        }

        /// <summary>
        /// returns the possession type as an integer
        /// </summary>
        public int GetPossessionTypeInt32(String possession_type_description)
        {
            int result = -1;
            int i = 0;
            while ((i < POSSESSION_TYPES.Length) && (result == -1))
            {
                if (POSSESSION_TYPES[i] == possession_type_description) result = i;
                i++;
            }
            return(result);
        }
        
        #endregion


        #region "saving and loading"

        public override XmlElement getXMLIdentifier(XmlDocument doc)
        {
            XmlElement elem = doc.CreateElement("Possession");
            doc.DocumentElement.AppendChild(elem);

            xml.AddTextElement(doc, elem, "Name", Name);
            xml.AddTextElement(doc, elem, "Type", GetPossessionType());
            return(elem);
        }

        public override XmlElement getXML(XmlDocument doc)
        {
            XmlElement elem = doc.CreateElement("Possession");
            doc.DocumentElement.AppendChild(elem);

            if (BelongsTo != null)
            {            
                XmlElement elemBelongsTo = doc.CreateElement("BelongsTo");
                elem.AppendChild(elemBelongsTo);
                
                elemBelongsTo.AppendChild(BelongsTo.getXMLIdentifier(doc));            
            }
            
            xml.AddTextElement(doc, elem, "Name", Name);
            xml.AddTextElement(doc, elem, "PossessionType", GetPossessionType());
            xml.AddTextElement(doc, elem, "AcquiredDate", GetAcquiredDate().ToString());
            xml.AddTextElement(doc, elem, "notOwned", notOwned.ToString());
            xml.AddTextElement(doc, elem, "RelinquishedDate", GetRelinquishedDate().ToString());

            if (AcquiredFrom != null)
            {            
                XmlElement elemAcquiredFrom = doc.CreateElement("AcquiredFrom");
                elem.AppendChild(elemAcquiredFrom);
                
                elemAcquiredFrom.AppendChild(AcquiredFrom.getXMLIdentifier(doc));            
            }
                
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
            
            if (xnod.Name == "BelongsTo")
            {
                BelongsTo = new personality();
                BelongsTo.LoadFromXml(xnod, level);
            }

            if (xnod.Name == "Name")
                Name = xnod.InnerText;
                
            if (xnod.Name == "PossessionType")
                SetPossessionType(xnod.InnerText);
                
            if (xnod.Name == "AcquiredDate")
                SetAcquiredDate(DateTime.Parse(xnod.InnerText));
                
            if (xnod.Name == "notOwned")
                notOwned = Convert.ToBoolean(xnod.InnerText);

            if (xnod.Name == "RelinquishedDate")
                SetRelinquishedDate(DateTime.Parse(xnod.InnerText));

            if (xnod.Name == "AcquiredFrom")
            {
                AcquiredFrom = new personality();
                AcquiredFrom.LoadFromXml(xnod, level);
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
