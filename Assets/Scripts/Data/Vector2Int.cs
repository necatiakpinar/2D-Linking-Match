using System;
using Interfaces; // Bunu eklemen lazım

namespace Data
{
    public struct Vector2Int : IEquatable<Vector2Int>, IVector2Int
    {
        public int x { get; set; }
        public int y { get; set; }

        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        
        public bool Equals(Vector2Int other)
        {
            return x == other.x && y == other.y;
        }
        
        public override bool Equals(object obj)
        {
            return obj is Vector2Int other && Equals(other);
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }

        public static Vector2Int operator +(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.x + b.x, a.y + b.y);
        }

        public static Vector2Int operator -(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.x - b.x, a.y - b.y);
        }
    }
}