using System;
using System.Collections.Generic;
using System.Text;
using GitFormatter.Models;

namespace GitFormatter.Utils
{
  internal sealed class PathComparer : IEqualityComparer<TreeEntry>
  {
    public bool Equals(TreeEntry x, TreeEntry y) => x.Path == y.Path;

    public int GetHashCode(TreeEntry obj) => obj.Path?.GetHashCode() ?? 0;

    public static PathComparer Default { get; } = new PathComparer();
  }
}
