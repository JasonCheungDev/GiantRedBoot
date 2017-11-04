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
        float alpha = image.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            image.color = newColor;
            yield return null;
        }
    }
}
