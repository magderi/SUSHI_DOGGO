using UnityEngine;
using UnityEngine.UI;

public class HideImagesAndTextOnKeyPress : MonoBehaviour
{
    public Image[] imagesToShow;
    public Text scoreText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Invoke("HideImagesAndText", 2f);
        }
    }

    void HideImagesAndText()
    {
        foreach (Image image in imagesToShow)
        {
            image.gameObject.SetActive(false);
        }
        scoreText.gameObject.SetActive(false);
        scoreText.text = "0";
    }
}
