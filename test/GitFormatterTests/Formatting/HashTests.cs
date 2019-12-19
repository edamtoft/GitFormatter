using GitFormatter;
using GitFormatter.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GitFormatterTests.Formatting
{
  [TestClass]
  public class HashTests
  {
    [TestMethod]
    public void ComputeEmptyTreeHash()
    {
      var tree = Tree.Empty;

      var hash = tree.GetGitHash();

      Assert.AreEqual("4b825dc642cb6eb9a060e54bf8d69288fbee4904", hash.ToString());
    }

    [TestMethod]
    public void ComputeInitialCommitHash()
    {
      var signature = new Signature(
        "Eric Damtoft",
        "edamtoft@gmail.com",
        DateTimeOffset.FromUnixTimeSeconds(1573304272).ToOffset(TimeSpan.FromHours(-5)));

      var commit = new Commit(
        Hash.FromHex("4b825dc642cb6eb9a060e54bf8d69288fbee4904"),
        new Hash[0],
        signature,
        signature,
        "initial commit");

      var hash = commit.GetGitHash();

      Assert.AreEqual("08e57b124c3fb7347b5cb7f31b44053eee414f1d", hash.ToString());
    }

    [DataTestMethod]
    [DataRow("hello world", "95d09f2b10159347eece71399a7e2e907ea3df4f")]
    [DataRow("hello world 2", "93ab6b3948bee6940715c24b46ceb5f48c715be7")]
    public void ComputeTextBlobHash(string text, string expectedHash)
    {
      var hash = Blob.FromString(text).GetGitHash();

      Assert.AreEqual(expectedHash, hash.ToString());
    }

    [TestMethod]
    public void TreeWithSingleEntry()
    {
      var tree = new Tree(new Dictionary<string, TreeEntry>
      {
        ["sample.txt"] = new TreeEntry(GitFileMode.File, Hash.FromHex("95d09f2b10159347eece71399a7e2e907ea3df4f"))
      });

      var hash = tree.GetGitHash();

      Assert.AreEqual("e977ed57f8becb373fa5dc9af3e5d7dba39fcacd", hash.ToString());
    }

    [TestMethod]
    public void TreeWithMultipleEntries()
    {
      var tree = new Tree(new Dictionary<string, TreeEntry>
      {
        ["sample.txt"] = new TreeEntry(GitFileMode.File, Hash.FromHex("95d09f2b10159347eece71399a7e2e907ea3df4f")),
        ["sample2.txt"] = new TreeEntry(GitFileMode.File, Hash.FromHex("93ab6b3948bee6940715c24b46ceb5f48c715be7"))
      });

      var hash = tree.GetGitHash();

      Assert.AreEqual("d04b476e5c85567cc72603fa0386d89b89dfe8ba", hash.ToString());
    }

    [TestMethod]
    public void CommitWithParent()
    {
      var author = new Signature(
        "Eric Damtoft",
        "edamtoft@gmail.com",
        DateTimeOffset.FromUnixTimeSeconds(1573308076).ToOffset(TimeSpan.FromHours(-5)));

      var committer = new Signature(
        "Eric Damtoft",
        "edamtoft@gmail.com",
        DateTimeOffset.FromUnixTimeSeconds(1573317570).ToOffset(TimeSpan.FromHours(-5)));

      var commit = new Commit(
        Hash.FromHex("d04b476e5c85567cc72603fa0386d89b89dfe8ba"),
        new Hash[] { Hash.FromHex("08e57b124c3fb7347b5cb7f31b44053eee414f1d") },
        author,
        committer,
        "Add Sample Files");

      var hash = commit.GetGitHash();

      Assert.AreEqual("78b4e971f5602d603a4e235944fff4d9585d9894", hash.ToString());
    }
  }
}
