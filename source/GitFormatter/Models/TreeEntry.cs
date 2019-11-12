using System;
using System.Collections.Generic;
using System.Text;

namespace GitFormatter.Models
{
  public readonly struct TreeEntry
  {
    public string Path { get; }
    public GitFileMode Mode { get; }
    public Hash Hash { get;  }
    
    public TreeEntry(string path, GitFileMode mode, Hash hash)
    {
      Path = path ?? throw new ArgumentNullException(nameof(path));
      Mode = mode;
      Hash = hash;
    }
  }
}
