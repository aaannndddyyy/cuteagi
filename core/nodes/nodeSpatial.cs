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
	/// this node stores information about a spatial position
	/// </summary>
	public class nodeSpatial : node
	{
	    // spatial coordinate for each dimension
	    public float[] position;
	    
	    // the units used to represent position
	    public const byte SPATIAL_UNITS_MILLIMETRES = 0;
	    public const byte SPATIAL_UNITS_INCHES = 1;
	    public const byte SPATIAL_UNITS_METRES = 2;
	    public const byte SPATIAL_UNITS_FEET = 3;
	    public const byte SPATIAL_UNITS_KILOMETERS = 4;
	    public const byte SPATIAL_UNITS_MILES = 5;
	    public const byte SPATIAL_UNITS_ASTRONOMICAL_UNITS = 6;
	    public const byte SPATIAL_UNITS_DEGREES = 7;
	    public byte position_units = SPATIAL_UNITS_MILLIMETRES;
	    
	    // which axis is used to represent vertical displacement
	    public int axis_vertical = 2;

	    // which axis is used to represent foreward displacement
	    public int axis_forward = 0;
				
		#region "spatial comparissons"
		
		/// <summary>
		/// returns the distance to another atom
		/// </summary>
		/// <param name="other_atom"></param>
        public float GetDistance(nodeSpatial other_atom)
        {
            // find the smallest number of dimensions
            int dimensions = this.position.Length;
            if (other_atom.position.Length < dimensions)
                dimensions = other_atom.position.Length;
            
            float dist = 0;
            for (int i = 0; i < dimensions; i++)
            {
                float diff = position[i] - other_atom.position[i];
                dist += (diff * diff);
            }
            dist = (float)Math.Sqrt(dist);
            
            return(dist);
        }

		/// <summary>
		/// returns the relative distance of atom1 from this atom compared to atom2
		/// eg. "is X further away from you than Y?"
		/// </summary>
		/// <param name="other_atom"></param>
		public float GetDistanceRelative(nodeSpatial atom1, nodeSpatial atom2)
		{
		    // distance to the first atom
		    float dist1 = GetDistance(atom1);

		    // distance to the second atom
		    float dist2 = GetDistance(atom2);		    
		    
		    return(dist1 - dist2);
		}

		/// <summary>
		/// returns the distance to another atom in 2D
		/// </summary>
		/// <param name="other_atom"></param>
        public float GetDistance2D(nodeSpatial other_atom)
        {
            return(GetDistance(other_atom, 2));
        }

		/// <summary>
		/// returns the distance to another atom in 3D
		/// </summary>
		/// <param name="other_atom"></param>
        public float GetDistance3D(nodeSpatial other_atom)
        {
            return(GetDistance(other_atom, 3));
        }

		/// <summary>
		/// returns the distance to another atom
		/// </summary>
		/// <param name="other_atom"></param>
		/// <param name="max_dimensions">maximum number of dimensions to use when calculating distance (for example you might want to restrict distance calculation to 2D)</param>
        public float GetDistance(nodeSpatial other_atom, int max_dimensions)
        {
            float dist = 0;
            for (int i = 0; i < max_dimensions; i++)
            {
                float diff = position[i] - other_atom.position[i];
                dist += (diff * diff);
            }
            dist = (float)Math.Sqrt(dist);
            
            return(dist);
        }
		
		/// <summary>
		/// is the other atom at the same position as this, within some tollerance distance
		/// </summary>
		/// <param name="other_atom"></param>
		public bool AtSamePosition(nodeSpatial other_atom, float tollerance)
		{
		    float dist = GetDistance(other_atom);
		    if (dist < tollerance)
		        return(true);
		    else
		        return(false);
		}

		/// <summary>
		/// is the other atom above this one
		/// </summary>
		/// <param name="other_atom"></param>
		public float Above(nodeSpatial other_atom)
		{
		    float dist = position[axis_vertical] - other_atom.position[axis_vertical];
		    if (dist > 0)
		        return(dist);
		    else
		        return(-1);
		}

		/// <summary>
		/// is the other atom below this one
		/// </summary>
		/// <param name="other_atom"></param>
		public float Below(nodeSpatial other_atom)
		{
		    float dist = other_atom.position[axis_vertical] - position[axis_vertical];
		    if (dist > 0)
		        return(dist);
		    else
		        return(-1);
		}

		/// <summary>
		/// is the first atom more distant from this one than the second
		/// if so return the relative distance
		/// eg. "is X further away from you than Y?" 
		/// </summary>
		/// <param name="other_atom"></param>
		public float MoreDistant(nodeSpatial atom1, nodeSpatial atom2)
		{
		    // distance to the first atom
		    float dist1 = GetDistance(atom1);

		    // distance to the second atom
		    float dist2 = GetDistance(atom2);		    
		    
		    if (dist1 > dist2)
		        return(dist1 - dist2);
		    else
		        return(-1);
		}

		/// <summary>
		/// is the first atom closer to this one than the second
		/// if so return the relative distance
		/// eg. "is X closer to you than Y?"
		/// </summary>
		/// <param name="other_atom"></param>
		public float CloserTo(nodeSpatial atom1, nodeSpatial atom2)
		{
		    // distance to the first atom
		    float dist1 = GetDistance(atom1);

		    // distance to the second atom
		    float dist2 = GetDistance(atom2);		    
		    
		    if (dist1 < dist2)
		        return(dist2 - dist1);
		    else
		        return(-1);
		}
				
		#endregion

        #region "constructors"
		
		/// <summary>
		/// constructor
		/// </summary>
		public nodeSpatial()
		{
		    position = new float[3];
		    SetFlag(atom.FLAG_SPATIAL);
		}

		/// <summary>
		/// constructor
		/// </summary>
		public nodeSpatial(float x, float y)
		{
		    position = new float[2];
		    position[0] = x;
		    position[1] = y;
		    SetFlag(atom.FLAG_SPATIAL);
		}

		/// <summary>
		/// constructor
		/// </summary>
		public nodeSpatial(float x, float y, float z)
		{
		    position = new float[3];
		    position[0] = x;
		    position[1] = y;
		    position[2] = z;
		    SetFlag(atom.FLAG_SPATIAL);
		}
		
		#endregion		
	}
}
