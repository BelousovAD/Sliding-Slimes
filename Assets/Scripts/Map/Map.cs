namespace Map
{
    using System;
    using System.Collections.Generic;
    using Savvy.Extensions;
    using UnityEngine;

    internal class Map
    {
        private const int GridOffset = 2;
        
        private List<AbstractModel>[,] _objects;
        
        public Vector2Int Size { get; private set; }

        public IReadOnlyList<AbstractModel> this[int x, int y] => _objects[x, y];

        public void Load(TextAsset map)
        {
            string[] data = map.text.Split(',', '\n');
            int indexOfSetting = 0;
            Size = new Vector2Int(data[0].ToIntOrDefault(), data[1].ToIntOrDefault());
            int settingsOffset = Size.x * Size.y + GridOffset;
            _objects = new List<AbstractModel>[Size.x, Size.y];

            for (int y = 0; y < Size.y; y++)
            {
                for (int x = 0; x < Size.x; x++)
                {
                    ObjectType type = (ObjectType)data[GridOffset + y * Size.x + x][0];
                    _objects[x, y] = new List<AbstractModel>
                    {
                        type == ObjectType.Wall ? new Wall() : new EmptyCell()
                    };

                    switch (type)
                    {
                        case ObjectType.EmptyCell:
                            break;
                        case ObjectType.Portal:
                            _objects[x, y]
                                .Add(new Portal(data[settingsOffset + indexOfSetting++]
                                    .ToEnumOrDefault<SlimeType>()));
                            break;
                        case ObjectType.LuckyBlock:
                            _objects[x, y]
                                .Add(new LuckyBlock(data[settingsOffset + indexOfSetting++]
                                    .ToIntOrDefault()));
                            break;
                        case ObjectType.Slime:
                            _objects[x, y]
                                .Add(new Slime(data[settingsOffset + indexOfSetting++]
                                        .ToEnumOrDefault<SlimeType>(),
                                    data[settingsOffset + indexOfSetting++]
                                        .ToIntOrDefault()));
                            break;
                        case ObjectType.Travelator:
                            _objects[x, y]
                                .Add(new Travelator(data[settingsOffset + indexOfSetting]
                                    .ToEnumOrDefault<TravelatorType>()));
                            break;
                        case ObjectType.Wall:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        private enum ObjectType
        {
            EmptyCell = 'O',
            Portal = 'P',
            LuckyBlock = 'L',
            Slime = 'S',
            Travelator = 'T',
            Wall = 'X'
        }
    }
}