using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    /// <summary>
    /// The moving racket of the player
    /// </summary>
    [SerializeField]
    private PlayerRacket m_PlayerRacket = null;

    /// <summary>
    /// Speed of the player's racket
    /// </summary>
    private float m_Speed = 0.15f;

	// Update is called once per frame
	void Update ()
    {
	    if(PlayerPreferences.m_PlayerWantsGyroscope)
        {
            if(Input.acceleration.x != 0)
            {
                m_PlayerRacket.RacketGoToX(m_Speed * Input.acceleration.x);
            }
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            m_PlayerRacket.RacketGoToX(-m_Speed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            m_PlayerRacket.RacketGoToX(m_Speed);
        }
    }

    /// <summary>
    /// To call each frame the left arrow is clicked 
    /// </summary>
    public void OnClickLeftArrow()
    {
        m_PlayerRacket.RacketGoToX(- m_Speed);
    }

    /// <summary>
    /// To call each frame the right arrow is clicked 
    /// </summary>
    public void OnClickRightArrow()
    {
        m_PlayerRacket.RacketGoToX(m_Speed);
    }
}
