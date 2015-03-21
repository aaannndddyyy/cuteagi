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

namespace cuteagi.core
{	
    /// <summary>
    /// stores information about an object existing in space and time
    /// </summary>
	public class nodeSpatioTemporal : nodeSpatial
	{
		public eventTemporal node_event;

        #region "constructors"
        
		public nodeSpatioTemporal()
		{
		    SetFlag(atom.FLAG_TEMPORAL);
		    node_event = new eventTemporal();		
		}

		public nodeSpatioTemporal(float x, float y, DateTime t) : base(x, y)
		{
		    SetFlag(atom.FLAG_TEMPORAL);
		    node_event = new eventTemporal();		
		    SetTime(t, t);
		}

		public nodeSpatioTemporal(float x, float y, float z, DateTime t) : base(x, y, z)
		{
		    SetFlag(atom.FLAG_TEMPORAL);
		    node_event = new eventTemporal();
		    SetTime(t, t);
		}
		
		#endregion
		
        #region "date that something occured"
        
        public void SetAcquiredDate(DateTime date)
        {
            SetTime(date, date);
        }

        public DateTime GetAcquiredDate()
        {
            return(node_event.start_time);
        }
        
        public void SetRelinquishedDate(DateTime date)
        {
            node_event.end_time = date;
        }

        public DateTime GetRelinquishedDate()
        {
            return(node_event.end_time);
        }

        #endregion

		public void SetTime(DateTime start, DateTime end)
		{
		    node_event.start_time = start;
		    node_event.end_time = end;
		}

		public DateTime GetStartTime()
		{
		    return(node_event.start_time);		    
		}

		public DateTime GetEndTime()
		{
		    return(node_event.end_time);		    
		}

		public void SetStartTime(DateTime t)
		{
		    node_event.start_time = t;		    
		}

		public void SetEndTime(DateTime t)
		{
		    node_event.end_time = t;
		}

	}
}
