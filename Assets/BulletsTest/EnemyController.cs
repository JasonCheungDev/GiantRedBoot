using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public GameObject[] shotPatterns;
    public float[] timings;

    private int currentShotIndex;

    public float currentTime;

    private bool switchingShots;

    private Animator anim;

    public string[] animations;

    public GameObject[] shotPatternObjects;

    // Use this for initialization
    void Start () {
        currentTime = 0.0f;
        switchingShots = false;
        anim = GetComponent<Animator>();
        shotPatternObjects = new GameObject[shotPatterns.Length];

        for (int i = 0; i < shotPatterns.Length; i++)
        {
            GameObject obj = Instantiate(shotPatterns[i], transform.position, transform.rotation);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            shotPatternObjects[i] = obj;
        }

        currentTime = timings[currentShotIndex];
        shotPatternObjects[currentShotIndex].SetActive(true);
        anim.Play(animations[currentShotIndex], -1, 0.0f);
    }
	
	// Update is called once per frame
	void Update () {

        

        if(currentTime <= 0.0f)
        {
            switchingShots = true;
        }

        if(switchingShots)
        {
            shotPatternObjects[currentShotIndex].SetActive(false);
            IncrementShotPattern();
            currentTime = timings[currentShotIndex];
            shotPatternObjects[currentShotIndex].SetActive(true);
            anim.Play(animations[currentShotIndex], -1, 0.0f);
            switchingShots = false;
        }

        currentTime -= Time.deltaTime;
	}

    void IncrementShotPattern()
    {
        if(currentShotIndex != shotPatterns.Length - 1)
        {
            currentShotIndex++;
        }
        else
        {
            currentShotIndex = 0;
        }
    }
}
