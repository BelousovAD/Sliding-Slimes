namespace Model
{
    using System;
    using UnityEngine;

    public class Travelator : AbstractModel
    {
        public TravelatorType Type { get; private set; }

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