using System;
using MySql.Data.MySqlClient;
using UnityEngine;

public class test : MonoBehaviour
{
	

	private void Start() {
		Debug.Log("Start: " + ConnectionTest());
		
	}

	public bool ConnectionTest() {

		string connection = "Server=localhost;Database=hello;userid=root;password=park;";

		try {
			using (MySqlConnection conn = new MySqlConnection(connection)) {
				conn.Open();
			}
			return true;
		}
		catch (Exception e) {
			Debug.Log(e);
			return false;
		}
	}
}
