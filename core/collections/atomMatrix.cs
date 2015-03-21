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
using System.Collections;
using System.Xml;
using sluggish.utilities.xml;

namespace cuteagi.core
{
    /// <summary>
    /// stores a matrix of atoms
    /// </summary>
	public class atomMatrix : atomCollection
	{
	    // the number of dimensions of the matrix
	    public int[] dimension;
	    
	    // atoms within the matrix
		protected atom[] matrix;
		
		#region "constructors"
		
		public atomMatrix(int dimensionX, int dimensionY)
		{
		    SetAtomType("matrix");
		    dimension = new int[2];
		    dimension[0] = dimensionX;
		    dimension[1] = dimensionY;
		    matrix = new atom[dimensionX * dimensionY];
		}	

		public atomMatrix(int dimensionX, int dimensionY, int dimensionZ)
		{
		    dimension = new int[3];
		    dimension[0] = dimensionX;
		    dimension[1] = dimensionY;
		    dimension[2] = dimensionZ;
		    matrix = new atom[dimensionX * dimensionY * dimensionZ];
		}
		
		#endregion
		
        /// <summary>
        /// clears the map
        /// </summary>
		public void Clear()
        {
            for (int i = 0; i < matrix.Length; i++)
                matrix[i] = null;
            
            ClearMembers();
        }
        
        #region "conversions"
        
        /// <summary>
        /// convert a matrix coordinate into an array index
        /// </summary>
        private int MatrixPositionToIndex(int x, int y)
        {
            return((y * dimension[0]) + x);
        }

        /// <summary>
        /// convert a matrix coordinate into an array index
        /// </summary>
        private int MatrixPositionToIndex(int x, int y, int z)
        {
            return((z * dimension[1] * dimension[0]) + (y * dimension[0]) + x);
        }
        
        #endregion
        
        #region "add and remove members"
        
        /// <summary>
        /// adds a new atom to the matrix
        /// </summary>
		public void Add(atom a, int x, int y)
		{
		    matrix[MatrixPositionToIndex(x, y)] = a;
		    AddMember(a);
		}

        /// <summary>
        /// removes the given atom from the matrix
        /// </summary>
		public virtual void Remove(int x, int y)
		{
		    int index = MatrixPositionToIndex(x, y);
		    atom a = matrix[index];
		    matrix[index] = null;
		    RemoveMember(a);
		}
		
		#endregion

        #region "retreiving atoms from the matrix"

        /// <summary>
        /// returns an atom from the given matrix position
        /// </summary>
        public atom Get(int x, int y)
        {
            int index = MatrixPositionToIndex(x, y);
            return((atom)matrix[index]);
        }

        /// <summary>
        /// returns an atom from the given matrix position
        /// </summary>
        public atom Get(int x, int y, int z)
        {
            int index = MatrixPositionToIndex(x, y, z);
            return((atom)matrix[index]);
        }
        
        #endregion
	}
}
