using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace ResourceTool.core
{
    public class PartManager
    {
        protected int TotalParts;

        public PartManager(int totalParts)
        {
            TotalParts = totalParts;
        }

        private static List<Part> GetAttachedParts(Part split, int splitCount, string separator,Part forbidden, List<Part> parts)
        {

            foreach (Part part in split.children) {
                if (part != forbidden) {
                    if (!IsMatch(part.name, separator))
                    {
                        parts.Add(part);
                        parts.Concat(GetAttachedParts(part, splitCount, separator, forbidden, parts));
                    }
                    else if (splitCount > 0)
                    {
                        parts.Add(part);
                        splitCount--;
                        parts.Concat(GetAttachedParts(part, splitCount, separator, forbidden, parts));
                    }
                }


            }

            
            return parts;
        }


        private static Part GetFarthestParent(Part part, int splitCount, string separator) {

            while (part.parent != null) {
                if (!IsMatch(part.parent.name, separator)) part = part.parent;
                else if (splitCount > 0) { part = part.parent; splitCount--; }
                else break;
            }

            return part;
        }

        public List<Part> GetChildParts(Part split, int splitCount = -1, string separator = "dockingPort") {
            splitCount = splitCount < 0 ? TotalParts : splitCount;
            return GetAttachedParts(split, splitCount, separator, split, new List<Part>());
        }

        public List<Part> GetParentParts(Part split, int splitCount = -1, string separator = "dockingPort") {
            splitCount = splitCount < 0 ? TotalParts : splitCount;
            return GetAttachedParts(GetFarthestParent(split, splitCount, separator), splitCount, separator, split, new List<Part>());
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
