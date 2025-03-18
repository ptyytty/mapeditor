using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Networking;

public class api : MonoBehaviour
{
	private void Start() {
		ServicePointManager.ServerCertificateValidationCallback =
			delegate (object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
				return true;  // ������ ���� ����
			};

	}
	private string apiUrl = "http://192.168.45.214:3000/player/";

    public void GetPlayerData(string playerId) {
		Debug.Log("Getting player data for ID: " + playerId);
		StartCoroutine(GetPlayerDataCoroutine(playerId));
    }

    private IEnumerator GetPlayerDataCoroutine(string playerId) {
        string url = apiUrl + playerId;

        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success) {
            Debug.LogError("Request failed: " + request.error);
        } else {
            Debug.Log("Response: " + request.downloadHandler.text);
			string jsonResponse = request.downloadHandler.text;
			HandlePlayerData(jsonResponse);
        }
    }
	private void HandlePlayerData(string jsonResponse) {
		// ����: JSON �Ľ� (Unity���� JSON ó���� ���� ���)
		// �� �κ��� ���� JSON ������ �°� �Ľ��ؾ� �մϴ�.
		// ���� ���, JSON �迭�� ��ȯ�� ���
		Debug.Log("Handling player data: " + jsonResponse);
		Player[] players = JsonHelper.FromJson<Player>(jsonResponse);
		foreach (var player in players) {
			Debug.Log($"Player ID: {player.id_job}, Ability: {player.ability}");
		}
	}
}

// JSON ������ ������ �´� C# Ŭ���� (����)
[System.Serializable]
public class Player
{
	public string id_job;
	public string ability;
}
