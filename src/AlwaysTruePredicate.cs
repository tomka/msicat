using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MsiTools
{
    class AlwaysTruePredicate : Predicate
    {
        public override bool eval(string attribute, string val)
        {
            return true;
        }
    }
}
