using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

/// <summary>
/// Edit values of a Post-Processing Profile during runtime.
/// </summary>
public class RuntimeEffectsEditor : MonoBehaviour {

    public PostProcessingBehaviour ppBehaviour;
    private PostProcessingProfile profile;     

	void Start ()
    {
        profile = Instantiate(ppBehaviour.profile);     // create a copy to not alter the original asset file
        ppBehaviour.profile = profile;                  // set the used profile to the copy
	}
	
}
