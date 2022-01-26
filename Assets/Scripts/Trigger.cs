using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public UnityEvent onEnter;
    public UnityEvent onExit;
    public UnityEvent onTimeout;

    [Tooltip("Активировать событие по таймеру (после активации триггера)")]
    [HideInInspector]
    public bool UseTimeout;
    [HideInInspector]
    public float Delay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            onEnter.Invoke();

            if (UseTimeout)
            {
                Invoke("OnTimerFinish", Delay);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            onExit.Invoke();
        }
    }

    private void OnTimerFinish()
    {
        onTimeout?.Invoke();
    }

}
