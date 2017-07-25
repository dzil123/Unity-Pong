using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour {
	
	public Game m_game;
	
	[HideInInspector]
	public Animator m_anim;
	
	[HideInInspector]
	public SetText m_text;
	
	private Action m_action;
	
	private int m_time_hash = Animator.StringToHash("Clock Time");
	//private int m_fade_hash = Animator.StringToHash("Fade Text");
	//private int m_hidden_hash = Animator.StringToHash("Hidden Text");
	
	void Awake() {
		m_anim = GetComponent<Animator>();
		m_text = GetComponent<SetText>();
		//Debug.Log(anim.ToString());
	}
	
	private int get_time() {
		return m_anim.GetInteger(m_time_hash);
	}
	
	private void set_time(int time) {
		m_anim.SetInteger(m_time_hash, time);
	}
	
	public void Start_Countdown(int time, Action func) {
		Debug.Log(string.Format("start {0}", time.ToString()));
		if (time == 0) {
			func();
			return;
		}
		set_time(time + 1);
		m_action = func;
	}
	
	private void Tick_Countdown() {
		int time = get_time();
		time -= 1;
		set_time(time);
		Debug.Log(string.Format("start {0}", time.ToString()));
		m_text.Set(time);
	}
	
	private void End_Countdown() {
		Debug.Log(string.Format("end {0}", get_time().ToString()));
		//m_game.End_Countdown();
		m_action();
	}
}
