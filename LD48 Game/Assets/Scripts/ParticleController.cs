using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    //This is for the blood particle effect when the player picks it up
    // it basically waits for the player to enter the trigger then sets radial in the velocity setting of the particle system to -10
    // which make the particles ball up
    // then it destroys the particle system to give the effect of the player "collecting" the blood

    public float bloodaddamount = 1000f;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var e = transform.GetChild(0).GetComponent<ParticleSystem>().emission;
        e.SetBurst(0, new ParticleSystem.Burst(0f, (Int16)PlayerPrefs.GetInt("BP"), (Int16)PlayerPrefs.GetInt("BP"), 0f));
        var b = transform.GetChild(0).GetComponent<ParticleSystem>().collision;
        b.bounceMultiplier = PlayerPrefs.GetFloat("BB");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            //first allow the player to destroy particles on contact
            var pss = transform.GetChild(0).GetComponent<ParticleSystem>().trigger;
            pss.SetCollider(0, GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>());

            //then add points to the gamemanager
            GameObject.Find("Game Manager").GetComponent<GameManager>().addBlood(bloodaddamount);

            //finally clump all the particles together so the player doesn't have to chase down particles
            var ps = transform.GetChild(0).GetComponent<ParticleSystem>().velocityOverLifetime;
            ps.radial = -10f;
        }
    }
}
