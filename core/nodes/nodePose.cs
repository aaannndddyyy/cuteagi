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
	public class nodePose : nodeSpatial
	{
	    // orientation
	    public float[] orientation;

        #region "constructors"
		
		/// <summary>
		/// constructor
		/// </summary>
		public nodePose()
		{
		    orientation = new float[3];
		}

		/// <summary>
		/// constructor
		/// </summary>
		public nodePose(float x, float y) : base(x, y)
		{
		    orientation = new float[2];
		}

		/// <summary>
		/// constructor
		/// </summary>
		public nodePose(float x, float y, float z) : base(x, y, z)
		{
		    orientation = new float[3];
		}
		
		#endregion		
	}
}
