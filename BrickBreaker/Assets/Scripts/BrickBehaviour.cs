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
}
