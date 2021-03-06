using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private GameObject map1;
    [SerializeField] private GameObject map2;
    [SerializeField] private GameObject map3;
    [SerializeField] private ShopKeeper shopKeeper;
    private List<GameObject> spawners = new List<GameObject>();
    private GameObject player;
    private AudioSource audio;

    private AudioClip stairsSound;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audio = player.GetComponent<AudioSource>();
        stairsSound = Resources.Load("Taking the Stairs") as AudioClip;
    }

    public void DeleteCurrentFloor()
    {
        GameObject currentFloor = GameObject.FindGameObjectWithTag("Map");
        GameObject[] currentEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        Destroy(GameObject.FindGameObjectWithTag("Player Spawner"));
        Destroy(currentFloor);
        for (int i = 0; i < currentEnemies.Length; i++)
        {
            Destroy(currentEnemies[i]);
        }
        GenerateNextFloor();
    }

    public void GenerateNextFloor()
    {
        GameObject newFloor;
        int rand = Random.Range(1, 4);
        Vector3 pos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        if (rand == 1)
        {
            newFloor = Instantiate(map1, pos, Quaternion.identity);
        }
        else if (rand == 2)
        {
            newFloor = Instantiate(map2, pos, Quaternion.identity);
        }
        else
        {
            newFloor = Instantiate(map3, pos, Quaternion.identity);
        }
        GetChildObject(newFloor.transform, "Player Spawner");
        Debug.Log(spawners);
        shopKeeper.ChangePosition(newFloor);
        Vector3 spawn = spawners[0].transform.position;
        player.transform.position = spawn;
        spawners[0].tag = "Untagged";
        spawners.Clear();
        audio.PlayOneShot(stairsSound);
    }

    private void GetChildObject(Transform parent, string _tag)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == _tag)
            {
                spawners.Add(child.gameObject);
            }
            if (child.childCount > 0)
            {
                GetChildObject(child, _tag);
            }
        }
    }
}
    
    

