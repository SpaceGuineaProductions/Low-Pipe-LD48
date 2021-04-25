using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Transform player;

    public GameObject particleEff;

    public Transform lf;
    public Transform rf;

    public float maxdist = 30f;
    public float ms = 1f;

    public int Health = 5;

    public bool movingLeft = true;

    public bool moving = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        lf = transform.GetChild(0);
        rf = transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        optimize();
        if (moving && Health > 0) checkforWalls();
    }

    public void takeHealth(int damage)
    {
        Health -= damage;
        if (Health <= 0) die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") collision.GetComponent<PlayerController>().TakeHealth(1);
    }

    void die()
    {
        moving = false;
        GetComponent<AudioSource>().Play();
        //Instantiate the death effect then set it to get destroyed
        GameObject o = Instantiate(particleEff, transform.position, transform.rotation);
        Destroy(o, 5f);
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void optimize()
    {
        float dist = Vector2.Distance(transform.position, player.position);
        if (dist > maxdist)
        {
            moving = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            moving = true;
            if (Health > 0) GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void checkforWalls()
    {
        //left and right foot are slightly off as to "see ahead"

        if (movingLeft)
        {
            //left foot first
            // test to see if hitting the ground and not hitting a wall
            if (Physics2D.Raycast(lf.position, -transform.up, .1f) && !Physics2D.Raycast(lf.position, -transform.right, .1f)) transform.position += new Vector3(ms * Time.deltaTime * -1f, 0f, 0f);
            //now check to see if there is no ground or if there is a wall 
            if (!Physics2D.Raycast(lf.position, -transform.up, .1f) || Physics2D.Raycast(lf.position, -transform.right, .1f)) movingLeft = false;
        }
        if (!movingLeft)
        {
            //now we check the right foot
            // test to see if hitting the ground and not hitting a wall
            if (Physics2D.Raycast(rf.position, -transform.up, .1f) && !Physics2D.Raycast(rf.position, transform.right, .1f)) transform.position += new Vector3(ms * Time.deltaTime, 0f, 0f);
            //now check to see if there is no ground or if there is a wall 
            if (!Physics2D.Raycast(rf.position, -transform.up, .1f) || Physics2D.Raycast(rf.position, transform.right, .1f)) movingLeft = true;
        }

        //right foot second
    }
}
