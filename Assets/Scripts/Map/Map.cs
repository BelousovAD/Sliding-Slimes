namespace Map
{
    using System;
    using System.Collections.Generic;
    using Savvy.Extensions;
    using UnityEngine;

    internal class Map
    {
        private const char Separator0 = ',';
        private const char Separator1 = '\n';
        private const int CellCapacity = 2;
        private const int GridOffset = 2;
        
        private List<AbstractModel>[,] _cells;
        
        public Vector2Int Size { get; private set; }

        public IReadOnlyList<AbstractModel> this[int x, int y] => _cells[x, y];

        public void Load(TextAsset map)
        {
            string[] data = map.text.Split(Separator0, Separator1);
            int indexOfSetting = 0;
            Size = new Vector2Int(data[0].ToIntOrDefault(), data[1].ToIntOrDefault());
            int settingsOffset = Size.x * Size.y + GridOffset;
            _cells = new List<AbstractModel>[Size.x, Size.y];

            for (int y = 0; y < Size.y; y++)
            {
                for (int x = 0; x < Size.x; x++)
                {
                    ObjectType type = (ObjectType)data[GridOffset + y * Size.x + x][0];
                    _cells[x, y] = new List<AbstractModel>(CellCapacity)
                    {
                        type == ObjectType.Wall ? new Wall() : new EmptyCell()
                    };

                    switch (type)
                    {
                        case ObjectType.EmptyCell:
                            break;
                        case ObjectType.Portal:
                            _cells[x, y]
                                .Add(new Portal(data[settingsOffset + indexOfSetting++]
                                    .ToEnumOrDefault<SlimeType>()));
                            break;
                        case ObjectType.LuckyBlock:
                            _cells[x, y]
                                .Add(new LuckyBlock(data[settingsOffset + indexOfSetting++]
                                    .ToIntOrDefault()));
                            break;
                        case ObjectType.Slime:
                            _cells[x, y]
                                .Add(new Slime(data[settingsOffset + indexOfSetting++]
                                        .ToEnumOrDefault<SlimeType>(),
                                    data[settingsOffset + indexOfSetting++]
                                        .ToIntOrDefault()));
                            break;
                        case ObjectType.Travelator:
                            _cells[x, y]
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