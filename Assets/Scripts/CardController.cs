using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour {

    public Text NameText;
    public Text BodyText;

    public string dialogueText;
    public float characterDelay = 0.2f; // time till next character is printed 
    public bool flipHorizontally = true;
    public bool LoadingText { get { return textIndex < dialogueText.Length; } }

    private int textIndex = 0;
    private Animator anim;
    private Coroutine textLoaderCoroutine; 


	// Use this for initialization
	void Awake ()
    {
        anim = GetComponent<Animator>();

		if (!flipHorizontally)
        {
            transform.FlipX();
            transform.FindDeepChild("Portrait Mask").FlipX();
            transform.FindDeepChild("Portrait").FlipX();
            transform.FindDeepChild("DialogueBox").FlipX();
        }
    }


    public void SetName(string text)
    {
        NameText.text = text;
    }


    public void SetCharacterDelay(float delay)
    {
        characterDelay = delay;
    }


    public void SetNextText(string message, int startAt)
    {
        Debug.Log("Setnext text called starting at character " + startAt);
        dialogueText = message;
        textIndex = startAt;
        RefreshText();

        if (textLoaderCoroutine != null)
            StopCoroutine(textLoaderCoroutine);

        textLoaderCoroutine = StartCoroutine(LoadText(message));
    }


    // seperated for UnityEvents 
    public void SetTextIndex(int startAt)
    {
        textIndex = startAt;
        RefreshText();
    }


    // seperated for UnityEvents 
    public void SetNextText(string message)
    {
        Debug.Log("Setnext text called " + message);
        dialogueText = message;
        textIndex = 0;
        RefreshText();

        if (textLoaderCoroutine != null)
            StopCoroutine(textLoaderCoroutine);

        textLoaderCoroutine = StartCoroutine(LoadText(message));
    }


    public void Skip()
    {
        Debug.Log(" Skiping text for " + gameObject);
        StopCoroutine(textLoaderCoroutine);

        textIndex = dialogueText.Length;
        RefreshText();
    }


    private IEnumerator LoadText(string message)
    {
        yield return null;  // wait one frame before doing anything

        while(textIndex < dialogueText.Length)
        {
            textIndex++;
            RefreshText();
            yield return new WaitForSeconds(characterDelay);
        }
    }


    private void RefreshText()
    {
        BodyText.text = dialogueText.Substring(0, textIndex);
    }


    // for fine tune control
    public void Action_SetAnimOn(string name)
    {
        anim.SetBool(name, true);
    }
    public void Action_SetAnimOff(string name)
    {
        anim.SetBool(name, false);
    }

    // convenience
    public void Action_SetState(string name)
    {
        switch(name)
        {
            case "CardMain":
                anim.SetBool("Large", true);
                anim.SetBool("Active", false);
                anim.SetBool("Offscreen", false);
                break;
            case "CardActive":
                anim.SetBool("Active", true);
                anim.SetBool("Large", false);
                anim.SetBool("Offscreen", false);
                break;
            case "CardInactive":
                anim.SetBool("Active", false);
                anim.SetBool("Large", false);
                anim.SetBool("Offscreen", false);
                break;
            case "CardOffScreen":
                anim.SetBool("Active", false);
                anim.SetBool("Large", false);
                anim.SetBool("Offscreen", true);
                break;
        }
    }

}
