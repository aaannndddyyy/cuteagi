/*
    CuteAGI
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

namespace cuteagi.core
{	
    /// <summary>
    /// a link which has temporal parameters associated with it
    /// </summary>
	public class linkTemporal : link
	{
		public eventTemporal link_event;

        #region "constructors"
		
		public linkTemporal(node source, node destination, truthvalue weight) :
		    base(source, destination, weight)
		{
		    SetFlag(atom.FLAG_TEMPORAL);
		}
		
		#endregion
		
		#region ""
		
		/// <summary>
		/// is the given link type a temporal one ?
		/// </summary>
		public static bool IsTemporalLinkType(String link_type)
		{
		    if (link_type == "journey")
		        return(true);
		    else
		        return(false);
		}
		
		#endregion
		
        #region "date that something occured"
        
        public void SetStartedDate(DateTime date)
        {
            link_event.start_time = date;
        }

        public DateTime GetStartedDate()
        {
            return(link_event.start_time);
        }

        public void SetEndedDate(DateTime date)
        {
            link_event.end_time = date;
        }

        public DateTime GetEndedDate()
        {
            return(link_event.end_time);
        }
        
        #endregion
		
		
        public override atom Clone()
		{
		    linkTemporal cpy = (linkTemporal)this.Clone();
		    return(cpy);
		}
		
		public void SetTime(DateTime start, DateTime end)
		{
		    link_event.start_time = start;
		    link_event.end_time = end;
		}
		
        #region "saving and loading"
                
        public override XmlElement getXML(XmlDocument doc)
        {        
            XmlElement elem = getLinkXML(doc);
            xml.AddTextElement(doc, elem, "StartedDate", GetStartedDate().ToString());
            xml.AddTextElement(doc, elem, "EndedDate", GetEndedDate().ToString());
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

            if (xnod.Name == "StartedDate")
                SetStartedDate(DateTime.Parse(xnod.InnerText));
            
            if (xnod.Name == "EndedDate")
                SetEndedDate(DateTime.Parse(xnod.InnerText));

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
