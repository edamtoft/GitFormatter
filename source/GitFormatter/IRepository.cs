using System.Threading.Tasks;
using GitFormatter.Models;

namespace GitFormatter
{
  public interface IRepository
  {
    IObjectDb ObjectDb { get; }
    IReferenceDb ReferenceDb { get; }
  }
}