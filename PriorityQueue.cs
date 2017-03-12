// Cory Hunt
// 3/12/2017
// ArenaNet Coding Test Q2
// Double-ended Priority Queue - Dual heap approach
//

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

    List<PriorityElement> min_heap = new List<PriorityElement>();
    List<PriorityElement> max_heap = new List<PriorityElement>();

    public int Count
    {
        get { return min_heap.Count; }
    }

    public void Enqueue( T inValue, int inPriority )
    {
        min_heap.Add( new PriorityElement( inValue, inPriority ) );
        max_heap.Add( new PriorityElement( inValue, inPriority ) );

        MinBubbleUp();
        MaxBubbleUp();
    }

    public bool DequeueMin( out T value )
    {
        if( min_heap.Count < 1 )
        {
            value = default( T );
            return false;
        }

        // Return front; swap and delete last
        value = min_heap[0].Value;
        max_heap.Remove( min_heap[0] );
        min_heap[0] = min_heap[min_heap.Count-1];
        min_heap.RemoveAt( min_heap.Count-1 );

        MinBubbleDown();
        MaxBubbleDown();

        return true;
    }

    public bool DequeueMax( out T value )
    {
        if( max_heap.Count < 1 )
        {
            value = default( T );
            return false;
        }

        // Return front; swap and delete last
        value = max_heap[0].Value;
        min_heap.Remove( max_heap[0] );
        max_heap[0] = max_heap[max_heap.Count-1];
        max_heap.RemoveAt( max_heap.Count-1 );

        MinBubbleDown();
        MaxBubbleDown();

        return true;
    }

    private void MinBubbleUp(  )
    {
        int p, i = min_heap.Count - 1;
        while( i > 0 )
        {
            // Parent index
            p = (i - 1) / 2;

            // Chld (i) is larger than parent (p), so we are finished inserting into heap
            if( min_heap[i].Priority > min_heap[p].Priority )
                break;

            // Bubble up
            PriorityElement tmp = min_heap[i];
            min_heap[i] = min_heap[p];
            min_heap[p] = tmp;
            i = p;
        }
    }

    private void MaxBubbleUp()
    {
        int p, i = max_heap.Count - 1;
        while( i > 0 )
        {
            // Parent index
            p = (i - 1) / 2;

            // Chld (i) is smaller than parent (p), so we are finished inserting into heap
            if( max_heap[i].Priority < max_heap[p].Priority )
                break;

            // Bubble up
            PriorityElement tmp = max_heap[i];
            max_heap[i] = max_heap[p];
            max_heap[p] = tmp;
            i = p;
        }
    }

    private void MinBubbleDown()
    {
        // Left child, right child, parent
        int i, j, p = 0;

        // Left child index
        i = (p * 2 + 1);

        while( i <= (min_heap.Count - 1) )
        {
            // If right child (i) exists and is less than left child j), we want to look at right child
            if( (j = i + 1) <= (min_heap.Count - 1) && (min_heap[j].Priority < min_heap[i].Priority) ) 
                i = j;

            // Chld (i) is larger than parent (p), so we are finished
            if( min_heap[i].Priority > min_heap[p].Priority )
                break; 

            // Bubble down
            PriorityElement tmp = min_heap[p];
            min_heap[p] = min_heap[i];
            min_heap[i] = tmp;
            p = i;

            // Look at next child
            i = (p * 2 + 1);
        }
    }

    private void MaxBubbleDown()
    {
        // Left child, right child, parent
        int i, j, p = 0;

        // Left child index
        i = (p * 2 + 1);

        while( i <= (max_heap.Count - 1) )
        {
            // If right child (i) exists and is greater than left child j), we want to look at right child
            if( (j = i + 1) <= (max_heap.Count - 1) && (max_heap[j].Priority > max_heap[i].Priority) )
                i = j;

            // Chld (i) is smaller than parent (p), so we are finished
            if( max_heap[i].Priority < max_heap[p].Priority )
                break;

            // Bubble down
            PriorityElement tmp = max_heap[p];
            max_heap[p] = max_heap[i];
            max_heap[i] = tmp;
            p = i;

            // Look at next child
            i = (p * 2 + 1);
        }
    }    
}