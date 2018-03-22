using System;
using System.Collections.Generic;

namespace DEnt
{
    /// <summary>
    /// A regular list that is also capable of raising events for when the collection is changed.
    /// </summary>
    /// <remarks>
    /// Copied from Apollyon.
    /// </remarks>
    public class ManagedList<T> : List<T>
    {

        /* ----------------------------------------------------------------------------------------------------------------------------------------- */

        #region Class Members

        #endregion

        /* ----------------------------------------------------------------------------------------------------------------------------------------- */

        #region Constructor & Intialisation

        /// <summary>
        /// Initializes a new instance of the System.Collections.Generic.List`1 class that
        /// is empty and has the default initial capacity.
        /// </summary>
        public ManagedList()
            : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the System.Collections.Generic.List`1 class that
        /// is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store.</param>
        public ManagedList( int capacity )
            : base( capacity )
        {

        }

        /// <summary>
        /// Initializes a new instance of the System.Collections.Generic.List`1 class that
        /// contains elements copied from the specified collection and has sufficient capacity
        /// to accommodate the number of elements copied.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        public ManagedList( IEnumerable<T> collection )
            : base( collection )
        {

        }

        #endregion

        /* ----------------------------------------------------------------------------------------------------------------------------------------- */

        #region Public Methods

        /// <summary>
        /// Adds an object to the end of the ManagedList&lt;T&gt;.
        /// </summary>
        /// <param name="item">The object to be added to the end of the ManagedList&lt;T&gt;. The value can be null for reference types.</param>
        public new void Add( T item )
        {
            base.Add( item );

            this.RaiseCollectionChanged();
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the ManagedList&lt;T&gt;.
        /// </summary>
        /// <param name="collection">
        /// The collection whose elements should be added to the end of the ManagedList&lt;T&gt;.
        /// The collection itself cannot be null, but it can contain elements that are null, if type T is a reference type.
        /// </param>
        public new void AddRange( IEnumerable<T> collection )
        {
            base.AddRange( collection );

            this.RaiseCollectionChanged();
        }

        /// <summary>
        /// Removes all elements from the ManagedList&lt;T&gt;.
        /// </summary>
        public new void Clear()
        {
            base.Clear();

            this.RaiseCollectionChanged();
        }

        /// <summary>
        /// Inserts an element into the ManagedList&lt;T&gt; at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert. The value can be null for reference types.</param>
        public new void Insert( int index, T item )
        {
            base.Insert( index, item );

            this.RaiseCollectionChanged();
        }

        /// <summary>
        /// Inserts the elements of a collection into the ManagedList&lt;T&gt; at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which the new elements should be inserted.</param>
        /// <param name="collection">
        /// The collection whose elements should be inserted into the ManagedList&lt;T&gt;.
        /// The collection itself cannot be null, but it can contain elements that are null, if type T is a reference type.
        /// </param>
        public new void InsertRange( int index, IEnumerable<T> collection )
        {
            base.InsertRange( index, collection );

            this.RaiseCollectionChanged();
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the ManagedList&lt;T&gt;.
        /// </summary>
        /// <param name="item">The object to remove from the ManagedList&lt;T&gt;. The value can be null for reference types.</param>
        /// <returns>true if item is successfully removed; otherwise, false. This method also returns false if item was not found in the ManagedList&lt;T&gt;.</returns>
        public new bool Remove( T item )
        {
            bool result = base.Remove( item );

            this.RaiseCollectionChanged();

            return result;
        }

        /// <summary>
        /// Removes all the elements that match the conditions defined by the specified predicate.
        /// </summary>
        /// <param name="match">The Predicate&lt;T&gt; delegate that defines the conditions of the elements to remove.</param>
        /// <returns>The number of elements removed from the ManagedList&lt;T&gt;.</returns>
        public new int RemoveAll( Predicate<T> match )
        {
            int result = base.RemoveAll( match );

            this.RaiseCollectionChanged();

            return result;
        }

        /// <summary>
        /// Removes the element at the specified index of the ManagedList&lt;T&gt;
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        public new void RemoveAt( int index )
        {
            base.RemoveAt( index );

            this.RaiseCollectionChanged();
        }

        /// <summary>
        /// Removes a range of elements from the ManagedList&lt;T&gt;
        /// </summary>
        /// <param name="index">The zero-based starting index of the range of elements to remove.</param>
        /// <param name="count">The number of elements to remove.</param>
        public new void RemoveRange( int index, int count )
        {
            base.RemoveRange( index, count );

            this.RaiseCollectionChanged();
        }

        #endregion

        /* ----------------------------------------------------------------------------------------------------------------------------------------- */

        #region Protected Methods

        #endregion

        /* ----------------------------------------------------------------------------------------------------------------------------------------- */

        #region Static Methods

        #endregion

        /* ----------------------------------------------------------------------------------------------------------------------------------------- */

        #region Private Methods

        /// <summary>
        /// Raised the collection changed event.
        /// </summary>
        private void RaiseCollectionChanged()
        {
            if ( this.CollectionChanged != null )
            {
                this.CollectionChanged.Invoke( this );
            }
        }

        #endregion

        /* ----------------------------------------------------------------------------------------------------------------------------------------- */

        #region Properties

        #endregion

        /* ----------------------------------------------------------------------------------------------------------------------------------------- */

        #region Derived Properties

        #endregion

        /* ----------------------------------------------------------------------------------------------------------------------------------------- */

        #region Events

        /// <summary>
        /// Raised when the number of items in the collection changes. Is not raised when an item in the collection is changed.
        /// </summary>
        public event Action<ManagedList<T>> CollectionChanged;

        #endregion

        /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    }
}
