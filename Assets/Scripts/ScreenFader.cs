using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour {

    public bool includeChildren = false;
    public float startingOpacity = 0.0f;

    private Image[] images = new Image[0];
    private Text[] texts = new Text[0];
    private Coroutine coroutine;


	void Awake ()
    {
        if (includeChildren)
        {
            images = GetComponentsInChildren<Image>();
            texts = GetComponentsInChildren<Text>();
        }
        else
        {
            var image = GetComponent<Image>();
            if (image != null)
            {
                images = new Image[1];
                images[0] = image; 
            }

            var text = GetComponent<Text>();
            if (text != null)
            {
                texts = new Text[1];
                texts[0] = GetComponent<Text>();
            }
        }

        Debug.Log("ScreenFader for " + gameObject.name);
        Debug.Log(images.Length);
        Debug.Log(texts.Length);
	}


    void Start()
    {
        foreach (Image i in images)
        {
            var currentColor = i.color;
            currentColor.a = startingOpacity;
            i.color = currentColor;
        }
        foreach (Text t in texts)
        {
            var currentColor = t.color;
            currentColor.a = startingOpacity;
            t.color = currentColor;
        }
    }


    public void FadeTo(float opacity)
    {
        if (gameObject.activeSelf)
        {
            // stop existing animation if it exists 
            if (coroutine != null)
                StopCoroutine(coroutine);
            // fade screen out over 2 seconds 
            coroutine = StartCoroutine(Fade(opacity, 2f));
        }
    }

    IEnumerator Fade(float aValue, float aTime)
    {
        float[] initialImageAlphas = images.Select(i => i.color.a).ToArray();
        float[] initialTextAlphas = texts.Select(t => t.color.a).ToArray();

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            // fade images 
            for (int i = 0; i < initialImageAlphas.Length; i++)
            {
                var currentColor = images[i].color;
                currentColor.a = Mathf.Lerp(initialImageAlphas[i], aValue, t);
                images[i].color = currentColor;
            }

            // fade text
            for (int i = 0; i < initialTextAlphas.Length; i++)
            {
                var currentColor = texts[i].color;
                currentColor.a = Mathf.Lerp(initialTextAlphas[i], aValue, t);
                texts[i].color = currentColor;
            }

            yield return null;
        }
    }
}
