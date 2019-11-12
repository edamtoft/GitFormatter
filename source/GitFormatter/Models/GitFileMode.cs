using System;
using System.Collections.Generic;
using System.Text;

namespace GitFormatter.Models
{
  public enum GitFileMode
  {
    File = 100644,
    Directory = 040000,
  }
}
