using UnityEngine;
using System.Collections;

public class PlayerRacket : MonoBehaviour
{
    /// <summary>
    /// The max (or min) position available for the racket
    /// </summary>
    private float m_ClampedPositionX = -1.0f;

    /// <summary>
    /// Set the intiail position of the racket
    /// </summary>
	public void InitRacket()
    {
        float wantedWidth = 0.2f * Camera.main.orthographicSize * Screen.width / Screen.height;
        this.transform.localScale = new Vector3(this.transform.localScale.x, wantedWidth, this.transform.localScale.z);
        this.transform.localPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.5f, Screen.height * 0.05f, 0));

        m_ClampedPositionX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - this.GetComponent<CapsuleCollider>().bounds.size.x / 2;
    }

    /// <summary>
    /// Move the racket with the offset in argument
    /// </summary>
    /// <param name="offsetX"></param>
    public void RacketGoToX(float offsetX)
    {
        if (this.transform.localPosition.x + offsetX > m_ClampedPositionX)
            this.transform.localPosition = new Vector3(m_ClampedPositionX, this.transform.localPosition.y, this.transform.localPosition.z);
        else if (this.transform.localPosition.x + offsetX <  - m_ClampedPositionX)
            this.transform.localPosition = new Vector3( - m_ClampedPositionX, this.transform.localPosition.y, this.transform.localPosition.z);
        else
            this.transform.localPosition = new Vector3(this.transform.localPosition.x + offsetX, this.transform.localPosition.y, this.transform.localPosition.z);
    }
}
