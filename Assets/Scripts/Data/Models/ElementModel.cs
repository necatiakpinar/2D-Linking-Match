using System;
using Miscs;

namespace Data.Models
{
    [Serializable]
    public class ElementModel
    {
        private GameElementType _elementType;
        
        public GameElementType ElementType
        {
            get => _elementType;
            set => _elementType = value;
        }
        
        public ElementModel(GameElementType elementType)
        {
            ElementType = elementType;
        }

        public virtual ElementModel Clone()
        {
            return (ElementModel)this.MemberwiseClone();
        }
    }
}