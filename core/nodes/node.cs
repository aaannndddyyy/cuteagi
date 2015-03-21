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
using System.Collections;

namespace cuteagi.core
{
    /// <summary>
    /// represents a node in a graph
    /// </summary>
	public class node : atom
	{
	    public String wikipediaURL;
	
		#region "constructors"
		
		public node()
		{
		    SetAtomType("node");
		    SetFlag(atom.FLAG_IS_NODE);
		}
		
		#endregion
		
		public override atom Clone()
		{
		    node cpy = new node();
		    return(cpy);
		}
		
        #region "types of node"
        
        // possible node types
        public String[] NODE_TYPES = { "node", "spatial", "temporal",
                                       "spatiotemporal", "pose",
                                       "objectphysical",
                                       "word", "sentence", "paragraph", "chapter", "article",
                                       "achievement", "anecdote", "belief", "location",
                                       "opinion", "personality", "possession", "profession",
                                       "role", "skill" };
        
        // the type of node
        protected byte node_type;
        
        /// <summary>
        /// sets the node type
        /// </summary>
        public void SetNodeType(String type)
        {
            node_type = 0;
            bool found = false;
            int i = 0;
            while ((i < NODE_TYPES.Length) && (!found))
            {
                if (NODE_TYPES[i] == type) node_type = (byte)i;
                i++;
            }
        }

        /// <summary>
        /// returns a description of the node type
        /// </summary>
        public String GetNodeType()
        {
            return(NODE_TYPES[node_type]);
        }

        /// <summary>
        /// returns the node type as an integer
        /// </summary>
        public int GetNodeTypeInt32()
        {
            return((int)node_type);
        }

        /// <summary>
        /// returns the node type as an integer
        /// </summary>
        public int GetNodeTypeInt32(String node_type_description)
        {
            int result = -1;
            int i = 0;
            while ((i < NODE_TYPES.Length) && (result == -1))
            {
                if (NODE_TYPES[i] == node_type_description) result = i;
                i++;
            }
            return(result);
        }
        
        #endregion
		

        #region "neighbouring nodes"
        
        /// <summary>
        /// returns the given collection as a set of link objects
        /// </summary>
        public ArrayList GetLinks(atomCollection collection)
        {
            ArrayList links = new ArrayList();
            if (collection != null)
            {            
                for (int i = 0; i < collection.Count(); i++)
                {
                    atom a = collection.Get(i);
                    if (a.GetFlag(atom.FLAG_IS_LINK))
                    {
                        links.Add((link)a);
                    }
                }
            }
            return(links);
        }
        
        /// <summary>
        /// returns all neighbouring nodes
        /// </summary>
        public ArrayList GetNeighbours()
        {
            ArrayList neighbours_incoming = GetNeighbours(atoms_incoming);
            ArrayList neighbours_outgoing = GetNeighbours(atoms_outgoing);
            ArrayList all_neighbours = neighbours_incoming; 
            
            for (int  i = 0; i < neighbours_outgoing.Count; i++)
                all_neighbours.Add((atom)neighbours_outgoing[i]);
                
            return(all_neighbours);
        }

        /// <summary>
        /// returns all outgoing neighbouring nodes
        /// </summary>
        public ArrayList GetNeighboursOutgoing()
        {
            ArrayList neighbours_outgoing = GetNeighbours(atoms_outgoing);
            return(neighbours_outgoing);
        }
        
        /// <summary>
        /// returns all incoming neighbouring nodes
        /// </summary>
        public ArrayList GetNeighboursIncoming()
        {
            ArrayList neighbours_incoming = GetNeighbours(atoms_incoming);                
            return(neighbours_incoming);
        }

