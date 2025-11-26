namespace Map
{
    using System;
    using System.Collections.Generic;
    using Reflex.Attributes;
    using UnityEngine;

    internal class MapBuilder : MonoBehaviour
    {
        private const int SizeDivider = 2;
        
        [SerializeField] private EmptyCellProvider _emptyCellProvider;
        [SerializeField] private LuckyBlockProvider _luckyBlockProvider;
        [SerializeField] private PortalProvider _portalProvider;
        [SerializeField] private SlimeProvider _slimeProvider;
        [SerializeField] private TravelatorProvider _travelatorProvider;
        [SerializeField] private WallProvider _wallProvider;

        private Dictionary<Type, MonoBehaviour> _providers;
        private Map _map;

        [Inject]
        private void Initialize(Map map) =>
            _map = map;

        private void Awake()
        {
            _providers = new Dictionary<Type, MonoBehaviour>
            {
                [typeof(EmptyCell)] = _emptyCellProvider,
                [typeof(LuckyBlock)] = _luckyBlockProvider,
                [typeof(Portal)] = _portalProvider,
                [typeof(Slime)] = _slimeProvider,
                [typeof(Travelator)] = _travelatorProvider,
                [typeof(Wall)] = _wallProvider,
            };
        }

        private void Start()
        {
            for (int y = 0; y < _map.Size.y; y++)
            {
                for (int x = 0; x < _map.Size.x; x++)
                {
                    IReadOnlyCollection<AbstractModel> cells = _map[x, y];

                    foreach (AbstractModel model in cells)
                    {
                        switch (model)
                        {
                            case EmptyCell emptyCell:
                                Spawn(emptyCell, new Vector2(x, y));
                                break;
                            case LuckyBlock luckyBlock:
                                Spawn(luckyBlock, new Vector2(x, y));
                                break;
                            case Portal portal:
                                Spawn(portal, new Vector2(x, y));
                                break;
                            case Slime slime:
                                Spawn(slime, new Vector2(x, y));
                                break;
                            case Travelator travelator:
                                Spawn(travelator, new Vector2(x, y));
                                break;
                            case Wall wall:
                                Spawn(wall, new Vector2(x, y));
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }

            transform.localPosition =
                new Vector3((float)-_map.Size.x / SizeDivider, 0f, (float)-_map.Size.y / SizeDivider);
        }

        private void Spawn<T>(T model, Vector2 position) where T : AbstractModel
        {
            AbstractProvider<T> provider = Instantiate(
                _providers[typeof(T)] as AbstractProvider<T>,
                new Vector3(position.x, 0f, position.y),
                Quaternion.identity, transform);
            provider.Initialize(model);
        }
    }
}