using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitFormatter.Models;

namespace GitFormatter
{
  public static class RepositoryExtensions
  {
    public static async Task<bool> Commit(this IRepository repository, string reference, Commit commit, bool force)
    {
      var existingHash = await repository.ReferenceDb.Get(reference);

      if (!force && existingHash.HasValue && commit.Parents.Contains(existingHash.Value))
      {
        return false;
      }

      var commitHash = await repository.ObjectDb.HashObject(commit);

      return await repository.ReferenceDb.Put(reference, commitHash, existingHash);
    }
  }
}
