using GitFormatter.Formatters;
using GitFormatter.Models;
using GitFormatter.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GitFormatter.Formatters
{
  public sealed class GitHashWriter : IGitHashWriter
  {
    private readonly IGitObjectFormatter<Commit> _commitFormatter;
    private readonly IGitObjectFormatter<Tree> _treeFormatter;
    private readonly IGitObjectFormatter<Blob> _blobFormatter;

    public GitHashWriter(
      IGitObjectFormatter<Commit> commitFormatter,
      IGitObjectFormatter<Tree> treeFormatter,
      IGitObjectFormatter<Blob> blobFormatter)
    {
      _commitFormatter = commitFormatter;
      _treeFormatter = treeFormatter;
      _blobFormatter = blobFormatter;
    }


    public Hash Write(GitObject obj, out ReadOnlySpan<byte> result)
    {
      switch (obj)
      {
        case Blob blob:
          return Write("blob", _blobFormatter.Write(blob), out result);
        case Commit commit:
          return Write("commit", _commitFormatter.Write(commit), out result);
        case Tree tree:
          return Write("tree", _treeFormatter.Write(tree), out result);
        default:
          throw new NotSupportedException("Unsupported git object type");
      }
    }

    private static Hash Write(string type, ReadOnlySpan<byte> content, out ReadOnlySpan<byte> result)
    {
      result = ReadOnlySpan<byte>.Empty
        .AppendText($"{type} {content.Length}\u0000", Encoding.ASCII)
        .Append(content);

      using (var sha = SHA1.Create())
      {
        var hashBytes = sha.ComputeHash(result.ToArray());
        return new Hash(hashBytes);
      }
    }
  }
}
