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
    public class societyevent : nodeSpatioTemporal
    {
        // if this is a sub-event of a more general event this points to the parent
        public societyevent parentevent;
    
        public String Type;                              // the type of event
        public String Description;                       // description of the event
        public ArrayList people = new ArrayList();       // people involved
        public ArrayList possessions = new ArrayList();  // possessions involved
        public ArrayList locations = new ArrayList();    // locations

        // more detailed events which occur inside this event
        public ArrayList subevents = new ArrayList();

        public String getPeopleList()
        {
            String str = "";
            for (int i = 0; i < people.Count; i++)
            {
                if (i > 0) str += ", ";
                str += ((personality)people[i]).GetName();
            }
            return (str);
        }

        public void Add(societyevent sub_event)
        {
            if (!subevents.Contains(sub_event))
                subevents.Add(sub_event);
        }

        #region "saving and loading"

        public XmlElement getXML(XmlDocument doc)
        {
            XmlElement elem = doc.CreateElement("Event");
            doc.DocumentElement.AppendChild(elem);
            xml.AddTextElement(doc, elem, "Type", Type);
            xml.AddTextElement(doc, elem, "Date", GetStartTime().ToString());
            xml.AddTextElement(doc, elem, "Description", Description);

            if (locations.Count > 0)
            {
	            XmlElement elemLocations = doc.CreateElement("Locations");
	            elem.AppendChild(elemLocations);
	            
	            for (int i = 0; i < locations.Count; i++)
	            {
	                location locn = (location)locations[i];
	                elemLocations.AppendChild(locn.getXMLIdentifier(doc));
	            }
            }
            if (people.Count > 0)
            {
	            XmlElement elemPeople = doc.CreateElement("People");
	            elem.AppendChild(elemPeople);
	            
	            for (int i = 0; i < people.Count; i++)
	            {
	                personality p = (personality)people[i];
	                elemPeople.AppendChild(p.getXMLIdentifier(doc));
	            }
            }

            if (possessions.Count > 0)
            {
	            XmlElement elemPossessions = doc.CreateElement("Possessions");
	            elem.AppendChild(elemPossessions);
	            
	            for (int i = 0; i < possessions.Count; i++)
	            {
	                possession p = (possession)possessions[i];
	                elemPossessions.AppendChild(p.getXMLIdentifier(doc));
	            }
            }

            return (elem);
        }

        /// <summary>
        /// parse an xml node
        /// </summary>
        /// <param name="xnod"></param>
        /// <param name="level"></param>
        public void LoadFromXml(XmlNode xnod, int level)
        {
            if (xnod.Name == "Type")
                Type = xnod.InnerText;
                
            if (xnod.Name == "Date")
                SetStartTime(DateTime.Parse(xnod.InnerText));
            
            if (xnod.Name == "Description")
                Description = xnod.InnerText;

            if (xnod.Name == "Locations")
            {
                locations.Clear();
                for (int i = 0; i < xnod.ChildNodes.Count; i++)
                {                            
                    location locn = new location();
                    locn.LoadFromXml((XmlNode)xnod.ChildNodes[i], level);
                    locations.Add(locn);
                }
            }

            if (xnod.Name == "People")
            {
                people.Clear();
                for (int i = 0; i < xnod.ChildNodes.Count; i++)
                {                            
                    personality p = new personality();
                    p.LoadFromXml((XmlNode)xnod.ChildNodes[i], level);
                    people.Add(p);
                }
            }

            if (xnod.Name == "Possessions")
            {
                possessions.Clear();
                for (int i = 0; i < xnod.ChildNodes.Count; i++)
                {                            
                    possession p = new possession();
                    p.LoadFromXml((XmlNode)xnod.ChildNodes[i], level);
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
