using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDDisplayer : MonoBehaviour
{
    /// <summary>
    /// Text displaying the current number of lifes of the player
    /// </summary>
    [SerializeField]
    private Text m_NumberLifesText = null;

    /// <summary>
    /// Text displaying the current number of points of the player
    /// </summary>
    [SerializeField]
    private Text m_NumberPointsText = null;

    /// <summary>
    /// Popup annoucing the end of the game
    /// </summary>
    [SerializeField]
    private PopupEnd m_PopupGameOver = null;

    /// <summary>
    /// Initialize the HUD
    /// </summary>
    public void Init()
    {
        if(!this.gameObject.activeSelf)
            this.gameObject.SetActive(true);

        SetScore(PlayerStatistics.PlayerNbPoints);
        SetLifes(PlayerStatistics.PlayerNbLifes);
        m_PopupGameOver.Init();

        EventManager.addActionToEvent<int>(EventType.PLAYER_NUMBER_SCORE_CHANGED, SetScore);
        EventManager.addActionToEvent<int>(EventType.PLAYER_NUMBER_LIFE_CHANGED, SetLifes);
    }

    void OnDestroy()
    {
        EventManager.removeActionFromEvent<int>(EventType.PLAYER_NUMBER_SCORE_CHANGED, SetScore);
        EventManager.removeActionFromEvent<int>(EventType.PLAYER_NUMBER_LIFE_CHANGED, SetLifes);
    }

    /// <summary>
    /// Set the text of the score of the player
    /// </summary>
    /// <param name="newScore"></param>
    public void SetScore(int newScore)
    {
        this.GetComponent<Animator>().SetTrigger("PlayerScoreChanged");
        m_NumberPointsText.text = newScore.ToString();
    }

    /// <summary>
    /// Set the text of the number of lifes of the player
    /// </summary>
    /// <param name="newLife"></param>
    public void SetLifes(int newLife)
    {
        this.GetComponent<Animator>().SetTrigger("PlayerLifeChanged");
        m_NumberLifesText.text = newLife.ToString();
    }
}
