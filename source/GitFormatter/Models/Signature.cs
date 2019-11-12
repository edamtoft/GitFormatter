using System;

namespace GitFormatter.Models
{
  public sealed class Signature
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
  }
}
