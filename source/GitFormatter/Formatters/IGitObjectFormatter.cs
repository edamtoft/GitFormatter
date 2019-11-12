using System;
using System.IO;
using GitFormatter.Models;

namespace GitFormatter.Formatters
{
  public interface IGitObjectFormatter
  {
    Hash Write(Blob blob, out ReadOnlySpan<byte> content);
    Hash Write(Commit commit, out ReadOnlySpan<byte> content);
    Hash Write(Tree tree, out ReadOnlySpan<byte> content);
  }
}