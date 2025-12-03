namespace Model
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class Travelator : AbstractModel
    {
        private readonly List<TravelatorType> _typeOrder = new()
        {
            TravelatorType.North,
            TravelatorType.East,
            TravelatorType.South,
            TravelatorType.West,
        };
        private int _index;

        public event Action TypeChanged;
        
        private int Index
        {
            get
            {
                return _index;
            }

            set
            {
                if (value != _index)
                {
                    _index = value;
                    TypeChanged?.Invoke();
                }
            }
        }

        public TravelatorType Type => _typeOrder[Index];

        public Vector2Int Direction
        {
            get
            {
                return Type switch
                {
                    TravelatorType.North => Vector2Int.up,
                    TravelatorType.East => Vector2Int.right,
                    TravelatorType.South => Vector2Int.down,
                    TravelatorType.West => Vector2Int.left,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        public void Initialize(TravelatorType type) =>
            Index = _typeOrder.FindIndex(element => element == type);

        public void NextType() =>
            Index = (Index + 1) % _typeOrder.Count;
    }
}