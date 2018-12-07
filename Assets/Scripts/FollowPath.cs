using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour {

    public GameObject Pollen;

    public List<GameObject> Followers;

    public float FollowerSpeed;

    public GameObject Newspawn;

    public GameObject StartPoint;

    //public float Count;

    public float SpawnLimit;

    public enum MovementType
    {
        MoveTowards,
        LerpTowards
    }

    public MovementType Type = MovementType.MoveTowards;
    public MovementPath MyPath;
    public float speed = 1;
    public float MaxDistanceToGoal = .1f;


    private IEnumerator<Transform> pointInPath;


    private void Awake()
    {
        if (CompareTag("Bee"))
        {
            StartPoint = GameObject.FindGameObjectWithTag("StartPoint").GetComponent<GameObject>();
            MyPath = GameObject.FindGameObjectWithTag("SpecificPath").GetComponent<MovementPath>();
        }
    }
    // Use this for initialization
    void Start () {
        

		if (MyPath == null)
        {
            Debug.LogError("Movement Path cannot be null.", gameObject);
            return;
        }



        pointInPath = MyPath.GetNextPathPoint();
        pointInPath.MoveNext();

        if(pointInPath.Current == null)
        {
            Debug.LogError("A path must have points in it to follow.", gameObject);
            return;
        }

        Followers = new List<GameObject>();
        transform.position = pointInPath.Current.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (pointInPath == null || pointInPath.Current == null)
        {
            return;
        }

        if(Type == MovementType.MoveTowards)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * speed);

        }
        else if (Type == MovementType.LerpTowards)
        {
            transform.position = Vector3.Lerp(transform.position, pointInPath.Current.position, Time.deltaTime * speed);

        }

        var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
        {
            pointInPath.MoveNext();
        }

        if (Followers.Count != 0)
        {

            for (int i = 0; i < Followers.Count; i++)
            {


                Followers[i].transform.position = Vector3.Lerp(Followers[i].transform.position, this.transform.position, FollowerSpeed);

            }

        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BeeHive")
        {
            for (int i = 0; i < Followers.Count; i++)
            {

                Destroy(Followers[i]);

            }
            Followers.Clear();
            if(CompareTag("Bee"))
            Destroy(this.gameObject);
        }
        else
        {

            GameObject NewFollower = Instantiate(Pollen);
            NewFollower.GetComponent<SpriteRenderer>().color = collision.GetComponent<SpriteRenderer>().color;
            NewFollower.transform.position = collision.transform.position;
            Followers.Add(NewFollower);

        }


    }
}
