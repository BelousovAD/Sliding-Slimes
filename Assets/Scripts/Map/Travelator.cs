namespace Map
{
    using System;
    using UnityEngine;

    internal class Travelator : AbstractModel
    {
        public Travelator(TravelatorType type) =>
            Type = type;

        public TravelatorType Type { get; }

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
    }
}