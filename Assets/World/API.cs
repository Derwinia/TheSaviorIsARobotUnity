using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class API : MonoBehaviour
{
    public Text nameAPI;
    public Text sizeAPI;
    public Text co2API;

    private string apiUrl = "https://localhost:7165/api/World/1";
    private GetWorldAPI getWorldAPI;

    private void Start()
    {
        //CallAPI();
    }
    public void CallAPI()
    {
        StartCoroutine(GetRequest(apiUrl, LoadJsonDataCallBack));
    }

    private IEnumerator GetRequest(string url, Action<string> callback)
    {
        string response;

        UnityWebRequest www = UnityWebRequest.Get(url);
        www.downloadHandler = new DownloadHandlerBuffer();

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ProtocolError)
        {
            response = null;
        }
        else if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            response = null;
        }
        else
        {
            response = www.downloadHandler.text;
        }
        callback(response);
    }

    private void LoadJsonDataCallBack(string res)
    {
        if (res != null)
        {
            getWorldAPI = JsonUtility.FromJson<GetWorldAPI>(res);

            nameAPI.text = getWorldAPI.name;
            sizeAPI.text = getWorldAPI.size.ToString();
            co2API.text = getWorldAPI.co2.ToString();
            Debug.Log(nameAPI.text);
        }
        else
        {
            Debug.Log("erreur api");
        }
    }

    private struct GetWorldAPI
    {
        public string name;
        public int size;
        public int co2;
    }
}