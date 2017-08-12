using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
	
    /// <summary>
    /// Set the velocity to a constant magnitude
    /// </summary>
    public void LateUpdate ()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.Normalize(this.GetComponent<Rigidbody>().velocity) * PlayerStatistics.BallsSpeed;
    }

    /// <summary>
    /// Launch the baall to a random direction
    /// </summary>
    public void Launch()
    {
        EventManager.addActionToEvent(EventType.PLAYER_LOST, DestroyBall);
        EventManager.addActionToEvent(EventType.PLAYER_WON, DestroyBall);

        Vector3 randomInit = new Vector3(Random.Range(-30, 30) / 30.0f, Random.Range(0, 30) / 30.0f, 0);
        randomInit = Vector3.Normalize(randomInit) * PlayerStatistics.BallsSpeed;
        this.GetComponent<Rigidbody>().velocity = randomInit;
    }

    /// <summary>
    /// to call when the ball must be destroyed
    /// </summary>
    public void DestroyBall()
    {
        Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        EventManager.removeActionFromEvent(EventType.PLAYER_LOST, DestroyBall);
        EventManager.removeActionFromEvent(EventType.PLAYER_WON, DestroyBall);
    }
}
