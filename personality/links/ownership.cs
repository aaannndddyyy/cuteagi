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
    /// stores information about ownership of a thing
    /// </summary>
	public class ownership : linkTemporal
	{		
	    #region "constructors"
	
		public ownership(node thingOwned, node personOwner) : 
		    base(personOwner, thingOwned, null)
		{
		    SetLinkType("ownership");
		    atom_value = new truthvalue();
		}
		
		#endregion
		
        #region "types of ownership"
        
        // possible ownership types
        public String[] OWNERSHIP_TYPES = { "", 
                                            "",
                                            "" };
        
        // the type of ownership
        protected short ownership_type;
        
        /// <summary>
        /// sets the ownership type
        /// </summary>
        public void SetOwnershipType(String type)
        {
            ownership_type = 0;
            bool found = false;
            int i = 0;
            while ((i < OWNERSHIP_TYPES.Length) && (!found))
            {
                if (OWNERSHIP_TYPES[i] == type)
                {
                    ownership_type = (short)i;
                    found = true;
                }
                i++;
            }
        }

        /// <summary>
        /// returns a description of the ownership type
        /// </summary>
        public String GetOwnershipType()
        {
            return(OWNERSHIP_TYPES[ownership_type]);
        }

        /// <summary>
        /// returns the ownership type as an integer
        /// </summary>
        public int GetOwnershipTypeInt32()
        {
            return((int)ownership_type);
        }

        /// <summary>
        /// returns the ownership type as an integer
        /// </summary>
        public int GetOwnershipTypeInt32(String ownership_type_description)
        {
            int result = -1;
            int i = 0;
            while ((i < OWNERSHIP_TYPES.Length) && (result == -1))
            {
                if (OWNERSHIP_TYPES[i] == ownership_type_description) result = i;
                i++;
            }
            return(result);
        }
        
        #endregion
		
	}
}
