using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDie : MonoBehaviour {
	
	public bool m_side = false;
	public Game m_game; // script Game, pass in GameObject, unity give script
	
	void OnTriggerEnter2D (Collider2D other) {
		
		m_game.HitEdge(m_side);
	}
}


