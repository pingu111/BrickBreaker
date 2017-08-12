
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
         /*   if (m_PlayerNbLifes == 0)
            {
                GameEnded = true;
                EventManager.raise(EventType.PLAYER_LOST);
            }*/
        }
    }

    public static int PlayerInitLifes = 3;

    public static float BallsSpeed = 3.0f;
    private static float BallsInitSpeed = 3.0f;

    public static bool GameEnded = false;


    /// <summary>
    /// Reset the information of the sattistics of the player : points, life...
    /// </summary>
    public static void ResetPlayer()
    {
        GameEnded = false;
        PlayerNbPoints = 0;
        PlayerNbLifes = PlayerInitLifes;
        BallsSpeed = BallsInitSpeed;
    }

}