        /// <summary>
        /// returns the nodes associated with the given collection of links
        /// </summary>
        private ArrayList GetNeighbours(atomCollection links_collection)
        {
            ArrayList neighbours = new ArrayList();
            
            if (links_collection != null)
            {
                for (int i = 0; i < links_collection.Count(); i++)
                {
                    // each link
                    atom lnk = links_collection.Get(i);
                    if (lnk.atoms_incoming != null)
                    {
                        // each node referred to by the link
                        for (int j = 0; j < lnk.atoms_incoming.Count(); j++)
                        {
                            neighbours.Add(lnk.atoms_incoming.Get(j));
                        }
                    }
                }
            }
            
            return(neighbours);
        }
        
        #endregion

        #region "adding and removing links"

        /// <summary>
        /// adds a link to this node
        /// </summary>
		public void AddIncomingLink(link lnk)
		{
		    AddToIncomingSet(lnk);
		}

        /// <summary>
        /// adds a link to this node
        /// </summary>
		public bool AddIncomingLink(String link_type,
		                            node source,
		                            truthvalue weight)
		{
		    bool success = false;
		    link lnk = null;
		    
		    if (!linkTemporal.IsTemporalLinkType(link_type))
		        lnk = new link(source, this, weight);
		    else
		        lnk = new linkTemporal(source, this, weight);

		    success = lnk.SetLinkType(link_type);		 
		    if (success)		    
		        AddIncomingLink(lnk);
		    return(success);
		}
		
        /// <summary>
        /// adds a link to this node
        /// </summary>
		public bool AddOutgoingLink(String link_type,
		                            node destination,
		                            truthvalue weight)
		{
		    bool success = false;		    
		    link lnk = null;
		    
		    if (!linkTemporal.IsTemporalLinkType(link_type))
		        lnk = new link(this, destination, weight);
		    else
		        lnk = new linkTemporal(this, destination, weight);

		    success = lnk.SetLinkType(link_type);
		    if (success)
		        AddOutgoingLink(lnk);
		    return(success);
		}

        /// <summary>
        /// adds a link to this node
        /// </summary>
		public void AddOutgoingLink(link lnk)
		{
		    AddToIncomingSet(lnk);
		}
		
        /// <summary>
        /// remove a link from the incoming connections
        /// </summary>
		public void RemoveFromIncoming(link lnk)
		{
		    RemoveFromIncomingSet(lnk);
		}		
		
        /// <summary>
        /// remove a link from the outgoing connections
        /// </summary>
		public void RemoveFromOutgoing(link lnk)
		{
		    RemoveFromOutgoingSet(lnk);
		}
		
        /// <summary>
        /// clear all links to this node
        /// </summary>
		public void ClearLinks()
		{
		    ClearIncoming();
		    ClearOutgoing();
		}
		
		#endregion
		
        #region "saving and loading"
        
        protected XmlElement getNodeXML(XmlDocument doc)
        {
            XmlElement elem = doc.CreateElement("Node");
            doc.DocumentElement.AppendChild(elem);
            xml.AddTextElement(doc, elem, "ID", atom_ID.ToString());
            xml.AddTextElement(doc, elem, "NodeType", GetNodeType());
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

            if (wikipediaURL != "") xml.AddTextElement(doc, elem, "WikipediaURL", wikipediaURL);
            
            return (elem);
        }
        
        public override XmlElement getXML(XmlDocument doc)
        {        
            XmlElement elem = getNodeXML(doc);
            return (elem);
        }

        protected void LoadNodeFromXml(XmlNode xnod, int level)
        {
            if (xnod.Name == "ID")
                atom_ID = Convert.ToInt64(xnod.InnerText);
                
            if (xnod.Name == "NodeType")
                SetNodeType(xnod.InnerText);

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
            
            if (xnod.Name == "WikipediaURL")
                wikipediaURL = xnod.InnerText;
        }
        
        
        /// <summary>
        /// parse an xml node
        /// </summary>
        /// <param name="xnod"></param>
        /// <param name="level"></param>
        public override void LoadFromXml(XmlNode xnod, int level)
        {
            LoadNodeFromXml(xnod, level);

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
