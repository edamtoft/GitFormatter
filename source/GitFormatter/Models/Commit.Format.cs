using GitFormatter.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace GitFormatter.Models
{
  partial class Commit
  {
    public override ReadOnlySpan<byte> GetGitFormat()
    {
      var textFormat = new StringBuilder();

      textFormat
          .AppendFormat("tree ")
          .AppendHash(Tree)
          .AppendUnixLineEnding();

      foreach (var parent in Parents)
      {
        textFormat
          .AppendFormat("parent ")
          .AppendHash(parent)
          .AppendUnixLineEnding();
      }

      textFormat
        .Append("author ")
        .AppendSignature(Author)
        .AppendUnixLineEnding();

      textFormat
        .Append("committer ")
        .AppendSignature(Committer)
        .AppendUnixLineEnding();

      textFormat
        .AppendUnixLineEnding()
        .Append(Message)
        .AppendUnixLineEnding();

      var content = Encoding.ASCII.GetBytes(textFormat.ToString());

      return ReadOnlySpan<byte>.Empty
        .AppendText($"commit {content.Length}\u0000", Encoding.ASCII)
        .Append(content);
    }
  }
}
