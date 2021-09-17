using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadeCollision : MonoBehaviour
{
    private AudioSource audioSrc;
    public AudioClip shadeHit;

    private GameObject text;
    void Start()
    {
        //Physics2D.IgnoreLayerCollision(8, 8);
        audioSrc = GetComponent<AudioSource>();
        text = GameObject.FindGameObjectWithTag("Text");

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        text.GetComponent<textScript>().legionKilled += 1;
        //audioSrc.PlayOneShot(shadeHit);
        //Debug.Log("Killed");
        //I must do it this way instead of audioSrc.PlayOneShot because I am destroying the game object right after
        AudioSource.PlayClipAtPoint(shadeHit, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0));
        Destroy(gameObject);
        StateNameController.legionKillCounter++;



    }

    
}
