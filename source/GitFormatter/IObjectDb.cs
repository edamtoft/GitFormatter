using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GitFormatter.Models;

namespace GitFormatter
{
  public interface IObjectDb
  {
    Task<Hash> HashObject(GitObject obj);

    Task<GitObject> Get(Hash hash);
  }
}
