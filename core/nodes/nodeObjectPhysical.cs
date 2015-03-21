// /home/motters/develop/cuteagi/core/nodes/nodePhysicalObject.cs created with MonoDevelop
// User: motters at 12:08 PM 7/16/2007
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Xml;
using sluggish.utilities.xml;

namespace cuteagi.core
{
	
	/// <summary>
	/// This is a physical object in the sense that it obeys
	/// newtonian laws.  It could be a real thing of something in a simulated world
	/// </summary>
	public class nodeObjectPhysical : nodeSpatioTemporal
	{
	    // mass of the object
		public float mass;
				
		// velocity of the object
		public float[] velocity;

		// size of the object
		public float[] size;
		
		// orientation of the object
		public float[] orientation;

		#region "constructors"
		
		public nodeObjectPhysical()
		{
		    SetNodeType("objectphysical");
		    velocity = new float[3];
		    size = new float[3];
		    orientation = new float[3];
		}

		public nodeObjectPhysical(float x, float y, DateTime t,
		                          float size_x, float size_y, float size_z,
		                          float mass) : base(x, y, t)
		{
		    SetNodeType("objectphysical");
		    velocity = new float[3];
		    size = new float[3];
		    orientation = new float[3];
		    size[0] = size_x;
		    size[1] = size_y;
		    size[2] = size_z;
		    this.mass = mass;
		}

		public nodeObjectPhysical(float x, float y, float z, DateTime t,
		                          float size_x, float size_y, float size_z,
		                          float mass) : base(x, y, z, t)
		{
		    SetNodeType("objectphysical");
		    velocity = new float[3];
		    size = new float[3];
		    orientation = new float[3];
		    size[0] = size_x;
		    size[1] = size_y;
		    size[2] = size_z;
		    this.mass = mass;
		}
		
		#endregion
	}
}
