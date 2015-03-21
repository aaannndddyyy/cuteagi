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
using sluggish.utilities.xml;
using cuteagi.core;

namespace cuteagi.personalitymodel
{
    /// <summary>
    /// specifies an affective stance towards some object
    /// </summary>
	public class affect : link
	{
        #region "constructors"
        
		public affect(node affect_object, node person, float strength) : 
		    base(person, affect_object, null)
		{
		    SetLinkType("affect");
		    atom_value = new truthvalue(strength);
		}
		
		#endregion
		
        #region "types of affective stance"
        
        // possible affect types
        public String[] AFFECT_TYPES = { "", 
                                         "",
                                         "" };
        
        // the type of affect
        protected short affect_type;
        
        /// <summary>
        /// sets the affect type
        /// </summary>
        public void SetAffectType(String type)
        {
            affect_type = 0;
            bool found = false;
            int i = 0;
            while ((i < AFFECT_TYPES.Length) && (!found))
            {
                if (AFFECT_TYPES[i] == type)
                {
                    affect_type = (short)i;
                    found = true;
                }
                i++;
            }
        }

        /// <summary>
        /// returns a description of the affect type
        /// </summary>
        public String GetAffectType()
        {
            return(AFFECT_TYPES[affect_type]);
        }

        /// <summary>
        /// returns the affect type as an integer
        /// </summary>
        public int GetAffectTypeInt32()
        {
            return((int)affect_type);
        }

        /// <summary>
        /// returns the affect type as an integer
        /// </summary>
        public int GetAffectTypeInt32(String affect_type_description)
        {
            int result = -1;
            int i = 0;
            while ((i < AFFECT_TYPES.Length) && (result == -1))
            {
                if (AFFECT_TYPES[i] == affect_type_description) result = i;
                i++;
            }
            return(result);
        }
        
        #endregion
		
        #region "saving and loading"

        public override XmlElement getXML(XmlDocument doc)
        {            
            XmlElement elem = doc.CreateElement("LinkAffect");
            doc.DocumentElement.AppendChild(elem);
            xml.AddTextElement(doc, elem, "AffectType", GetAffectType());            
            
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
            
            if (xnod.Name == "AffectType")
                SetAffectType(xnod.InnerText);
                                
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
