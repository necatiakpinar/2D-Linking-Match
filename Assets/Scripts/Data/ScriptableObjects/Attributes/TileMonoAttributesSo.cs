using UnityEngine;

namespace Data.ScriptableObjects.Attributes
{
    [CreateAssetMenu(fileName = "SO_TileMonoAttributes", menuName = "Data/ScriptableObjects/Attributes/TileMonoAttributes", order = 1)]
    public class TileMonoAttributesSo : ScriptableObject
    {
        [SerializeField] private float _explodeDuration = 1.0f;

        public float ExplodeDuration => _explodeDuration;
    }
}