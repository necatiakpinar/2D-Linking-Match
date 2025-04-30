using System.Collections.Generic;
using Adapters;
using Cysharp.Threading.Tasks;
using EventBus;
using EventBus.Events;
using Extensions;
using Interfaces;
using Miscs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityObjects;

namespace Abstracts
{
    public class BaseTileMono : MonoBehaviour, ITile, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
    {
        private IVector2Int _coordinates;
        private ITileElement _tileElement;
        private Dictionary<TileDirectionType, ITile> _neighbours = new();
        private Dictionary<TileDirectionType, ITile> _aboveNeighbours = new();
        private Dictionary<TileDirectionType, ITile> _belowNeighbours = new();

        public IVector2Int Coordinates => _coordinates;
        public ITileElement TileElement => _tileElement;
        public ITransform Transform => new UnityTransform(transform);

        public Dictionary<TileDirectionType, ITile> Neighbours
        {
            get => _neighbours;
            set => _neighbours = value;
        }

        public Dictionary<TileDirectionType, ITile> AboveNeighbours
        {
            get => _aboveNeighbours;
            set => _aboveNeighbours = value;
        }

        public Dictionary<TileDirectionType, ITile> BelowNeighbours
        {
            get => _belowNeighbours;
            set => _belowNeighbours = value;
        }

        public async UniTask Init(IVector2Int coordinates, ITileElement slotObject)
        {
            transform.name = $"Tile {coordinates.x} {coordinates.y}";
            _coordinates = coordinates;
            SetTileElement(slotObject);
        }

        public void SetTileElement(ITileElement tileElement)
        {
            _tileElement = tileElement;
            if (_tileElement == null)
                return;

            _tileElement.Transform.LocalPosition = new Vector3Adapter(Vector3.zero.ToDataVector3());
        }

        public bool HasTileInNeighbours(ITile tile)
        {
            if (tile == null)
                return false;

            foreach (var neighbour in _neighbours)
            {
                if (neighbour.Value == null)
                    continue;

                if (neighbour.Value == tile)
                    return true;
            }

            return false;
        }

        public async UniTask SelectTile()
        {
            if (_tileElement == null)
                return;

            await TileElement.Select();
        }

        public async UniTask DeselectTile()
        {
            if (_tileElement == null)
                return;

            await TileElement.Deselect();
        }

        public async UniTask TryToActivate()
        {
            if (_tileElement == null)
                return;

            await Activate();
        }

        public async UniTask Activate()
        {
            await _tileElement.TryToActivate();
        }
        public async UniTask<bool> TryToDropTileElement(ITile requestedTile)
        {
            var hasBelowNeighbours = _belowNeighbours.Count > 0;
            if (!hasBelowNeighbours)
                return false;

            var result = await DropTileElement(requestedTile);
            return result;
        }

        public async UniTask<bool> DropTileElement(ITile requestedTile)
        {
            await TileElement.SetTile(requestedTile);
            _tileElement = null;
            return true;
        }

        public async UniTask TryToRequestTileElement()
        {
            var hasAboveNeighbours = _aboveNeighbours.Count > 0;
            if (!hasAboveNeighbours)
                return;

            await RequestTileElement();
        }

        public async UniTask RequestTileElement()
        {
            foreach (var aboveNeighbour in _aboveNeighbours.Values)
            {
                if (aboveNeighbour != null && aboveNeighbour.TileElement != null)
                {
                    var result = await aboveNeighbour.TryToDropTileElement(this);
                    if (result)
                        return;
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            EventBus<TilePressedEvent>.Raise(new TilePressedEvent(this));
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            EventBus<TileReleasedEvent>.Raise(new TileReleasedEvent(this));
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            EventBus<TryToAddTileEvent>.Raise(new TryToAddTileEvent(this));
        }
    }
}