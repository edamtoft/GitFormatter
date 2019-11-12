using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GitFormatter.Models;
using Newtonsoft.Json;

namespace GitFormatter.ObjectGraph
{
  public static class ObjectVisitor
  {
    public static async Task<Hash> VisitObject(object item, IRepository repository)
    {
      var blob = Blob.FromString(JsonConvert.SerializeObject(item));

      return await repository.ObjectDb.HashObject(blob);
    }

    public static async Task<Hash> VisitTree(object item, IRepository repository)
    {
      var itemType = item.GetType();
      var properties = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

      var entries = await Task.WhenAll(properties.Select(async property =>
      {
        var isTree = property.IsDefined(typeof(TreeAttribute));
        var value = property.GetValue(item);
        var hash = isTree
          ? await VisitTree(value, repository)
          : await VisitObject(value, repository);

        return new TreeEntry(property.Name, isTree ? GitFileMode.Directory : GitFileMode.File, hash);
      }));

      var tree = new Tree();

      foreach (var entry in entries)
      {
        tree.Add(entry);
      }

      return await repository.ObjectDb.HashObject(tree);
    }
  }
}
