using System;
using System.Collections.Generic;
using System.Text;

namespace GitFormatter.Models
{
  public sealed class Commit : GitObject
  {
    public Commit(Hash tree, Hash[] parents, Signature author, Signature committer, string message)
    {
      Tree = tree;
      Parents = parents ?? throw new ArgumentNullException(nameof(parents));
      Author = author ?? throw new ArgumentNullException(nameof(author));
      Committer = committer ?? throw new ArgumentNullException(nameof(committer));
      Message = message ?? throw new ArgumentNullException(nameof(message));
    }

    public Hash Tree { get;  }
    public Hash[] Parents { get; }
    public Signature Author { get; }
    public Signature Committer { get; }
    public string Message { get; }

    public override string ToString() => Message;
  }
}
