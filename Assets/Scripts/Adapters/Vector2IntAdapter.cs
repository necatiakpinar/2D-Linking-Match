using Interfaces;

namespace Adapters
{
    public class Vector2IntAdapter : IVector2Int
    {
        private Data.Vector2Int _vector;

        public Vector2IntAdapter(Data.Vector2Int vector)
        {
            _vector = vector;
        }

        public int x
        {
            get => _vector.x;
            set => _vector.x = value;
        }

        public int y
        {
            get => _vector.y;
            set => _vector.y = value;
        }

    }
}