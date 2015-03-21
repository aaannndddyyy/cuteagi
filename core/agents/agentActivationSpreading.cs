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
    /// this agent is used to spread activation throughout the network
    /// or nodes and links
    /// </summary>
	public class agentActivationSpreading : agent
	{
	    // random number generator
	    private Random rnd = new Random();
	    
	    // different sequences in which atoms can be updated
	    public const int SEQUENCE_STRICT = 0;
	    public const int SEQUENCE_RANDOM = 1;
	    public byte update_sequence = SEQUENCE_STRICT; 
	
	    // rate of conductance of each activation channel
	    protected float[] conductance;
	    
	    // default value for conductance
	    public const float DEFAULT_CONDUCTANCE = 0.05f;
	
		#region "constructors"

        /// <summary>
        /// initialise activetion spreading agent
        /// </summary>
        private void initAgentActivationSpreading(atomCollection members,
                                                  ArrayList activeChannels)
        {
            atoms = members;
        
		    // create default decay rates
		    conductance = new float[atom.ACTIVATION_CHANNELS];
		    
		    if (activeChannels == null)
		    {
		        // conduct through all channels
		        for (int i = 0; i < atom.ACTIVATION_CHANNELS; i++)
		            conductance[i] = DEFAULT_CONDUCTANCE;
		    }
		    else
		    {
		        // conduct only through a limited number of channels
		        for (int i = 0; i < activeChannels.Count; i++)
		        {
		            int channel = (int)activeChannels[i];
		            conductance[channel] = DEFAULT_CONDUCTANCE; 
		        }
		    }
        }
		
		public agentActivationSpreading(atomCollection members)
		{
		    initAgentActivationSpreading(members, null);
		}

		public agentActivationSpreading(atomCollection members,
		                                ArrayList activeChannels)
		{
		    initAgentActivationSpreading(members, activeChannels);
		}
		
		#endregion
		
		#region "condustance levels"
		
	    /// <summary>
	    /// sets the level of conductance for this channel
	    /// <summary>
		public void SetConductance(int channel, float conductance_value)
		{
		    conductance[channel] = conductance_value;
		}

	    /// <summary>
	    /// returns the level of conductance for this channel
	    /// <summary>
		public float GetConductance(int channel)
		{
		    return (conductance[channel]);
		}
		
		#endregion
		
		#region "update activations"
		
		/// <summary>
		/// activation function for activation spreading
		/// </summary>
		public virtual float ActivationFunction(float presynaptic,
		                                        float postsynaptic,
		                                        float weight,
		                                        float conduction_rate)
		{
		    float result = 0;
		    
		    // potential difference between pre and postsynaptic activation levels
		    float potential = presynaptic - postsynaptic;
		    
		    float change = potential * weight * conduction_rate;
		    
		    result = postsynaptic + change;
		    
		    return(result);
		}
		
		/// <summary>
		/// diffuses activity throughout the nodes and links
		/// </summary>
		private void DiffuseActivation()
		{
		    int no_of_atoms = atoms.Count();
		    int max_updates = updates_per_time_step;
		    if (max_updates > no_of_atoms) max_updates = no_of_atoms;
		    if (max_updates == 0) max_updates = no_of_atoms;
		
		    for (int tries = 0; tries < max_updates; tries++)
		    {
		        // decide what order updates take place
		        int index = 0;
		        switch(update_sequence)
		        {
		            case SEQUENCE_STRICT: { index = updates_elapsed; break; }
		            case SEQUENCE_RANDOM: { index = rnd.Next(no_of_atoms); break; }
		        }
		        
		        if ((index > -1) && (index < no_of_atoms))
		        {
		            atom atm = atoms.Get(index);
		            
		            // only use nodes
		            if (atm.GetFlag(atom.FLAG_IS_NODE))
		            {
		                node n = (node)atm;
		                
		                if (n.atoms_incoming != null)
		                {
		                    // get the activation levels for this node
		                    float[] postsynaptic = n.GetActivation();
		                    if (postsynaptic != null)
		                    {		               
		                        // look through the incoming links of this node
		                        for (int i = 0; i < n.atoms_incoming.Count(); i++)
		                        {		                            
		                            atom lnk = n.atoms_incoming.Get(i);
		                            if (lnk.GetFlag(atom.FLAG_IS_LINK))
		                            {
		                                float weight = ((link)lnk).GetWeight();
		                                
		                                // look through the (presynaptic) atoms of this link
		                                for (int j = 0; j < lnk.atoms_incoming.Count(); j++)
		                                {
		                                    atom incoming_node = lnk.atoms_incoming.Get(j);

		                                    float[] presynaptic = incoming_node.GetActivation();
		                                    
		                                    // create zero activations for presynaptic atom if needed
		                                    if (presynaptic == null)
		                                    {
		                                        incoming_node.SetActivation(0, 0);
		                                        presynaptic = incoming_node.GetActivation();
		                                    }
		                                    		                                    		                                    
		                                    // diffuse through channels
		                                    for (int ch = 0; ch < atom.ACTIVATION_CHANNELS; ch++)
		                                        postsynaptic[ch] = ActivationFunction(presynaptic[ch], postsynaptic[ch], weight, conductance[ch]);
		                                }
		                            }
		                        }
		                    }
		                }
		            }
		        }
		        updates_elapsed++;
		    }
		    
		    if (updates_elapsed > no_of_atoms)
		        updates_elapsed -= no_of_atoms;
		}
		
		protected override void StepAgent()
		{
		    if (atoms != null)
		    {
		        running = true;
		        DiffuseActivation();
		        running = false;
		    }
	    }

		protected override void RunAgent()
		{
		    StepAgent();
	    }
	    
	    #endregion

	}
}
