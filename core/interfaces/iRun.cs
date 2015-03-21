// iRun.cs created with MonoDevelop
// User: motters at 9:35 PM 8/20/2007
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;

namespace cuteagi.core
{	
    /// <summary>
    /// 
    /// </summary>
	public interface iRun
	{
        #region "running and stopping"
				
		/// <summary>
		/// run the object
		/// </summary>
		void Run();

		/// <summary>
		/// perform a single processing step
		/// </summary>
		void Step();

		/// <summary>
		/// stop running the object
		/// </summary>
		void Stop();
		
		#endregion
		
	}
}
