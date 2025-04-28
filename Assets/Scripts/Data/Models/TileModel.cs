using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Models
{
    [Serializable]
    public class TileModel
    {
        public bool IsDisabled;
        public Vector2Int Coordinates;
        [SerializeReference] public List<ElementModel> ElementModels = new List<ElementModel>();
    }
}