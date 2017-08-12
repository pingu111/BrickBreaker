using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{

    private float m_ConstantVelocity = 3.0f;
	
    public void LateUpdate ()
    {
        Debug.Log(this.GetComponent<Rigidbody>().velocity + " ");
        this.GetComponent<Rigidbody>().velocity = Vector3.Normalize(this.GetComponent<Rigidbody>().velocity) * m_ConstantVelocity;
    }

    public void Launch()
    {
        Vector3 randomInit = new Vector3(Random.Range(-30, 30) / 30.0f, Random.Range(0, 30) / 30.0f, 0);
        randomInit = Vector3.Normalize(randomInit) * m_ConstantVelocity;
        Debug.Log("Launch ball " + randomInit);
        this.GetComponent<Rigidbody>().velocity = randomInit ;
    }

    void OnCollisionEnter(Collision e)
    {
        ContactPoint cp = e.contacts[0];
        Vector3 _revertDirection = Vector3.Reflect(e.rigidbody.velocity, cp.normal * -1);
        this.GetComponent<Rigidbody>().velocity = (_revertDirection * m_ConstantVelocity);
    }
}
