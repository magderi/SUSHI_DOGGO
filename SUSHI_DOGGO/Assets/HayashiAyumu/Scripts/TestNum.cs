using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNum : MonoBehaviour
{
    private PlayerInputManager inputManager;
    public int _playerNum;
    void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
        //_playerNum = inputManager._playerNum;
    }

    private void Update()
    {
        //_playerNum = inputManager._playerNum;
    }
}
