using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDie : MonoBehaviour {
	
	public bool m_side = false;
	public Game m_game; // script Game, pass in GameObject, unity give script
	
	void OnTriggerEnter2D (Collider2D other) {
		
		m_game.HitEdge(m_side);
		
		/*
		Debug.Log("Exit");
		SpriteRenderer sprite = other.gameObject.GetComponent<SpriteRenderer>();
		sprite.color = Color.cyan;
		Rigidbody2D rigid = other.gameObject.GetComponent<Rigidbody2D>();
		rigid.velocity = Vector3.zero;
		GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		*/
		
	}
	/*
	void OnTriggerEnter2D ( Collider2D other ) {
		Debug.Log("Exit");
		other.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
		//o	
	}*/
}


