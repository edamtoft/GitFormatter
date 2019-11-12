using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GitFormatter.Models
{
  public sealed class Tree : GitObject, ICollection<TreeEntry>
  {
    private readonly HashSet<TreeEntry> _items = new HashSet<TreeEntry>(PathComparer.Default);

    public int Count => _items.Count;

    public bool IsReadOnly => false;

    public void Add(TreeEntry entry) => _items.Add(entry);

    public bool Remove(TreeEntry item) => _items.Remove(item);

    public bool Remove(string path) => _items.RemoveWhere(item => item.Path == path) > 0;

    public IEnumerator<TreeEntry> GetEnumerator() => _items.OrderBy(item => item.Path).GetEnumerator();

    public void Clear() => _items.Clear();

    bool ICollection<TreeEntry>.Contains(TreeEntry item) => _items.Contains(item);

    void ICollection<TreeEntry>.CopyTo(TreeEntry[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
  }
}
