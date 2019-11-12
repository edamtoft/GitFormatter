using GitFormatter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GitFormatter.Formatters
{
  public interface IObjectFormatter<T> where T : GitObject
  {
    ReadOnlySpan<byte> Write(T item);
  }
}
