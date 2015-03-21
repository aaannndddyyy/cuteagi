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
	/// arbitrates between mutually exclusive agents
	/// </summary>
	public class agentArbiter : agent
	{
	    // list of mutually exclusive agents
		private ArrayList mutexes;

        #region "constructors"
        
		public agentArbiter()
		{
		    //SetAtomType("agentArbiter");
		    mutexes = new ArrayList();
		}
		
		#endregion
		
		#region "granting permission"
		
		/// <summary>
		/// the given agent makes a request to run
		/// </summary>
		public bool RequestPermission(agent a)
		{
		    bool has_permission = true;
		    
		    int i = 0;
		    while ((i < mutexes.Count) && (has_permission))
		    {
		        agent[] mutex = (agent[])mutexes[i];		        
		        if ((mutex[0] == a) && (mutex[1].running)) has_permission = false;
		        if ((mutex[0].running) && (mutex[1] == a)) has_permission = false;
		        i++;
		    }
		    
		    return(has_permission);
		}
		
		#endregion
		
		#region "adding and removing agents"
		
		/// <summary>
		/// is the given mutex exists return its index
		/// </summary>
		private int MutexExists(agent a1, agent a2)
		{
		    int index = -1;
		    int i = 0;
		    while ((i < mutexes.Count) && (index == -1))
		    {
		        agent[] mutex = (agent[])mutexes[i];
		        if (((mutex[0] == a1) && (mutex[1] == a2)) ||
		            ((mutex[0] == a2) && (mutex[1] == a1)))
		            index = i;
		        i++;
		    }
		    return(index);
		}
		
		/// <summary>
		/// add a new mutex
		/// </summary>
		public void AddMutex(agent a1, agent a2)
		{
		    int index = MutexExists(a1, a2);
		    if (index == -1)
		    {
		        agent[] mutex = new agent[2];
		        mutex[0] = a1;
		        mutex[1] = a2;
		        mutexes.Add(mutex);
		    }
		}
		
		/// <summary>
		/// remove a mutex
		/// </summary>
		public void RemoveMutex(agent a1, agent a2)
		{
		    int index = MutexExists(a1, a2);
		    if (index > -1) mutexes.RemoveAt(index);
		}
		
		#endregion
	}
}
