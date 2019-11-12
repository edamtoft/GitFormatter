using System;
using System.IO;
using GitFormatter.Models;

namespace GitFormatter.Formatters
{
  public interface IGitHashWriter
  {
    Hash Write(GitObject obj, out ReadOnlySpan<byte> content);
  }
}