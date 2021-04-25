using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Camera cam;

    public AudioSource playerhurt;

    public GameObject petgobtmmText;
    public TextMeshProUGUI diedtext;
    public TextMeshProUGUI healthText;

    public int health = 3;

    public float moveSpeed = 5f;

    bool isAlive = true;

    //how long the player flashes for
    float flashlasttime = 2f;
    float fltMin = 0f;

    //how often to flash
    float flashingtime = .1f;
    float ftMin = 0f;

    bool spriteison = true;
    bool isHurt = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("LOWESTDEPTH")) PlayerPrefs.SetInt("LOWESTDEPTH", 0);

        diedtext.enabled = false;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("Main Menu");

        if (!isAlive)
        {
            int prevhighscore = PlayerPrefs.GetInt("LOWESTDEPTH");

            if (Mathf.Abs(Mathf.FloorToInt(transform.position.y)) > prevhighscore)
            {
                PlayerPrefs.SetInt("LOWESTDEPTH", Mathf.Abs(Mathf.FloorToInt(transform.position.y)));
            }

            //this is a really dumb way to end the game but it works so dont @ me
            if (cam.orthographicSize < .6f)
            {
                petgobtmmText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Return)) SceneManager.LoadScene("Main Menu");
                GetComponent<SpriteRenderer>().enabled = true;
                diedtext.enabled = true;
                diedtext.text = "YOU DIED! \n Depth: " + Mathf.FloorToInt(transform.position.y) * -1f;
            }

            cam.transform.parent = transform;
            cam.transform.position = new Vector3(transform.position.x, transform.position.y, -11f);
            cam.GetComponent<CameraController>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, .3f, Time.deltaTime * 1.1f);
        }

        if (isAlive)
        {
            if (health <= 0) die();
            if (!isHurt) GetComponent<SpriteRenderer>().enabled = true; // sometimes flashsprite will end and won't set spriterenderer.enabled to true, rather than fix that I'm doing this.
            if (isHurt) flashSprite();
            healthText.text = "Health: " + health;
            if (Input.GetKey(KeyCode.A)) transform.position += new Vector3(moveSpeed * Time.deltaTime * -1f, 0f, 0f);
            if (Input.GetKey(KeyCode.D)) transform.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
        }
    }

    void die()
    {
        isAlive = false;
    }

    void flashSprite()
    {
        fltMin += Time.deltaTime;
        if (fltMin >= flashlasttime)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            spriteison = true;
            isHurt = false;
            fltMin = 0f;
        }
        ftMin += Time.deltaTime;
        if (ftMin > flashingtime)
        {
            spriteison = !spriteison;
            GetComponent<SpriteRenderer>().enabled = spriteison;
            ftMin = 0f;
        }

    }

    public void TakeHealth(int damage)
    {
        playerhurt.Play();
        isHurt = true;
        health -= damage;
    }
}

