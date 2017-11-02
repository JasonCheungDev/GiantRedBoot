using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Actionable
{
    public UnityEvent myEvent;
    public string AnimationName;
    public bool SetTo;

    public void Act(Animator anim)
    {
        anim.SetBool(AnimationName, SetTo);
        if (myEvent != null)
            myEvent.Invoke();
    }
}
