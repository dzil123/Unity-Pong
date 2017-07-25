using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControl : MonoBehaviour {
	
	public bool m_enabled = false; // by default
	
	public float m_min_y = -3.4f;
	public float m_max_y =  3.4f;
	
	public float m_speed =  1.00f; // This is how much it will move per physics tick
	
	public BallControl m_ball;
	public Game m_game;
	
	public bool m_side;
	
	public string m_up_key   = "up";
	public string m_down_key = "down";
	
	void FixedUpdate () {
		
		if (!m_enabled) { // m_enabled is whether input is enabled
			return;
		}
		
		float change;
		
		bool up		= Input.GetKey(m_up_key);
		bool down	= Input.GetKey(m_down_key);
		
		/*
			bool left	= Input.GetKey("left");
			bool right	= Input.GetKey("right");
			
			if ( left ) {
				m_speed = m_speed - 0.05f;
				Debug.Log(m_speed.ToString());
			} else if ( right ) {
				m_speed = m_speed + 0.05f;
			}
		*/
				
		if ( up && !down ) {
			change =  Time.deltaTime;
		} else if ( down && !up ) {
			change = -Time.deltaTime;
		} else {
			return;
		}
		
		change = change * m_speed;
		
		Vector3 pos = transform.position;
		
		float new_y = pos.y + change;
		
		if ( new_y > m_max_y ) {
			new_y = m_max_y;
		} else if ( new_y < m_min_y ) {
			new_y = m_min_y;
		}
		
		transform.position = new Vector3(pos.x, new_y, pos.z);
	}
	
	public void Reset() {
		Vector3 pos = transform.position;
		transform.position = new Vector3(pos.x, 0, pos.z);
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		//Debug.Log(m_side);
		m_game.HitPaddle(m_side);
		
	}
	
	void OnTriggerStay2D (Collider2D other) {
		//this.OnTriggerEnter2D(other);
	}
}
