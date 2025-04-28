using System;
using Miscs;

namespace Data.Models
{
    [Serializable]
    public class ElementModel
    {
        public GameElementType ElementType;

        public virtual ElementModel Clone()
        {
            return (ElementModel)this.MemberwiseClone();
        }
    }
}