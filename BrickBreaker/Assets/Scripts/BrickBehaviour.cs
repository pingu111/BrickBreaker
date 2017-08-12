using UnityEngine;
using System.Collections;

public enum BrickType
{
    Normal,
    PowerUp_Life
}

public class BrickBehaviour : MonoBehaviour
{
    /// <summary>
    /// This brick type
    /// </summary>
    private BrickType m_ThisBrickType;

    public void InitThisbrick(BrickType thisBrickType)
    {
        m_ThisBrickType = thisBrickType;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Ball>() != null)
        {
            PlayerStatistics.PlayerNbPoints += 100;
            EventManager.raise(EventType.PLAYER_NUMBER_SCORE_CHANGED, PlayerStatistics.PlayerNbPoints);

            Destroy(this.gameObject, 0.1f);
        }
    }
}
