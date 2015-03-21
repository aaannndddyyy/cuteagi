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
    public class atom
    {        
        #region "flags"

        public const int FLAGS = 8;
        
        public const int FLAG_TOKEN = 0;
        public const int FLAG_IS_NODE = 1;
        public const int FLAG_IS_LINK = 2;
        public const int FLAG_IS_AGENT = 3;
        public const int FLAG_TEMPORAL = 4;
        public const int FLAG_SPATIAL = 5;
        public const int FLAG_SPATIAL_DISPLACEMENT = 6;
        public const int FLAG_FIRING = 7;
        
        // various flags
        protected bool[] flags;

        /// <summary>
        /// sets the state of a flag
        /// </summary>
        public void SetFlag(int flag_index, bool state)
        {
            // create some flags if necessary
            if (flags == null) flags = new bool[FLAGS];
        
            flags[flag_index] = state;
        }

        /// <summary>
        /// sets the state of a flag
        /// </summary>
        public void SetFlag(int flag_index)
        {
            // create some flags if necessary
            if (flags == null) flags = new bool[FLAGS];

            flags[flag_index] = true;
        }

        /// <summary>
        /// clears the state of a flag
        /// </summary>
        public void ClearFlag(int flag_index)
        {
            if (flags != null) flags[flag_index] = false;
        }

        /// <summary>
        /// returns the state of a flag
        /// </summary>
        public bool GetFlag(int flag_index)
        {
            if (flags != null)
                return(flags[flag_index]);
            else
                return(false);
        }
        
        #endregion
        
        #region "unique identifiers"
        
        // a unique identifier for the atom
        public long atom_ID;
        
        protected String Name;
        
        public virtual String GetName()
        {
            return(Name);
        }

        public virtual void SetName(String Name)
        {
            this.Name = Name;
        }
        
        #endregion
        
        #region "routine admin"
        
        // used to indicate that this atom is to be removed
        public bool garbage_collect;
        
        /// <summary>
        /// TODO returns a copy of this atom
        /// </summary>
        public virtual atom Clone()
        {
            atom cpy = null;
            return(cpy);
        }
        
        #endregion

        #region "set membership"
        
        // the collection of atoms of which this is a member
        public atomCollection member_of_collection;

        #endregion
        
        #region "current state of the atom"
        
        // truth value for the atom        
        public truthvalue atom_value;
        
        #endregion
        
        #region "importance value used to define attentional state"
        
        public const int CHANNEL_IMPORTANCE = 0;  // the activation channel base index used to store importance levels
        public const int IMPORTANCE_LEVELS = 2;   // the number of importance levels
        
        // note that importance levels are used to represent
        // importance over different time scales, such as short term or long term
        public const int SHORT_TERM_IMPORTANCE = 0;
        public const int LONG_TERM_IMPORTANCE = 1;
                
        /// <summary>
        /// gets the importance value for this atom at the given importance level
        /// </summary>
		public float GetImportance(int level)
		{
		    return(GetActivation(CHANNEL_IMPORTANCE + level));
		}

        /// <summary>
        /// sets the importance value for this atom at the given impirtance level
        /// </summary>
		public void SetImportance(float imp, int level)
		{
		    SetActivation(imp, CHANNEL_IMPORTANCE + level);
		}
		
		#endregion

        #region "activation levels"
        
        // activation consists of an array of floating point
        // values representing different activation channels

        // number of channels used to represent activation
        public const int ACTIVATION_CHANNELS = 6;

        // how active has this atom been
        protected float[] activation;
        
        /// <summary>
        /// gets the activation value for this atom
        /// </summary>
		public float GetActivation(int channel)
		{
		    if (activation != null)
		        return (activation[channel]);
		    else
		        return(0);
		}

        /// <summary>
        /// gets the activation values
        /// </summary>
		public float[] GetActivation()
		{
		    if (activation != null)
		        return (activation);
		    else
		        return(null);
		}

        /// <summary>
        /// sets the activation value for this atom
        /// </summary>
		public void SetActivation(float activity, int channel)
		{
		    if (activation == null) activation = new float[ACTIVATION_CHANNELS];
		    activation[channel] = activity;
		}
		
		#endregion
		
		#region "energy level - how much work has this atom done?"
        
        // how much energy does this atom contain 
        protected float energy;        

        /// <summary>
        /// gets the energy value for this atom
        /// </summary>
		public float GetEnergy()
		{
		    return (energy);
		}

        /// <summary>
        /// sets the energy value for this atom
        /// </summary>
		public void SetEnergy(float e)
		{
		    energy = e;
		}
                
        #endregion        
                
        #region "connectivity"

        // connectivity to collections of other atoms
        public atomCollection atoms_incoming, atoms_outgoing;

        /// <summary>
        /// adds an atom to the incoming set
        /// </summary>
        public void AddToIncomingSet(atom a)
        {
            AddToSet(a, ref atoms_incoming);
        }

        /// <summary>
        /// removes an atom from the incoming set
        /// </summary>
        public void RemoveFromIncomingSet(atom a)
        {
            RemoveFromSet(a, atoms_incoming);
        }

        /// <summary>
        /// adds an atom to the outgoing set
        /// </summary>
        public void AddToOutgoingSet(atom a)
        {
            AddToSet(a, ref atoms_outgoing);
        }

        /// <summary>
        /// removes an atom from the outgoing set
        /// </summary>
        public void RemoveFromOutgoingSet(atom a)
        {
            RemoveFromSet(a, atoms_outgoing);
        }

        /// <summary>
        /// adds an atom to the incoming or outgoing set
        /// </summary>
        private void AddToSet(atom a, ref atomCollection atomset)
        {
            if (atomset == null) atomset = new atomCollection();
            atomset.Add(a);
        }

        /// <summary>
        /// removes an atom from the incoming or outgoing set
        /// </summary>
        private void RemoveFromSet(atom a, atomCollection atomset)
        {
            if (atomset != null)
            {
                atomset.Remove(a);
            }
        }
        
        /// <summary>
        /// clears the incoming set
        /// </summary>
        public void ClearIncoming()
        {
            if (atoms_incoming != null)
                atoms_incoming.Clear();
        }

        /// <summary>
        /// clears the outgoing set
        /// </summary>
        public void ClearOutgoing()
        {
            if (atoms_outgoing != null)
                atoms_outgoing.Clear();
        }

        #endregion
                
        #region "types of atom"
        
        // possible atom types
        public String[] ATOM_TYPES = { "atom", "node", "link", 
                                       "collection", "map", "agent" };
        
        // the type of atom
        protected byte atom_type;
        
        /// <summary>
        /// sets the atom type
        /// </summary>
        public void SetAtomType(String type)
        {
            atom_type = 0;
            bool found = false;
            int i = 0;
            while ((i < ATOM_TYPES.Length) && (!found))
            {
                if (ATOM_TYPES[i] == type) atom_type = (byte)i;
                i++;
            }
        }

        /// <summary>
        /// returns a description of the atom type
        /// </summary>
        public String GetAtomType()
        {
            return(ATOM_TYPES[atom_type]);
        }

        /// <summary>
        /// returns the atom type as an integer
        /// </summary>
        public int GetAtomTypeInt32()
        {
            return((int)atom_type);
        }

        /// <summary>
        /// returns the atom type as an integer
        /// </summary>
        public int GetAtomTypeInt32(String atom_type_description)
        {
            int result = -1;
            int i = 0;
            while ((i < ATOM_TYPES.Length) && (result == -1))
            {
                if (ATOM_TYPES[i] == atom_type_description) result = i;
                i++;
            }
            return(result);
        }
        
        #endregion
        
        
        #region "constructors"
        
        public atom()
        {
        }
        
        #endregion
        
        #region "conversions"

        /// <summary>
        /// convert the atom to a string
        /// <summary>
        public override String ToString()
        {
            return(atom_ID.ToString());
        }

        /// <summary>
        /// convert to an xml element
        /// <summary>
        public virtual XmlElement ToXml(XmlDocument doc)
        {
            return(getXML(doc));
        }
        
        #endregion
        
        #region "saving and loading"
        
        public virtual XmlElement getXMLIdentifier(XmlDocument doc)
        {
            return(null);
        }
        
        public virtual XmlElement getXML(XmlDocument doc)
        {
            XmlElement elem = doc.CreateElement("Atom");
            doc.DocumentElement.AppendChild(elem);
            xml.AddTextElement(doc, elem, "ID", atom_ID.ToString());
            xml.AddTextElement(doc, elem, "Type", atom_type.ToString());
            if (Name != "") xml.AddTextElement(doc, elem, "Name", Name);
            
            if (member_of_collection != null)
                if (member_of_collection.Name != "")
                    xml.AddTextElement(doc, elem, "MemberOfCollection", member_of_collection.Name);
            
            if (flags != null)
            {
                String str = "";
                for (int i = 0; i < flags.Length; i++)
                {
                    str += Convert.ToString(flags[i]);
                    if (i < flags.Length-1) str += ",";
                }
                xml.AddTextElement(doc, elem, "Flags", str);
            }
            
            if (energy != 0) xml.AddTextElement(doc, elem, "Energy", energy.ToString());

            if (activation != null)
            {
                String str = "";
                for (int i = 0; i < activation.Length; i++)
                {
                    str += Convert.ToString(activation[i]);
                    if (i < activation.Length-1) str += ",";
                }
                xml.AddTextElement(doc, elem, "Activation", str);
            }
            
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
            if (xnod.Name == "ID")
                atom_ID = Convert.ToInt64(xnod.InnerText);
                
            if (xnod.Name == "Type")
                atom_type = Convert.ToByte(xnod.InnerText);

            if (xnod.Name == "Name")
                Name = xnod.InnerText;

            if (xnod.Name == "MemberOfCollection")
            {
                String CollectionName = xnod.InnerText;
            }
            
            if (xnod.Name == "Flags")
            {
                flags = new bool[FLAGS];
                String[] liststr = xnod.InnerText.Split(',');
                for (int i = 0; i < liststr.Length; i++)
                    flags[i] = Convert.ToBoolean(liststr[i]);
            }
                
            if (xnod.Name == "Energy")
                energy = Convert.ToSingle(xnod.InnerText);
                
            if (xnod.Name == "Activation")
            {
                activation = new float[ACTIVATION_CHANNELS];
                String[] liststr = xnod.InnerText.Split(',');
                for (int i = 0; i < liststr.Length; i++)
                    activation[i] = Convert.ToSingle(liststr[i]);
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