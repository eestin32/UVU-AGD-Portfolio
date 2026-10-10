using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StartEventOnClick : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private UnityEvent startEvent;
    public void OnPointerDown(PointerEventData eventData)
    {
        startEvent.Invoke();
    }
}
