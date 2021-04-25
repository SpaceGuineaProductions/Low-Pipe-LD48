using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //SPRITE MUST BE FIRST CHILD
    Transform sprite;

    Vector2 mousePos;

    public int damagedone;

    public float attackSpeed;
    float attackrotAmount = 0f;

    public bool isofEnem = false;
    bool attacking = false;

    float targetRot;

    bool isLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        sprite = transform.GetChild(0);
        GetComponent<Collider2D>().enabled = false;
        sprite.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)) beginAttack(-160f, -20f, false);
        if (Input.GetKeyDown(KeyCode.RightArrow)) beginAttack(-70f, 70f, false);
        if (Input.GetKeyDown(KeyCode.UpArrow)) beginAttack(20f, 200f, false);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) beginAttack(-130f, -230f, true);

        if (attacking)
        {
            sprite.gameObject.SetActive(true);
            GetComponent<Collider2D>().enabled = true;

            transform.localRotation = Quaternion.Euler(0f, 0f, attackrotAmount);

            if (!isLeft)
            {
                if (attackrotAmount < targetRot) attackrotAmount += Time.deltaTime * attackSpeed;
                else
               {
                     sprite.gameObject.SetActive(false);
                    GetComponent<Collider2D>().enabled = false;
                    attacking = false;
                    attackrotAmount = 0f;
                    transform.localRotation = Quaternion.Euler(0f, 0f, attackrotAmount);
                }
            }
            if (isLeft)
            {
                if (attackrotAmount > targetRot) attackrotAmount -= Time.deltaTime * attackSpeed;
                else
                {
                    sprite.gameObject.SetActive(false);
                    GetComponent<Collider2D>().enabled = false;
                    attacking = false;
                    attackrotAmount = 0f;
                    transform.localRotation = Quaternion.Euler(0f, 0f, attackrotAmount);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") collision.GetComponent<EnemyController>().takeHealth(damagedone);
    }

    public void beginAttack(float beginrot, float targetrot, bool b)
    {
        if (!attacking)
        {
            isLeft = b;
            targetRot = targetrot;
            attackrotAmount = beginrot;
            attacking = true;
        }
    }
}
