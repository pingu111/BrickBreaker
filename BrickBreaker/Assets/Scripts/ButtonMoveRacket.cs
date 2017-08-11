using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

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
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_IsCurrentlyPressed = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_IsCurrentlyPressed = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_IsCurrentlyPressed = false;
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
