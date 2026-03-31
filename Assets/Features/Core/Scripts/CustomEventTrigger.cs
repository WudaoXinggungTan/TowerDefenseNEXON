using UnityEngine;
using UnityEngine.Events;


public class CustomEventTrigger : MonoBehaviour
{
    public UnityEvent m_MyEvent;

    private void Start()
    {
        if (m_MyEvent == null)
        {
            m_MyEvent = new UnityEvent();
        }
    }

    public void InvokeEvent()
    {
        if (m_MyEvent != null)
        {
            m_MyEvent.Invoke();
        }
    }
}