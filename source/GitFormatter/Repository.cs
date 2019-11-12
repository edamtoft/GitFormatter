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
  }
}
