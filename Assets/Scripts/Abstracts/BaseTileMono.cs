using Adapters;
using Cysharp.Threading.Tasks;
using Extensions;
using Interfaces;
using UnityEngine;
using UnityObjects;

public class BaseTileMono : MonoBehaviour, ITile
{
    private IVector2Int _coordinates;
    private ITileElement _tileElement;

    public IVector2Int Coordinates => _coordinates;
    public ITileElement TileElement => _tileElement;
    public ITransform Transform => new UnityTransform(transform);

    public async UniTask Init(IVector2Int coordinates, ITileElement slotObject)
    {
        _coordinates = coordinates;
        _tileElement = slotObject;
        _tileElement.Transform.LocalPosition = new Vector3Adapter(Vector3.zero.ToDataVector3());
    }

}