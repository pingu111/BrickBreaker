using UnityEngine;
using System.Collections;

public enum BrickType
{
    Normal, // 1 hit
    PowerUp_Life, // add a life
    PowerUp_Speed, // increase speed of balls
    PowerUp_Ball, // add a ball
    Strong, // 2 hits
    SuperStrong // 3 hits
}

public class BrickBehaviour : MonoBehaviour
{
    /// <summary>
    /// This brick type
    /// </summary>
    public BrickType m_ThisBrickType;

    private GameCreator m_Creator = null;

    public void SetStateThisBrick(BrickType thisBrickType, GameCreator creator)
    {
        m_ThisBrickType = thisBrickType;
        m_Creator = creator;

        switch (m_ThisBrickType)
        {
            case BrickType.Normal:
                this.GetComponent<MeshRenderer>().material.color = Color.white;
                break;
            case BrickType.PowerUp_Life:
                this.GetComponent<MeshRenderer>().material.color = Color.red;
                break;
            case BrickType.PowerUp_Speed:
                this.GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case BrickType.PowerUp_Ball:
                this.GetComponent<MeshRenderer>().material.color = Color.yellow;
                break;
            case BrickType.Strong:
                this.GetComponent<MeshRenderer>().material.color = Color.gray;
                break;
            case BrickType.SuperStrong:
                this.GetComponent<MeshRenderer>().material.color = Color.black;
                break;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Ball>() != null)
        {
            m_Creator.OnBrickWasTouched(this);
        }
    }
}
