﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Logic.interfaces;

namespace Logic.Abstract
{
    public abstract class AbstractTrueTable : ITruthTable
    {
        protected IArgumentController _manager;
        protected IAsciiBasePropositionalOperator _operator;

        public AbstractTrueTable(IAsciiBasePropositionalOperator oper)
        {
            _manager = oper.GetArgumentsManager();
            _operator = oper;
        }

        public abstract byte[][] GetTable();

        public string ToHex()
        {
            return BinToDec(string.Join("", GetTable().Reverse().Select(x => x.Last())));
        }

        public IAsciiBasePropositionalOperator GetOperator()
        {
            return _operator;
        }

        private string BinToDec(string value)
        {
            // BigInteger can be found in the System.Numerics dll
            BigInteger res = 0;
            foreach (var c in value)
            {
                res <<= 1;
                res += c == '1' ? 1 : 0;
            }

            var hex = res.ToString("X");
            return hex[0] == '0' ? hex.Substring(1) : hex;
        }

        protected bool? GetResults(ref bool[] data)
        {
            var names = _operator.GetArguments();
            for (var i = 0; i < data.Length; i++)
                _manager.SetArgumentValue(names[i], data[i]);
            return _operator.Result();
        }

        protected bool[][] GetAllOptions(int length)
        {
            var result = new List<bool[]>();

            //calculate the amount of different options 
            var num = (int) Math.Pow(2, length) - 1;

            for (var i = 0; i <= num; i++)
            {
                var array = new bool[length];
                for (var pos = 0; pos < length; pos++)
                    array[pos] = (i & (1 << pos)) <= 0;
                result.Add(array.Reverse().ToArray());
            }
            result.Reverse();
            return result.ToArray();
        }
    }
}