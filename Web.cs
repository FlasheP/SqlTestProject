using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(GetRequest("http://localhost/SqlTestProject/GetDate.php"));
        //StartCoroutine(GetRequest("http://localhost/SqlTestProject/GetUsers.php"));

        //StartCoroutine(Login("testuser1","1234"));

        //StartCoroutine(RegisterUser("testuser3","123456"));
    }/*

    public void ShowUserItems()
    {
        StartCoroutine(GetItemsIDs(GameManager.Instance.playerInfo.PlayerID));
    }*/
    public IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }
    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/SqlTestProject/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Login.php" + ": Error: " + www.error);
            }
            else //연결 성공시
            {
                Debug.Log("Login.php" + ":\nReceived: " + www.downloadHandler.text);
                if (www.downloadHandler.text == "wrong_password" || www.downloadHandler.text == "cant_find_username")
                    yield break;
                else
                {
                    GameManager.Instance.playerInfo.SetCredentials(username, password);
                    GameManager.Instance.playerInfo.SetID(www.downloadHandler.text);
                }
            }
        }
    }
    public IEnumerator RegisterUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/SqlTestProject/RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Login.php" + ": Error: " + www.error);
            }
            else //연결 성공시
            {
                Debug.Log("Login.php" + ":\nReceived: " + www.downloadHandler.text);
            }
        }
    }
    public IEnumerator GetItemsIDs(string userID, Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userID);

        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/SqlTestProject/GetItemsIDs.php", form))
        {
            yield return webRequest.SendWebRequest();
            string page = "GetItemsIDs.php";

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(page + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(page + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(page + ":\nReceived: " + webRequest.downloadHandler.text);
                    string jsonArray = webRequest.downloadHandler.text;
                    callback(jsonArray);
                    break;
            }
        }
    }
    public IEnumerator GetItem(string itemID, Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);

        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/SqlTestProject/GetItem.php", form))
        {
            yield return webRequest.SendWebRequest();
            string page = "GetItem.php";

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(page + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(page + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(page + ":\nReceived: " + webRequest.downloadHandler.text);
                    string jsonArray = webRequest.downloadHandler.text;
                    callback(jsonArray);
                    break;
            }
        }
    }
    public IEnumerator GetItemIcon(string itemID, Action<byte[]> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);

        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/SqlTestProject/GetItemIcon.php", form))
        {
            yield return webRequest.SendWebRequest();
            string page = "GetItem.php";

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(page + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(page + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log("Downloading Item Icon NO." + itemID);
                    byte[] bytes = webRequest.downloadHandler.data;
                    callback(bytes);
                    break;
            }
        }
    }
    public IEnumerator SellItem(string itemID, string userID)
    {
        WWWForm form = new WWWForm();
        form.AddField("itemID", itemID);
        form.AddField("userID", userID);

        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/SqlTestProject/SellItem.php", form))
        {
            yield return webRequest.SendWebRequest();
            string page = "SellItem.php";

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(page + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(page + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(page + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }
}
