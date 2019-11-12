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
  public sealed class GitObjectFormatter : IGitObjectFormatter
  {
    private readonly IObjectFormatter<Commit> _commitFormatter;
    private readonly IObjectFormatter<Tree> _treeFormatter;
    private readonly IObjectFormatter<Blob> _blobFormatter;

    public GitObjectFormatter(
      IObjectFormatter<Commit> commitFormatter,
      IObjectFormatter<Tree> treeFormatter,
      IObjectFormatter<Blob> blobFormatter)
    {
      _commitFormatter = commitFormatter;
      _treeFormatter = treeFormatter;
      _blobFormatter = blobFormatter;
    }

    public Hash Write(Commit commit, out ReadOnlySpan<byte> result) => Write("commit", _commitFormatter.Write(commit), out result);
    public Hash Write(Tree tree, out ReadOnlySpan<byte> result) => Write("tree", _treeFormatter.Write(tree), out result);
    public Hash Write(Blob blob, out ReadOnlySpan<byte> result) => Write("blob", _blobFormatter.Write(blob), out result);

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
