using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace GitFormatter.Models
{
  [TypeConverter(typeof(Converter))]
  partial struct Hash
  {
    private const string HexChars = "0123456789abcdef";

    public string ToHex() => string.Create(_bytes.Length * 2, _bytes, (chars, bytes) =>
    {
      var charIndex = 0;
      var bufferIndex = 0;
      while (charIndex < chars.Length)
      {
        var b = bytes.Span[bufferIndex];
        chars[charIndex] = HexChars[b / 16];
        chars[charIndex + 1] = HexChars[b % 16];
        bufferIndex++;
        charIndex += 2;
      }
    });

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

      var hexSpan = hexString.AsSpan();
      var bytes = new byte[HashLength];
      var charIndex = 0;
      var bufferIndex = 0;
      while (charIndex < hexString.Length)
      {
        bytes[bufferIndex] = byte.Parse(hexSpan.Slice(charIndex, 2), NumberStyles.HexNumber);
        bufferIndex++;
        charIndex += 2;
      }

      return new Hash(bytes);
    }

    class Converter : StringConverter
    {
      public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof(Hash);
      public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) => FromHex((string)value);
      public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) => ((Hash)value).ToHex();
    }
  }
}
