using Abstracts;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SO_GridData", menuName = "Data/ScriptableObjects/GridData", order = 1)]
    public class GridDataSo : ScriptableObject
    {
        [Header("Tile Prefab")] [SerializeField]
        private BaseTileMono _tilePrefab;

        [SerializeField] private float _offset = 1.0f;

        public BaseTileMono TilePrefab => _tilePrefab;

        public float Offset => _offset;
    }
}