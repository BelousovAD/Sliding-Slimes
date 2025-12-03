namespace Model
{
    using System;
    using UnityEngine;

    public class Travelator : AbstractModel
    {
        private TravelatorType _type;
        
        public event Action TypeChanged;

        public TravelatorType Type
        {
            get
            {
                return _type;
            }

            private set
            {
                if (value != _type)
                {
                    _type = value;
                    TypeChanged?.Invoke();
                }
            }
        }

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
            Type = type;
    }
}