using System;
using MySql.Data.MySqlClient;
using UnityEngine;

public class test : MonoBehaviour
{
	

	private void Start() {
		Debug.Log("Start: " + ConnectionTest());
		
	}

	public bool ConnectionTest() {

		string connection = "Server=192.168.45.214;Database=job;userid=root;password=park;";

		try {
			using (MySqlConnection conn = new MySqlConnection(connection)) {
				conn.Open();
				string query = "Select * from ability";
				using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
					using (MySqlDataReader reader = cmd.ExecuteReader()) {
						while (reader.Read()) {
							int id_job = reader.GetInt32("id_job");
							string name = reader.GetString("id_name");
							int hp = reader.GetInt32("hp");
							int atk = reader.GetInt32("atk");
							int def = reader.GetInt32("def");
							int reg = reader.GetInt32("reg");
							int mag = reader.GetInt32("mag");
							int avoid = reader.GetInt32("avoid");
							int hit = reader.GetInt32("hit");

							Debug.Log($"id_code: {id_job}, Name: {name}, hp: {hp}, atk: {atk}, def: {def}, reg: {reg}, mag: {mag}, avoid: {avoid}, hit: {hit}");
						}
					}
				}
			}
			return true;
		}
		catch (Exception e) {
			Debug.Log(e);
			return false;
		}
	}
}
