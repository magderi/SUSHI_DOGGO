using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    //public int _playerNum = 0;
    // �v���C���[�������Ɏ󂯎��ʒm
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        print($"�v���C���[#{playerInput.user.index}�������I");
       // _playerNum = playerInput.user.index;
    }

    // �v���C���[�ގ����Ɏ󂯎��ʒm
    public void OnPlayerLeft(PlayerInput playerInput)
    {
        print($"�v���C���[#{playerInput.user.index}���ގ��I");
    }
}
