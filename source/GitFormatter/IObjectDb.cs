using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GitFormatter.Models;

namespace GitFormatter
{
  public interface IObjectDb
  {
    Task<Hash> Put(GitObject obj);

    Task<TGitObject> Get<TGitObject>(Hash hash) where TGitObject : GitObject;
  }
}
