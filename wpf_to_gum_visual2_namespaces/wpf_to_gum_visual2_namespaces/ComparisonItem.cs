using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_to_gum_visual2_namespaces
{
    public class ComparisonItem : IEquatable<ComparisonItem>
    {
        public Rectangle Bounds { get; set; }
        public string Name { get; set; }
        public string NamePath { get; set; }
        public string TypePath { get; set; }

        public ComparisonItem()
        { 

        }
        public ComparisonItem(Rectangle bounds)
        {
            Bounds = bounds;
        }

        public ComparisonItem(Rectangle bounds, string name)
        {
            Bounds = bounds;
            Name = name;
        }

        public bool Equals(ComparisonItem other)
        {
            if (other == null) return false;

            return Bounds.Equals(other.Bounds) &&
                   string.Equals(Name, other.Name, StringComparison.Ordinal) &&
                   string.Equals(NamePath, other.NamePath, StringComparison.Ordinal) &&
                   string.Equals(TypePath, other.TypePath, StringComparison.Ordinal);
        }

        public override bool Equals(object obj) => Equals(obj as ComparisonItem);

        public override int GetHashCode()
        {
            return HashCode.Combine(Bounds, Name, NamePath, TypePath);
        }
    }
}
