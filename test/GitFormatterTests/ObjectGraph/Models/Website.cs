using System;
using System.Collections.Generic;
using System.Text;
using GitFormatter.ObjectGraph;

namespace GitFormatterTests.ObjectGraph.Models
{
  class Website
  {
    [Tree]
    public SeoConfig Seo { get; set; }

    public WebsiteHeader Header { get; set; }
  }
}
