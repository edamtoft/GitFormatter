using GitFormatter.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitFormatter.Models
{
  partial class Blob
  {
    public override ReadOnlySpan<byte> GetGitFormat()
    {
      return ReadOnlySpan<byte>.Empty
        .AppendText($"blob {Content.Length}\u0000", Encoding.ASCII)
        .Append(Content.Span);
    }
  }
}
