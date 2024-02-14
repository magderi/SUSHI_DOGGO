using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    private ISPlayerMove ISPlayerMove;

    [SerializeField]
    private Sprite Tutorial1;
    [SerializeField]
    private Sprite Tutorial2;

    [SerializeField]
    private GameObject Salmon;
    [SerializeField]
    private GameObject Maguro;


    private Image tutorialImage;

    // Start is called before the first frame update
    void Start()
    {
        ISPlayerMove = new ISPlayerMove();
        ISPlayerMove.Enable();

        Time.timeScale = 0f;
        tutorialImage = GetComponent<Image>();

        bool isActive = this.gameObject.activeSelf;
        if(isActive == false)
            this.gameObject.SetActive(true);
        tutorialImage.sprite = Tutorial1;

        tutorialImage.color = Color.white;

        Salmon.SetActive(false);
        Maguro.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TutorialNext();
    }

    private void TutorialNext()
    {
        if (ISPlayerMove.UI.GameStart.WasPressedThisFrame())
        {
            if (tutorialImage.sprite == Tutorial1)
            {
                tutorialImage.sprite = Tutorial2;
            }
            else if (tutorialImage.sprite == Tutorial2)
            {
                this.gameObject.SetActive(false);

                Salmon.SetActive(true);
                Maguro.SetActive(true);

                Time.timeScale = 1f;
            }
        }
        else if (ISPlayerMove.UI.Skip.WasPressedThisFrame())
        {
            this.gameObject.SetActive(false);

            Salmon.SetActive(true);
            Maguro.SetActive(true);

            Time.timeScale = 1f;
        }
    }
}
