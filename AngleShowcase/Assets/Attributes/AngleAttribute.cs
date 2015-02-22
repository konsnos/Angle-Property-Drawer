using UnityEngine;

namespace KonLab.Attributes
{
	/// <summary>
	/// Attributes for Angle attribute.
	/// </summary>
	/// <author>Konstantinos Egkarchos</author>
	public class AngleAttribute : PropertyAttribute 
	{
	    public readonly float Offset = 0;

	    public AngleAttribute(float offset)
	    {
	        this.Offset = offset;
	    }

	    /// <summary>
	    /// Sets no limit for the angle.
	    /// </summary>
	    public AngleAttribute()
	    {
	        this.Offset = 0;
	    }
	}
}