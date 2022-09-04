using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace ResourceTool.core
{
    public class PartManager
    {
        protected Vessel Vessel;
        protected int TotalParts;

        public PartManager(Vessel vessel)
        {
            Vessel = vessel;
            TotalParts = Vessel.parts.Count();
        }


        protected static Part GetFarthestParent(Part part, int splitCount, string separator) 
        {
            while (part.parent != null) {
                if (!IsMatch(part.parent.name, separator)) part = part.parent;
                else if (splitCount > 0) { part = part.parent; splitCount--; }
                else break;
            }

            return part;
        }

        protected List<Part> GetAttachedParts(Part node, List<Part> parts)
        {
            if (!parts.Contains(node))
            {
                parts.Add(node);
                if (node.children != null) foreach (var item in node.children) parts.Concat(GetAttachedParts(item, parts));

            }

            return parts;
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

        public List<Part> GetChildParts(Part part)
        {
            var parents = GetAttachedParts(part, new List<Part>());
            var output = new List<Part>();


            foreach (var p in Vessel.parts) { if (!parents.Contains(p)) output.Add(p); }


            return output;
        }

        public List<Part> GetParentParts(Part part) {
            return GetAttachedParts(part, new List<Part>());
        }

    }
}
