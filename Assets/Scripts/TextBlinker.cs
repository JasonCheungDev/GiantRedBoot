using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlinker : MonoBehaviour {

    public float blinkDelay = 0.5f;
    private float counter = 0;
    private Text t;


    void Awake()
    {
        t = GetComponent<Text>();
    }


    void Start()
    {
        StartCoroutine(Blink());
    }


    IEnumerator Blink()
    {
        while (true)
        {
            t.enabled = !t.enabled;
            yield return new WaitForSeconds(blinkDelay);
        }
    }
}
