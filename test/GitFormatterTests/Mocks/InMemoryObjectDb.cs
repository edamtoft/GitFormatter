using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GitFormatter;
using GitFormatter.Formatters;
using GitFormatter.Models;

namespace GitFormatterTests.Mocks
{
  public sealed class InMemoryObjectDb : IObjectDb
  {
    private readonly IGitHashWriter _hashWriter;
    private readonly Dictionary<Hash, GitObject> _objects = new Dictionary<Hash, GitObject>();

    public InMemoryObjectDb(IGitHashWriter hashWriter)
    {
      _hashWriter = hashWriter;
    }

    public Task<GitObject> Get(Hash hash)
    {
      return Task.FromResult(_objects[hash]);
    }

    public Task<Hash> HashObject(GitObject obj)
    {
      var hash = _hashWriter.Write(obj, out var _);
      _objects[hash] = obj;
      return Task.FromResult(hash);
    }
  }
}
