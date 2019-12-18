using System;
using System.Collections.Generic;
using System.Text;

namespace GitFormatter.Models
{
  public readonly struct TreeEntry : IEquatable<TreeEntry>
  {
    public TreeEntry(GitFileMode mode, Hash hash)
    {
      Hash = hash;
      Mode = mode;
    }

    public GitFileMode Mode { get;  }
    public Hash Hash { get; }

    public override bool Equals(object obj)
    {
      return obj is TreeEntry entry && Equals(entry);
    }

    public bool Equals(TreeEntry other)
    {
      return Mode == other.Mode && Hash.Equals(other.Hash);
    }

    public override int GetHashCode() => HashCode.Combine(Mode, Hash);

    public override string ToString() => $"{(int)Mode} {Hash:x2}";

    public static bool operator ==(TreeEntry left, TreeEntry right) => left.Equals(right);

    public static bool operator !=(TreeEntry left, TreeEntry right) => !(left == right);

    public static implicit operator TreeEntry((GitFileMode Mode, Hash Hash) item) => new TreeEntry(item.Mode, item.Hash);
  }
}
