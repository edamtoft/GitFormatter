using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GitFormatter;
using GitFormatter.Models;

namespace GitFormatterTests.Mocks
{
  public sealed class InMemoryObjectDb : IObjectDb
  {
    private readonly ConcurrentDictionary<Hash, GitObject> _objects = new ConcurrentDictionary<Hash, GitObject>();

    Task<TGitObject> IObjectDb.Get<TGitObject>(Hash hash) => Task.FromResult(Get<TGitObject>(hash));

    Task<Hash> IObjectDb.Put(GitObject obj) => Task.FromResult(Put(obj));

    public TGitObject Get<TGitObject>(Hash hash) where TGitObject : GitObject
    {
      return _objects.TryGetValue(hash, out var obj) 
        ? obj as TGitObject 
        : null;
    }

    public Hash Put(GitObject obj)
    {
      var hash = obj.GetGitHash();
      _objects.TryAdd(hash, obj);
      return hash;
    }
  }
}
