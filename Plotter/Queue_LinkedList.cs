using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotter
{
    public class Queue_LinkedList<T>
    {
        private Node head = null;
        private Node tail = null;
        private Node cursor = null;
        internal class Node
        {
            public T Item;
            public Node Next;
        }

        public void enqueue(T item)
        {
            if (tail == null && head == null)
            {
                Node n = new Node
                {
                    Item = item,
                    Next = null
                };
                tail = n;
                head = n;
            }
            else
            {
                Node n = new Node
                {
                    Item = item,
                    Next = null
                };
                tail.Next = n;
                tail = n;
            }
        }

        public T dequeue()
        {
            if (IsEmpty())
            {
                return default(T);
            }
            else if (head == tail) //Check for single item
            {
                var dequeued_item = head.Item;
                head = null;
                tail = null;
                return dequeued_item;
            }
            else //Check for single item
            {
                var dequeued_item = head.Item;
                head = head.Next;
                return dequeued_item;
            }
        }

        public bool IsEmpty()
        {
            return (head == null && tail == null) ? true : false;
        }

        public int Size()
        {
            if (IsEmpty())
            {
                return 0;
            }
            else
            {
                int count = 1;
                var temp = head;
                while (temp.Next != null)
                {
                    count++;
                    temp = temp.Next;
                }
                return count;
            }
        }


        public T MoveNext()
        { 
            if (cursor != null)
            {
                cursor=cursor.Next;
            }
            else
            {
                cursor = head;
            }
            return cursor != null ? cursor.Item : default(T);
        }

        public void Reset()
        {
            cursor = null;
        }
    }
}
