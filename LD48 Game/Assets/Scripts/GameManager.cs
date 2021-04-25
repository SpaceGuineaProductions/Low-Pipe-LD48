using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    Transform player;

    public TextMeshProUGUI speeduptext;
    public TextMeshProUGUI depthText;

    public Image playerbloodfill;

    // max amount of blood the player can have before reset.
    public float maxbloodFill = 100000;

    public float currentBlood = 0f;

    public bool canaddBlood = true;
    public bool resetting = false;

    // Start is called before the first frame update
    void Start()
    {
        speeduptext.color = new Color(speeduptext.color.r, speeduptext.color.g, speeduptext.color.b, 0f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        depthText.text = "Depth: " + Mathf.FloorToInt(player.position.y) * -1f;
        setfillAmount();
    }

    void setfillAmount()
    {
        // if blood fill is over the maxblood fill then give the player a powerup (perma speed boost)
        //then reset currentBlood and make maxbloodfill twice as large
        if (currentBlood > maxbloodFill)
        {
            speeduptext.color = new Color(speeduptext.color.r, speeduptext.color.g, speeduptext.color.b, 1f);
            resetting = true;
            canaddBlood = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().health += 1;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().moveSpeed += .9f;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().gravityScale += .4f;

            currentBlood = 0f;
            maxbloodFill = maxbloodFill + (maxbloodFill * 1.1f);
        }
        if (!resetting && !canaddBlood)
        {
            //lerp upwards when gaining blood
            playerbloodfill.fillAmount = Mathf.Lerp(playerbloodfill.fillAmount, (currentBlood / maxbloodFill), Time.deltaTime * 5f);
            //check if fill amount is within .03 of what it needs to be as lerping takes too long
            if (playerbloodfill.fillAmount > ((currentBlood / maxbloodFill) - .03f)) canaddBlood = true;
        }
        else if (resetting && !canaddBlood)
        {
            //lerp the meter down for a smoother effect
            playerbloodfill.fillAmount = Mathf.Lerp(playerbloodfill.fillAmount, 0f, Time.deltaTime * 5f);
            if (playerbloodfill.fillAmount < 0.03f) canaddBlood = true;
        }

        //lerp the text opacity outside of the reset part so it can stay up longer
        if (canaddBlood) speeduptext.color = Color.Lerp(speeduptext.color, new Color(speeduptext.color.r, speeduptext.color.g, speeduptext.color.b, 0f), Time.deltaTime);

    }

    public void addBlood(float amount)
    {
        if (canaddBlood)
        {
            currentBlood += amount;
            canaddBlood = false;
            resetting = false;
            setfillAmount();
        }
    }
}
