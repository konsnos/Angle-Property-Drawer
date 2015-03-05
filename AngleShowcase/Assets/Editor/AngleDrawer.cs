using UnityEditor;
using UnityEngine;
using KonLab.Attributes;

namespace KonLab.KL_Editor
{
	/// <summary>
	/// Draws a circle with a line to visualize the angle.
	/// </summary>
	/// <author>Konstantinos Egkarchos</author>
	[CustomPropertyDrawer(typeof(AngleAttribute))]
	public class AngleDrawer : PropertyDrawer
	{
	    const int graphicHeight = 64;
	    const int textHeight = 16;
	    Texture2D circle;
	    const int texSize = 64;

	    AngleAttribute angleAttr { get { return ((AngleAttribute)attribute); } }

	    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	    {
	        return base.GetPropertyHeight(property, label) + graphicHeight;
	    }

	    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	    {
	        if (circle == null)
	            circle = InitializeTexture();

	        float value = EditorGUI.FloatField(new Rect(position.x, position.y, position.width, textHeight), property.name, property.floatValue);
	        property.floatValue = value;

	        position.y += 16;

	        /// Add offset.
	        float offset = angleAttr.Offset;
	        value += offset;

	        FloatAngle(position, value);
	    }

	    public float FloatAngle(Rect rect, float value)
	    {
	        Rect knobRect = new Rect(rect.x, rect.y, graphicHeight, graphicHeight);
	 
	        Matrix4x4 matrix = GUI.matrix;
	 
	        GUIUtility.RotateAroundPivot(value, knobRect.center);
	        
	        GUI.DrawTexture(knobRect, circle);
	        GUI.matrix = matrix;
	 
	        return value;
	    }

	    /// <summary>
	    /// Creates a texture of a circle with its radius for indication for direction.
	    /// </summary>
	    /// <returns>Returns the texture.</returns>
	    private static Texture2D InitializeTexture()
	    {
	        Texture2D tex = new Texture2D(texSize, texSize, TextureFormat.ARGB32, false);

	        // Create a transparent texture
	        Color32[] clr32 = new Color32[texSize * texSize];
	        Color transparentC = new Color(1f,1f,1f,0f);
	        for (int i = 0; i < clr32.Length; i++)
	            clr32[i] = transparentC;

	        tex.SetPixels32(clr32);

	        /// Make inner circle black.
	        int cx = texSize / 2, cy = texSize / 2;
	        int r = texSize / 2;
	        int x, y, px, nx, py, ny, d;
	        for (x = 0; x <= r; x++)
	        {
	            d = (int)Mathf.Ceil(Mathf.Sqrt(r * r - x * x));
	            for (y = 0; y <= d; y++)
	            {
	                px = cx + x;
	                nx = cx - x;
	                py = cy + y;
	                ny = cy - y;

	                tex.SetPixel(px, py, Color.black);
	                tex.SetPixel(nx, py, Color.black);
	                tex.SetPixel(px, ny, Color.black);
	                tex.SetPixel(nx, ny, Color.black);
	            }
	        }

	        /// Creates a green line for the angle.
	        int initX = texSize / 2;
	        int initY = texSize / 2;
	        Color[] clr = tex.GetPixels(initX, initY, initX, 1);
	        for (int i = 0; i < clr.Length; i++)
	            clr[i] = Color.green;

	        tex.SetPixels(initX, initY, initX, 1, clr);
	        tex.Apply();
	        return tex;
	    }
	}
}