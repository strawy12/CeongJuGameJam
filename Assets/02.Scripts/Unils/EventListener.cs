using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityObserverEvent : UnityEvent
{

}


public class EventListener : MonoBehaviour
{
    public EventSO gEvent;
    public UnityObserverEvent responseObj = new UnityObserverEvent();

    public bool playCashing;

    private void Awake()
    {
        if(playCashing)
            gEvent.Register(this);

        gameObject.SetActive(false);
    }
    public void OnEventOccurs()
    {
        responseObj.Invoke();
    }

    private void OnDestroy()
    {
        gEvent.UnRegister(this);
    }

    private void OnApplicationQuit()
    {
        gEvent.UnRegister(this);
    }

}
