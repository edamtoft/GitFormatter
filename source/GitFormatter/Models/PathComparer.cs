using System;
using System.Collections.Generic;
using System.Text;

namespace GitFormatter.Models
{
  internal sealed class PathComparer : IEqualityComparer<TreeEntry>
  {
    public bool Equals(TreeEntry x, TreeEntry y)
    {
      return EqualityComparer<string>.Default.Equals(x.Path, y.Path);
    }

    public int GetHashCode(TreeEntry obj)
    {
      return EqualityComparer<string>.Default.GetHashCode(obj.Path);
    }

    public static PathComparer Default { get; } = new PathComparer();
  }
}
