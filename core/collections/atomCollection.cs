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
//using cuteagi.nlp;
using sluggish.utilities.xml;

namespace cuteagi.core
{
    /// <summary>
    /// represents a collection of atoms
    /// <summary>
	public class atomCollection : atom
	{
	    // list of atoms within this collection
	    private ArrayList atoms;
	    	    
	    // indexes on various parameters
	    //private ArrayList index_value;
	    private ArrayList index_atom_type;
	    private ArrayList index_atom_name;
	    private ArrayList index_atom_ID;
	
		#region "constructors"
		
		public atomCollection()
		{
		    SetAtomType("collection");
		    atoms = new ArrayList();
		    //index_value = new ArrayList();
		    index_atom_type = new ArrayList();
		    index_atom_name = new ArrayList();
		    index_atom_ID = new ArrayList();
		}
		
		#endregion
		
		#region "updating indexes"
		
		/// <summary>
		/// returns the given index
		/// </summary>
		public ArrayList GetIndex(String index_name)
		{
		    ArrayList index = null;
		    
		    if (index_name == "ID") index = index_atom_ID;
		    if (index_name == "name") index = index_atom_name;
		    //if (index_name == "value") index = index_value;
		    return(index);
		}
		
		/// <summary>
		/// inserts the given atom into the atom type index
		/// </summary>
		private void UpdateIndexAtomType(atom a)
		{
		    int type = a.GetAtomTypeInt32();
		    
		    // add any new lists if needed
		    for (int i = index_atom_type.Count; i <= type; i++)
		        index_atom_type.Add(new ArrayList());
		        
		    ArrayList typelist = (ArrayList)index_atom_type[type];
		    typelist.Add(a);		    
		}

		/// <summary>
		/// inserts the given atom into the atom name index
		/// </summary>
		private void UpdateIndexAtomName(atom a, int atoms_index)
		{
            bool found = false;
		    String name = a.GetName();
		         
		    if (name != null)
		    {
                if (name != "")
	            {
		            int i = 0;
		            while ((i < index_atom_name.Count) && (!found))
		            {
		                int idx = (int)index_atom_name[i];
		                atom a2 = (atom)atoms[idx];
		                string a2_name = a2.GetName();
		                if (a2_name.CompareTo(name) > 0)
		                {
		                    index_atom_name.Insert(i, atoms_index);
		                    found = true;
		                }
		                i++;
		            }
	            }
            }
            
            if (!found)
                index_atom_name.Add(atoms_index);
		}

		/// <summary>
		/// inserts the given atom into the atom ID index
		/// </summary>
		private void UpdateIndexAtomID(atom a, int atoms_index)
		{
            bool found = false;
		    long ID = a.atom_ID;
		         
		    int i = 0;
		    while ((i < index_atom_ID.Count) && (!found))
		    {
		        int idx = (int)index_atom_ID[i];
		        atom a2 = (atom)atoms[idx];
		        long a2_ID = a2.atom_ID;
		        if (ID < a2_ID)
		        {
		            index_atom_ID.Insert(i, atoms_index);
		            found = true;
		        }
		        i++;
            }
            
            if (!found)
                index_atom_ID.Add(atoms_index);
		}
		
		
		/// <summary>
		/// shows indexes for diagnostic purposes
		/// </summary>
		public void ShowIndexes()
		{
		    // IDs
		    Console.Write("IDs: ");
		    for (int i = 0; i < index_atom_ID.Count; i++)
		    {
		        int idx = (int)index_atom_ID[i];
		        atom a = (atom)atoms[idx];
		        
		        if (i < index_atom_ID.Count-1)
		            Console.Write(a.atom_ID.ToString() + ", ");
		        else
		            Console.WriteLine(a.atom_ID.ToString());
		    }
		    
		    // names
		    Console.Write("Names: ");
		    for (int i = 0; i < index_atom_name.Count; i++)
		    {
		        int idx = (int)index_atom_name[i];
		        atom a = (atom)atoms[idx];
		        
		        if (i < index_atom_name.Count-1)
		            Console.Write(a.GetName() + ", ");
		        else
		            Console.WriteLine(a.GetName());
		    }
		}

		
		#endregion
		
		#region "range of values on a continuous scale"
		
	    // position of each atom within a continuous range of values
	    public ArrayList atom_position_within_range;

        /// <summary>
        /// returns the position within a continous range of the given atom index
        /// the range is typically 0.0 - 1.0
        /// </summary>
	    private float GetPositionWithinRange(int index)
	    {
	        float position = -1;
	        
	        if (atom_position_within_range != null)
	        {
	            if ((index > -1) && (index < atom_position_within_range.Count))
	            {
	                position = (float)atom_position_within_range[index];
	            }
	        }
	        
	        return(position);
	    }

