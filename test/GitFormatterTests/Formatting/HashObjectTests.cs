using GitFormatter.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitFormatterTests.Formatting
{
  [TestClass]
  public class HashObjectTests
  {
    public void EqualityComparison()
    {
      var a = Hash.FromHex("4b825dc642cb6eb9a060e54bf8d69288fbee4904");
      var b = Hash.FromHex("4b825dc642cb6eb9a060e54bf8d69288fbee4904");

      Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
      Assert.IsTrue(a.Equals(b));
      Assert.IsTrue(a == b);
    }

    public void NegativeEqualityComparison()
    {
      var a = Hash.FromHex("4b825dc642cb6eb9a060e54bf8d69288fbee4904");
      var b = Hash.FromHex("4b825dc642cb6eb9a060e54bf8d69288fbee4905");

      Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());
      Assert.IsFalse(a.Equals(b));
      Assert.IsFalse(a == b);
    }
  }
}
