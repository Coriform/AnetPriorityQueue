using System.Collections.Generic;

public class PriorityQueue<T>
{
    struct PriorityElement
    {
        public PriorityElement( T inVal, int inP )
        {
            Value = inVal;
            Priority = inP;
        }

        public T Value;
        public int Priority;
    }

    private List<PriorityElement> min_heap = new List<PriorityElement>();

    public int Count
    {
        get { return min_heap.Count; }
    }

    public void Clear()
    {
        min_heap.Clear();
    }

    public void Enqueue( T inValue, int inPriority )
    {
        min_heap.Add( new PriorityElement( inValue, inPriority ) );
		
        BubbleUp();
    }

    public bool DequeueMin( out T value )
    {
        if( min_heap.Count < 1 )
        {
            value = default( T );
            return false;
        }

        value = min_heap[0].Value;
        min_heap[0] = min_heap[min_heap.Count-1];
        min_heap.RemoveAt( min_heap.Count-1 );

        BubbleDown();

        return true;
    }

    private void BubbleUp(  )
    {
        int i, p;

        for( i = min_heap.Count - 1; i > 0; i = p )
        {
            p = (i - 1) / 2;

            if( min_heap[i].Priority > min_heap[p].Priority )
                break;

            PriorityElement tmp = min_heap[i];
            min_heap[i] = min_heap[p];
            min_heap[p] = tmp;
        }
    }

    private void BubbleDown()
    {
        int i, j, p = 0;

        for( i = 1; i <= min_heap.Count - 1; i = (p * 2 + 1) )
        {
            if( (j = i + 1) <= (min_heap.Count - 1) && (min_heap[j].Priority < min_heap[i].Priority) ) 
                i = j;

            if( min_heap[i].Priority > min_heap[p].Priority )
                break; 

            PriorityElement tmp = min_heap[p];
            min_heap[p] = min_heap[i];
            min_heap[i] = tmp;
            p = i;
        }
    }
}