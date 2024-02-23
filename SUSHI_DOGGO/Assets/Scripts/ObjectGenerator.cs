using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    /// <summary>
    /// �^�C�g���V�[���̉_�𐶐�����X�N���v�g�ł�
    /// </summary>


    // �ȉ��Q��
    public GameObject   objectToGenerate;           // ��������I�u�W�F�N�g
    public float        generationInterval = 3f;    // �����Ԋu
    public float        objectLifetime     = 10f;   // �I�u�W�F�N�g�̎���

    private float timer = 0f;

    void Update()
    {
        // �^�C�}�[���X�V
        timer += Time.deltaTime;

        // ��莞�Ԃ��ƂɃI�u�W�F�N�g�𐶐�
        if (timer >= generationInterval)
        {
            GenerateObject();
            timer = 0f; // �^�C�}�[�����Z�b�g
        }

        // �I�u�W�F�N�g�̎�����������j��
        if (timer >= objectLifetime)
        {
            DestroyGeneratedObject();
        }
    }

    void GenerateObject()
    {
        // �I�u�W�F�N�g�𐶐�
        GameObject newObject = Instantiate(objectToGenerate, transform.position, Quaternion.identity);

        // �������ꂽ�I�u�W�F�N�g�����̃X�N���v�g���A�^�b�`����Ă���Q�[���I�u�W�F�N�g�̎q�ɂ���
        newObject.transform.parent = transform;
    }

    void DestroyGeneratedObject()
    {
        // �q�I�u�W�F�N�g��j��
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // �^�C�}�[�����Z�b�g
        timer = 0f;
    }
}
