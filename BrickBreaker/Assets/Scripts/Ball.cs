using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    /// <summary>
    /// The constant speed of the ball
    /// </summary>
    private float m_ConstantVelocity = 3.0f;
	
    /// <summary>
    /// Set the velocity to a constant magnitude
    /// </summary>
    public void LateUpdate ()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.Normalize(this.GetComponent<Rigidbody>().velocity) * m_ConstantVelocity;
    }

    /// <summary>
    /// Launch the baall to a random direction
    /// </summary>
    public void Launch()
    {
        Vector3 randomInit = new Vector3(Random.Range(-30, 30) / 30.0f, Random.Range(0, 30) / 30.0f, 0);
        randomInit = Vector3.Normalize(randomInit) * m_ConstantVelocity;
        this.GetComponent<Rigidbody>().velocity = randomInit;
    }
}
