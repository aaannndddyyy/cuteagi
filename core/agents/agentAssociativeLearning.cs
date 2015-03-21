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

namespace cuteagi.core
{	
	/// <summary>
	/// performs hebbian type adjustments of link weights
	/// </summary>
	public class agentAssociativeLearning : agent
	{
	    // random number generator
	    Random rnd = new Random();
	
	    // learning rate
	    public float learning_rate;
	    
	    // the maximum number of connections
	    public int maximum_connections;
	    
	    // time period within which the system is sensitive to weight changes
	    public float association_time_period_mS;
		
		#region "constructors"

        private void initAgentAssociativeLearning()
        {
		    learning_rate = 0.2f;
		    association_time_period_mS = 5000;
		    maximum_connections = 1000;
        }
		
		public agentAssociativeLearning()
		{
		    initAgentAssociativeLearning();
		}

		public agentAssociativeLearning(atomCollection members)
		{
		    atoms = members;
		    initAgentAssociativeLearning();
		}
		
		#endregion

		/// <summary>
		/// makes new connections
		/// </summary>
        private void MakeNewConnections()
        {
            link lnk = null;
            const int no_of_tries = 500;
            
            int i = 0;
            while ((i < no_of_tries) && (lnk == null))
            {
                // pick a pair of nodes at random
                int node1_index = rnd.Next(atoms.Count());
                int node2_index = rnd.Next(atoms.Count());
                if (node1_index != node2_index)
                {
                    atom a1 = atoms.Get(node1_index);
                    if (a1.GetFlag(atom.FLAG_IS_NODE))
                    {
                        atom a2 = atoms.Get(node2_index);
                        if (a2.GetFlag(atom.FLAG_IS_NODE))
                        {
                            //if (!a1.atoms_incoming.ContainsAtomID(a1.atom_ID))
                            {
	                            if ((a1.GetFlag(atom.FLAG_TEMPORAL)) &&
	                                (a2.GetFlag(atom.FLAG_TEMPORAL)))
	                            {
	                                // for temporal nodes are we inside the association time period?
	                            }
	                            else
	                            {
	                                // ordinary hebb rule - look for simmultaneous firing
	                            }
                            }
                        }
                    }
                }
                
                i++;
            }
        }

		/// <summary>
		/// adjusts link weight values according to a hebb type or STDP rule
		/// </summary>
        private void AdjustWeights()
        {
		    for (int i = 0; i < updates_per_time_step; i++)
		    {
		        int index = rnd.Next(atoms.Count()-1);
		        atom a  = (atom)atoms.Get(index);
		        if (a.GetFlag(atom.FLAG_IS_NODE))
		        {
		            node n = (node)a;
		            ArrayList links = n.GetLinks(a.atoms_incoming);
		            for (int j = 0; j < links.Count; j++)
		            {
		                link lnk = (link)links[j];
		                lnk.Learn(learning_rate, association_time_period_mS);
		            }
		        }
		    }
        }
		
		/// <summary>
		/// perform a single processing step
		/// </summary>
		protected override void StepAgent()
		{
		    if (atoms != null)
		    {
		        MakeNewConnections();		        
		        AdjustWeights();
		    }
		}

		/// <summary>
		/// 
		/// </summary>
		protected override void RunAgent()
		{
		    StepAgent();
		}
		
	}
}
