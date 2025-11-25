namespace Map
{
    using System;
    using System.Collections.Generic;
    using Reflex.Attributes;
    using UnityEngine;

    internal class MapBuilder : MonoBehaviour
    {
        private const int SizeDivider = 2;
        
        [SerializeField] private EmptyCellView _emptyCellView;
        [SerializeField] private LuckyBlockView _luckyBlockView;
        [SerializeField] private PortalView _portalView;
        [SerializeField] private SlimeView _slimeView;
        [SerializeField] private TravelatorView _travelatorView;
        [SerializeField] private WallView _wallView;

        private Dictionary<Type, AbstractView<AbstractModel>> _views;
        private Map _map;

        [Inject]
        private void Initialize(Map map) =>
            _map = map;

        private void Awake()
        {
            _views = new Dictionary<Type, AbstractView<AbstractModel>>
            {
                [typeof(EmptyCell)] = _emptyCellView,
                [typeof(LuckyBlock)] = _luckyBlockView,
                [typeof(Portal)] = _portalView,
                [typeof(Slime)] = _slimeView,
                [typeof(Travelator)] = _travelatorView,
                [typeof(Wall)] = _wallView,
            };
        }

        private void Start()
        {
            for (int y = 0; y < _map.Size.y; y++)
            {
                for (int x = 0; x < _map.Size.x; x++)
                {
                    IReadOnlyList<AbstractModel> models = _map[x, y];

                    foreach (AbstractModel model in models)
                    {
                        switch (model)
                        {
                            case EmptyCell emptyCell:
                                Spawn(emptyCell, new Vector2(x, -y));
                                break;
                            case LuckyBlock luckyBlock:
                                Spawn(luckyBlock, new Vector2(x, -y));
                                break;
                            case Portal portal:
                                Spawn(portal, new Vector2(x, -y));
                                break;
                            case Slime slime:
                                Spawn(slime, new Vector2(x, -y));
                                break;
                            case Travelator travelator:
                                Spawn(travelator, new Vector2(x, -y));
                                break;
                            case Wall wall:
                                Spawn(wall, new Vector2(x, -y));
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }

            transform.localPosition =
                new Vector3((float)-_map.Size.x / SizeDivider, 0f, (float)_map.Size.y / SizeDivider);
        }

        private void Spawn<T>(T model, Vector2 position) where T : AbstractModel
        {
            AbstractView<AbstractModel> view =
                Instantiate(_views[typeof(T)], new Vector3(position.x, 0f, position.y), Quaternion.identity, transform);
            view.Initialize(model);
        }
    }
}