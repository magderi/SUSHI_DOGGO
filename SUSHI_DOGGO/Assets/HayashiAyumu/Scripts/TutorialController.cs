using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    //  InputSystem�擾
    private ISPlayerMove ISPlayerMove;

    [SerializeField]
    private Sprite Tutorial1;               //  �`���[�g���A��1��Sprite
    [SerializeField]
    private Sprite Tutorial2;               //  �`���[�g���A��2��Sprite
    [SerializeField]
    private TMP_Text StartCountdownText;    //  �J�E���g�_�E���̃e�L�X�g
    [SerializeField]
    private Image tutorialImage;            //  Sprite������Image

    [SerializeField]
    private GameObject StartTextGameObject;
    [SerializeField]
    private GameObject tutorialCanvas;
    [SerializeField]
    private GameObject salmon;
    [SerializeField]
    private GameObject maguro;
    private StandMoving maguroStandMoving;
    private StandMoving salmonStandMoving;

    //  �`���[�g���A����i�߂��邩�ǂ���
    private bool canTutorialNext = true;
    //  �J�E���g�_�E������
    private int countdownSeconds = 3;

    // Start is called before the first frame update
    void Start()
    {
        ISPlayerMove = new ISPlayerMove();
        ISPlayerMove.Enable();

        Time.timeScale = 0f;

        bool isActive = tutorialCanvas.activeSelf;
        if(isActive == false)
            tutorialCanvas.SetActive(true);
        tutorialImage.sprite = Tutorial1;

        tutorialImage.color = Color.white;

        //salmon.SetActive(false);
        //maguro.SetActive(false);
        salmonStandMoving = salmon.GetComponent<StandMoving>();
        maguroStandMoving = maguro.GetComponent<StandMoving>();
        salmonStandMoving.enabled = false;
        maguroStandMoving.enabled = false;


        //  �J�n��b�͑��삪�����Ȃ��悤��
        StartCoroutine(WaitNextCor());
    }

    // Update is called once per frame
    void Update()
    {
        TutorialNext();
    }

    private void TutorialNext()
    {
        //  ��ɃR���[�`�����g���Ă���Α�����󂯕t���Ȃ�
        if (!canTutorialNext)   return;

        if (ISPlayerMove.UI.GameStart.WasPressedThisFrame())
        {
            if (tutorialImage.sprite == Tutorial1)
            {
                tutorialImage.sprite = Tutorial2;
                //  ���̏����܂ň�b�ҋ@
                StartCoroutine(WaitNextCor());
            }
            else if (tutorialImage.sprite == Tutorial2)
            {
                StartCoroutine(WaitStartCor());
            }
        }
        else if (ISPlayerMove.UI.Skip.WasPressedThisFrame())
        {
            StartCoroutine(WaitStartCor());
        }
    }

    /// <summary>
    /// ��b�҂����̃R���[�`��
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitNextCor()
    {
        canTutorialNext = false;
        //  TimeScale�ɉe������Ȃ��P�b�ҋ@
        yield return new WaitForSecondsRealtime(1.0f);
        canTutorialNext = true;
    }

    /// <summary>
    /// �X�^�[�g���̊J�n�J�E���g�_�E��
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitStartCor()
    {
        //  TutorialCanvas�𖳌���
        tutorialCanvas.SetActive(false);

        //  �J�E���g�_�E�����I���܂ŌJ��Ԃ�
        for (int i = countdownSeconds; i >= 0; i--)
        {
            //  �J�E���g�_�E����Text�Ƃ��ĕ\��
            if (i == 0)
            {
                StartCountdownText.SetText("GO");
            }
            else
            {
                string str = i.ToString();
                StartCountdownText.SetText(str);
            }
            //  1�b���ƂɌJ��Ԃ��悤��
            yield return new WaitForSecondsRealtime(1.0f);
        }

        //  ���i�������̑����L����
        salmonStandMoving.enabled = true;
        maguroStandMoving.enabled = true;
        //  �Q�[���̎��Ԃ�i�߂�
        Time.timeScale = 1f;
        //  �e�L�X�g�̃I�u�W�F�N�g�𖳌���
        StartTextGameObject.SetActive(false);

    }
}
