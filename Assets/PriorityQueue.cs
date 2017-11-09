using System.Collections.Generic;
using System.Linq;

namespace DEnt
{
    public class PriorityQueue<P, V>
    {

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Class Members

        /// <summary>
        /// Holds the list of queues, sorted by the priority value.
        /// </summary>
        private SortedDictionary<P, Queue<V>> list = new SortedDictionary<P, Queue<V>>();

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Constructors/Initialisation

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Public Methods

        /// <summary>
        /// Adds a new item to the queue, using the provided priority value as the key.
        /// </summary>
        public void Enqueue( P priority, V value )
        {
            Queue<V> q;
            if ( !list.TryGetValue( priority, out q ) )
            {
                q = new Queue<V>();
                list.Add( priority, q );
            }
            q.Enqueue( value );
        }

        /// <summary>
        /// Gets the most recent item in the queue.
        /// </summary>
        public V Dequeue()
        {
            // will throw if there isn’t any first element!
            var pair = list.First();
            var v = pair.Value.Dequeue();
            if ( pair.Value.Count == 0 ) // nothing left of the top priority.
                list.Remove( pair.Key );
            return v;
        }

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Private Methods

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Properties

        /// <summary>
        /// Gets whether or not the current list of queues is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return !list.Any(); }
        }

        /// <summary>
        /// Gets the current length of the priority queue.
        /// </summary>
        public int Length
        {
            get { return list.Count; }
        }

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

    }
}
