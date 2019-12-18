using System.Threading.Tasks;
using GitFormatter.Models;

namespace GitFormatter
{
  public interface IRepository
  {
    IReferenceDb ReferenceDb { get; }
    IObjectDb ObjectDb { get; }

    Task<bool> Commit(string reference, Commit commit, bool force);
  }
}