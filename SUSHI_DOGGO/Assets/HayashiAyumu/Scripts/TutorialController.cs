using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    //  InputSystem�擾
    private ISPlayerMove ISPlayerMove;

    //  �`���[�g���A���̃L�����o�X�֘A
    [SerializeField]
    private GameObject tutorialCanvas;      //  �`���[�g���A���̃L�����o�X
    [SerializeField]
    private Sprite Tutorial1;               //  �`���[�g���A��1��Sprite
    [SerializeField]
    private Sprite Tutorial2;               //  �`���[�g���A��2��Sprite
    [SerializeField]
    private Image tutorialImage;            //  Sprite������Image


    //  �Q�[���J�n���O�̃J�E���g�_�E���e�L�X�g�֘A
    [SerializeField]
    private TMP_Text StartCountdownText;    //  �J�E���g�_�E���̃e�L�X�g
    [SerializeField]
    private GameObject StartTextGameObject; //  �J�E���g�_�E���e�L�X�g�̃Q�[���I�u�W�F�N�g
    

    //  ���i�������̊i�[�ꏊ
    [SerializeField]
    private GameObject salmon;
    [SerializeField]
    private GameObject maguro;

    //  ���i���̈ړ��X�N���v�g
    private StandMoving maguroStandMoving;
    private StandMoving salmonStandMoving;


    //  �`���[�g���A����i�߂��邩�ǂ���
    private bool canTutorialNext = true;
    //  �J�E���g�_�E������
    private int countdownSeconds = 3;

    // Start is called before the first frame update
    void Start()
    {
        //  InputSystem��L����
        ISPlayerMove = new ISPlayerMove();
        ISPlayerMove.Enable();
        //  �Q�[�������Ԓ�~
        Time.timeScale = 0f;


        //  �`���[�g���A����\��
        bool isActive = tutorialCanvas.activeSelf;
        if(!isActive)
            tutorialCanvas.SetActive(true);
        tutorialImage.sprite = Tutorial1;
        tutorialImage.color = Color.white;


        //  ���i�������̑���𖳌���
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

    /// <summary>
    /// �`���[�g���A�����̑����t����
    /// </summary>
    private void TutorialNext()
    {
        //  ��ɃR���[�`�����g���Ă���Α�����󂯕t���Ȃ�
        if (!canTutorialNext)   return;

        //  �i�߂�{�^�����������Ƃ�
        if (ISPlayerMove.UI.GameStart.WasPressedThisFrame())
        {
            //  �`���[�g���A�����ꖇ�ڂ̎�
            if (tutorialImage.sprite == Tutorial1)
            {
                //  �`���[�g���A�������̊G��
                tutorialImage.sprite = Tutorial2;
                //  ���̏����܂ň�b�ҋ@
                StartCoroutine(WaitNextCor());
            }
            //  �`���[�g���A�����񖇖ڂ̎�
            else if (tutorialImage.sprite == Tutorial2)
            {
                StartCoroutine(WaitStartCor());
            }
        }
        //  �X�L�b�v�{�^�����������Ƃ�
        else if (ISPlayerMove.UI.Skip.WasPressedThisFrame())
        {
            StartCoroutine(WaitStartCor());
        }
    }

    /// <summary>
    /// ��b��ɑ���\�ɂ���R���[�`��
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
