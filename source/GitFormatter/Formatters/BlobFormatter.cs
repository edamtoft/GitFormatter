using GitFormatter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GitFormatter.Formatters
{
  public sealed class BlobFormatter : IObjectFormatter<Blob>
  {
    public ReadOnlySpan<byte> Write(Blob blob)
    {
      return blob.Content;
    }
  }
}
