﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.interfaces
{
	public interface IAsciiDubbleOperator : IAsciiSingleOperator
	{
		void Instantiate(IAsciiBaseOperator a, IAsciiBaseOperator b);

	}
}