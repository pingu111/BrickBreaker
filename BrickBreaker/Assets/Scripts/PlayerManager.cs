
/// <summary>
/// Preferences of the player, its settings
/// </summary>
public static class PlayerPreferences
{
    public static bool m_PlayerWantsGyroscope = false;
}

/// <summary>
/// Statistics of the player : number of points, ...
/// </summary>
public static class PlayerStatistics
{
    private static int m_PlayerNbPoints = 0;
    public static int PlayerNbPoints
    {
        get
        {
            return m_PlayerNbPoints;
        }
        set
        {
            EventManager.raise<int>(EventType.PLAYER_NUMBER_SCORE_CHANGED, value);
            m_PlayerNbPoints = value;
        }
    }

    private static int m_PlayerNbLifes = 3;
    public static int PlayerNbLifes
    {
        get
        {
            return m_PlayerNbLifes;
        }
        set
        {
            EventManager.raise<int>(EventType.PLAYER_NUMBER_LIFE_CHANGED, value);
            m_PlayerNbLifes = value;
        }
    }

    public static int PlayerInitLifes = 3;

    public static void ResetPlayer()
    {
        PlayerNbPoints = 0;
        PlayerNbLifes = PlayerInitLifes;
    }

}
