using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructTest
{
    public class CListNode<T>
    {
        T data;
        CListNode<T> pre;
        CListNode<T> next;
        public LinkedListNode<T> ee;
        public CListNode(){}
        public CListNode(T e, CListNode<T> p = null, CListNode<T> n = null)
        {
            this.data = e;
            pre = p;
            next = n;
        }
        public T Data
        {
            get { return this.data; }
            set { this.data = value; }
        }
        public CListNode<T> Previous { get { return this.pre; } set { this.pre = value; } }
        public CListNode<T> Next { get { return this.next; } set { this.next = value; } }
        public CListNode<T> InsertAsPre(T e)
        {
            CListNode<T> x = new CListNode<T>(e, pre, this);
            pre.next = x; pre = x;
            return x;
        }
        public CListNode<T> InsertAsNext(T e)
        {
            CListNode<T> x = new CListNode<T>(e, this, next);
            next.pre = x; next = x;
            return x;
        }
    }
    /// <summary>
    /// 链表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CList<T> where T : IComparable
    {
        int _size;
        CListNode<T> header;
        CListNode<T> trailer;
        public int Size { get { return this._size; } }
        public CList()
        {
            Init();
        }
        public CList(CListNode<T> p, int n)
        {
            CopyNodes(p, n);
        }
        public CList(CList<T> CL)
        {
            CopyNodes(CL.First, CL.Size);
        }
        //public CList(CList<T> CL, int r, int n)
        //{
        //    CopyNodes(CL[r], n);
        //}
        ~CList()
        {
            Clear();
            header = null;
            trailer = null;
        }
        void Init()
        {
            header = new CListNode<T>();
            trailer = new CListNode<T>();
            header.Next = trailer; header.Previous = null;
            trailer.Next = null; trailer.Previous = header;
            _size = 0;
        }
        public CListNode<T> First { get { return header.Next; } }
        public CListNode<T> Last { get { return trailer.Previous; } }
        public T this[int index]
        {
            get
            {
                CListNode<T> p = header;
                while (0 < index--) p = p.Next;
                return p.Data;
            }
            set
            {
                CListNode<T> p = header;
                while (0 < index--) p = p.Next;
                p.Data=value;
            }
        }
        public CListNode<T> Find(T e, int n, CListNode<T> p)
        {
            while (0 < n--)
                if (e.Equals((p = p.Previous).Data)) return p;
            return null;
        }
        public CListNode<T> InsertAsFirst(T e)
        {
            _size++;
            return header.InsertAsNext(e);
        }
        public CListNode<T> InsertAsLast(T e)
        {
            _size++;
            return trailer.InsertAsPre(e);
        }
        public CListNode<T> InsertAfter(CListNode<T> p, T e)
        {
            _size++;
            return p.InsertAsNext(e);
        }
        public CListNode<T> InsertBefore(CListNode<T> p, T e)
        {
            _size++;
            return p.InsertAsPre(e);
        }
        private void CopyNodes(CListNode<T> p, int n)
        {
            if (p == null) return;
            Init();
            while (0 < n--) { InsertAsLast(p.Data); p = p.Next; if (p.Equals(trailer))break; }
        }
        public T Remove(CListNode<T> p)
        {
            T e = p.Data;
            p.Previous.Next = p.Next; p.Next.Previous = p.Previous;
            p = null; //default(CListNode<T>);
            _size--;
            return e;
        }
        public int Clear()
        {
            int oldSize = _size;
            while (0 < _size) Remove(header.Next);
            return oldSize;
        }
        public int Deduplicate()
        {
            if (_size < 2) return 0;
            int oldSize = _size;
            CListNode<T> p = header; int r = 0;
            while (trailer != (p = p.Next))
            {
                CListNode<T> q = Find(p.Data, r, p);
                if (q == null) r++;
                else Remove(q);
            }
            return oldSize - _size;
        }
        public void Traverse(Action<T> visit)
        {
            for (CListNode<T> p = header.Next; p != null && !p.Equals(trailer); p = p.Next)
                visit(p.Data);
        }

        public int Uniquify()
        {
            if (_size < 2) return 0;
            int oldSize = _size;
            CListNode<T> p = First; CListNode<T> q;
            while (trailer != (q = p.Next))
            {
                if (!p.Data.Equals(q.Data)) p = q;
                else Remove(q);
            }
            return oldSize - _size;
        }
        public CListNode<T> Search(T e, int n, CListNode<T> p)
        {
            while (0 <= n--)
            {
                p = p.Previous;
                if (p == null || p.Equals(header) || p.Data.CompareTo(e)<=0) break;
            }
            return p;
        }
        public void InsertionSort(CListNode<T> p, int n)
        {
            for (int r = 0; r < n; r++)
            {
                if (p.Equals(trailer)) break;
                InsertAfter(Search(p.Data, r, p), p.Data);
                p = p.Next;  Remove(p.Previous);
            }
        }
        CListNode<T> SelectMax(CListNode<T> p, int n)
        {
            CListNode<T> max = p;
            for (CListNode<T> cur = p; 1 < n; n--)
            {
                cur = cur.Next;
                if (cur.Data.CompareTo(max.Data) >= 0) max = cur;
            }
            return max;
        }
        public void SelectionSort(CListNode<T> p, int n)
        {
            CListNode<T> head = p.Previous; CListNode<T> tail = p;
            for (int r = 0; r < n; r++) tail = tail.Next;
            while (1 < n)
            {
                CListNode<T> max = SelectMax(head.Next, n);
                InsertBefore(tail, Remove(max));
                tail = tail.Previous;
                n--;
            }
        }
        void Merge(ref CListNode<T> p, int n, CList<T> L,ref  CListNode<T> q, int m)
        {
            //if (p.Equals(header) || q.Equals(trailer)) return;
            CListNode<T> pp = p.Previous;
            while (0 < m)
            {
                if (p.Equals(header) || q.Equals(trailer)) break;
                if ((0 < n) && (p.Data.CompareTo(q.Data) <= 0))
                {
                    if (q .Equals (p = p.Previous)) break;
                    n--;
                }
                else
                {
                    InsertBefore(p, L.Remove((q = q.Next).Previous)); //将q转移至p之前
                    m--;
                }
            }
            p = pp.Next;
        }
        public void MergeSort(CListNode<T> p, int n)
        {
            if (n < 2) return;
            int m = n >> 1;
            CListNode<T> q = p; for (int i = 0; i < m; i++) q = q.Next;
            MergeSort(p, m); MergeSort(q, n - m);
            CList<T> tmp = new CList<T>(p, n - m);
            Merge(ref p, m,this, ref q, n - m);
            Traverse(Output);
            Console.Write('\n');
        }
        void Output(T i)
        {
            Console.Write(i.ToString());
            Console.Write(' ');
        }
    }
}
