using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopupEnd : MonoBehaviour
{
    [SerializeField]
    private GameObject m_TextWon = null;

    [SerializeField]
    private GameObject m_TextLost = null;

    [SerializeField]
    private Text m_HighestScoreText = null;

    /// <summary>
    /// Init the popup
    /// </summary>
    public void Init()
    {
        if (this.gameObject.activeSelf)
            this.gameObject.SetActive(false);
        EventManager.addActionToEvent(EventType.PLAYER_LOST, OnLost);
        EventManager.addActionToEvent(EventType.PLAYER_WON, OnWon);
    }

    /// <summary>
    /// To call when the player won
    /// </summary>
    public void OnWon()
    {
        GetHighScore();
        if (!this.gameObject.activeSelf)
            this.gameObject.SetActive(true);
        if (m_TextLost.activeSelf)
            m_TextLost.SetActive(false);
        if (!m_TextWon.activeSelf)
            m_TextWon.SetActive(true);
    }

    /// <summary>
    /// To call when the player lost
    /// </summary>
    public void OnLost()
    {
        GetHighScore();
        if (!this.gameObject.activeSelf)
            this.gameObject.SetActive(true);
        if (m_TextWon.activeSelf)
            m_TextWon.SetActive(false);
        if (!m_TextLost.activeSelf)
            m_TextLost.SetActive(true);
    }

    private void GetHighScore()
    {
        if(PlayerPrefs.HasKey("Highscore"))
        {
            m_HighestScoreText.text = "Highscore = " + PlayerPrefs.GetInt("Highscore");
        }
        else
        {
            m_HighestScoreText.text = "Pas encore de highscore... Courage !";
        }
    }

    /// <summary>
    /// To call when the player wants to restart the game
    /// </summary>
    public void OnClickRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    /// <summary>
    /// To call when the player wants to quit the game
    /// </summary>
    public void OnClickQuit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
    }

    void OnDestroy()
    {
        EventManager.removeActionFromEvent(EventType.PLAYER_LOST, OnLost);
        EventManager.removeActionFromEvent(EventType.PLAYER_WON, OnWon);
    }
	
}
