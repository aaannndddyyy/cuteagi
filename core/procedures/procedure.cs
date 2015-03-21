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
	public class nodeProcedure: node, iRun
	{
	    // is this procedure running?
	    protected bool running;
		
		#region "constructors"
		
		public nodeProcedure()
		{
		}
		
		#endregion
		
        #region "running and stopping"
		
		/// <summary>
		/// the main run routine
		/// </summary>
		protected virtual void RunProcedure()
		{
            running = true;
		}
		
		/// <summary>
		/// run the procedure
		/// </summary>
		public void Run()
		{		    
		    RunProcedure();
		}

		/// <summary>
		/// perform a single processing step
		/// </summary>
		protected virtual void StepProcedure()
		{
		}

		/// <summary>
		/// perform a single processing step
		/// </summary>
		public void Step()
		{
		    StepProcedure();
		}

		/// <summary>
		/// stop running (exit) the procedure
		/// </summary>
		public virtual void Stop()
		{
		    running = false;
		}
		
		#endregion
		
	}
}
