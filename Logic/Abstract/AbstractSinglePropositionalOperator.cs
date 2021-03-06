﻿using System;
using System.Linq;
using Logic.interfaces;
using Logic.Operators;

namespace Logic.Abstract
{
    public abstract class AbstractSinglePropositionalOperator : AbstractBasePropositionalOperator, IAsciiSinglePropositionalOperator
    {
        protected IAsciiBasePropositionalOperator _A;

        public AbstractSinglePropositionalOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override void Instantiate(IAsciiBasePropositionalOperator[] arg)
        {
            _A = arg[0];
        }

        public override char[] GetArguments()
        {
            return _A.GetArguments();
        }

        public override IAsciiBasePropositionalOperator[] GetChilds()
        {
            IAsciiBasePropositionalOperator[] array = {_A};
            return array;
        }

        public override int GetOperatorNeededArguments()
        {
            return 1;
        }

        public abstract char GetAsciiSymbol();
        public abstract char GetLogicSymbol();

        public override string ToString()
        {
            return string.Format("{0}({1})", GetAsciiSymbol(), _A);
        }

        public override string ToLogicString()
        {
            var a = _A is ScalarPropositionalOperator || !(_A is IAsciiDubblePropositionalOperator)  ? _A.ToLogicString() : "(" + _A.ToLogicString() + ")";
            return string.Format("{0}{1}", GetLogicSymbol(), a); 
        }

        public override bool HasResult()
        {
            return _A.HasResult();
        }

        public override string ToName()
        {
            return GetLogicSymbol().ToString();
        }

        public override bool Equals(object obj)
        {
            var oper = obj as AbstractSinglePropositionalOperator;
            var result =  oper != null && obj.GetType() == GetType() && GetChilds()[0].Equals(oper.GetChilds()[0]);
            return result;
        }

        public override void UpdateChild(int index, IAsciiBasePropositionalOperator baseOperator)
        {
            if (index != 0)
            {
                throw new NotImplementedException();
            }
            _A = baseOperator;
        }
    }
}