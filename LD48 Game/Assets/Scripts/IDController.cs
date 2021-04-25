using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDController : MonoBehaviour
{
    public float curspeed = 0f;
    public float speedaddRate = .1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0f, -curspeed * Time.deltaTime, 0f);
        curspeed += speedaddRate * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //INSTAKILL
            collision.GetComponent<PlayerController>().TakeHealth(10000000);
            gameObject.SetActive(false);
        }
    }
}
