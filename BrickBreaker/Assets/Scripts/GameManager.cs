using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameCreator m_GameCreator = null;

    [SerializeField]
    private PlayerRacket m_PlayerRacket = null;

    /// <summary>
    /// Init all the objects :it needs to be the only start of the scene
    /// </summary>
    void Start ()
    {
        m_GameCreator.CreateGame();
        m_PlayerRacket.InitRacket();
    }
}