        /// <summary>
        /// returns the position within a continous range of the given atom
        /// the range is typically 0.0 - 1.0
        /// </summary>
	    public float GetPositionWithinRange(atom a)
	    {
	        int index = atoms.IndexOf(a);
	        return(GetPositionWithinRange(index));
	    }

        /// <summary>
        /// returns the position within a continous range of the given piece of text
        /// the range is typically 0.0 - 1.0, with 0.5 returned as default
        /// </summary>
	    public float GetPositionWithinRange(String text, ref String text_found)
	    {
	        float position = -1;
	        text_found = "";
	        int hits = 0;
	        int index = 0;
	        while (index < atoms.Count)
	        {
	            atom a = (atom)atoms[index];
	            String name = a.GetName();
	            if (text.IndexOf(" " + name + " ") > -1)
	            {
	                text_found = name;
	                if (hits == 0)
	                    position = GetPositionWithinRange(index);
	                else
	                    position += GetPositionWithinRange(index);
	                hits++;
	                //sections = nlpUtilities.SplitWithString(text, name);
	            }
	            index++;
	        }
	        if (hits > 0) position /= hits;
	        return(position);
	    }

        /// <summary>
        /// returns a list of range positions from a collection of ranges
        /// using the given text
        /// </summary>
	    public ArrayList GetPositionWithinRangeCollection(String text)
	    {
	        String text_str = " " + text.ToLower() + " ";
	        ArrayList result = new ArrayList();
	        
	        for (int i = 0; i < atoms.Count; i++)
	        {
	            atom a = (atom)atoms[i];
	            if (a.GetAtomType() == "collection")
	            {
	                atomCollection collection = (atomCollection)a;
	                String text_found = "";
	                float range_position = collection.GetPositionWithinRange(text_str, ref text_found);
	                if (range_position > -1)
	                {
	                    result.Add(collection.GetName() + "," + text_found + "," + range_position.ToString());
	                }
	            }
	        }
	        
	        return(result);
	    }


        /// <summary>
        /// creates a list of concepts along a continuous range
        /// </summary>
        public void CreateRange(String[] name)
        {
            CreateRange(name, true);
        }
        
        /// <summary>
        /// creates a list of concepts along a continuous range
        /// </summary>
        public void CreateRange(String[] name, bool ascending_order)
        {
            Clear();
            for (int i = 0; i < name.Length; i++)
            {
                // create a new node with this name
                node n = new node();
                n.SetName(name[i]);
                
                // get a position within the range of values
                float range_position = i / (float)name.Length;
                if (!ascending_order) range_position = 1.0f - range_position;
                
                // add the node
                Add(n, range_position);
            }
        }
	    
		#endregion
		
		#region "adding and removing"		

        protected void ClearMembers()
        {
            atoms.Clear();
            index_atom_type.Clear();
            index_atom_name.Clear();
            index_atom_ID.Clear();
            //index_value.Clear();
            if (atom_position_within_range != null)
                atom_position_within_range.Clear();
        }

        public virtual void Clear()
        {
            ClearMembers();
        }
		
        /// <summary>
        /// adds a new member atom to the collection
        /// </summary>
		protected void AddMember(atom a)
		{
		    atoms.Add(a);
		    UpdateIndexAtomType(a);
		    UpdateIndexAtomName(a, atoms.Count-1);
		    UpdateIndexAtomID(a, atoms.Count-1);
		}
		
        /// <summary>
        /// adds a new atom to the collection
        /// </summary>
		public virtual void Add(atom a)
		{
		    AddMember(a);
		}

        /// <summary>
        /// adds a new atom to the collection, placing it at
        /// a particular position within a continuous range
        /// </summary>
		public void Add(atom a, float position_within_range)
		{
		    if (atom_position_within_range == null)
		        atom_position_within_range = new ArrayList();
		        
		    Add(a);
		    atom_position_within_range.Add(position_within_range);
		}

        /// <summary>
        /// removes the given atom from the collection
        /// </summary>
		protected void RemoveMember(atom a)
		{
		    int index = atoms.IndexOf(a);
		    if (index > -1)
		    {
		        atoms.Remove(a);
		        RemoveFromAtomTypeIndex(a);
		        RemoveFromAtomNameIndex(a);
		        RemoveFromAtomIDIndex(a);
		        if (atom_position_within_range != null)
		            atom_position_within_range.RemoveAt(index);
		    }
		}

        /// <summary>
        /// removes the given atom from the collection
        /// </summary>
		public virtual void Remove(atom a)
		{
		    RemoveMember(a);
		}

