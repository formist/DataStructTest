using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructTest
{
    public unsafe class Vector<T> where T:IComparable
    {
        const int DEFAULT_CAPACITY = 3;
        int _size; int _capacity;T[] _elem;
        public Vector(int capacity = DEFAULT_CAPACITY, int s = 0)
        {
            _elem = new T[capacity];
            _capacity = capacity;
            _size = s;
        }
        ~Vector()
        {
            _size = 0;
            //_elem = null;
        }
        List<T> eee;
        public int Size { get { return _size; } }
        public bool IsEmpty { get { return _size == 0; } }
        
        public void CopyFrom(ref T[] A, int lo, int hi)
        {
            _elem = new T[_capacity = 2 * (hi - lo)];_size = 0;
            while (lo < hi)
                _elem[_size++] = A[lo++];
        }
        void Expand()
        {
            if (_size < _capacity) return;
            if (_capacity < DEFAULT_CAPACITY) _capacity = DEFAULT_CAPACITY;
            T[] oldElem = _elem;_elem = new T[_capacity <<= 1];
            for (int i = 0; i < _size; i++)
                _elem[i] = oldElem[i];
            //oldElem = null
        }
        void Shrink()
        {
            if (_capacity < DEFAULT_CAPACITY << 1) return;
            if (_size << 2 > _capacity) return;//以25%为界
            T[] oldElem = _elem;_elem = new T[_capacity >>= 1];
            for (int i = 0; i < _size; i++)
                _elem[i] = oldElem[i];
        }
        public T this[int index] { get { return _elem[index]; } set { if (index > _size - 1) throw new IndexOutOfRangeException(); _elem[index] = value; } }
        void Permute()
        {
            Random rand = new Random();
            for (int i = Size; i > 0; i--)
                Swap(ref _elem[i - 1], ref _elem[rand.Next() % i]);
        }
        public void Unsort(int lo,int hi)
        {
            Random rnd=new Random();
            T[] V = new T[hi-lo];
            Array.Copy (_elem,lo,V,0,hi-lo);
            for (int i = hi - lo; i > 0; i--)
                Swap(ref V[i - 1], ref V[rnd.Next() % i]);
            Array.Copy(V, 0, _elem, lo, hi - lo);
        }
        public static void Swap(ref T t1,ref T t2)
        {
            T tmp = t1;
            t1 = t2;
            t2 = tmp;
        }
        public int Find(T e, int lo, int hi)
        {
            while ((lo < hi--) && (!e.Equals(_elem[hi]))) ;
            return hi;
        }
        public int Insert(int r, T e)
        {
            Expand();
            for (int i = _size; i > r; i--) _elem[i] = _elem[i - 1];
            _elem[r] = e; _size++;
            return r;
        }
        public int Add(T e)
        {
            return Insert(_size, e);
        }
        public int Remove(int lo, int hi)
        {
            if (lo == hi) return 0;
            while (hi < _size) _elem[lo++] = _elem[hi++];
            _size = lo;
            Shrink();
            return hi - lo;
        }
        public T Remove(int r)
        {
            T e = _elem[r];
            Remove(r, r + 1);
            return e;
        }
        public int Deduplicate()
        {
            int oldSize = _size;
            int i = 1;
            while (i < _size)
            {
                if (Find(_elem[i], 0, i) < 0)
                    i++;
                else
                    Remove(i);
            }
            return oldSize - _size;
        }
        public void Traverse(Action<T> visit)
        {
            for (int i = 0; i < _size; i++) visit(_elem[i]);
        }
        public int Disordered()
        {
            int n = 0;
            for (int i = 1; i < _size; i++)
                if (_elem[i - 1].CompareTo(_elem[i]) > 0) n++;
            return n;
        }
        public static int BinSearch()
        {
            return 0;
        }
        public void BubbleSort(int lo, int hi)
        {
            while (!Bubble(lo, hi--)) ;
        }
        private bool Bubble(int lo, int hi)
        {
            bool sorted = true;
            while (++lo < hi)
            {
                if (_elem[lo - 1].CompareTo(_elem[lo]) > 0)
                {
                    sorted = false;
                    Swap(ref _elem[lo - 1], ref _elem[lo]);
                }
            }
            return sorted;
        }
        public int Uniquify()
        {
            int i = 0, j = 0;
            while (++j < _size)
                if (!_elem[i] .Equals( _elem[j]))
                    _elem[++i] = _elem[j];
            _size = ++i; Shrink();
            return j - i;

        }
        public static int BinSearch(Vector<T> A, T e, int lo, int hi)
        {
            while (lo < hi)
            {
                int mi = (lo + hi) >> 1;
                if (e.CompareTo(A[mi]) < 0) hi = mi;
                else if (A[mi].CompareTo(e) < 0) lo = mi + 1;
                else return mi;
            }
            return -1;
        }
        public void MergeSort(int lo, int hi)
        {
            if (hi - lo < 2) return;
            int mi = (lo + hi) >> 1;
            MergeSort(lo, mi); MergeSort(mi, hi);
            Merge(lo, mi, hi);
        }
        private void Merge(int lo, int mi, int hi)
        {
            T[] A = new T[hi - lo];
            Array.Copy(_elem, lo, A, 0, hi - lo);
            //for (int i = 0; i < hi - lo; A[i] = _elem[lo + i++]) ;
            int lb = mi - lo; T[] B = new T[lb];
            for (int i = 0; i < lb; B[i] = A[i++]) ;
            int lc = hi - mi; T[] C = new T[lc];
            for (int i = 0; i < lc; C[i] = A[lb+ i++]) ;
            for (int i = 0, j = 0, k = 0; (j < lb) || (k < lc); )
            {
                if ((j < lb) && (!(k < lc) || (B[j].CompareTo(C[k]) <= 0))) A[i++] = B[j++];
                if ((k < lc) && (!(j < lb) || (C[k].CompareTo(B[j]) < 0))) A[i++] = C[k++];
            }
            Array.Copy(A, 0, _elem, lo, hi - lo);
        }
        //public static int FibSearch(Vector<T> A, T e, int lo, int hi)
        //{

        //}
        //public static Vector<T> operator =(Vector<T> V)
        //{
        //    _size = 0;_elem = null;
        //    CopyFrom(ref V._elem, 0, V.Size);
        //    return this;
        //} 
        //public static Vector<T> operator +(Vector<T> lhs, Vector<T> rhs)
        //{
            
        //}
    }
}
