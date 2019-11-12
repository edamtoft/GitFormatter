using GitFormatter.Models;
using GitFormatter.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GitFormatter.Formatters
{
  public sealed class CommitFormatter : IObjectFormatter<Commit>
  {
    public ReadOnlySpan<byte> Write(Commit commit)
    {
      var result = new StringBuilder();

      result
          .AppendFormat("tree ")
          .Append(commit.Tree.ToString())
          .AppendUnixLineEnding();

      foreach (var parent in commit.Parents)
      {
        result
          .AppendFormat("parent ")
          .Append(parent.ToString())
          .AppendUnixLineEnding();
      }

      result
        .Append("author ")
        .AppendSignature(commit.Author)
        .AppendUnixLineEnding();

      result
        .Append("committer ")
        .AppendSignature(commit.Committer)
        .AppendUnixLineEnding();

      result
        .AppendUnixLineEnding()
        .Append(commit.Message)
        .AppendUnixLineEnding();

      return Encoding.ASCII.GetBytes(result.ToString());
    }
  }
}
