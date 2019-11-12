using System;
using System.Collections.Generic;
using System.Text;
using GitFormatter.ObjectGraph;

namespace GitFormatterTests.ObjectGraph
{
  class SampleRoot
  {
    [Tree]
    public SampleSection Section { get; set; }
    public SampleValue Value { get; set; }
  }
}
