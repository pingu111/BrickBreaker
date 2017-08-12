using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDDisplayer : MonoBehaviour {

    [SerializeField]
    private Text m_NumberLifesText = null;

    [SerializeField]
    private Text m_NumberPointsText = null;

    public void Init()
    {
        SetScore(PlayerStatistics.PlayerNbPoints);
        SetLifes(PlayerStatistics.PlayerNbLifes);

        EventManager.addActionToEvent<int>(EventType.PLAYER_NUMBER_SCORE_CHANGED, SetScore);
        EventManager.addActionToEvent<int>(EventType.PLAYER_NUMBER_LIFE_CHANGED, SetLifes);
    }

    void OnDestroy()
    {
        EventManager.removeActionFromEvent<int>(EventType.PLAYER_NUMBER_SCORE_CHANGED, SetScore);
        EventManager.removeActionFromEvent<int>(EventType.PLAYER_NUMBER_LIFE_CHANGED, SetLifes);
    }

    public void SetScore(int newScore)
    {
        m_NumberPointsText.text = newScore.ToString();
    }

    public void SetLifes(int newLife)
    {
        m_NumberLifesText.text = newLife.ToString();
    }
}
