using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Allow a button to be maintened, 
/// and will call the input maanger each frame that this button is clicked
/// </summary>
public class ButtonMoveRacket : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private bool m_IsRightArrow = false;

    [SerializeField]
    private InputManager m_InputManager = null;

    private bool m_IsCurrentlyPressed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        m_IsCurrentlyPressed = true;
        this.GetComponent<UnityEngine.UI.Image>().color = Color.red;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_IsCurrentlyPressed = true;
        this.GetComponent<UnityEngine.UI.Image>().color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_IsCurrentlyPressed = false;
        this.GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_IsCurrentlyPressed = false;
        this.GetComponent<UnityEngine.UI.Image>().color = Color.white;
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