        /// <summary>
        /// removes an atom at the given index position
        /// </summary>
		public void RemoveAt(int index)
		{
		    atom a = (atom)atoms[index];
		    RemoveFromAtomTypeIndex(a);	
		    RemoveFromAtomNameIndex(a);
		    RemoveFromAtomIDIndex(a);
		    atoms.RemoveAt(index);
	        if (atom_position_within_range != null)
	            atom_position_within_range.RemoveAt(index);
		}

        /// <summary>
        /// removes an atom from the atom type index
        /// </summary>
		private void RemoveFromAtomTypeIndex(atom a)
		{
		    int type = a.GetAtomTypeInt32();
		    ArrayList typelist = (ArrayList)index_atom_type[type];
		    typelist.Remove(a);  // warning: this is slow compared to RemoveAt
		}

        /// <summary>
        /// removes an atom from the atom name index
        /// </summary>
		private void RemoveFromAtomNameIndex(atom a)
		{
		    int index = atoms.IndexOf(a);
		    if (index > -1)
		    {
		        index_atom_name.Remove((int)index);
		    }
		}

        /// <summary>
        /// removes an atom from the atom ID index
        /// </summary>
		private void RemoveFromAtomIDIndex(atom a)
		{
		    int index = atoms.IndexOf(a);
		    if (index > -1)
		    {
		        index_atom_ID.Remove((int)index);
		    }
		}
			
		#endregion

        /// <summary>
        /// returns an atom from the collection at the given index
        /// </summary>
        public atom Get(int index)
        {
            return((atom)atoms[index]);
        }

        /// <summary>
        /// returns the atom with the given ID
        /// </summary>
        public atom GetID(long ID)
        {
            int i = 0;
            atom a = null;
            while ((i < atoms.Count) && (a == null))
            {
                atom atm = (atom)atoms[i];
                if (atm.atom_ID == ID) a = atm;
                i++;
            }
            return(a);
        }


        /// <summary>
        /// returns the number of atom types within this collection
        /// </summary>
        public int GetNumberOfAtomTypes()
        {
            if (index_atom_type != null)
                return(index_atom_type.Count);
            else
                return(0);
        }
        
        /// <summary>
        /// returns a list of atoms of the given type
        /// </summary>
        public ArrayList GetAtomsOfType(String type)
        {
            int index =  GetAtomTypeInt32(type);
            if (index > -1)
            {
                if (index_atom_type.Count > index)
                    return((ArrayList)index_atom_type[index]);
                else 
                    return(null);
            }            
            else return(null);
        }

        /// <summary>
        /// returns a list of atoms of the given type
        /// </summary>
        public ArrayList GetAtomsOfType(int index)
        {
            if (index > -1)
            {
                if (index_atom_type.Count > index)
                    return((ArrayList)index_atom_type[index]);
                else 
                    return(null);
            }            
            else return(null);
        }

        /// <summary>
        /// returns a list of atoms with the given name
        /// </summary>
        public ArrayList GetAtomsWithName(String name)
        {
        /*
            if (name != "")
            {
                int index = 0;
                while (index < 
                if (index_name.Count > 0)
                    return((ArrayList)index_atom_name[index]);
                else 
                    return(null);
            }            
            else
            */
            return(null);
        }
		
		public int Count()
		{
		    return(atoms.Count);
		}
		
		#region "saving and loading"

        public override XmlElement getXML(XmlDocument doc)
        {
            XmlElement elem = doc.CreateElement("AtomCollection");
            doc.DocumentElement.AppendChild(elem);
            xml.AddTextElement(doc, elem, "ID", atom_ID.ToString());
                        
            String str = "";
            for (int i = 0; i < atoms.Count; i++)
            {
                atom a = (atom)atoms[i];
                str += a.atom_ID.ToString();
                if (i < atoms.Count-1) str += ",";
            }
            xml.AddTextElement(doc, elem, "Members", str);
            
            return (elem);
        }
        
        /// <summary>
        /// parse an xml node
        /// </summary>
        /// <param name="xnod"></param>
        /// <param name="level"></param>
        public override void LoadFromXml(XmlNode xnod, int level, atomCollection all_atoms)
        {
            if (xnod.Name == "ID")
                atom_ID = Convert.ToInt64(xnod.InnerText);
                
            if (xnod.Name == "Members")
            {
                String[] IDs = xnod.InnerText.Split(','); 
                for (int i = 0; i < IDs.Length; i++)
                {
                    long ID = Convert.ToInt64(IDs[i]);
                    if (all_atoms != null)
                    {
                        atom atm = all_atoms.GetID(ID);
                        Add(atm);
                    }
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
