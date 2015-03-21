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
	public class truthvalue
	{
	    protected float tval;
	    protected long count;

        #region "constructors"
        
        public truthvalue()
        {
        }
        
        public truthvalue(float v)
        {
            SetValue(v);
        }
        
        #endregion

        /// <summary>
        /// clear the current state
        /// </summary>
        public void Clear()
        {
            tval = 0;
            count = 0;
        }
        
        /// <summary>
        /// returns a copy of this truth value
        /// </summary>
        public truthvalue Clone()
        {
            truthvalue cpy = new truthvalue();
            cpy.tval = tval;
            cpy.count = count;
            return(cpy);
        }
        
        #region "conversions"
        
        public override String ToString()
        {
            String retval = tval.ToString();
            return(retval);
        }
        
	    public static truthvalue FromString(String str)
	    {
	        truthvalue t = new truthvalue();
	        t.SetValue(Convert.ToSingle(str));
	        return(t);
	    }        
        
        #endregion

        #region "averages"
        
        /// <summary>
        /// update an average value
        /// </summary>
		public void SetAverage(float v)
		{
		    tval += v;
		    count++;
		}

        /// <summary>
        /// get the average value
        /// </summary>
		public float GetAverage()
		{
		    if (count > 0)
		        return(tval / count);
		    else
		        return(0);
		}
		
		#endregion
		
		#region "simple set and get"
		
        /// <summary>
        /// add the given values together
        /// </summary>
		public void SumValue(float v)
		{
		    tval += v;
		    count = 1;
		}

        /// <summary>
        /// set the value
        /// </summary>
		public void SetValue(float v)
		{
		    tval = v;
		    count = 1;
		}
		
        /// <summary>
        /// return the value
        /// </summary>
		public float GetValue()
		{
		    return(tval);
		}
		
		#endregion
				
        #region "saving and loading"
        
        public virtual XmlElement getXML(XmlDocument doc)
        {
            XmlElement elem = doc.CreateElement("TruthValue");
            doc.DocumentElement.AppendChild(elem);
            xml.AddTextElement(doc, elem, "Value", tval.ToString());
            xml.AddTextElement(doc, elem, "Hits", count.ToString());
            
            return (elem);
        }

        /// <summary>
        /// parse an xml node
        /// </summary>
        /// <param name="xnod"></param>
        /// <param name="level"></param>
        /// <param name="all_atoms"></param>
        public virtual void LoadFromXml(XmlNode xnod, int level, atomCollection all_atoms)
        {
            LoadFromXml(xnod, level);
        }

        
        /// <summary>
        /// parse an xml node
        /// </summary>
        /// <param name="xnod"></param>
        /// <param name="level"></param>
        public virtual void LoadFromXml(XmlNode xnod, int level)
        {
            if (xnod.Name == "Value")
                tval = Convert.ToSingle(xnod.InnerText);
                
            if (xnod.Name == "Hits")
                count = Convert.ToInt64(xnod.InnerText);

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
