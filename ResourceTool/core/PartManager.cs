using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace ResourceTool.core
{
    public class PartManager
    {

        public static List<Part> GetChildParts(Part split, int splitCount, string separator,Part forbidden, List<Part> parts)
        {

            foreach (Part part in split.children) {
                if (part != forbidden) {
                    if (!IsMatch(part.name, separator))
                    {
                        parts.Add(part);
                        parts.Concat(GetChildParts(part, splitCount, separator, forbidden, parts));
                    }
                    else if (splitCount > 0)
                    {
                        parts.Add(part);
                        splitCount--;
                        parts.Concat(GetChildParts(part, splitCount, separator, forbidden, parts));
                    }
                }


            }

            
            return parts;
        }

        public static List<Part> GetParentParts(Part split, int splitCount, string separator, List<Part> parts) {

            Part FarParent = GetFarthestParent(split, splitCount, separator);

            return GetChildParts(FarParent, splitCount, separator, split, new List<Part>() { FarParent });
        }

        private static Part GetFarthestParent(Part part, int splitCount, string separator) {

            while (part.parent != null) {
                if (!IsMatch(part.parent.name, separator)) part = part.parent;
                else if (splitCount > 0) { part = part.parent; splitCount--; }
                else break;
            }

            return part;
        }


        public static List<Part> Matched(List<Part> parts, string reg) {
            List<Part> output = new List<Part>();


            foreach (Part part in parts) {
                if (IsMatch(part.name, reg)) output.Add(part);
            }


            return output;
        }

        public static bool IsMatch(string name, string reg) {
            Regex pattern = new Regex(reg);
            return pattern.IsMatch(name);
        }




            
    }
}
