using System.Collections;

public class Damage
{
	// base	damage types
	public static int _TypeSlash = 1;

	// base damage elements
	public static int _ElementNone = 1;

	public int _min;
	public int _max;
	public int _avg
	{
		get
		{
			return (int)((_max + _min) * 0.5f);
		}
	}





}
