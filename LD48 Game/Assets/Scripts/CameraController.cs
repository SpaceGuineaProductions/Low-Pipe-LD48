using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform player;

    public float lerpSpeed = 5f;

    float origFOV;
    // Start is called before the first frame update
    void Start()
    {
        origFOV = GetComponent<Camera>().orthographicSize;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        setfov();
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, player.position.y, -10f), lerpSpeed * Time.deltaTime);
    }

    void setfov()
    {
        //zooms the camera out the faster the player is going
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, (origFOV - (player.GetComponent<Rigidbody2D>().velocity.y / 9f)), Time.deltaTime * 3f);
    }
}
