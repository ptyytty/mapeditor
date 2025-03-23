using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

public class BattleManager : MonoBehaviour
{
    //싱글톤 패턴 생성
    public static BattleManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);  // 중복 생성 방지
        }
    }

    void Start()
    {
        GetJobInfo(1);
    }

    private string connectionString = "Server = 127.0.0.1; database = hoi; UID = root; pwd = park";

    public void GetJobInfo(int jobId){
        string query = "Select * FROM job";

        try{
            using (MySqlConnection connection = new MySqlConnection(connectionString)){
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read()){
                    int idJob = Convert.ToInt32(reader["id_job"]);
                    string name_job = reader["name_job"].ToString();
                    int hp = Convert.ToInt32(reader["hp"]);
                    int def = Convert.ToInt32(reader["def"]);
                    int res = Convert.ToInt32(reader["res"]);
                    int spd = Convert.ToInt32(reader["spd"]);
                    int hit = Convert.ToInt32(reader["hit"]);

                    Debug.Log($"Job ID: {idJob}");
                    Debug.Log($"Job Name: {name_job}");
                    Debug.Log($"Job HP: {hp}");
                    Debug.Log($"Job DEF: {def}");
                }else{
                    Debug.Log("No job found");
                }
            }
        }catch (Exception ex){
            Debug.LogError("Error: " + ex.Message);
        }
    }
}
