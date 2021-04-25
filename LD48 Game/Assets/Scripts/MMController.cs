using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MMController : MonoBehaviour
{
    public TextMeshProUGUI lowestdepth;

    public TMP_InputField bp;
    public Slider bB;

    // Start is called before the first frame update
    void Start()
    {
        bp.text = PlayerPrefs.GetInt("BP").ToString();
        bB.value = PlayerPrefs.GetFloat("BB");
    }

    // Update is called once per frame
    void Update()
    {
        lowestdepth.text = "LOWEST DEPTH: " + PlayerPrefs.GetInt("LOWESTDEPTH");

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(PlayerPrefs.GetInt("BP"));
            Debug.Log(PlayerPrefs.GetFloat("BB"));
        }

        PlayerPrefs.SetInt("BP", Mathf.FloorToInt(int.Parse(bp.text)));
        PlayerPrefs.SetFloat("BB", bB.value);

        if (Input.GetKeyDown(KeyCode.Return)) SceneManager.LoadScene("Main Game");
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
}
