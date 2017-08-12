using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameCreator m_GameCreator = null;


    [SerializeField]
    private HUDDisplayer m_InitHUD;

    /// <summary>
    /// Init all the objects :it needs to be the only start of the scene
    /// </summary>
    void Start ()
    {
        PlayerStatistics.ResetPlayer();
        m_GameCreator.CreateGame();
        m_InitHUD.Init();
    }

    public void OnClickStartGame()
    {
        m_GameCreator.CreateBallAtRacket();
    }
}
