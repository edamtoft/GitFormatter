using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace GitFormatter.Models
{
  public abstract class GitObject
  {
    internal GitObject()
    {
    }

    public abstract ReadOnlySpan<byte> GetGitFormat();

    public Hash GetGitHash()
    {
      using var sha = SHA1.Create();
      var bytes = GetGitFormat();
      return new Hash(sha.ComputeHash(bytes.ToArray()));
    }
  }
}
