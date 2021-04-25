using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject finalroom;
    public GameObject[] enemies;

    public Transform roomparent;
    public Transform EnemyParent;
    public GameObject[] rooms;

    public int roomstogenerate = 10;

    float origplacingY = -8.5f;
    float curY = -8.5f;

    // Start is called before the first frame update
    void Start()
    {
        generateRooms();
    }

    void generateRooms()
    {
        for (int i = 0; i < roomstogenerate + 1; i++)
        {
            GameObject ob = Instantiate(rooms[Random.Range(0, rooms.Length)], new Vector3(0f, curY, 0f), transform.rotation, roomparent);
            List<Transform> spots = new List<Transform>();
            spots.Add(ob.transform.GetChild(0).transform.GetChild(0));
            spots.Add(ob.transform.GetChild(0).transform.GetChild(1));
            Instantiate(enemies[Random.Range(0, enemies.Length)], spots[0].transform.position, transform.rotation, EnemyParent);
            Instantiate(enemies[Random.Range(0, enemies.Length)], spots[1].transform.position, transform.rotation, EnemyParent);

            curY -= 13f;

            if (i == roomstogenerate)
            {
                Instantiate(finalroom, new Vector3(0f, curY, 0f), transform.rotation, roomparent);
            }
        }
        curY = origplacingY;
    }
}
