using System;
using System.Collections.Generic;
using Model;
using Savvy.Extensions;
using SlimeTypeable;
using UnityEngine;

namespace Map
{
    internal class MapBuilder : MonoBehaviour
    {
        private const char Separator0 = ',';
        private const char Separator1 = '\n';
        private const int GridOffset = 2;
        private const int SizeDivider = 2;

        [SerializeField] private EmptyCell _emptyCell;
        [SerializeField] private LuckyBlock _luckyBlock;
        [SerializeField] private Portal _portal;
        [SerializeField] private Slime _slime;
        [SerializeField] private Travelator _travelator;
        [SerializeField] private Wall _wall;

        private Dictionary<Type, MonoBehaviour> _models;

        private enum ObjectType
        {
            EmptyCell = 'O',
            Portal = 'P',
            LuckyBlock = 'L',
            Slime = 'S',
            Travelator = 'T',
            Wall = 'X',
        }

        public void Build(Map map, TextAsset textMap)
        {
            _models = new Dictionary<Type, MonoBehaviour>
            {
                [typeof(EmptyCell)] = _emptyCell,
                [typeof(LuckyBlock)] = _luckyBlock,
                [typeof(Portal)] = _portal,
                [typeof(Slime)] = _slime,
                [typeof(Travelator)] = _travelator,
                [typeof(Wall)] = _wall,
            };

            string[] data = textMap.text.Split(Separator0, Separator1);
            int indexOfSetting = 0;
            Vector2Int size = new (data[0].ToIntOrDefault(), data[1].ToIntOrDefault());
            int settingsOffset = size.x * size.y + GridOffset;
            List<LuckyBlock> luckyBlocks = new ();
            List<Portal> portals = new ();
            List<Slime> slimes = new ();
            List<Travelator> travelators = new ();

            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    ObjectType type = (ObjectType)data[GridOffset + (size.y - y - 1) * size.x + x][0];

                    if (type != ObjectType.Wall)
                    {
                        Spawn<EmptyCell>(new Vector2(x, y));
                    }

                    switch (type)
                    {
                        case ObjectType.EmptyCell:
                            break;
                        case ObjectType.Portal:
                            Portal portal = Spawn<Portal>(new Vector2(x, y));
                            portal.Initialize((SlimeType)data[settingsOffset + indexOfSetting++][0]);
                            portals.Add(portal);
                            break;
                        case ObjectType.LuckyBlock:
                            LuckyBlock luckyBlock = Spawn<LuckyBlock>(new Vector2(x, y));
                            luckyBlock.Initialize(data[settingsOffset + indexOfSetting++].ToIntOrDefault());
                            luckyBlocks.Add(luckyBlock);
                            break;
                        case ObjectType.Slime:
                            Slime slime = Spawn<Slime>(new Vector2(x, y));
                            slime.Initialize(
                                (SlimeType)data[settingsOffset + indexOfSetting++][0],
                                data[settingsOffset + indexOfSetting++].ToIntOrDefault());
                            slimes.Add(slime);
                            break;
                        case ObjectType.Travelator:
                            Travelator travelator = Spawn<Travelator>(new Vector2(x, y));
                            travelator.Initialize((TravelatorType)data[settingsOffset + indexOfSetting++][0]);
                            travelators.Add(travelator);
                            break;
                        case ObjectType.Wall:
                            Spawn<Wall>(new Vector2(x, y));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            transform.localPosition = new Vector3((float)-size.x / SizeDivider, 0f, (float)-size.y / SizeDivider);
            map.Initialize(size, luckyBlocks, portals, slimes, travelators);
        }

        private T Spawn<T>(Vector2 position)
            where T : AbstractModel
        {
            return Instantiate(
                _models[typeof(T)] as T,
                new Vector3(position.x, 0f, position.y),
                Quaternion.identity,
                transform);
        }
    }
}