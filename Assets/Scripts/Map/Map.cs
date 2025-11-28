namespace Map
{
    using System;
    using System.Collections.Generic;
    using Savvy.Extensions;
    using UnityEngine;

    public class Map : IDisposable
    {
        public const int MinPortalCount = 0;
        private const char Separator0 = ',';
        private const char Separator1 = '\n';
        private const int CellCapacity = 2;
        private const int GridOffset = 2;
        
        private Stack<AbstractModel>[,] _cells;
        private int _portalCount;
        
        public event Action PortalCountChanged;
        
        public Vector2Int Size { get; private set; }
        
        public int PortalCount
        {
            get
            {
                return _portalCount;
            }

            private set
            {
                if (value == _portalCount)
                {
                    return;
                }
                
                _portalCount = value < MinPortalCount ? MinPortalCount : value;
                PortalCountChanged?.Invoke();
            }
        }

        public IReadOnlyCollection<AbstractModel> this[int x, int y] => _cells[x, y];

        public void Dispose()
        {
            foreach (Stack<AbstractModel> abstractModels in _cells)
            {
                foreach (AbstractModel abstractModel in abstractModels)
                {
                    if (abstractModel is Portal portal)
                    {
                        portal.SlimeCaught -= CountDown;
                    }
                }
            }
        }

        public void Load(TextAsset map)
        {
            string[] data = map.text.Split(Separator0, Separator1);
            int indexOfSetting = 0;
            Size = new Vector2Int(data[0].ToIntOrDefault(), data[1].ToIntOrDefault());
            int settingsOffset = Size.x * Size.y + GridOffset;
            _cells = new Stack<AbstractModel>[Size.x, Size.y];

            for (int y = 0; y < Size.y; y++)
            {
                for (int x = 0; x < Size.x; x++)
                {
                    ObjectType type = (ObjectType)data[GridOffset + (Size.y - y - 1) * Size.x + x][0];
                    _cells[x, y] = new Stack<AbstractModel>(CellCapacity);
                    _cells[x, y].Push(type == ObjectType.Wall ? new Wall() : new EmptyCell());

                    switch (type)
                    {
                        case ObjectType.EmptyCell:
                            break;
                        case ObjectType.Portal:
                            PortalCount++;
                            Portal portal = new((SlimeType)data[settingsOffset + indexOfSetting++][0]);
                            portal.SlimeCaught += CountDown;
                            _cells[x, y].Push(portal);
                            break;
                        case ObjectType.LuckyBlock:
                            _cells[x, y]
                                .Push(new LuckyBlock(data[settingsOffset + indexOfSetting++].ToIntOrDefault()));
                            break;
                        case ObjectType.Slime:
                            _cells[x, y]
                                .Push(new Slime((SlimeType)data[settingsOffset + indexOfSetting++][0],
                                    data[settingsOffset + indexOfSetting++].ToIntOrDefault()));
                            break;
                        case ObjectType.Travelator:
                            _cells[x, y]
                                .Push(new Travelator((TravelatorType)data[settingsOffset + indexOfSetting][0]));
                            break;
                        case ObjectType.Wall:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        private void CountDown(Portal portal)
        {
            portal.SlimeCaught -= CountDown;
            PortalCount--;
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