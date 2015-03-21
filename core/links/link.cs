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
using sluggish.utilities.logging;
using sluggish.utilities.xml;

namespace cuteagi.core
{	
    /// <summary>
    /// represents a link in a graph
    /// </summary>
	public class link : atom
	{	
		#region "constructors"

        private void initLink(node source, 
                              node destination, 
                              truthvalue weight,
                              int linkType)
        {
		    SetAtomType("link");
		    SetFlag(atom.FLAG_IS_LINK);
		    		    
		    // set the link properties
		    AddToIncomingSet(source);
		    AddToOutgoingSet(destination);
		    atom_value = weight;
		    
		    // update the node links
		    source.AddOutgoingLink(this);
		    destination.AddIncomingLink(this);
		    
		    link_type = (byte)linkType;
        }
		
        /// <summary>
        /// constructor
        /// </summary>
		public link(node source, node destination, truthvalue weight)
		{
		    int linkType = GetLinkTypeInt32("inheritance");
		    initLink(source, destination, weight, linkType);
		}

        /// <summary>
        /// constructor
        /// </summary>
		public link(node source, node destination, truthvalue weight,
		            int linkType)
		{
		    initLink(source, destination, weight, linkType);
		}

        /// <summary>
        /// constructor
        /// </summary>
		public link(node source, node destination, truthvalue weight,
		            String linkTypeStr)
		{
		    int linkType = GetLinkTypeInt32(linkTypeStr);
		    initLink(source, destination, weight, linkType);
		}
		
		#endregion

        #region "types of link"
        
        // possible link types
        public String[] LINK_TYPES = { "hebbian", "inheritance", "inheritanceExtensional",
                                       "ownership", "locality", "belief", "relationship",
                                       "affect", "ownership", "similarity", "credibility",
                                       "familiarity", "partof", "propertyof", "journey" };
        
        // the type of link
        protected byte link_type;
        
        /// <summary>
        /// sets the link type
        /// </summary>
        public bool SetLinkType(String type)
        {
            link_type = 0;
            bool found = false;
            int i = 0;
            while ((i < LINK_TYPES.Length) && (!found))
            {
                if (LINK_TYPES[i] == type)
                {
                    link_type = (byte)i;
                    found = true;
                }
                else i++;
            }
            
            // if this type is not supported then log an offician complaint
            if (!found)
                EventLog.AddError("Invalid link type: " + type);
                
            return(found);
        }

        /// <summary>
        /// returns a description of the link type
        /// </summary>
        public String GetLinkType()
        {
            return(LINK_TYPES[link_type]);
        }

        /// <summary>
        /// returns the link type as an integer
        /// </summary>
        public int GetLinkTypeInt32()
        {
            return((int)link_type);
        }

        /// <summary>
        /// returns the link type as an integer
        /// </summary>
        public int GetLinkTypeInt32(String link_type_description)
        {
            int result = -1;
            int i = 0;
            while ((i < LINK_TYPES.Length) && (result == -1))
            {
                if (LINK_TYPES[i] == link_type_description) result = i;
                i++;
            }
            return(result);
        }
        
        #endregion
	
	
		
		#region "link state"
		
        /// <summary>
        /// gets the weight value for this link
        /// </summary>
		public float GetWeight()
		{
		    return (atom_value.GetValue());
		}

        /// <summary>
        /// sets the weight value for this link
        /// </summary>
		public void SetWeight(float w)
		{
		    atom_value.SetValue(w);
		}
		
		#endregion
		
		#region "learning"
				
		/// <summary>
		/// hebbian learning
		/// </summary>
		public virtual void Learn(float learning_rate,
		                          float association_time_period_mS)
		{
		    float w = GetWeight();
		
		    for (int i = 0; i < atoms_incoming.Count(); i++)
		    {
		        atom presynaptic = atoms_incoming.Get(i);
	            float presynaptic_importance = presynaptic.GetImportance(atom.SHORT_TERM_IMPORTANCE);
		        for (int j = 0; j < atoms_outgoing.Count(); j++)
		        {
		            atom postsynaptic = atoms_outgoing.Get(j);
	                float postsynaptic_importance = postsynaptic.GetImportance(atom.SHORT_TERM_IMPORTANCE);

                    // learning is more likely to occur if the atoms involved are being attended to
		            float co_importance = (presynaptic_importance * postsynaptic_importance);
		            if (co_importance != 0)
		            {		                		                    
			            // are these temporal nodes?
			            if ((presynaptic.GetFlag(atom.FLAG_TEMPORAL)) &&
			                (postsynaptic.GetFlag(atom.FLAG_TEMPORAL)))
			            {		            
			                //get the ideal weight value according to STDP
			                nodeTemporal a1 = (nodeTemporal)presynaptic;
			                nodeTemporal a2 = (nodeTemporal)postsynaptic;
			                float ideal_w = (a1.node_event).SpikeTimeDependentPlasticity(a2.node_event, association_time_period_mS);		                
			                
			                // move the existing weight towards the ideal		                
			                w += ((ideal_w - w) * learning_rate * co_importance);		            
			            }
			            else
			            {
			                // these are not temporal nodes, so use a conventional hebb rule
			                node a1 = (node)presynaptic;
			                node a2 = (node)postsynaptic;			                

		                    // subtract a small value so that small co-importances
		                    // reduce weight size
		                    co_importance -= 0.1f;
		                
		                    // increment the weight
			                w += (learning_rate * co_importance * (1.0f - (float)Math.Abs(w)));		                

			                // constrain weight value within range
			                if (w > 0.9f) w = 0.9f;
			                if (w < -0.9f) w = -0.9f;			            
			            }
			            
		            }
		        }
		    }
		    
		    // update the weight value of this link
		    SetWeight(w);
		}
		
		#endregion
		
        #region "saving and loading"
        
        public override XmlElement getXMLIdentifier(XmlDocument doc)
        {
            return(null);
        }

        protected XmlElement getLinkXML(XmlDocument doc)
        {
            XmlElement elem = doc.CreateElement("Link");
            doc.DocumentElement.AppendChild(elem);
            xml.AddTextElement(doc, elem, "ID", atom_ID.ToString());
            xml.AddTextElement(doc, elem, "LinkType", GetLinkType());
            if (Name != "") xml.AddTextElement(doc, elem, "Name", Name);
            
            if (member_of_collection != null)
                if (member_of_collection.GetName() != "")
                    xml.AddTextElement(doc, elem, "MemberOfCollection", member_of_collection.GetName());
            
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
        
        public override XmlElement getXML(XmlDocument doc)
        {        
            XmlElement elem = getLinkXML(doc);
            return (elem);
        }

        protected void LoadLinkFromXml(XmlNode xnod, int level)
        {
            if (xnod.Name == "ID")
                atom_ID = Convert.ToInt64(xnod.InnerText);
                
            if (xnod.Name == "LinkType")
                SetLinkType(xnod.InnerText);

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
        }
        
        
        /// <summary>
        /// parse an xml node
        /// </summary>
        /// <param name="xnod"></param>
        /// <param name="level"></param>
        public override void LoadFromXml(XmlNode xnod, int level)
        {
            LoadLinkFromXml(xnod, level);

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
