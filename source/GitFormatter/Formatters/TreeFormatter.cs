using GitFormatter.Models;
using GitFormatter.Utils;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GitFormatter.Formatters
{
  public sealed class TreeFormatter : IObjectFormatter<Tree>
  {
    public ReadOnlySpan<byte> Write(Tree tree)
    {
      var result = ReadOnlySpan<byte>.Empty;

      foreach (var entry in tree)
      {
        result = result.AppendText($"{(int)entry.Mode} {entry.Path}\u0000", Encoding.ASCII);
        result = result.Append(entry.Hash.Bytes);
      }

      return result;
    }
  }
}
