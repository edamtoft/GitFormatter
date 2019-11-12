using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GitFormatter.Models;

namespace GitFormatter
{
  public interface IReferenceDb
  {
    Task Delete(string name);
    Task<bool> Put(string name, Hash reference, Hash? expect);
    Task<Hash?> Get(string name);
  }
}
