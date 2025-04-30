namespace Miscs
{
    public enum GameElementType
    {
        None,

        // Playables
        Type1 = PlayableEntityType.Type1,
        Type2 = PlayableEntityType.Type2,
        Type3 = PlayableEntityType.Type3,
        Type4 = PlayableEntityType.Type4,
    }

    public enum PlayableEntityType
    {
        Type1 = 51231,
        Type2 = 89925,
        Type3 = 146593,
        Type4 = 246810,
    }

    public enum TileDirectionType
    {
        Up,
        RightUp,
        Right,
        RightDown,
        Down,
        LeftDown,
        Left,
        LeftUp
    }

    public enum CurrencyType
    {
        Coin = 0,
        Gem = 10,
        Life = 20,
    }

    public enum LevelType
    {
        Normal,
        Hard,
        VeryHard,
    }
    
    public enum PlayableEntitySetType
    {
        Set1,
        Set2,
        Set3
    }

    public enum WindowType
    {
        GameplayWindow = 10
    }
}