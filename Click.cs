using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public AudioSource click;
 
    void Start()
    {
        click = GetComponent<AudioSource>();
    }

    public void ClickSound()
    {
        click.Play();
    }
}
