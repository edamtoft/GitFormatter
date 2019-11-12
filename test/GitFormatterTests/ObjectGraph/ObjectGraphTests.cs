using System;
using System.Collections.Generic;
using System.Text;
using GitFormatter;
using GitFormatter.Formatters;
using GitFormatter.Models;
using GitFormatter.ObjectGraph;
using GitFormatterTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitFormatterTests.ObjectGraph
{
  [TestClass]
  public class ObjectGraphTests
  {
    [TestMethod]
    public void SerializesStuff()
    {
      var hashWriter = new GitHashWriter(
        new CommitFormatter(), new TreeFormatter(), new BlobFormatter());

      var odb = new InMemoryObjectDb(hashWriter);
      var refDb = new InMemoryRefDb();

      var repo = new Repository(refDb, odb);

      var obj = new SampleRoot
      {
        Section = new SampleSection
        {
          Value1 = new SampleValue
          {
            Property1 = "Hello",
            Property2 = "World",
          },
          Value2 = new SampleValue
          {
            Property1 = "Fizz",
            Property2 = "Buzz",
          }
        },
        Value = new SampleValue
        {
          Property1 = "Foo",
          Property2 = "Bar"
        }
      };

      var treeHash = ObjectVisitor.VisitTree(obj, repo).Result;

      var tree = odb.Get(treeHash).Result as Tree;

      Assert.IsTrue(tree.Contains("Section"));
      Assert.IsTrue(tree.Contains("Value"));
      Assert.AreEqual(GitFileMode.Directory, tree["Section"].Mode);
      Assert.AreEqual(GitFileMode.File, tree["Value"].Mode);
    }
  }
}
