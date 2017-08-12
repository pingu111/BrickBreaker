using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameCreator m_GameCreator = null;

    [SerializeField]
    private PlayerRacket m_PlayerRacket = null;

    [SerializeField]
    private Ball m_Ball;

    [SerializeField]
    private HUDDisplayer m_InitHUD;

    /// <summary>
    /// Init all the objects :it needs to be the only start of the scene
    /// </summary>
    void Start ()
    {
        PlayerStatistics.ResetPlayer();
        m_GameCreator.CreateGame();
        m_PlayerRacket.InitRacket();
        m_InitHUD.Init();
    }

    public void OnClickStartGame()
    {
        m_Ball.transform.position = new Vector3(
            m_PlayerRacket.transform.position.x,
            m_PlayerRacket.transform.position.y + m_Ball.GetComponent<SphereCollider>().bounds.size.y * 0.51f + m_PlayerRacket.GetComponent<CapsuleCollider>().bounds.size.y * 0.5f,
            m_PlayerRacket.transform.position.z);

        m_Ball.Launch();
    }
}
