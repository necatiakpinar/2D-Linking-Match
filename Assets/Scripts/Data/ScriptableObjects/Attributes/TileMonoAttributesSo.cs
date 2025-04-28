using UnityEngine;

namespace Data.ScriptableObjects.Attributes
{
    [CreateAssetMenu(fileName = "SO_TileMonoAttribute", menuName = "Data/ScriptableObjects/TileMonoData", order = 1)]
    public class TileMonoAttributesSo : ScriptableObject
    {
        [SerializeField] private float _explodeDuration = 1.0f;

        public float ExplodeDuration => _explodeDuration;
    }
}