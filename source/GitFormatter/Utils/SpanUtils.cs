using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace GitFormatter.Utils
{
  internal static class SpanUtils
  {
    public static ReadOnlySpan<T> Append<T>(this ReadOnlySpan<T> source, ReadOnlySpan<T> next)
    {
      var buffer = new T[source.Length + next.Length].AsSpan();
      source.CopyTo(buffer.Slice(0, source.Length));
      next.CopyTo(buffer.Slice(source.Length, next.Length));
      return buffer;
    }

    public static ReadOnlySpan<byte> AppendText(this ReadOnlySpan<byte> source, ReadOnlySpan<char> next, Encoding encoding)
    {
      var byteCount = encoding.GetByteCount(next);
      var buffer = new byte[source.Length + byteCount].AsSpan();
      source.CopyTo(buffer.Slice(0, source.Length));
      encoding.GetBytes(next, buffer.Slice(source.Length, byteCount));
      return buffer;
    }
  }
}
