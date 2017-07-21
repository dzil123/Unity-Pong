using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
	
	public GameObject m_ball;
	
	public PaddleControl m_left_paddle;
	public PaddleControl m_right_paddle;
	
	public SetText m_left_score_text;
	public SetText m_right_score_text;
	
	public float m_middle_angle	= 90f; // 90f is left, 270f is right
	public float m_range_angle	= 75f;
	
	private int m_left_score = 0;
	private int m_right_score = 0;
	
	void RestartScene () {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
	
	void RestartRound () {
		// idk
	}
	
	void Start () {
		//m_score = new Tuple<int, int>(0, 0);
	}
	
	void Update () {
		if ( Input.GetKeyDown(KeyCode.Escape) ) {
			Application.Quit();
		}
		UpdateScore();
	}
	
	void FixedUpdate () {
	
	}
	
	public void UpdateScore () {
		m_left_score_text.Set( m_left_score );
		m_right_score_text.Set( m_right_score );
	}
	
	public void AddPoint ( bool side ) {
		if ( side ) {
			m_right_score += 1;
		} else {
			m_left_score += 1;
		}
	}
	
	public void SetControl (bool enabled) {
		m_left_paddle.m_enabled = enabled;
		m_right_paddle.m_enabled = enabled;
	}
	
	public IEnumerator FunctionTimer(Action func, float delay) {
		yield return new WaitForSeconds(delay);
		func();
	}
	
	public void HitEdge (bool side) { // false = left, right = true
		AddPoint(!side);
		
		SetControl(false);
		
		m_ball.GetComponent<SpriteRenderer>().color = Color.red;
		m_ball.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		
		Debug.Log(side.ToString());
		
		StartCoroutine(FunctionTimer(RestartScene, 2f));
	}
	
}
