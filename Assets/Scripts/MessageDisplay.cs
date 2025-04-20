using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageDisplay : MonoBehaviour
{
    public Text messageText;
    public float displayTime = 5f;

    void Start()
    {
        ShowMessage("Collect all items to save your zombie friends and turn them back to human!");
    }

    public void ShowMessage(string message)
    {
        StartCoroutine(DisplayMessage(message));
    }

    IEnumerator DisplayMessage(string message)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        messageText.gameObject.SetActive(false);
    }
}
