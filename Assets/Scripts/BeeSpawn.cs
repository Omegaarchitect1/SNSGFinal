using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSpawn : MonoBehaviour {

    public int MaxBees = 8;

    public GameObject Newspawn;

    public GameObject StartPoint;

    public int Count;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    private void ComparesBees()
    {

        if (MaxBees > Count)
        {
            Instantiate(Newspawn, StartPoint.transform.position, Quaternion.identity);
            Count++;
            Instantiate(Newspawn, StartPoint.transform.position, Quaternion.identity);
            Count++;
           
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bee")
        {
            ComparesBees();
        }
    }
}
