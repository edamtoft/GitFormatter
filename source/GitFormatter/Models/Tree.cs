using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using GitFormatter.Utils;

namespace GitFormatter.Models
{
  public sealed partial class Tree : GitObject, IEnumerable<KeyValuePair<string,TreeEntry>>
  {
    public Tree(IEnumerable<KeyValuePair<string, TreeEntry>> items)
    {
      _items = items.ToImmutableSortedDictionary(StringComparer.Ordinal);
    }

    public Tree()
    {
      _items = ImmutableSortedDictionary.Create<string, TreeEntry>(StringComparer.Ordinal);
    }

    private readonly ImmutableSortedDictionary<string, TreeEntry> _items;

    public ImmutableSortedDictionary<string, TreeEntry>.Enumerator GetEnumerator() => _items.GetEnumerator();
    IEnumerator<KeyValuePair<string, TreeEntry>> IEnumerable<KeyValuePair<string, TreeEntry>>.GetEnumerator() => GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public TreeEntry this[string key] => _items[key];
    public bool ContainsPath(string path) => _items.ContainsKey(path);
    public bool TryGetValue(string path, out TreeEntry entry) => _items.TryGetValue(path, out entry);
    public Tree Update(string path, TreeEntry entry) => new Tree(_items.SetItem(path, entry));
    public Tree Update(string path, GitFileMode mode, Hash hash) => new Tree(_items.SetItem(path, new TreeEntry(mode, hash)));

    public static Tree Empty { get; } = new Tree();
  }
}
