using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.GetComponent<Ball>() != null)
        {
            PlayerStatistics.PlayerNbLifes--;
            Debug.Log("Player lost a life");
        }
    }
}
