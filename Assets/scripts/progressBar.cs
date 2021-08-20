using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progressBar : MonoBehaviour
{
   
    private Slider slider = null;
    private ParticleSystem particleSys = null;

    private float    fillSpeed       = 0.2f;
    public  float    targetProgress = 0f;

    private Animator    anim            = null;
    private settings    setting         = null;
    private controller  controller      = null;
    private bool        is_increase     = true;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
        particleSys = GameObject.Find("progress bar particles").GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        setting = FindObjectOfType<settings>();
        controller = FindObjectOfType<controller>();
    }

    // Update is called once per frame
    //HACK
    void Update()
    {
        if(is_increase == true)
        {
            increase();
        }
        else
        {
            decrese();
        }

        if(slider.value > 0.999)
        {
            controller.win();
        }

    }

    private void increase()
    {
        if (slider.value < targetProgress && slider.value < 1)
        {
            slider.value += fillSpeed * Time.deltaTime;
            if (!particleSys.isPlaying)
            {
                particleSys.Play();
                anim.SetBool("zoom", true);
                playProgressSound();
            }
        }
        else
        {
            if (particleSys.isPlaying)
            {
                particleSys.Stop();
            }
           
        }
    }

    private void decrese()
    {
        if (slider.value > targetProgress && slider.value > 0)
        {
            slider.value -= fillSpeed * Time.deltaTime;
            if (!particleSys.isPlaying)
            {
                particleSys.Play();
                anim.SetBool("zoom", true);
                playLoseProgressSound();
            }

        }
        else
        {
            if (particleSys.isPlaying)
            {
                particleSys.Stop();
            }
           

        }
    }

    public void stopAnimation()
    {
        anim.SetBool("zoom", false);
    }

    private void playProgressSound()
    {

        playeSound(setting.gameSounds.porgressSound);
        playeSound(setting.gameSounds.clap);

    }

    private void playLoseProgressSound()
    {

        playeSound(setting.gameSounds.loseProgressSound);
        playeSound(setting.gameSounds.hoo);

    }

    public void increamentProgress()
    {
        is_increase = true;
        float newProgress = setting.progressIncreaseAmount;
        targetProgress += newProgress;
    }

    public void decreaseProgress()
    {
        is_increase = false;
        float newProgress = setting.progressIncreaseAmount;
        if(targetProgress > 0)
            targetProgress -=  newProgress;
    }

    private void playeSound(AudioClip theSound)
    {
        if(theSound != null)
            AudioSource.PlayClipAtPoint(theSound, Camera.main.transform.position, 1f);
    }

}
