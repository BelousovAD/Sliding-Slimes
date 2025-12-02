namespace Map
{
    using System;
    using System.Collections.Generic;
    using Model;
    using UnityEngine;

    public class Map
    {
        private Stack<AbstractModel>[,] _cells;

        public event Action Initialized;
        
        public bool IsInitialized { get; private set; }
        
        public Vector2Int Size { get; private set; }

        public IReadOnlyCollection<AbstractModel> this[int x, int y] => _cells[x, y];

        public void Initialize(Stack<AbstractModel>[,] cells)
        {
            if (IsInitialized == false)
            {
                _cells = cells;
                Size = new Vector2Int(_cells.GetLength(0), _cells.GetLength(1));
                IsInitialized = true;
                Initialized?.Invoke();
            }
        }
    }
}