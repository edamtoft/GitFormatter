using System;
using System.Collections.Generic;
using System.Text;

namespace GitFormatter.ObjectGraph
{
  [AttributeUsage(AttributeTargets.Property)]
  public sealed class TreeAttribute : Attribute
  {
  }
}
