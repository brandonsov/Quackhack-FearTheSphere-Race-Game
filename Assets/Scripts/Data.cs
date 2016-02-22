using UnityEngine;
using System.Collections;

public class Data : MonoBehaviour {

	private int number;
	// Use this for initialization
	void Start () 
	{
		number = 3;
	}
	
	// Update is called once per frame
	void Update () {
		Application.LoadLevel("Scene2");
	}
}
