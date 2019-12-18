using System;

namespace GitFormatter.Models
{
  public readonly struct Signature : IEquatable<Signature>
  {
    public Signature(string name, string email, DateTimeOffset when)
    {
      Name = name ?? throw new ArgumentNullException(nameof(name));
      Email = email ?? throw new ArgumentNullException(nameof(email));
      When = when;
    }

    public string Name { get; }
    public string Email { get; }
    public DateTimeOffset When { get; }

    public override bool Equals(object obj)
    {
      return obj is Signature signature && Equals(signature);
    }

    public bool Equals(Signature other)
    {
      return Name == other.Name &&
             Email == other.Email &&
             When.Equals(other.When);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Name, Email, When);
    }

    public override string ToString() => $"{Name} <{Email}>";

    public static bool operator ==(Signature left, Signature right) => left.Equals(right);

    public static bool operator !=(Signature left, Signature right) => !(left == right);
  }
}
