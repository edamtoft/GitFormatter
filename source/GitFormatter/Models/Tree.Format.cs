using GitFormatter.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitFormatter.Models
{
  partial class Tree
  {
    public override ReadOnlySpan<byte> GetGitFormat()
    {
      var content = ReadOnlySpan<byte>.Empty;

      foreach (var entry in _items)
      {
        content = content
          .AppendText($"{(int)entry.Value.Mode} {entry.Key}\u0000", Encoding.ASCII)
          .Append(entry.Value.Hash.Span);
      }

      return ReadOnlySpan<byte>.Empty
        .AppendText($"tree {content.Length}\u0000", Encoding.ASCII)
        .Append(content);
    }
  }
}
