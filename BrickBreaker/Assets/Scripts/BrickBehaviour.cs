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
    /// <summary>
    /// All the materials for each bonus
    /// </summary>
    [SerializeField]
    private Material m_NormalMaterial = null;
    [SerializeField]
    private Material m_LifeMaterial = null;
    [SerializeField]
    private Material m_BallMaterial = null;
    [SerializeField]
    private Material m_SpeedMaterial = null;
    #endregion

    /// <summary>
    /// Explosion prefab, to insatntiate when this brick is destroyed
    /// </summary>
    [SerializeField]
    private GameObject m_ExplostionPrefab;

    /// <summary>
    /// True if this brick is falling, false otherwise
    /// </summary>
    public bool m_IsBrickFalling = false;

    /// <summary>
    /// True if this brick is already destroyed, false otherwise
    /// </summary>
    private bool m_IsDestroyed = false;

    /// <summary>
    /// Init this brick with the given state
    /// </summary>
    /// <param name="thisBrickType"></param>
    /// <param name="creator"></param>
    public void SetStateThisBrick(BrickType thisBrickType, GameCreator creator)
    {
        m_IsDestroyed = false;
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
        // prevent double touch on a brick already in the process of destruction
        if (m_IsDestroyed)
        {
            return;
        }

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

    /// <summary>
    /// To call when this brick needs to be destroyed
    /// </summary>
    public void DestroyThisBrick()
    {
        if (m_IsDestroyed)
            return;

        m_IsDestroyed = true;
        GameObject explosion = Instantiate(m_ExplostionPrefab);
        explosion.transform.position = this.transform.position;
        Destroy(this.gameObject, 0.1f);
    }
}