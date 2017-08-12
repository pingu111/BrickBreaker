using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameCreator m_GameCreator = null;

    [SerializeField]
    private HUDDisplayer m_InitHUD = null;

    /// <summary>
    /// Init all the objects
    /// </summary>
    void Start ()
    {
        Input.multiTouchEnabled = false;
        PlayerStatistics.ResetPlayer();
        m_GameCreator.CreateGame();
        m_InitHUD.Init();
    }

    /// <summary>
    /// To call when the player wants to start the game
    /// </summary>
    public void OnClickStartGame()
    {
        m_GameCreator.CreateBallAtRacket();
    }
}
