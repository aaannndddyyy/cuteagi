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
	/// this atom stores information about some temporal event
	/// </summary>
	public class eventTemporal
	{
	    // start and end time of the event
		public DateTime start_time;
		public DateTime end_time;
				
		#region "temporal comparissons"
		
		/// <summary>
		/// did the other atom occur at the same time as this one?
		/// if so return an atom representing the overlap
		/// </summary>
		/// <param name="other_atom"></param>
		public eventTemporal AtSameTime(eventTemporal other_atom)
		{
		    eventTemporal overlap = null;
		    
		    if ((this.start_time >= other_atom.start_time) &&
		        (this.start_time <= other_atom.end_time))
		    {
		        overlap = new eventTemporal(this.start_time, other_atom.end_time);
		    }
		    
            if ((this.end_time >= other_atom.start_time) &&
		        (this.end_time <= other_atom.end_time))		         
		    {
		        overlap = new eventTemporal(other_atom.start_time, this.end_time);
		    }

		    if ((this.start_time >= other_atom.start_time) &&
		        (this.end_time <= other_atom.end_time))
		    {
		        overlap = new eventTemporal(this.start_time, this.end_time);
		    }

		    if ((other_atom.start_time >= this.start_time) &&
		        (other_atom.end_time <= this.end_time))
		    {
		        overlap = new eventTemporal(this.start_time, this.end_time);
		    }
		    
		    return(overlap);
		}
		
		/// <summary>
		/// did this atom occur before the given one
		/// if so return the time difference
		/// </summary>
		/// <param name="other_atom"></param>
		public TimeSpan Before(eventTemporal other_atom)
		{
		    TimeSpan time_difference = TimeSpan.Zero;
		    
		    if (this.end_time < other_atom.start_time)
		        time_difference = other_atom.start_time.Subtract(this.end_time);
		    
		    return(time_difference);
		}

		/// <summary>
		/// did this atom occur inside the given one
		/// if so return the time difference
		/// </summary>
		/// <param name="other_atom"></param>
		public TimeSpan Within(eventTemporal other_atom)
		{
		    TimeSpan time_difference = TimeSpan.Zero;
		    
		    if (this.end_time < other_atom.end_time)
		        if (this.start_time > other_atom.start_time)
		            time_difference = this.start_time.Subtract(other_atom.start_time);
		    
		    return(time_difference);
		}

		/// <summary>
		/// returns the time difference between this and another temporal atom
		/// </summary>
		/// <param name="other_atom"></param>
		public TimeSpan TimeDifference(eventTemporal other_atom)
		{
		    TimeSpan time_difference = other_atom.start_time.Subtract(this.end_time);		    
		    return(time_difference);
		}

		/// <summary>
		/// did this atom occur after the given one
		/// if so return the time difference
		/// </summary>
		/// <param name="other_atom"></param>
		public TimeSpan After(eventTemporal other_atom)
		{
		    TimeSpan time_difference = TimeSpan.Zero;
		    
		    if (this.start_time > other_atom.end_time)
		        time_difference = this.start_time.Subtract(other_atom.end_time);
		        
            return(time_difference);
		}
		
		#endregion

        #region "constructors"
		
		/// <summary>
		/// constructor
		/// </summary>
		public eventTemporal()
		{
		    start_time = DateTime.Now;
		    end_time = start_time;
		}

		/// <summary>
		/// constructor
		/// </summary>
		public eventTemporal(DateTime start_time, DateTime end_time)
		{
		    this.start_time = start_time;
		    this.end_time = end_time;
		}
		
		#endregion
		
		#region "temporal learning"
		
		/// <summary>
		/// returns the weight plasticity in the range -1 <= w <= 1 according to an STDP rule
		/// </summary>
		/// <param name="other_atom"></param>
		/// <param name="association_time_period_mS">the time period within which the weight is sensitive to being changed</param>
		public float SpikeTimeDependentPlasticity(eventTemporal other_atom,
		                                          float association_time_period_mS)
		{
		    float weight_plasticity = 0;
		    const float max_weight_value = 1.0f;
		    
		    TimeSpan s = TimeDifference(other_atom);
		    if (s.TotalMilliseconds < 0)
                weight_plasticity = max_weight_value * (float)Math.Exp(s.TotalMilliseconds / association_time_period_mS);
            else
                weight_plasticity = max_weight_value * (float)Math.Exp(s.TotalMilliseconds / (association_time_period_mS*2));
		    
		    return(weight_plasticity);
		}
		
		#endregion
	}
}
