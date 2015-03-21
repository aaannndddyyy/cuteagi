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
using System.IO;
using System.Xml;
using System.Collections;
using System.Text;
using sluggish.utilities.xml;
using cuteagi.core;

namespace cuteagi.personalitymodel
{
    /// <summary>
    /// stores information about a geographical location
    /// </summary>
    public class location : nodeSpatial
    {
        public String Country;

        #region "types of location"
        
        // possible location types
        public String[] LOCATION_TYPES = { "", 
                                           "",
                                           "" };
        
        // the type of location
        protected short location_type;
        
        /// <summary>
        /// sets the location type
        /// </summary>
        public void SetLocationType(String type)
        {
            location_type = 0;
            bool found = false;
            int i = 0;
            while ((i < LOCATION_TYPES.Length) && (!found))
            {
                if (LOCATION_TYPES[i] == type)
                {
                    location_type = (short)i;
                    found = true;
                }
                i++;
            }
        }

        /// <summary>
        /// returns a description of the location type
        /// </summary>
        public String GetLocationType()
        {
            return(LOCATION_TYPES[location_type]);
        }

        /// <summary>
        /// returns the location type as an integer
        /// </summary>
        public int GetLocationTypeInt32()
        {
            return((int)location_type);
        }

        /// <summary>
        /// returns the location type as an integer
        /// </summary>
        public int GetLocationTypeInt32(String location_type_description)
        {
            int result = -1;
            int i = 0;
            while ((i < LOCATION_TYPES.Length) && (result == -1))
            {
                if (LOCATION_TYPES[i] == location_type_description) result = i;
                i++;
            }
            return(result);
        }
        
        #endregion

        
        #region "latitude and longitude"
        
        public void SetLatitude(float lat)
        {
            position[1] = lat;
        }

        public float GetLatitude()
        {
            return(position[1]);
        }

        public void SetLongitude(float lng)
        {
            position[0] = lng;
        }

        public float GetLongitude()
        {
            return(position[0]);
        }
        
        #endregion
        
        #region "constructors"
        
        public location()
        {
            SetNodeType("location");
        }

        public location(String Name, String Country)
        {
            SetNodeType("location");
            this.Name = Name;
            this.Country = Country;
        }

        public location(String Name, float Latitude, float Longitude) : base(Longitude, Latitude)
        {
            SetNodeType("location");
            this.Name = Name;
        }
        
        #endregion

        #region "saving and loading"

        public override XmlElement getXMLIdentifier(XmlDocument doc)
        {
            XmlElement elem = doc.CreateElement("location");
            doc.DocumentElement.AppendChild(elem);
            xml.AddTextElement(doc, elem, "Name", GetName());
            xml.AddTextElement(doc, elem, "Country", Country);
            return (elem);
        }

        public override XmlElement getXML(XmlDocument doc)
        {
            XmlElement elem = doc.CreateElement("location");
            doc.DocumentElement.AppendChild(elem);
            xml.AddTextElement(doc, elem, "Name", GetName());
            xml.AddTextElement(doc, elem, "Country", Country);
            xml.AddTextElement(doc, elem, "Latitude", Convert.ToString(GetLatitude()));
            xml.AddTextElement(doc, elem, "Longitude", Convert.ToString(GetLongitude()));
            xml.AddTextElement(doc, elem, "LocationType", GetLocationType());
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
                SetName(xnod.InnerText);
                
            if (xnod.Name == "Country")
                Country = xnod.InnerText;
                
            if (xnod.Name == "Latitude")
                SetLatitude(Convert.ToSingle(xnod.InnerText));

            if (xnod.Name == "Longitude")
                SetLongitude(Convert.ToSingle(xnod.InnerText));
                
            if (xnod.Name == "LocationType")
                SetLocationType(xnod.InnerText);
                
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

    /// <summary>
    /// stores information about a location which has been visited or inhabited by a person
    /// </summary>
    public class personalityLocation
    {
        public location Locn;
        public DateTime Date;
        public String Affect;
    }
}
