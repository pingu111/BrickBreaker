using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Allow a button to be maintened, 
/// and will call the input maanger each frame that this button is clicked
/// </summary>
public class ButtonMoveRacket : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    /// <summary>
    /// True if this arrrow is the right one, false if left one
    /// </summary>
    [SerializeField]
    private bool m_IsRightArrow = false;

    /// <summary>
    /// The input manager of the racket
    /// </summary>
    [SerializeField]
    private InputManager m_InputManager = null;

    /// <summary>
    /// True if the arrow is currently pressed, false otherwise
    /// </summary>
    private bool m_IsCurrentlyPressed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        m_IsCurrentlyPressed = true;
        this.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0, 0, 0.2f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_IsCurrentlyPressed = true;
        this.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0, 0, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_IsCurrentlyPressed = false;
        this.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 0.2f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_IsCurrentlyPressed = false;
        this.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 0.2f);
    }

    void Update()
    {
        if (!m_IsCurrentlyPressed)
            return;

        if(m_IsRightArrow)
        {
            m_InputManager.OnClickRightArrow();
        }
        else
        {
            m_InputManager.OnClickLeftArrow();
        }
    }
}
