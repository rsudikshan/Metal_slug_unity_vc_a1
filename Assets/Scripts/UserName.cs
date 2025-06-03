using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserName : MonoBehaviour
{
    public static string playernamestr;

    public Text playername;

    private void Start()
    {
        playername.text = "Welcome : "  + PlayerPrefs.GetString("username");
    }
}
