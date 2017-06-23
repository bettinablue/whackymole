using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour {

	public Player player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision){
		Debug.Log ("Mole was Hit by Something.....transform=" + collision.transform.name + ", collider=" + collision.collider.name);
		if (collision.collider.GetComponent<Hammer>() != null){
			Debug.Log ("Mole was hit by HAMMER!!!!!");
			Mole mole = FindObjectOfType<Mole> ();
			mole.Hide ();
			player.score++;
		}

	}
}
