using System;

namespace MsiTools
{
    class Program
    {
        static int Main(string[] args)
        {
            Predicate p = null;
            // try to parse the arguments
            try
            {
               p  = parseArgs(args);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("An argument parsing error occured: " + e.Message);
                printUsage();
                return 1;
            }
            // print msi list
            MsiCat.print( p );
            // let user close the window
            System.Console.WriteLine("Please press some key to exit...");
            System.Console.ReadKey();

            return 0;
        }

        /// <summary>
        /// Parses command line arguments and creates
        /// a filtering predicate.
        /// </summary>
        /// <param name="args">The commant line args array.</param>
        /// <returns>A fitering predicate.</returns>
        static Predicate parseArgs(string[] args)
        {
            // if there is no argument, set no constraints
            if (args.Length == 0)
                return new AlwaysTruePredicate();

            foreach (String a in args) {
                String[] constraints = a.Split('=');
                // is there a "attribute==val" pattern?
                if (constraints.Length == 3 && constraints[1].Equals(String.Empty))
                {
                    return new SpecificAttributePredicate(constraints[0], constraints[2], true);
                }
                // is there a "attribute=val" pattern?
                if (constraints.Length == 2)
                {
                    return new SpecificAttributePredicate(constraints[0], constraints[1], false);
                }
            }

            throw new ArgumentException("An \"=\" or a \"==\" is missing!");
        }

        /// <summary>
        /// Prints usage information of msicat.
        /// </summary>
        static void printUsage()
        {
            Console.WriteLine("MsiCat by Tom Kazimiers");
            Console.WriteLine("Synopsis: msicat [ arg=val | arg==val ]");
            Console.WriteLine("\t=  - non-strict checking, val must be _contained_ in value of arg");
            Console.WriteLine("\t== - strict checking, val must be _the same_ as value of arg");
        }
    }
}
