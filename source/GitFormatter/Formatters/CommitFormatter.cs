using GitFormatter.Models;
using GitFormatter.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GitFormatter.Formatters
{
  public sealed class CommitFormatter : IGitObjectFormatter<Commit>
  {
    public ReadOnlySpan<byte> Write(Commit commit)
    {
      var textFormat = new StringBuilder();

      textFormat
          .AppendFormat("tree ")
          .AppendHash(commit.Tree)
          .AppendUnixLineEnding();

      foreach (var parent in commit.Parents)
      {
        textFormat
          .AppendFormat("parent ")
          .AppendHash(parent)
          .AppendUnixLineEnding();
      }

      textFormat
        .Append("author ")
        .AppendSignature(commit.Author)
        .AppendUnixLineEnding();

      textFormat
        .Append("committer ")
        .AppendSignature(commit.Committer)
        .AppendUnixLineEnding();

      textFormat
        .AppendUnixLineEnding()
        .Append(commit.Message)
        .AppendUnixLineEnding();

      return Encoding.ASCII.GetBytes(textFormat.ToString());
    }
  }
}
