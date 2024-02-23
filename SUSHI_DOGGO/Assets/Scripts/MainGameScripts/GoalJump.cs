using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalJump : MonoBehaviour
{

    /// <summary>
    /// ���i���B���W�����v���ĖڕW�n�_�Ɉړ�����X�N���v�g�ł�
    /// </summary>


    //    [SerializeField]
    // private BoxCollider _goalManager;

    // [SerializeField]
    // private DogMoving _dogMoving;
    // �S�[���J�����N���p
    [SerializeField]
    private GameObject _goalCamera;

    public Transform    target;     �@ // �ڕW�n�_��Transform
    public float        height = 5f; �@// �������̍���

    private float       startTime;
    private float       journeyLength;
    private Vector3     startPos;

    public float        speed = 2.0f;  // �ړ����x

    // �A�j���[�^�Q��
    [SerializeField]
    private Animator  _maguroanimator;

    [SerializeField]
    private Animator  _salmonanimator;

    [SerializeField]
    private Animator  _maguroStandanimator;

    [SerializeField]
    private Animator  _salmonStandanimator;

    // ������������Bool
    private bool      _goalEnd = false;

    void Start()
    {
        // �����ʒu�̐ݒ�
        startPos = transform.position;

        // �ړ��̊J�n����
        startTime = Time.time;

        // �����ʒu����ڕW�n�_�܂ł̋���
        journeyLength = Vector3.Distance(startPos, target.position);
    }

    void Update()
    {
        if(_goalEnd == false)
        {
            Jump();
        }
        else
        {
            GoalEnd();
        }
    }
    // �ڕW�n�_�ɓ���������
    public void GoalEnd()
    {
        _goalEnd = true;

        _salmonanimator.SetBool("GoalJump", false);
        _maguroanimator.SetBool("GoalJump", false);
    }

    // �W�����v������
    public void Jump()
    {
        _salmonStandanimator.SetTrigger("StandUp");
        _maguroStandanimator.SetTrigger("StandUp");

        _goalCamera.SetActive(true);

        _salmonanimator.SetBool("GoalJump", true);
        _maguroanimator.SetBool("GoalJump", true);

        // ���݂̌o�ߎ���
        float distCovered = (Time.time - startTime) * speed;

        // �i�����i0����1�͈̔́j
        float fracJourney = distCovered / journeyLength;

        // �������̌v�Z
        Vector3 currentPos = Vector3.Lerp(startPos, target.position, fracJourney);
        currentPos.y += Mathf.Sin(fracJourney * Mathf.PI) * height;

        // �I�u�W�F�N�g�̈ړ�
        transform.position = currentPos;


        // �ڕW�n�_�ɓ��B������X�N���v�g�𖳌��ɂ���
        if (fracJourney >= 1.0f)
        {
            GoalEnd();
            enabled = false;
        }
    }
}
