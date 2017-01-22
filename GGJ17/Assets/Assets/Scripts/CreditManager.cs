using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CreditManager : MonoBehaviour {

    public void ExitButton()
    {
        SceneManager.LoadScene("StartScene");
    }

}
