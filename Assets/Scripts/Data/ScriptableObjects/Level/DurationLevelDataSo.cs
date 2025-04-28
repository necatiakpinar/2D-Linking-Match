using UnityEngine;

namespace Data.ScriptableObjects.Level
{
    public class DurationLevelDataSo : LevelDataSo
    {
        [SerializeField] private float _levelDuration;
        
        public float LevelDuration => _levelDuration;
    }
}