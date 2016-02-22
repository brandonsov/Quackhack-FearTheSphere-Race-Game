using UnityEngine;
using System.Collections;

public class WallDrop : MonoBehaviour {

	public Vector3 endPoint;
	public float duration;

	private Vector3 startPoint;
	private float startTime;

	// Use this for initialization
	void Start () {
		startPoint = transform.position;
    	startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(startPoint, endPoint, (Time.time - startTime) / duration);
	}
}
