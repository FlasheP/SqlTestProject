using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public string PlayerID { get; private set; } = "0";
    string PlayerName;
    string PlayerPassword;
    string Level;
    string Coins;

    public void SetCredentials(string username, string userpassword)
    {
        PlayerName = username;
        PlayerPassword = userpassword;
    }
    public void SetID(string id)
    {
        PlayerID = id;
    }
}
