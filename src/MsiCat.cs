using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MsiTools
{
    class MsiCat
    {
        /// <summary>
        /// This method REQUIRES a referance to msi.dll in Windows\System32.  (com interopt)
        /// </summary>
        public static void print( Predicate predicate )
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");
            
            // List of all the attributes you can query for.
            var attributes = new string[]
                                 {
                                     "HelpLink",
                                     "HelpTelephone",
                                     "InstallDate",
                                     "InstallLocation",
                                     "InstalledProductName",
                                     "InstallSource",
                                     "LocalPackage",
                                     "ProductID",
                                     "Publisher",
                                     "PackageName",
                                     "RegCompany",
                                     "RegOwner",
                                     "URLInfoAbout",
                                     "URLUpdateInfo",
                                     "VersionMinor",
                                     "VersionMajor",
                                     "VersionString",
                                     "Transforms",
                                     "Language",
                                     "AssignmentType",
                                     "PackageCode",
                                     "PackageName",
                                     "ProductIcon",
                                     "ProductName",
                                     "Version",
                                     "InstanceType",
                                 };

            // Get a referance to the WindowsInstaller.Installer via com interopt.
            WindowsInstaller.Installer winInstaller = null;

            Type oType = Type.GetTypeFromProgID("WindowsInstaller.Installer");
            if (oType == null)
                return;

            winInstaller = (WindowsInstaller.Installer)Activator.CreateInstance(oType);
            if (winInstaller == null)
                return;

            StringBuilder resultBuilder = new StringBuilder();
            
            // Iterate through all the productIds of the installed apps.
            foreach (string productId in winInstaller.Products)
            {
                /* Keep track of how many attributes we have found
                 * for a specific product id.
                 */
                int foundAttributes = 0;
                // keep track of the attribute matches
                int predicateMatches = 0;
                // create new string builder for entry
                StringBuilder productBuilder = new StringBuilder();

                // Print delimiter
                productBuilder.AppendLine("-----------------------");
                // Write out the product code.
                productBuilder.AppendLine(productId);

                // try to get and print each attribute
                foreach (string attribute in attributes)
                {
                    string aVal = winInstaller.get_ProductInfo(productId, attribute);
                    if (string.IsNullOrEmpty(aVal))
                        continue;

                    // check if attribute/value matches predicate
                    if (predicate.eval(attribute, aVal))
                    {
                        predicateMatches++;
                    }

                    foundAttributes++;

                    productBuilder.AppendLine( String.Format("{0}:{1}", attribute, aVal) );
                }

                /* add a "nothing found" information if no attributes
                 * where found
                 */
                if (foundAttributes == 0)
                {
                    productBuilder.AppendLine("\t(No attributes found)");
                }

                // if some attribute matched, store product entry in results
                if (predicateMatches > 0)
                {
                    resultBuilder.Append(productBuilder);
                }
            }

            Console.Write( resultBuilder.ToString() );
        }

        public static void print()
        {
            print( new AlwaysTruePredicate() );
        }
    }
}
