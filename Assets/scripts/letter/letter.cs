using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class letter : MonoBehaviour
{

    /*
     this class is about check the letter is right or not when it clicked
     - it played some animations and sounds
     - it get the sound and letter name from letter info
     - it controled by the controller
     - the right answer is stored in controller
    */

    private LetterInfo  letterInfo  = null;
    private controller  controller  = null;
    private settings    settings    = null;
    private Animator    anim        = null;
    private progressBar progressBar = null;



    private void Start()
    {
        //get information about the letter sound and name and voulume;
        letterInfo = transform.GetChild(0).GetComponent<LetterInfo>();

        //get Controller
        controller = FindObjectOfType<controller>();

        //get settings
        settings = FindObjectOfType<settings>();

        //get Progress Bar
        progressBar = FindObjectOfType<progressBar>();

        //get Animator
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

    }

    private void OnMouseDown()
    {
        if(controller.waite <= 0)
        {
            if (letterInfo.letterName == controller.rightLetter.letterName)
            {
                rightAnswer();
            }
            else
            {
                falseAnswer();
            }
        }
        
    }

    private void rightAnswer()
    {
        playSound(settings.gameSounds.trueAnswer);
        anim.SetBool("TrueAnswer", true);
        progressBar.increamentProgress();
        controller.rightAnswer();
    }

    private void falseAnswer()
    {
        playSound(settings.gameSounds.falseAnswer);
        anim.SetBool("FalseAnswer", true);
        progressBar.decreaseProgress();
        controller.falseAnswer();
    }

    private void playSound(AudioClip theSound)
    {
        if(theSound != null)
        AudioSource.PlayClipAtPoint(theSound, Camera.main.transform.position);  
    }



}
