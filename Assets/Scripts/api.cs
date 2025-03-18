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
				return true;  // 인증서 오류 무시
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
		// 예시: JSON 파싱 (Unity에서 JSON 처리를 위한 방법)
		// 이 부분은 실제 JSON 구조에 맞게 파싱해야 합니다.
		// 예를 들어, JSON 배열로 반환된 경우
		Debug.Log("Handling player data: " + jsonResponse);
		Player[] players = JsonHelper.FromJson<Player>(jsonResponse);
		foreach (var player in players) {
			Debug.Log($"Player ID: {player.id_job}, Ability: {player.ability}");
		}
	}
}

// JSON 데이터 구조에 맞는 C# 클래스 (예시)
[System.Serializable]
public class Player
{
	public string id_job;
	public string ability;
}
