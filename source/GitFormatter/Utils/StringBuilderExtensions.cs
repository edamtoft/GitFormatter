using GitFormatter.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitFormatter.Utils
{
  internal static class StringBuilderExtensions
  {
    internal static StringBuilder AppendSignature(this StringBuilder builder, Signature signature)
    {
      return builder
        .Append(signature.Name)
        .Append(" <")
        .Append(signature.Email)
        .Append("> ")
        .Append(signature.When.ToUnixTimeSeconds())
        .Append(signature.When.Offset < TimeSpan.Zero ? " -" : " ")
        .Append(signature.When.Offset.ToString("hhmm"));
    }

    internal static StringBuilder AppendUnixLineEnding(this StringBuilder builder)
    {
      return builder.Append('\n');
    }

    internal static StringBuilder AppendHash(this StringBuilder builder, Hash hash)
    {
      foreach (var b in hash.Bytes)
      {
        builder.AppendFormat(b.ToString("x2"));
      }
      return builder;
    }
  }
}
