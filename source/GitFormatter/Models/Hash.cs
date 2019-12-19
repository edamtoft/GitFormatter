using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using GitFormatter.Utils;

namespace GitFormatter.Models
{
  public readonly partial struct Hash : IEquatable<Hash>
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

    public override bool Equals(object obj) => obj is Hash hash && Equals(hash);

    public bool Equals(Hash other) => Span.SequenceEqual(other.Span);

    public override int GetHashCode()
    {
      var hashCode = new HashCode();
      foreach (var b in Span)
      {
        hashCode.Add(b);
      }
      return hashCode.ToHashCode();
    }

    public override string ToString() => ToHex();

    public static bool operator ==(Hash left, Hash right) => left.Equals(right);
    public static bool operator !=(Hash left, Hash right) => !(left == right);

    public static implicit operator string(Hash hash) => hash.ToHex();
    public static explicit operator Hash(string hexString) => Hash.FromHex(hexString);
  }
}
