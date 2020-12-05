using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTextScript : MonoBehaviour
{
    public string numberToShow;
    private Animator textAnimator;
    // Start is called before the first frame update
    void Start()
    {
        textAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RestartAnimation()
    {
       textAnimator.SetTrigger("Restart");
    }
}
