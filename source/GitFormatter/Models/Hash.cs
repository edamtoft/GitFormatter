using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using GitFormatter.Utils;

namespace GitFormatter.Models
{
  public readonly struct Hash : IEquatable<Hash>
  {
    private readonly ReadOnlyMemory<byte> _bytes { get; }

    private const int HashLength = 20;

    public Hash(ReadOnlyMemory<byte> bytes)
    {
      if (bytes.Length != HashLength)
      {
        throw new ArgumentException("Incorrect hash size");
      }

      _bytes = bytes;
    }

    public ReadOnlySpan<byte> Span => _bytes.Span;

    public string ToHex() => string.Create(_bytes.Length * 2, _bytes, (chars, bytes) =>
    {
      const string alphabet = "0123456789abcdef";
      var charIndex = 0;
      var bufferIndex = 0;
      while (charIndex < chars.Length)
      {
        var b = bytes.Span[bufferIndex];
        chars[charIndex] = alphabet[b / 16];
        chars[charIndex + 1] = alphabet[b % 16];
        bufferIndex++;
        charIndex += 2;
      }
    });

    public override string ToString() => ToHex();

    public static Hash FromHex(string hexString)
    {
      if (hexString is null)
      {
        throw new ArgumentNullException(nameof(hexString));
      }

      if (hexString.Length != HashLength * 2)
      {
        throw new FormatException("Hex string length must be divisible by 2");
      }

      var hexStringSpan = hexString.AsSpan();

      var bytes = new byte[HashLength];

      for (var i = 0; i < bytes.Length; i++)
      {
        var substring = new string(hexStringSpan.Slice(i * 2, 2));
        bytes[i] = Convert.ToByte(substring, 16);
      }

      return new Hash(bytes);  
    }

    public override bool Equals(object obj) => obj is Hash hash && Equals(hash);

    public bool Equals(Hash other) => _bytes.Span.SequenceEqual(other._bytes.Span);

    public override int GetHashCode()
    {
      var hashCode = new HashCode();
      foreach (var b in _bytes.Span)
      {
        hashCode.Add(b);
      }
      return hashCode.ToHashCode();
    }

    public static bool operator ==(Hash left, Hash right) => left.Equals(right);
    public static bool operator !=(Hash left, Hash right) => !(left == right);

    public static implicit operator string(Hash hash) => hash.ToHex();
    public static explicit operator Hash(string hexString) => Hash.FromHex(hexString);
  }
}
