using System;

namespace MsiTools
{
    class SpecificAttributePredicate : Predicate
    {
        protected String testAttribute;
        protected String testValue;
        /// <summary>
        /// Indicates if the value checking should be strict. If it is
        /// strict, this predicate can only be true if the values are equal.
        /// If the test is not strict, the current value must only contain
        /// the current test value.
        /// </summary>
        bool strict = false;

        public SpecificAttributePredicate(String attribute, String value, bool strict)
        {
            this.testAttribute = attribute;
            this.testValue = value;
            this.strict = strict;
        }

        public override bool eval(string attribute, string val)
        {
            if (strict)
                return (attribute.Equals(testAttribute) && val.Equals(testValue));
            else
                return (attribute.Equals(testAttribute) && val.Contains(testValue));
        }
    }
}
