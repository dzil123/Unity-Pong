using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetText : MonoBehaviour {
	
	public string m_format_string = "{0}"; // create string, put '{0}' where you want to replace with param
	public string m_default_string = "";
	
	private Text m_text;
	
	void Start () {
		m_text = GetComponent<Text>();
		Set(m_default_string);
	}
	
	public void Set ( int number ) {
		m_text.text = string.Format(m_format_string, number.ToString());
	}
	
	public void Set ( string text ) {
		m_text.text = string.Format(m_format_string, text);
	}
}
