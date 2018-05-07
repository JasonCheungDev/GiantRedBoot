using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour {

    private Image image;

	void Start ()
    {
        image = GetComponent<Image>();		
	}

    public void FadeTo(float opacity)
    {
        StartCoroutine(Fade(opacity, 2f));
    }

    IEnumerator Fade(float aValue, float aTime)
    {
        Color currentColor = image.color;
        float initialAlpha = image.color.a;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            currentColor.a = Mathf.Lerp(initialAlpha, aValue, t);
            image.color = currentColor;
            yield return null;
        }
    }
}
