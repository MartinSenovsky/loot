using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MUtils
{
	static public string _uid()
	{
		return Guid.NewGuid().ToString("N");
	}
}

