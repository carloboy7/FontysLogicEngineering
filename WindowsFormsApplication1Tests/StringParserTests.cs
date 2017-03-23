﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsApplication1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.Operators;
using WindowsFormsApplication1.Exception;
using WindowsFormsApplication1.interfaces;
using System.Diagnostics;

namespace WindowsFormsApplication1.Tests
{
	[TestClass()]
	public class StringParserTests
	{



		[TestMethod()]
		public void SingleScalerReceive()
		{
			StringParser parser = StringParser.Create("A");
			Assert.IsTrue(parser.ToOuterOperator() is ScalarOperator);
		}

		[TestMethod()]
		public void SingleScalerReceiveValueChangeFalse()
		{
			StringParser parser = StringParser.Create("A");
			parser.ToOuterOperator().GetArgumentsManager().SetArgumentValue('A', false);
			Assert.IsFalse(parser.ToOuterOperator().Result());
		}

		[TestMethod()]
		public void SingleScalerReceiveValueChangeTrue()
		{
			StringParser parser = StringParser.Create("A");
			parser.ToOuterOperator().GetArgumentsManager().SetArgumentValue('A', true);
			Assert.IsTrue(parser.ToOuterOperator().Result());
		}

		[TestMethod()]
		[ExpectedException(typeof(ScalarInvalidValue))]
		public void SingleScalerReceiveValueNonChange()
		{
			StringParser parser = StringParser.Create("A");
			parser.ToOuterOperator().Result();
			Assert.Fail();
		}

		[TestMethod()]
		public void GreaterThenOperatorSuccess()
		{

			StringParser parser = StringParser.Create(">(A,B)");
			ArgumentsManager manager = parser.ToOuterOperator().GetArgumentsManager();
			manager.SetArgumentValue('A', true);
			manager.SetArgumentValue('B', false);
			Assert.IsTrue(parser.ToOuterOperator().Result());
		}

		[TestMethod()]
		public void GreaterThenOperatorNestedFail()
		{

			StringParser parser = StringParser.Create(">(A,>(A,B))");
			ArgumentsManager manager = parser.ToOuterOperator().GetArgumentsManager();
			manager.SetArgumentValue('A', true);
			manager.SetArgumentValue('B', false);
			Assert.IsFalse(parser.ToOuterOperator().Result());
		}

		[TestMethod()]
		public void GreaterThenOperatorFailSame()
		{
			
			StringParser parser = StringParser.Create(">(A,B)");
			ArgumentsManager manager = parser.ToOuterOperator().GetArgumentsManager();
			manager.SetArgumentValue('A', false);
			manager.SetArgumentValue('B', false);
			Assert.IsFalse(parser.ToOuterOperator().Result());
		}


		[TestMethod()]
		public void ArgumentManagerChange()
		{
			
			StringParser parser = StringParser.Create(">(A,B)");
			ArgumentsManager manager = parser.ToOuterOperator().GetArgumentsManager();

			manager.SetArgumentValue('A', false);
			manager.SetArgumentValue('B', true);

			bool result = parser.ToOuterOperator().Result();

			manager.SetArgumentValue('A', true);
			manager.SetArgumentValue('B', false);

			Assert.IsTrue( result != parser.ToOuterOperator().Result());
		}

		[TestMethod()]
		public void GetArgumentsFromOperator()
		{
			StringParser parser = StringParser.Create(">(A ,B");
			IAsciiBaseOperator ope = parser.ToOuterOperator();

			List<char> check = new List<char>(ope.GetArguments());
			Assert.IsTrue(check.Contains('A') && check.Contains('B'));
		}


	}
}