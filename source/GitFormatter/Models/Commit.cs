using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace GitFormatter.Models
{
  public sealed partial class Commit : GitObject
  {
    public Commit(Hash tree, IEnumerable<Hash> parents, Signature author, Signature committer, string message) 
      : this(tree, parents.ToImmutableList(), author, committer, message)
    {
    }

    public Commit(Hash tree, ImmutableList<Hash> parents, Signature author, Signature committer, string message)
    {
      Tree = tree;
      Parents = parents ?? throw new ArgumentNullException(nameof(parents));
      Author = author;
      Committer = committer;
      Message = message ?? throw new ArgumentNullException(nameof(message));
    }

    public Hash Tree { get;  }
    public ImmutableList<Hash> Parents { get; }
    public Signature Author { get; }
    public Signature Committer { get; }
    public string Message { get; }

    public override string ToString() => Message;
  }
}
