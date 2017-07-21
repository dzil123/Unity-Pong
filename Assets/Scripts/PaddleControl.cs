using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControl : MonoBehaviour {
	
	public bool m_enabled		=  true;
	
	public float m_min_y		= -3.4f;
	public float m_max_y		=  3.4f;
	
	public float m_speed		=  1.00f; // This is how much it will move per physics tick
	
	public string m_up_key		=  "up";
	public string m_down_key	=  "down";
	
	// Update is called once per frame, FixedUpdate is called once per unit time
	// Update for timers, FixedUpdate for physics objects
	void FixedUpdate () {
		//float h = Input.GetAxis("Vertical"); // GetAxis is -1.0 - 1.0
		// Use GetAxis and apply force, not translate
		
		if (!m_enabled) {
			return;
		}
		
		float change;
		
		bool up		= Input.GetKey(m_up_key);
		bool down	= Input.GetKey(m_down_key);
		
		// start
		
		bool left	= Input.GetKey("left");
		bool right	= Input.GetKey("right");
		// /*
		if ( left ) {
			m_speed = m_speed - 0.05f;
			Debug.Log(m_speed.ToString());
		} else if ( right ) {
			m_speed = m_speed + 0.05f;
		}
		// */
		// end
		
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
}

