namespace Map
{
    using System;

    internal interface ICountable
    {
        protected const int MinCount = 0;
        
        public event Action CountChanged;
        
        public int Count { get; }
    }
}