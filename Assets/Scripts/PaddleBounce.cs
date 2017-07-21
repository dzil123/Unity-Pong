using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBounce : MonoBehaviour {
	
	public GameObject m_ball;
	
	private Collider2D m_collider;
	
	// Use this for initialization
	void Start () {
		m_collider = m_ball.GetComponent<Collider2D>();
		Debug.Log("hello there");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		Debug.Log("Start");
		Debug.Log(m_collider.ToString());
		Debug.Log(other.ToString());
		bool a = (other == m_collider);
		Debug.Log(a.ToString());
		bool b = System.Object.ReferenceEquals(other, m_collider);
		Debug.Log(b.ToString());
		
		Debug.Log(other.IsTouching(m_collider).ToString());
		
		Time.timeScale = 0.01f;
		
	}
	
	void OnTriggerStay2D (Collider2D other) {
		this.OnTriggerEnter2D(other);
	}
}
