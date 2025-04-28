namespace Addressables
{
    public static class AddressablesKeys
    {
        public enum AssetKeys
        {
            //Sprite Atlases
            SA_TileElements,
                
            //Scriptable Objects
                
        }
        public static string GetKey(AssetKeys key)
        {
            return key.ToString();
        }
    }
}