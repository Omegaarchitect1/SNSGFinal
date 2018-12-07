using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflySpawn : MonoBehaviour {

    private float nextspawntime;

    [SerializeField]
    private GameObject butteflyprefab;

    [SerializeField]
    private float spawnDelay;

	// Update is called once per frame
	void Update () {
        if (ShouldSpawn())
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        nextspawntime = Time.time + spawnDelay;
        Instantiate(butteflyprefab, transform.position, transform.rotation);
    }

    private bool ShouldSpawn()
    {
        return Time.time > nextspawntime;
    }
	
	
}
