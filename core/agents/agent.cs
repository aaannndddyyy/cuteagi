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
    /// <summary>
    /// an agent used to execute some process or algorithm
    /// </summary>
	public class agent : node, iRun
	{
	    // whether the agent is currently running
	    public bool running;
	    
	    // the maximum number of updates per time step
	    // this is used to restrict the amount of computation performed by each agent
	    // at any point in time
	    public int updates_per_time_step;
	    public int updates_elapsed;
	    
	    // atoms used by this agent
	    public atomCollection atoms;
	    
	    // an arbiter who decides whether this agent can be run
	    public agentArbiter arbiter;
	
	    #region "constructors"
	
		public agent()
		{
		    SetAtomType("agent");
		    SetFlag(atom.FLAG_IS_AGENT);
		    atoms = new atomCollection();
		    updates_per_time_step = 1000;		    
		}
		
		#endregion

        #region "running and stopping"

        /// <summary>
        /// returns whether this agent has permission to run
        /// </summary>
		public bool HasPermissionToRun()
		{
		    if (arbiter == null)
		        return(true);
		    else
		    {
		        if (arbiter.RequestPermission(this))
		            return(true);
		        else
		            return(false);
		    }
		}
		
		/// <summary>
		/// the main run routine
		/// </summary>
		protected virtual void RunAgent()
		{
		    if (HasPermissionToRun())
		    {
		        running = true;
		    }
		}
		
		/// <summary>
		/// run the agent
		/// </summary>
		public void Run()
		{
		    if (HasPermissionToRun())
		        RunAgent();
		}

		/// <summary>
		/// perform a single processing step
		/// </summary>
		protected virtual void StepAgent()
		{
		}

		/// <summary>
		/// perform a single processing step
		/// </summary>
		public void Step()
		{
		    if (HasPermissionToRun())
		    {
		        StepAgent();
		    }
		}

		/// <summary>
		/// stop running the agent
		/// </summary>
		public virtual void Stop()
		{
		    running = false;
		}
		
		#endregion
	}
}
