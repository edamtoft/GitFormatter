using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GitFormatter;
using GitFormatter.Models;

namespace GitFormatterTests.Mocks
{
  public sealed class InMemoryRefDb : IReferenceDb
  {
    private readonly Dictionary<string, Hash> _dict = new Dictionary<string, Hash>();

    public Task Delete(string name)
    {
      _dict.Remove(name);
      return Task.CompletedTask;
    }

    public Task<Hash?> Get(string name)
    {
      return Task.FromResult(_dict.TryGetValue(name, out var hash) ? (Hash?) hash : null);
    }

    public Task<bool> Put(string name, Hash reference, Hash? expect)
    {
      _dict[name] = reference;
      return Task.FromResult(true);
    }
  }
}
