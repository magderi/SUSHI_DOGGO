using System.Collections.Generic;
using Cinemachine;
using Cinemachine.Utility;
using UnityEngine;

[AddComponentMenu("Cinemachine/CinemachineVerticalLoopPath")]
[SaveDuringPlay]
public class CinemachineVerticalLoopPath : CinemachineSmoothPath
{
    private readonly List<Vector3> waypointPositions = new List<Vector3>();
    private readonly List<Quaternion> waypointRotations = new List<Quaternion>();

    public override Quaternion EvaluateOrientation(float pos)
    {
        // �܂��͖{����EvaluateOrientation�Ɠ����悤�ɏ������Ă���
        var pathRotation = this.transform.rotation;
        if (this.m_Waypoints.Length <= 0)
        {
            return pathRotation;
        }
        var worldForward = this.EvaluateTangent(pos);
        if (worldForward.AlmostZero())
        {
            return pathRotation;
        }

        // �����Ė{����EvaluateOrientation�Ɠ��l��LookRotation�ɂ���]�����߂�
        var worldUp = pathRotation * Vector3.up;
        var baseLookRotation = Quaternion.LookRotation(worldForward, worldUp);

        // �����ŁA�{����EvaluateOrientation�����s���ĉ�]�𓾂Ă���...
        var baseRotation = base.EvaluateOrientation(pos);

        // �������烍�[�������𒊏o����
        var rollRotation = Quaternion.Inverse(baseLookRotation) * baseRotation;

        // ���ɁA���݂̃Z�O�����g�̗��[�̃C���f�b�N�X�𓾂�...
        var t = this.GetBoundingIndices(pos, out var indexA, out var indexB) % 1.0f;

        // �e�E�F�C�|�C���g�ł̎p�����X�V�����̂��A�Z�O�����g���[�̉�]��������...
        this.UpdateWaypointRotations();
        var rotation = pathRotation * Quaternion.Slerp(this.waypointRotations[indexA], this.waypointRotations[indexB], t);

        // ���̉�]���w������������g����LookRotation�ɂ���]�����߁A
        // ��������[�������Ƒg�ݍ��킹�ĕԂ�
        return Quaternion.LookRotation(worldForward, rotation * Vector3.up) * rollRotation;
    }

    // �e�E�F�C�|�C���g�ɂ������]�����߂邽�߂̃��\�b�h��p�ӂ���
    private void UpdateWaypointRotations()
    {
        var waypointCount = this.m_Waypoints.Length;
        var waypointCacheCount = this.waypointPositions.Count;
        var needsUpdate = waypointCount > waypointCacheCount;
        if (needsUpdate)
        {
            var waypointCountToAdd = waypointCount - waypointCacheCount;
            for (var i = 0; i < waypointCountToAdd; i++)
            {
                this.waypointPositions.Add(new Vector3());
                this.waypointRotations.Add(new Quaternion());
            }
        }
        else
        {
            for (var i = 0; i < waypointCount; i++)
            {
                if (this.m_Waypoints[i].position != this.waypointPositions[i])
                {
                    needsUpdate = true;
                    break;
                }
            }
        }

        if (waypointCount < waypointCacheCount)
        {
            var elementCountToRemove = waypointCacheCount - waypointCount;
            this.waypointPositions.RemoveRange(waypointCount, elementCountToRemove);
            this.waypointRotations.RemoveRange(waypointCount, elementCountToRemove);
        }

        if ((waypointCount == 0) || !needsUpdate)
        {
            return;
        }

        // �ŏ��̃E�F�C�|�C���g�͖{����CinemachineSmoothPath�Ɠ��l��
        // Vector3.up��������Ƃ�����]���̗p��...
        var inversePathRotation = Quaternion.Inverse(this.transform.rotation);
        var previousTangent = inversePathRotation * this.EvaluateTangent(0);
        this.waypointPositions[0] = this.m_Waypoints[0].position;
        this.waypointRotations[0] = Quaternion.LookRotation(previousTangent, Vector3.up);
        for (var i = 1; i < waypointCount; i++)
        {
            // �ȍ~�̃E�F�C�|�C���g�͈�O�̃E�F�C�|�C���g�̉�]�ɑ΂���
            // �O�����̕ω����狁�߂�������]������������]���̗p����
            var tangent = inversePathRotation * this.EvaluateTangent(i);
            this.waypointPositions[i] = this.m_Waypoints[i].position;
            this.waypointRotations[i] = Quaternion.FromToRotation(previousTangent, tangent) * this.waypointRotations[i - 1];
            previousTangent = tangent;
        }
    }

    // CinemachineSmoothPath��GetBoundingIndices�����̂܂܎����Ă���
    private float GetBoundingIndices(float pos, out int indexA, out int indexB)
    {
        pos = this.StandardizePos(pos);
        var numWaypoints = this.m_Waypoints.Length;
        if (numWaypoints < 2)
        {
            indexA = indexB = 0;
        }
        else
        {
            indexA = Mathf.FloorToInt(pos);
            if (indexA >= numWaypoints)
            {
                pos -= this.MaxPos;
                indexA = 0;
            }

            indexB = indexA + 1;
            if (indexB == numWaypoints)
            {
                if (this.Looped)
                {
                    indexB = 0;
                }
                else
                {
                    --indexB;
                    --indexA;
                }
            }
        }

        return pos;
    }


}