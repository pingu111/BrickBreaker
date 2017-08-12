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

    /// <summary>
    /// The creator of this brick, to call when this brick is hitten, to apply the rules
    /// </summary>
    private GameCreator m_Creator = null;

    #region BonusMaterial
    [SerializeField]
    private Material m_NormalMaterial = null;
    [SerializeField]
    private Material m_LifeMaterial = null;
    [SerializeField]
    private Material m_BallMaterial = null;
    [SerializeField]
    private Material m_SpeedMaterial = null;
    #endregion

    [SerializeField]
    private GameObject m_ExplostionPrefab;

    public bool m_IsBrickFalling = false;

    /// <summary>
    /// Init this brick with the given state
    /// </summary>
    /// <param name="thisBrickType"></param>
    /// <param name="creator"></param>
    public void SetStateThisBrick(BrickType thisBrickType, GameCreator creator)
    {
        m_ThisBrickType = thisBrickType;
        m_Creator = creator;

        switch (m_ThisBrickType)
        {
            case BrickType.Normal:
                this.GetComponent<MeshRenderer>().material = m_NormalMaterial;
                this.GetComponent<MeshRenderer>().material.color = Color.white;
                break;
            case BrickType.PowerUp_Life:
                this.GetComponent<MeshRenderer>().material = m_LifeMaterial;
                break;
            case BrickType.PowerUp_Speed:
                this.GetComponent<MeshRenderer>().material = m_SpeedMaterial;
                break;
            case BrickType.PowerUp_Ball:
                this.GetComponent<MeshRenderer>().material = m_BallMaterial;
                break;
            case BrickType.Strong:
                this.GetComponent<MeshRenderer>().material = m_NormalMaterial;
                this.GetComponent<MeshRenderer>().material.color = Color.gray;
                break;
            case BrickType.SuperStrong:
                this.GetComponent<MeshRenderer>().material = m_NormalMaterial;
                this.GetComponent<MeshRenderer>().material.color = Color.black;
                break;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Ball>() != null)
        {
            if (m_IsBrickFalling)
                m_Creator.OnBrickFallingTouched(this, true);
            else
            {
                m_Creator.NonFallingBrickTouchedByBall(this);
            }
        }
        else if (collision.transform.GetComponent<Ground>() != null)
        {
            if(m_IsBrickFalling)
            {
                m_Creator.OnBrickFallingTouched(this, false);
            }
        }
    }

    public void DestroyThisBrick()
    {
        GameObject explosion = Instantiate(m_ExplostionPrefab);
        explosion.transform.position = this.transform.position;
        Destroy(this.gameObject, 0.01f);
    }
}