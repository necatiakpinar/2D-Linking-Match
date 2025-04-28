namespace Addressables
{
    public static class AddressablesKeys
    {
        public enum AssetKeys
        {
            //Sprite Atlases
            SA_Set1TileElements,

            //Scriptable Objects
            SO_GridData,
            SO_TileMonoAttributes,
            SO_LevelContainer,
        }

        public static string GetKey(AssetKeys key)
        {
            return key.ToString();
        }
    }
}