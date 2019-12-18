using GitFormatter.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GitFormatter.Models
{
  public sealed partial class Blob : GitObject
  {
    public Blob(ReadOnlyMemory<byte> content)
    {
      Content = content;
    }

    public ReadOnlyMemory<byte> Content { get; }

    public override string ToString()
    {
      return Encoding.Default.GetString(Content.Span);
    }

    public static Blob FromString(string s)
    {
      return new Blob(Encoding.Default.GetBytes(s));
    }
  }
}
