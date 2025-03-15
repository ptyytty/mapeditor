using System;
using MySql.Data.MySqlClient;
using UnityEngine;

public class MariaDB_test : MonoBehaviour
{
    private string connectionString = "Server = localhost; Database = game_db; User ID = root; Password = park;";
    private MySqlConnection connection;

    void Start()
    {
        connection = new MySqlConnection(connectionString);
        OpenConnection();
    }

    void OpenConnection()
    {
        try
        {
            connection.Open();
            Debug.Log("MariaDD ���� ����");
        }
        catch (Exception e)
        {
            Debug.Log("���� ����: " + e.Message);
        }
    }

    void SavePlayerData(string playerName, int experience)
    {
        string query = "INSERT INTO players (player_name, experience) VALUES (@playerName, @experience)";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@playerName", playerName);
        cmd.Parameters.AddWithValue("@experience", experience);

        try
        {
            cmd.ExecuteNonQuery();
            Debug.Log("���� �Ϸ�");
        }
        catch(Exception e)
        {
            Debug.Log("����");
        }
    }

    void LoadPlayerData()
    {
        string query = "SELECT * FROM players";
        MySqlCommand cmd = new MySqlCommand( query, connection);

        try
        {
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int playerID = reader.GetInt32("player_id");
                string playerName = reader.GetString("player_name");
                int experience = reader.GetInt32("experience");
                Debug.Log($"Player ID: {playerID}, Name: {playerName}, Experience: {experience}");
            }
            reader.Close();
        }
        catch( Exception e )
        {
            Debug.Log("�ҷ����� ����: " + e.Message);
        }
    }

    private void OnApplicationQuit()
    {
        if(connection != null && connection.State == System.Data.ConnectionState.Open)
        {
            connection.Close();
            Debug.Log("���� ����");
        }
    }

}
