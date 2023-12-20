using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
/// <summary>
/// ���i���̃W�����v�Ɋւ��鏈�������֐�
/// �A�j���[�V�����̃g���K�[��DogMoving�X�N���v�g�Ŕ��΂��Ă܂�
/// </summary>
public class SushiJump : MonoBehaviour
{
  //  public float jumpPower;
    private Rigidbody rb;

    // �������n�̐���r���̖��c�A���V��ł͎g���ĂȂ�
    public bool isSalmonJumping = false;
    public bool isMaguroJumping = false;

    // �A�j���[�V�����͂����ł�����
    [SerializeField]
    private DogMoving _dogMoving;

    // �������n�̐���r���̖��c�A���V��ł͎g���ĂȂ�
    [SerializeField]
    private SalmonJumpJudgement _salmonJumpJudgement;
    [SerializeField]
    private MaguroJumpJudgement _maguroJumpJudgement;

    [SerializeField]
    private BoxCollider _boxCollider;

    // �W�����v��������FX�Đ�
    [SerializeField]
    private ParticleSystem _jumpSalmonParticle;
    [SerializeField]
    private ParticleSystem _jumpMaguroParticle;

    [SerializeField]
    private SE_Manager _seManager;
    void Start()
    {
        _jumpSalmonParticle.Stop();

        _jumpMaguroParticle.Stop();

        rb = GetComponent<Rigidbody>();
    }
    // �T�[������FX�Đ���SE�̍Đ�
    public void SalmonDogJumpParticle()
    {
        _jumpSalmonParticle.Play();

        // FX��SE
        _seManager.Play(2);

        // ����SE
        _seManager.Play(4);
    }

    public void MaguroDogJumpParticle()
    {
        _jumpMaguroParticle.Play();

        // FX��SE
        _seManager.Play(2);

        // ����SE
        _seManager.Play(3);
    }

    async void Update()
    {
        // �T�[�����W�����v
        if (Input.GetKeyDown(KeyCode.LeftShift) && isSalmonJumping)
        {

            // DogMoving�̊֐����g�p
            _dogMoving.SalmonDogJumpMotion();

            // rb.velocity = Vector3.up * jumpPower;
            //await UniTask.Delay(TimeSpan.FromSeconds(1));

            // �ȉ��W�����v�̃N�[���^�C������
            isSalmonJumping = false;

            
            _salmonJumpJudgement._jumpCoolTime = false;

            await UniTask.Delay(TimeSpan.FromSeconds(1.5));

            _salmonJumpJudgement._jumpCoolTime = true;


            Debug.Log("test");
        }

        // �}�O���W�����v
        if (Input.GetKeyDown(KeyCode.RightShift) && isMaguroJumping)
        {

            // DogMoving�̊֐����g�p
            _dogMoving.MaguroDogJumpMotion();

            // rb.velocity = Vector3.up * jumpPower;
            //await UniTask.Delay(TimeSpan.FromSeconds(1));
            // �ȉ��W�����v�̃N�[���^�C������
            isMaguroJumping = false;

            
            _maguroJumpJudgement._jumpCoolTime = false;

            await UniTask.Delay(TimeSpan.FromSeconds(1.5));

            _maguroJumpJudgement._jumpCoolTime = true;


            Debug.Log("test");
        }
    }   

}
