//  バグってるから使わんほうがいいやつ(ほぼ DogMoving のコピペ)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandMoving : MonoBehaviour
{
    private bool isCurving = false;
    private bool isRightMoving = false;
    private bool isLeftMoving = false;

    private int _laneNamber;

    private DogStatus dogStatus;
    protected Rigidbody standRB;

    private Vector3 standPos;
    // Start is called before the first frame update
    void Start()
    {
        standRB = GetComponent<Rigidbody>();
        dogStatus = GetComponent<DogStatus>();
        standPos = transform.localPosition;

        _laneNamber = 3;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        if (isCurving) CurveMoveLimit(dogStatus._maxMoveLimit);
    }

    /// <summary>
    /// 寿司犬の左右の移動処理
    /// </summary>
    private void PlayerMove()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            isLeftMoving = true;
            _laneNamber--;
        }
        else if (Input.GetKeyUp(KeyCode.A))
            isLeftMoving = false;

        if (Input.GetKeyDown(KeyCode.D))
        {
            isRightMoving = true;
            _laneNamber++;
        }
        else if (Input.GetKeyUp(KeyCode.D))
            isRightMoving = false;

        if (isLeftMoving) standRB.AddForce(Vector3.left * dogStatus._movePower);
        if (isRightMoving) standRB.AddForce(Vector3.right * dogStatus._movePower);
    }

    private void CurveMoveLimit(float MaxMoveLimit)
    {
        float _currentMoveSpeed = standRB.velocity.z;
        if (_currentMoveSpeed > MaxMoveLimit)
        {
            _currentMoveSpeed /= MaxMoveLimit;
            standRB.velocity = new Vector3(
                standRB.velocity.x,
                standRB.velocity.y,
                _currentMoveSpeed);
        }
    }
}
