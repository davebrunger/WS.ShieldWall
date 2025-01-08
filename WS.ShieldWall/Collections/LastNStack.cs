using System.Collections;

namespace WS.ShieldWall.Collections;

public class LastNStack<T> : ICollection<T>
{
    private readonly List<T> stack = [];

    public int Count => stack.Count;
    
    public int MaxCount { get; private init; }
    
    public bool IsReadOnly => false;

    public LastNStack(int maxCount)
    {
        MaxCount = maxCount;
    }

    public void Add(T item)
    {
        stack.Add(item);
        if (stack.Count > MaxCount)
        {
            stack.RemoveAt(0);
        }
    }

    public void Clear()
    {
        stack.Clear();
    }

    public bool Contains(T item)
    {
        return stack.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        stack.CopyTo(array, arrayIndex);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return stack.GetEnumerator();
    }

    public bool Remove(T item)
    {
        return stack.Remove(item);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
