using GitFormatter.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GitFormatter.ObjectGraph
{
  public sealed class GitObjectGraph<TRoot>
  {
    public GitObjectGraph(IRepository repository)
    {
      Repository = repository;
    }

    private IRepository Repository { get; }

    public async Task<Hash> Set<TNode>(Hash commit, Expression<Func<TRoot, TNode>> expression, TNode node)
    {
      var parentCommit = await Repository.ObjectDb.Get<Commit>(commit);

      var path = ImmutableArray.CreateRange(ExpressionToPath(expression.Body));

      var root = await Visit(parentCommit.Tree, path, node);

      return root.Hash;
    }

    private async Task<TreeEntry> Visit<TNode>(Hash? hash, ImmutableArray<string> path, TNode node)
    {
      if (path.Length == 0)
      {
        var blob = new Blob(JsonSerializer.SerializeToUtf8Bytes(node, new JsonSerializerOptions { WriteIndented = true }));
        return new TreeEntry(GitFileMode.File, await Repository.ObjectDb.Put(blob));
      }

      var originalTree = hash.HasValue 
        ? await Repository.ObjectDb.Get<Tree>(hash.Value) 
        : new Tree();

      var next = originalTree.TryGetValue(path[0], out var entry) ? entry.Hash : (Hash?)null;

      var updatedTree = originalTree.Update(path[0], await Visit(next, path.RemoveAt(0), node));

      return new TreeEntry(GitFileMode.Directory, await Repository.ObjectDb.Put(updatedTree));
    }

    private IEnumerable<string> ExpressionToPath(Expression expression)
    {
      switch (expression)
      {
        case MemberExpression e:
          foreach (var segment in ExpressionToPath(e.Expression))
          {
            yield return segment;
          }
          yield return e.Member.Name;
          break;
        case ParameterExpression _:
          break;
        default:
          throw new NotSupportedException("Path not supported");
      }
    }
  }
}
