using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using GitFormatter;
using GitFormatter.Models;
using GitFormatter.ObjectGraph;
using GitFormatterTests.Mocks;
using GitFormatterTests.ObjectGraph.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitFormatterTests.ObjectGraph
{
  [TestClass]
  public class ObjectGraphTests
  {
    [TestMethod]
    public async Task ObjectUpdate()
    {
      var odb = new InMemoryObjectDb();
      var refDb = new InMemoryRefDb();
      var repo = new Repository(refDb, odb);
      var graph = new GitObjectGraph<Website>(repo);

      var emptyTree = odb.Put(new Tree());

      var newTree = await graph.Set(emptyTree, o => o.Seo.General, new GeneralSeoConfig
      {
        Description = "Sample Description",
        Keywords = new List<string>
        {
          "sample"
        }
      });

      var root = odb.Get<Tree>(newTree);
      
      Assert.IsNotNull(root);
      Assert.IsTrue(root.ContainsPath(nameof(Website.Seo)));
      Assert.AreEqual(GitFileMode.Directory, root[nameof(Website.Seo)].Mode);

      var seo = odb.Get<Tree>(root[nameof(Website.Seo)].Hash);

      Assert.IsNotNull(seo);
      Assert.IsTrue(seo.ContainsPath(nameof(SeoConfig.General)));
      Assert.AreEqual(GitFileMode.File, seo[nameof(SeoConfig.General)].Mode);

      var general = odb.Get<Blob>(seo[nameof(SeoConfig.General)].Hash);

      Assert.IsNotNull(general);

      var json = JsonSerializer.Deserialize<GeneralSeoConfig>(general.Content.Span);

      Assert.AreEqual("Sample Description", json.Description);
    }
  }
}
