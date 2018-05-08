using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public CardController leftCard;
    public CardController rightCard;
    public GameObject player;
    public GameObject dragon;
    public UnityEvent[] actions;
    public float counter = 0;
    public bool unskippable = false;    // timed event (cannot skip)
    private int index = 0;
    private CardController activeCard;
    private Animator anim;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        actions[index].Invoke();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // timed (unskippable) event 
        if (unskippable)
        {
            counter -= Time.deltaTime;

            if (counter <= 0)
            {
                unskippable = false;
                NextAction();
            }
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            // try to skip 
            if (leftCard.LoadingText)
                leftCard.Skip();
            else if (rightCard.LoadingText)
                rightCard.Skip();
            else
                NextAction();        
        }	
	}

    public void PauseGame()
    {
        player.GetComponent<MovementController>().enabled = false;
        dragon.GetComponent<DragonController>().enabled = false;
    }

    public void ResumeGame()
    {
        player.GetComponent<MovementController>().enabled = true;
        dragon.GetComponent<DragonController>().enabled = true;
    }

    public void NextAction()
    {
        Debug.Log("GameController: Next Action");
        index++;
        if (index < actions.Length)
            actions[index].Invoke();
    }

    public void Action_SetEventDuration(float seconds)
    {
        unskippable = true;
        counter = seconds;
    }

    public void Action_SetActiveCard(string card)
    {
        if (card == "left")
        {
            anim.SetBool("RightCardActive", false);
            anim.SetBool("LeftCardActive", true);
            activeCard = leftCard;
        }
        else if (card == "right") 
        {
            anim.SetBool("RightCardActive", true);
            anim.SetBool("LeftCardActive", false);
            activeCard = rightCard;
        }
    }

    public void Action_SetMainCard(string card)
    {
        if (card == "left")
        {
            anim.SetBool("RightCardActive", false);
            anim.SetBool("LeftCardActive", true);
            anim.SetBool("LeftCardMain", true);
            activeCard = leftCard;
        }
        else if (card == "right")
        {
            anim.SetBool("LeftCardActive", false);
            anim.SetBool("RightCardActive", true);
            anim.SetBool("RightCardMain", true);
            activeCard = rightCard;
        }
    }

    public void Action_SetText(string text)
    {
        activeCard.SetNextText(text);
    }

    public void Action_SetInitialIndex(int index)
    {
        
    }

    public void Action_SetAnimOn(string name)
    {
        anim.SetBool(name, true);
    }

    public void Action_SetAnimOff(string name)
    {
        anim.SetBool(name, true);
    }
}
