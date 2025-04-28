using System.Collections.Generic;
using Adapters;
using Cysharp.Threading.Tasks;
using Extensions;
using Helpers;
using Interfaces;
using Miscs;
using UnityEngine;
using UnityObjects;

public class BaseTileMono : MonoBehaviour, ITile
{
    private IVector2Int _coordinates;
    private ITileElement _tileElement;
    private Dictionary<TileDirectionType, ITile> _neighbours = new();

    public IVector2Int Coordinates => _coordinates;
    public ITileElement TileElement => _tileElement;
    public ITransform Transform => new UnityTransform(transform);
    public Dictionary<TileDirectionType, ITile> Neighbours => _neighbours;

    public async UniTask Init(IVector2Int coordinates, ITileElement slotObject)
    {
        transform.name = $"Tile {coordinates.x} {coordinates.y}";
        _coordinates = coordinates;
        _tileElement = slotObject;
        _tileElement.Transform.LocalPosition = new Vector3Adapter(Vector3.zero.ToDataVector3());
    }
    
    public void SetNeighbours(Dictionary<TileDirectionType, ITile> neighbours)
    {
        _neighbours = neighbours;
        LoggerUtil.Log(_neighbours.Count);
    }

}