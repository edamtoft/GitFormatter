using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GitFormatter.Models
{
  public sealed class Blob : GitObject
  {
    public Blob(byte[] content)
    {
      Content = content ?? throw new ArgumentNullException(nameof(content));
    }

    public byte[] Content { get; }

    public static Blob FromString(string s)
    {
      return new Blob(Encoding.Default.GetBytes(s));
    }
  }
}
