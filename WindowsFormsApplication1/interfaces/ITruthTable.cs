﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.interfaces
{
	public interface ITruthTable
	{
		byte[][] GetTable();
		string ToHex();

		IParser GetParser();
	}
}
