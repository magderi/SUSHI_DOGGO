using UnityEngine;

public class LeftMovement : MonoBehaviour
{
    /// <summary>
    /// �^�C�g���V�[���̉_���ړ�������X�N���v�g�ł�
    /// </summary>


    // �ȉ��Q��
    public float    speed       = 5f;   // �ړ����x
    public float    destroyTime = 10f;  // �j��܂ł̎��ԁi�b�j

    void Update()
    {
        // ���Ɉړ�����x�N�g�����쐬
        Vector3 moveDirection = Vector3.left;

        // �t���[�����ƂɃI�u�W�F�N�g���ړ�������
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // destroyTime�b��Ɏ�����j��
        destroyTime -= Time.deltaTime;
        if (destroyTime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
