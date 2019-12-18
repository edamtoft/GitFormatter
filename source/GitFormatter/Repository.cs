using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitFormatter.Models;

namespace GitFormatter
{
  public sealed class Repository : IRepository
  {
    public Repository(IReferenceDb referenceDb, IObjectDb objectDb)
    {
      ReferenceDb = referenceDb;
      ObjectDb = objectDb;
    }

    public IReferenceDb ReferenceDb { get; }
    public IObjectDb ObjectDb { get; }

    public async Task<bool> Commit(string reference, Commit commit, bool force)
    {
      var existingHash = await ReferenceDb.Get(reference);

      if (!force && existingHash.HasValue && commit.Parents.Contains(existingHash.Value))
      {
        return false;
      }

      var commitHash = await ObjectDb.Put(commit);

      return await ReferenceDb.Put(reference, commitHash, existingHash);
    }
  }
}
