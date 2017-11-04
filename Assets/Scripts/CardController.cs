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
    private float counter = 0;
    private Animator anim;

	// Use this for initialization
	void Start ()
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
	
	void Update ()
    {
        // display next character
        if (counter < 0)
        {
            Debug.Log("text length" + dialogueText.Length);
            Debug.Log("text index" + textIndex);
            // update if dialogue is not done yet
            if (textIndex < dialogueText.Length)
            {
                counter += characterDelay;
                textIndex++;
                RefreshText();
            }
        }

        counter -= Time.deltaTime;
	}
        
    // Use to start dialogue with some text
    public void SetIndex(int index)
    {
        textIndex = index;
    }

    public void SetNextText(string message)
    {
        Debug.Log("Setnext text called " + message);
        BodyText.text = "";
        dialogueText = message;
        Debug.Log(dialogueText);
        Debug.Log(dialogueText.Length);
        textIndex = 0;
        counter = 0;
        RefreshText();
    }

    public void SetName(string text)
    {
        NameText.text = text;
    }

    private void RefreshText()
    {
        BodyText.text = dialogueText.Substring(0, textIndex);
    }

    public void Skip()
    {
        textIndex = dialogueText.Length - 1;
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
