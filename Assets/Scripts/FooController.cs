using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FooController : MonoBehaviour {

    private CardController card;

	// Use this for initialization
	void Start () {
        card = GetComponent<CardController>();
        card.SetName("Rainbow");
        StartCoroutine(SetText("Hello, I'm Rainbow and my lifes goal is to restore color to the world!", 2f));
	}
	
	// Update is called once per frame
	void Update () {

        // Invoke(card.SetNextText("Hello, I'm Rainbow and my lifes goal is to restore color to the world!"), 2f);
	}

    IEnumerator SetText(string text, float delay)
    {
        yield return new WaitForSeconds(delay);
        card.SetNextText(text);
    }


}
