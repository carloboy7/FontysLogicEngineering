﻿using System;
using System.Collections.Generic;
using System.Linq;
using Logic.interfaces;
using Logic.Operators;

namespace Logic.Abstract
{
    public abstract class AbstractDubblePropositionalOperator : AbstractSinglePropositionalOperator, IAsciiDubblePropositionalOperator
    {
        protected IAsciiBasePropositionalOperator _B;

        public AbstractDubblePropositionalOperator(ArgumentsManager manager) : base(manager)
        {
        }

        public override void Instantiate(IAsciiBasePropositionalOperator[] arg)
        {
            _A = arg[0];
            _B = arg[1];
        }

        public override IAsciiBasePropositionalOperator[] GetChilds()
        {
            IAsciiBasePropositionalOperator[] array = {_A, _B};
            return array;
        }

        public override char[] GetArguments()
        {
            var list = new HashSet<char>(_A.GetArguments());
            list.UnionWith(_B.GetArguments());
            return list.OrderBy(x=> x).ToArray();
        }

        public override int GetOperatorNeededArguments()
        {
            return 2;
        }

        public override string ToString()
        {
            return string.Format("{0}( {1}, {2} )", GetAsciiSymbol(), _A, _B);
        }

        public override string ToLogicString()
        {
            var a = _A is ScalarPropositionalOperator || !(_A is IAsciiDubblePropositionalOperator) ? _A.ToLogicString() : "(" + _A.ToLogicString() + ")";
            var b = _B is ScalarPropositionalOperator || !(_B is IAsciiDubblePropositionalOperator) ? _B.ToLogicString() : "(" + _B.ToLogicString() + ")";
            return string.Format("{0} {1} {2}", a, GetLogicSymbol(), b);
        }
        public override bool HasResult()
        {
            return _A.HasResult() && _B.HasResult();
        }
        public override bool Equals(object obj)
        {
            var result =  
                obj is AbstractDubblePropositionalOperator oper 
                && obj.GetType() == GetType()
                && (
                    (GetChilds()[0].Equals(oper.GetChilds()[0]) && GetChilds()[1].Equals(oper.GetChilds()[1]))
                    ||  (GetChilds()[1].Equals(oper.GetChilds()[0]) && GetChilds()[0].Equals(oper.GetChilds()[1])));
            return result;
        }
        public override bool IsAdvanced()
        {
            return true;
        }
        public override void UpdateChild(int index, IAsciiBasePropositionalOperator baseOperator)
        {
            switch (index)
            {
                case 0:
                    _A = baseOperator;
                    return;
                case 1:
                    _B = baseOperator;
                    return;
            }
            throw new NotImplementedException();
        }
        
    }
}