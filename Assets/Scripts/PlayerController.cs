using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour {

	public float speed;
	public string HorizontalInput;
	public string VerticalInput;
	public string CheckpointTag;
	public GameObject Checkpoint1;
	public GameObject Checkpoint2;
	public Text lapText;
	public GameObject finishLine;
	public Text winText;
	public string FinishTag;
	public Transform SpawnPoint;
	public int goal_laps;

	private bool respawn;
	private int checkerCtr = 0;
	private Rigidbody rb;
	private int score = 0;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log(checkerCtr);
		float moveHorizontal = Input.GetAxis(HorizontalInput);
		float moveVertical = Input.GetAxis(VerticalInput);

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

	    rb.AddForce (movement * speed * Time.deltaTime);
	    if (transform.position.y < - 20)
	    {
	    	respawn = true;
	    }
	    else
	    {
	    	respawn = false;
	    }

	    if (respawn)
	    {	
	    	checkerCtr -= 2;
	    	transform.position = SpawnPoint.position;
	    	rb.Sleep();
	    }
	    if (checkerCtr == goal_laps)
	    {
	    	Checkpoint1.SetActive (false);
	    	finishLine.SetActive (true);
	    }
	    else
	    {
		    if (Math.Abs(checkerCtr) % 2 == 0)
		    {
		    	Checkpoint1.SetActive (true);
		    	Checkpoint2.SetActive (false);
		    	SetLapText ();
		    }

		    if (Math.Abs(checkerCtr) % 2 == 1 && checkerCtr != goal_laps)
		    {
		    	finishLine.SetActive (false);
		    	Checkpoint1.SetActive (false);
		    	Checkpoint2.SetActive (true);
		    }
	    }
	}

	void SetLapText ()
	{
		lapText.text = this.name + " Lap: " + (checkerCtr/2).ToString();
	}

	void OnTriggerEnter(Collider other) 
	{
		
		if (other.name == "Grass")
		{
			speed = speed / 4 ;	
		}

		if (other.gameObject.tag == CheckpointTag)
		{
			checkerCtr += 1;
		}

		if (other.gameObject.tag == FinishTag)
		{
			StartCoroutine(Win());
		}

	}

	IEnumerator Win()
	{
		winText.text = this.name + ": You Win!";
	    Time.timeScale = .0000001f;
	    yield return new WaitForSeconds(3f * Time.timeScale);
	    Time.timeScale = 1.0f;
	    score += 1;
	    Application.DontDestroyOnLoad(this);
	    Application.LoadLevel("Scene2");
	}

	void OnTriggerExit(Collider other) 
	{
		if (other.name == "Grass")
		{
			speed = speed * 4;
		}
	}
}

