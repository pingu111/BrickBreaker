using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.GetComponent<Ball>() != null)
        {
            PlayerStatistics.PlayerNbLifes--;
            EventManager.raise(EventType.PLAYER_NUMBER_LIFE_CHANGED, PlayerStatistics.PlayerNbLifes);
        }
    }
}
