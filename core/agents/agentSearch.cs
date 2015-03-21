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
    /// an agent used to search for stuff
    /// </summary>
	public class agentSearch : agent
	{
	    // the maximum number of search results to be returned
	    // if left as zero all results are returned
	    public int max_search_results;
	
	    // the parameter to be compared against
	    public String search_parameter;
	    
	    // an additional index value which may be used during the search
	    public int search_parameter_index;
	    
	    // current search state
	    private atomCollection search_types; // exemplars currently being searched for
	    private int search_type_index;       // current atom type being searched
	    private int search_atom_index;       // index of the current atom being searched
	    		
		#region "constructors"
		
		private void initAgentSearch(atomCollection search_pool)
		{
		    atoms = search_pool;
		    SetAtomType("agentSearch");
		}
		
		public agentSearch()
		{
		    initAgentSearch(null);
		}

		public agentSearch(atomCollection search_pool)
		{
		    initAgentSearch(search_pool);
		}

		public agentSearch(String searchParameter)
		{
		    initAgentSearch(null);
		    search_parameter = searchParameter;
		}

		public agentSearch(String searchParameter, 
		                   int searchParameterIndex)
		{
		    initAgentSearch(null);
		    search_parameter = searchParameter;
		    search_parameter_index = searchParameterIndex;
		}

		public agentSearch(atomCollection search_pool, 
		                   String searchParameter)
		{
		    initAgentSearch(search_pool);
		    search_parameter = searchParameter;
		}

		public agentSearch(atomCollection search_pool, 
		                   String searchParameter,
		                   int searchParameterIndex)
		{
		    initAgentSearch(search_pool);
		    search_parameter = searchParameter;
		    search_parameter_index = searchParameterIndex;
		}
		
		#endregion
		
		#region "perform the search"

        private const float INVALID = 9999;
        ArrayList working_set;
        ArrayList working_set_difference;

        /// <summary>
        /// records the difference score for each evaluated candidate atom
        /// </summary>
        private void AddToWorkingSet(atom candidate, float diff)
        {
            working_set.Add(candidate);
            working_set_difference.Add(diff);
        }
        
        /// <summary>
        /// sorts the working set into difference order and
        /// updates the outgoing collection
        /// </summary>
        private void SortWorkingSet()
        {
            int max = working_set.Count;
            if ((max > max_search_results) && (max_search_results > 0))
            {
                max = max_search_results;
            }
            
            for (int i = 0; i < max; i++)
            {                
                float diff1 = (float)working_set_difference[i];
                float min_diff = diff1;
                int min_diff_index = i;
                for (int j = i + 1; j < working_set.Count; j++)
                {                    
                    float diff2 = (float)working_set_difference[j];
                    if (diff2 < min_diff)
                    {
                        min_diff = diff2;
                        min_diff_index = j;
                    }
                }
                if (min_diff_index != i)
                {
                    // swap
                    atom a1 = (atom)working_set[i];
                    atom a2 = (atom)working_set[min_diff_index];
                    working_set[i] = a2;
                    working_set_difference[i] = min_diff;
                    working_set[min_diff_index] = a1;
                    working_set_difference[min_diff_index] = diff1;
                }
                
                // update the outgoing set
                AddToOutgoingSet((atom)working_set[i]);
            }                        
        }
        
        /// <summary>
        /// returns the difference between the result at the given index and the exemplar
        /// the working set must have been previously sorted
        /// </summary>
        public float GetDifference(int index)
        {
            float diff = 0;
            if (working_set != null)
            {
                if (index < working_set.Count)
                {
                    diff = (float)working_set_difference[index];
                }
            }
            return(diff);
        }
        
        /// <summary>
        /// returns the results of the search
        /// </summary>
        public atomCollection GetSearchResults()
        {
            return (atoms_outgoing);
        }

        /// <summary>
        /// returns a measure of the similarity between the two given atoms
        /// </summary>
		protected virtual float GetDifference(atom exemplar, atom candidate)
		{
		    float exemplar_value = 0;
		    float candidate_value = 0;
		    float diff = 0;
		    
		    if (search_parameter == "weight")
		    {
		        exemplar_value = ((link)exemplar).GetWeight();
		        candidate_value = ((link)candidate).GetWeight();
		        diff = exemplar_value - candidate_value;
		    }

		    if (search_parameter == "importance")
		    {
		        int importance_level = search_parameter_index;
		        exemplar_value = exemplar.GetImportance(importance_level);
		        candidate_value = candidate.GetImportance(importance_level);
		        diff = exemplar_value - candidate_value;
		    }

		    if (search_parameter == "energy")
		    {
		        exemplar_value = exemplar.GetEnergy();
		        candidate_value = candidate.GetEnergy();
		        diff = exemplar_value - candidate_value;
		    }

		    if (search_parameter == "average")
		    {
		        exemplar_value = exemplar.atom_value.GetAverage();
		        candidate_value = candidate.atom_value.GetAverage();
		        diff = exemplar_value - candidate_value;
		    }

		    if (search_parameter == "truth")
		    {
		        exemplar_value = exemplar.atom_value.GetValue();
		        candidate_value = candidate.atom_value.GetValue();
		        diff = exemplar_value - candidate_value;
		    }

            // the start time of the candidate must be between the
            // start and end time of the exemplar
		    if (search_parameter == "start time")
		    {
		        if ((exemplar.GetFlag(atom.FLAG_TEMPORAL)) && (exemplar.GetFlag(atom.FLAG_IS_NODE)))
		        {
		            if ((candidate.GetFlag(atom.FLAG_TEMPORAL)) && (candidate.GetFlag(atom.FLAG_IS_NODE)))
		            {
		                nodeTemporal exemplarTemporal = (nodeTemporal)exemplar;
		                nodeTemporal candidateTemporal = (nodeTemporal)candidate;
		                TimeSpan time_diff = candidateTemporal.node_event.start_time.Subtract(exemplarTemporal.node_event.start_time);
		                if (time_diff.TotalSeconds >= 0)
		                {
		                    if (candidateTemporal.node_event.start_time < exemplarTemporal.node_event.end_time)
		                    {
		                        diff = (float)time_diff.TotalSeconds;
		                    }
		                    else diff = INVALID;
		                }
		                else diff = INVALID;
		            }
		        }
		    }

            // the end time of the candidate must be between the
            // start and end time of the exemplar
		    if (search_parameter == "end time")
		    {
		        if ((exemplar.GetFlag(atom.FLAG_TEMPORAL)) && (exemplar.GetFlag(atom.FLAG_IS_NODE)))
		        {
		            if ((candidate.GetFlag(atom.FLAG_TEMPORAL)) && (candidate.GetFlag(atom.FLAG_IS_NODE)))
		            {
		                nodeTemporal exemplarTemporal = (nodeTemporal)exemplar;
		                nodeTemporal candidateTemporal = (nodeTemporal)candidate;
		                TimeSpan time_diff = candidateTemporal.node_event.start_time.Subtract(exemplarTemporal.node_event.start_time);
		                if (time_diff.TotalSeconds >= 0)
		                {
		                    if (candidateTemporal.node_event.end_time < exemplarTemporal.node_event.end_time)
		                    {
		                        diff = (float)time_diff.TotalSeconds;
		                    }
		                    else diff = INVALID;
		                }
		                else diff = INVALID;
		            }
		        }
		    }

            // the start and end time of the candidate must be between the
            // start and end time of the exemplar
		    if (search_parameter == "within time")
		    {
		        if ((exemplar.GetFlag(atom.FLAG_TEMPORAL)) && (exemplar.GetFlag(atom.FLAG_IS_NODE)))
		        {
		            if ((candidate.GetFlag(atom.FLAG_TEMPORAL)) && (candidate.GetFlag(atom.FLAG_IS_NODE)))
		            {
		                nodeTemporal exemplarTemporal = (nodeTemporal)exemplar;
		                nodeTemporal candidateTemporal = (nodeTemporal)candidate;
		                
		                TimeSpan time_diff = candidateTemporal.node_event.Within(exemplarTemporal.node_event);
		                if (time_diff != TimeSpan.Zero)
		                    diff = (float)time_diff.TotalSeconds;
		                else 
		                    diff = INVALID;
		            }
		        }
		    }
		    

		    if (search_parameter == "activation")
		    {
		        float[] exemplar_values = exemplar.GetActivation();
		        float[] candidate_values = candidate.GetActivation();
		        int hits = 0;
		        
		        if ((exemplar_values != null) &&
		            (candidate_values != null))
		        {
		            // compare each channel
		            for (int i = 0; i < exemplar_values.Length; i++)
		            {
		                if ((exemplar_values[i] != 0) &&
		                    (candidate_values[i] != 0))
		                {
		                    diff += (exemplar_values[i] - candidate_values[i]);
		                    hits++;
		                }
		            }
		        }
		        
		        if (hits == 0)
		            diff = INVALID;
		        else
		            diff /= hits;  // average over all channels
		    }		    
		    
		    float similarity = diff * diff;
		    return(similarity);
		}

        /// <summary>
        /// perform a single search step
        /// searching all atoms may take several steps for large collections
        /// </summary>
        protected override void StepAgent()
        {        
            // on the first update initialise the search exemplars 
            // by examining incoming links
		    if (updates_elapsed == 0)
		    {
		        running = true;
		        
		        // clear the working set
		        working_set = new ArrayList();
		        working_set_difference = new ArrayList();
		        
		        // clear the outgoing results
		        ClearOutgoing();
		            
		        search_types = new atomCollection();		                
		            
		        // get the search request from the incoming links to this node
		        // note that by using an atomcollection we automatically index
		        // the atoms referenced by incoming links into separate types
		        for (int i = 0; i < atoms_incoming.Count(); i++)
		        {
		            // get the incoming link
		            link search_request_link = (link)(atoms_incoming.Get(i));
		                    
		            // get the atom referenced by the incoming link
		            atom search_request = search_request_link.atoms_incoming.Get(0);
		                    
		            // add this atom to the collection of search exemplars
		            search_types.Add(search_request);
		        }
		        
		        updates_elapsed++;
		        search_atom_index = 0;
		        search_type_index = 0;
		    }
        
            // search atoms using the given search exemplars
		    while ((search_type_index < search_types.GetNumberOfAtomTypes()) &&
		           (updates_elapsed < updates_per_time_step))
		    {
		        // get search exemplars for this atom type
		        ArrayList search_request = search_types.GetAtomsOfType(search_type_index);
		        if (search_request != null)
		        {
		            // if some exemplars exist
		            if (search_request.Count > 0)
		            {
		                // pick the first exemplar
		                atom exemplar = (atom)search_request[0];
		                            
		                // get atoms of the same type as the exemplar
		                ArrayList atoms_of_search_type = atoms.GetAtomsOfType(search_type_index);
		                            
		                // compare these atoms to the exemplar		                
		                while ((search_atom_index < atoms_of_search_type.Count) &&
		                       (updates_elapsed < updates_per_time_step))
		                {
		                     atom candidate = (atom)atoms_of_search_type[search_atom_index];
		                                
		                     // calculate similarity
		                     float diff = GetDifference(exemplar, candidate);
		                     
		                     if (diff != INVALID) AddToWorkingSet(candidate, diff);
		                     
		                     updates_elapsed++;
		                     search_atom_index++;
		                }
		                
		                if (search_atom_index == atoms_of_search_type.Count)
		                    search_atom_index = 0;
		            }
		        }
		                    
		        if (search_atom_index == 0)
		        {
		            search_type_index++;
		            if (search_type_index == search_types.GetNumberOfAtomTypes())
		            {
		                // sort the results
		                SortWorkingSet();
		            
		                // reset the number of updates
		                updates_elapsed = 0;
		                running = false;
		            }
		        }
		    }
        }
		
		protected override void RunAgent()
		{
		    if ((atoms != null) && (search_parameter != ""))		    
		    {
		        // incoming atoms specify the exemplars for which to search
		        if (atoms_incoming != null)
		        {
		            // have any search exemplars been specified ?
		            if (atoms_incoming.Count() > 0)
		            {
		                // perform a single search step
		                Step();
		    
		                // set the activity
		                //SetActivation(similarity, 0);
		                
		                // clear the incoming links
		                //ClearIncoming();
		            }
		        }
		    }
		}
		
		#endregion
	}
}
