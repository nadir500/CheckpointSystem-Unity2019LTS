using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class IKManager : MonoBehaviour
{
    private Vector3 _rightFootPosition, _leftFootPosition, _leftFootIKPosition, _rightFootIKPosition;
    private Quaternion _leftFootIKRotation, _rightFootIKRotation, _headIKRotation;
    private float _lastPelvisPositionY, _lastRightFootPositionY, _lastLeftFootPositionY;

    [Header("----Feet Grounder----")] public bool enableFeet = true;

    [Range(0, 2)] [SerializeField] private float heightFromGraoundRaycast = 1.14f;
    [Range(0, 2)] [SerializeField] private float raycastDownDistance = 1.5f;
    [SerializeField] private LayerMask _environmentLayer;
    [SerializeField] private float _pelvisOffset = 0.0f;
    [Range(0, 1)] [SerializeField] private float _pelvisUpAndDownSpeed = 0.28f;
    [Range(0, 1)] [SerializeField] private float _feetToIKPositionSpeed = 0.5f;
    [SerializeField] private Animator _animator;
    public string leftFootAnimationVariableName = "LeftFootCurve";
    public string rightFootAnimationVariableName = "RightFootCurve";
    public string headAnimationVariableName = "HeadCurve";

    [Header("----Head Position IK----")] [SerializeField]
    private bool _headFollow;

    [SerializeField] private Transform _headTargetToLookAt;
    // [Header("---Chest Position IK---")]
    // [SerializeField] private Transform _chestTargetIK; 
    //

    public bool useProIKFeatures = false;
    public bool showSolverDebugMode = true;

    // [SerializeField] private Transform leftfootTR; 

    #region FeetGround

    private void FixedUpdate()
    {
        if (!enableFeet)
        {
            return;
        }

        if (_animator == null)
        {
            return;
        }

        AdjustFeetTarget(ref _rightFootPosition, HumanBodyBones.RightFoot);
        AdjustFeetTarget(ref _leftFootPosition, HumanBodyBones.LeftFoot);

        //find and ray cast to the ground to detect surface 
        FeetPositionSolver(_rightFootPosition, ref _rightFootIKPosition,
            ref _rightFootIKRotation); //handle solver for right foot
        FeetPositionSolver(_leftFootPosition, ref _leftFootIKPosition, ref _leftFootIKRotation); //handle left foot 

        /*
         * Head rotation IK Solver
         * Calculate the angle between a random point and the head, make strict rotation based on it
         */
        AdjustHeadTarget(ref _headIKRotation, HumanBodyBones.Head);

        //head solver for finding angle 
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!_headFollow)
        {
            return;
        }

        if (!enableFeet)
        {
            return;
        }

        if (_animator == null)
        {
            return;
        }

        MovePelvisHeight();

        //right foot ik position and rotation -- utilise pro ik feature here 
        _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
        if (useProIKFeatures)
        {
            // _animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot,  _animator.GetFloat(leftFootAnimationVariableName));
            _animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, _animator.GetFloat(rightFootAnimationVariableName));
        }

        MoveFeetToIKPoint(AvatarIKGoal.RightFoot, _rightFootIKPosition, _rightFootIKRotation,
            ref _lastRightFootPositionY);
        //left foot 
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        if (useProIKFeatures)
        {
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, _animator.GetFloat(leftFootAnimationVariableName));
            // _animator.SetIKRotationWeight(AvatarIKGoal.RightFoot,  _animator.GetFloat(rightFootAnimationVariableName));
        }

        MoveFeetToIKPoint(AvatarIKGoal.LeftFoot, _leftFootIKPosition, _leftFootIKRotation, ref _lastLeftFootPositionY);

        /*
         * Head IK 
         */
        if (useProIKFeatures)
        {
            _animator.SetLookAtWeight(_animator.GetFloat(headAnimationVariableName));
        }
        else
        {
            _animator.SetLookAtWeight(1);
        }

        RotateHeadToIKPoint(_headIKRotation, _headTargetToLookAt.position);
    }

    #endregion

    #region FeetGroundingMethods

    private void MoveFeetToIKPoint(AvatarIKGoal foot, Vector3 positionIKHolder, Quaternion rotationIKHolder,
        ref float lastFootPositionY)
    {
        Vector3 targetIKPosition = _animator.GetIKPosition(foot);
        if (positionIKHolder != Vector3.zero)
        {
            targetIKPosition = transform.InverseTransformPoint(targetIKPosition);
            positionIKHolder = transform.InverseTransformPoint(positionIKHolder);

            float yAxis =
                Mathf.Lerp(lastFootPositionY, positionIKHolder.y,
                    _feetToIKPositionSpeed); //linear interpolation between 2 points 
            targetIKPosition.y += yAxis;

            lastFootPositionY = yAxis;

            targetIKPosition = transform.TransformPoint(targetIKPosition);
            _animator.SetIKRotation(foot, rotationIKHolder);
        }

        _animator.SetIKPosition(foot, targetIKPosition);
    }

    private void MovePelvisHeight()
    {
        if (_rightFootIKPosition == Vector3.zero || _leftFootIKPosition == Vector3.zero || _lastPelvisPositionY == 0)
        {
            _lastPelvisPositionY = _animator.bodyPosition.y;
            return;
        }

        float leftOffsetPosition = _leftFootIKPosition.y - transform.position.y;
        float rightOffsetPosition = _rightFootIKPosition.y - transform.position.y;

        float totalOffset =
            (leftOffsetPosition < rightOffsetPosition)
                ? leftOffsetPosition
                : rightOffsetPosition; //choosing the lowest position 
        Vector3 newPelvisPosition = _animator.bodyPosition + Vector3.up * totalOffset;

        newPelvisPosition.y = Mathf.Lerp(_lastPelvisPositionY, newPelvisPosition.y, _pelvisUpAndDownSpeed);

        _animator.bodyPosition = newPelvisPosition;

        _lastPelvisPositionY = _animator.bodyPosition.y;
    }

    private void FeetPositionSolver(Vector3 fromSkyPosition, ref Vector3 feetIKPosition, ref Quaternion feetIKRotations)
    {
        //raycast handle 
        RaycastHit feetOutHit;
        if (showSolverDebugMode)
        {
            Debug.DrawLine(fromSkyPosition,
                fromSkyPosition + Vector3.down * (raycastDownDistance + heightFromGraoundRaycast), Color.yellow);
        }

        if (Physics.Raycast(fromSkyPosition, Vector3.down, out feetOutHit,
            raycastDownDistance + heightFromGraoundRaycast, _environmentLayer))
        {
            //finding our feet ik position from the sky position 
            feetIKPosition = fromSkyPosition;
            feetIKPosition.y = feetOutHit.point.y + _pelvisOffset;
            feetIKRotations = Quaternion.FromToRotation(Vector3.up, feetOutHit.normal) * transform.rotation;
            return;
        }

        feetIKPosition = Vector3.zero; // 
        Debug.Log("feet ik position is zero, didn't work");
    }

    private void AdjustFeetTarget(ref Vector3 feetPosition, HumanBodyBones foot)
    {
        feetPosition = _animator.GetBoneTransform(foot).position;
        // feetPosition = leftfootTR.position;
        feetPosition.y =
            transform.position.y +
            heightFromGraoundRaycast; //assign raycast offset the ground TODO: fix spelling error 
    }

    #endregion

    #region Head Ground Method

    private void RotateHeadToIKPoint(Quaternion headIKRotation, Vector3 target)
    {
        Quaternion targetRotation = Quaternion.Euler(target);
        float angle;
        float finalLookWeight = 0;
        // float minRotation = -45;
        // float maxRotation = 45;
        // Vector3 currentRotation = transform.localRotation.eulerAngles;
        // currentRotation.y = Mathf.Clamp(currentRotation.y, minRotation, maxRotation);
        // transform.localRotation = Quaternion.Euler (currentRotation);
        if (headIKRotation != Quaternion.identity)
        {
            //calculate the angle and restrict it 
            angle = Quaternion.Angle(headIKRotation, targetRotation);
            Debug.Log("Angle 2 vectors " + angle);

            _animator.SetLookAtPosition(target);
        }
    }

    //assign head pass by ref 
    private void AdjustHeadTarget(ref Quaternion headRotation, HumanBodyBones head)
    {
        headRotation = _animator.GetBoneTransform(head).rotation; //get bone current rotation of the animator bone 
    }

    #endregion
}