using System;
using System.Text;

namespace MsiTools
{
    /// <summary>
    /// Evaluates if a pattern is true or not.
    /// </summary>
    abstract class Predicate
    {
        public abstract bool eval(String attribute, String val);
    }
}
