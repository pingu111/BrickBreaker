using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameCreator m_GameCreator = null;

	// Use this for initialization
	void Start ()
    {
	    if(m_GameCreator == null)
        {
            Debug.LogError("No GameCreator attached");
            return;
        }

        m_GameCreator.CreateGame();
    }
}
