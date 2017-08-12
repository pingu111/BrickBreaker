using UnityEngine;
using System.Collections;

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
    public static int PlayerNbPoints = 0;
    public static int PlayerNbLifes = 3;

}

public class PlayerManager : MonoBehaviour
{

}
