using UnityEngine;
using System.Collections;

/// <summary>
/// Manager of the main menu, and its inputs
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// To call when the player click on Play button
    /// </summary>
    public void OnClickOnLaunchGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    /// <summary>
    /// To call when the player changes its settings about
    /// the use of gyro
    /// </summary>
    /// <param name="newValue"></param>
    public void OnValueGyroscopeChanged(bool newValue)
    {
        PlayerPreferences.m_PlayerWantsGyroscope = newValue;
    }

    /// <summary>
    /// To call when the player wants to quit the applciation
    /// </summary>
    public void OnClickQuitApplication()
    {
        Application.Quit();
    }
}
