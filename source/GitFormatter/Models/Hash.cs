using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GitFormatter.Models
{
  public readonly struct Hash : IEquatable<Hash>
  {
    public byte[] Bytes { get; }

    public const int HashLength = 20;

    public Hash(byte[] bytes)
    {
      if (bytes == null)
      {
        throw new ArgumentNullException(nameof(bytes));
      }

      if (bytes.Length != HashLength)
      {
        throw new ArgumentException("Incorrect hash size");
      }

      Bytes = bytes;
    }

    public override string ToString()
    {
      var result = new StringBuilder(Bytes.Length * 2);
      foreach (var b in Bytes)
      {
        result.Append(b.ToString("x2"));
      }
      return result.ToString();
    }

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

      var bytes = new byte[HashLength];

      for (var i = 0; i < bytes.Length; i++)
      {
        bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
      }

      return new Hash(bytes);  
    }

    public override bool Equals(object obj) => obj is Hash hash && Equals(hash);

    public bool Equals(Hash other) => Enumerable.SequenceEqual(Bytes, other.Bytes);

    public override int GetHashCode() => ToString().GetHashCode();

    public static bool operator ==(Hash left, Hash right) => left.Equals(right);

    public static bool operator !=(Hash left, Hash right) => !(left == right);
  }
}
