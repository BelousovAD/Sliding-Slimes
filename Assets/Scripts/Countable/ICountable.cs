using System;

namespace Countable
{
    public interface ICountable
    {
        protected const int MinCount = 0;

        public event Action CountChanged;

        public int Count { get; }
    }
}