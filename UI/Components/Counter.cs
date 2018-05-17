using System;

namespace LiveSplit.UI.Components
{
    public delegate void CounterChangedHandler(Counter sender, CounterChangedEventArgs args);

    public class CounterChangedEventArgs : EventArgs
    {
        public int Count { get; }
        public CounterChangedEventArgs(int count)
        {
            Count = count;
        }
    }

    public class Counter : ICounter
    {
        public event CounterChangedHandler CounterChanged;

        private int increment = 1;
        private int initialValue = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Counter"/> class.
        /// </summary>
        /// <param name="initialValue">The initial value for the counter.</param>
        /// <param name="increment">The amount to be used for incrementing/decrementing.</param>
        public Counter(int initialValue = 0, int increment = 1)
        {
            this.initialValue = initialValue;
            this.increment = increment;
            Count = initialValue;
        }

        private int count = 0;
        public int Count
        {
            get
            {
                return count;
            }
            private set
            {
                count = value;
                CounterChanged?.Invoke(this, new CounterChangedEventArgs(count));
            }
        }

        /// <summary>
        /// Increments this instance.
        /// </summary>
        public bool Increment()
        {
            if (Count == int.MaxValue)
                return false;

            try
            {
                Count = checked(Count + increment);
            }
            catch (System.OverflowException)
            {
                Count = int.MaxValue;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Decrements this instance.
        /// </summary>
        public bool Decrement()
        {
            if (Count == int.MinValue)
                return false;

            try
            {
                Count = checked(Count - increment);
            }
            catch (System.OverflowException)
            {
                Count = int.MinValue;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            Count = initialValue;
        }

        /// <summary>
        /// Sets the count.
        /// </summary>
        public void SetCount(int value)
        {
            Count = value;
        }

        /// <summary>
        /// Sets the value that the counter is incremented by.
        /// </summary>
        /// <param name="incrementValue"></param>
        public void SetIncrement(int incrementValue)
        {
            increment = incrementValue;
        }
    }

    public interface ICounter
    {
        event CounterChangedHandler CounterChanged;

        int Count { get; }

        bool Increment();
        bool Decrement();
        void Reset();
        void SetCount(int value);
        void SetIncrement(int incrementValue);
    }
}