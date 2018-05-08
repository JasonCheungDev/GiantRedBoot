using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // UI 
    public CardController leftCard, rightCard;
    public ScreenFader gameoverScreen;
    // Game objects
    public GameObject player;
    public GameObject dragon;
    [Tooltip("Order of scripted events that occur")]
    public UnityEvent[] actions;
    public Material playerMaterial, dragonMaterial, environmentMaterial;
    private Material pMat, dMat, eMat;

    private int index = 0;              // current event that is playing
    private bool unskippable = false;   // timed event (cannot skip)
    private float counter = 0;          // duration for a timed event (in seconds) 
    private CardController activeCard;  
    private Animator anim;
    private bool dead = false;


    void Start()
    {
        anim = GetComponent<Animator>();
        actions[index].Invoke();

        SetupSharedMaterials();

        // gameoverScreen.FadeTo(0.8f);

        ////// DEBUG 
        //index = 6;
        ////// 
    }


    // duplicates materials on startup to prevent writing directly to assets 
    private void SetupSharedMaterials()
    {
        pMat = new Material(playerMaterial);
        dMat = new Material(dragonMaterial);
        eMat = new Material(environmentMaterial);

        var allRenderers = (Renderer[])FindObjectsOfType(typeof(Renderer));
        var allImages = (Image[])FindObjectsOfType(typeof(Image));
         
        foreach (Renderer R in allRenderers)
        {
            if (R.sharedMaterial == playerMaterial)
                R.sharedMaterial = pMat;
            else if (R.sharedMaterial == dragonMaterial)
                R.sharedMaterial = dMat;
            else if (R.sharedMaterial == environmentMaterial)
                R.sharedMaterial = eMat;
        }

        foreach (Image I in allImages)
        {
            if (I.material == playerMaterial)
                I.material = pMat;
            else if (I.material == dragonMaterial)
                I.material = dMat;
            else if (I.material == environmentMaterial)
                I.material = eMat;
        }
    }


    void Update()
    {
        // stop listening for next event if player is dead or unskippable event. 
        if (dead || unskippable) return;

        if (Input.GetButtonDown("Fire1"))
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

    public void GameOver()
    {
        StartCoroutine(DeathCinematic());
    }

    private IEnumerator DeathCinematic()
    {
        dead = true;
        gameoverScreen.FadeTo(0.8f);
        leftCard.Action_SetState("CardOffScreen");
        rightCard.Action_SetState("CardOffScreen");
        PauseGame();
        yield return new WaitForSeconds(2.0f);

        // wait for player to retry 
        while (dead)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Retry();
                break;
            }

            yield return null;
        }
    }

    public void Retry()
    {
        gameoverScreen.FadeTo(0.0f);

        var dc = dragon.GetComponent<DragonController>();
        dc.SetEventIndex(0);    // restart the fight 
        var mc = player.GetComponent<MovementController>();
        mc.Resurrect();         // restore control to player
        dead = false;
        ResumeGame();
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
        StartCoroutine(WaitForEventFinish(seconds));
    }

    private IEnumerator WaitForEventFinish(float duration)
    {
        unskippable = true;
        yield return new WaitForSeconds(duration);
        unskippable = false;
        NextAction();
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


    public void TransitionPlayerGreyscale(float greyscalePercent)
    {
        StartCoroutine(TransitionGreyscale(pMat, greyscalePercent, 2.0f));
    }


    public void TransitionDragonGreyscale(float greyscalePercent)
    {
        StartCoroutine(TransitionGreyscale(dMat, greyscalePercent, 2.0f));
    }


    public void TransitionEnvironmentGreyscale(float greyscalePercent)
    {
        StartCoroutine(TransitionGreyscale(eMat, greyscalePercent, 2.0f));
    }


    private IEnumerator TransitionGreyscale(Material mat, float effectTarget, float duration)
    {
        float start = mat.GetFloat("_EffectAmount");
        float timeElapsed = 0;
        float percentage = 0;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            percentage = timeElapsed / duration;
            mat.SetFloat("_EffectAmount", Mathf.Lerp(start, effectTarget, percentage));
            yield return null;
        }
    }
}
