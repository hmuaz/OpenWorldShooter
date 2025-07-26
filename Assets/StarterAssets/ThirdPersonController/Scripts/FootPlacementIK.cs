using System;
using UnityEngine;

public class FootPlacementIK : MonoBehaviour
{
    public Animator animator;
    public Transform leftFootHeel, leftFootToe;
    public Transform rightFootHeel, rightFootToe;
    public Transform rightToeRaycast;
    public LayerMask groundLayer;
    public float raycastDist = 0.5f;
    public float footYOffsetLeft = 0.02f;
    public float footYOffsetRight = 0.02f;

    public Transform leftKneeHint;
    public Transform rightKneeHint;

    void OnAnimatorIK(int layerIndex)
    {
        Debug.Log("ik çalıştı");
        /*if (leftKneeHint != null)
        {
            animator.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, 1f);
            animator.SetIKHintPosition(AvatarIKHint.LeftKnee, leftKneeHint.position);
        }
        if (rightKneeHint != null)
        {
            animator.SetIKHintPositionWeight(AvatarIKHint.RightKnee, 1f);
            animator.SetIKHintPosition(AvatarIKHint.RightKnee, rightKneeHint.position);
        }*/

        DoFootIKLeft(AvatarIKGoal.LeftFoot, leftFootHeel, leftFootToe);
        DoFootIKRight(AvatarIKGoal.RightFoot, rightFootHeel, rightFootToe, rightToeRaycast);
    }

    private void DoFootIKRight(AvatarIKGoal foot, Transform heel, Transform toe, Transform toeRaycast)
    {
        Vector3 heelOrigin = heel.position + Vector3.up * 0.1f;
        
        Vector3 toeOrigin = toe.position + Vector3.up * 0.1f;
        Vector3 toeDirection = (Vector3.down + toe.forward * 0.5f).normalized;
        
        Vector3 toeDirectionRaycast = (Vector3.down + toeRaycast.forward * 0.5f).normalized;

        bool heelHit = Physics.Raycast(heelOrigin, Vector3.down, out RaycastHit heelRay, raycastDist, groundLayer);
        bool toeHit = Physics.Raycast(toeOrigin, toeDirection, out RaycastHit toeRay, raycastDist, groundLayer);
        bool toeHitPosition = Physics.Raycast(toeOrigin, toeDirectionRaycast, out RaycastHit toeRayPosition, raycastDist, groundLayer);


        if (heelHit && toeHit)
        {
            Vector3 footPos = (heelRay.point + toeRayPosition.point) * 0.5f + Vector3.up * footYOffsetLeft;
            Debug.Log(footPos);

            Vector3 footForward = (toeRay.point - heelRay.point);
            Vector3 footNormal = (heelRay.normal + toeRay.normal).normalized;

            if (footNormal == Vector3.up)
            {
                return;
            }
            
            Vector3 toeForwardProjected = Vector3.ProjectOnPlane(toe.forward, toeRayPosition.normal).normalized;
            

            if (toeForwardProjected.sqrMagnitude < 0.0001f)
                toeForwardProjected = toe.forward;

            Quaternion footRot = Quaternion.LookRotation(toeForwardProjected, toeRayPosition.normal);

            animator.SetIKPositionWeight(foot, 1f);
            animator.SetIKRotationWeight(foot, 1f);
            animator.SetIKPosition(foot, footPos);
            animator.SetIKRotation(foot, footRot);

            float slopeAngle = Vector3.Angle(heelRay.normal, Vector3.up);
        }
        else if (toeHitPosition)
        {
            Vector3 footPos = toeRayPosition.point + Vector3.up * footYOffsetLeft;

            Vector3 toeForwardProjected = Vector3.ProjectOnPlane(toe.forward, toeRayPosition.normal).normalized;

            if (toeForwardProjected.sqrMagnitude < 0.0001f)
                toeForwardProjected = transform.forward;

            Quaternion footRot = Quaternion.LookRotation(toeForwardProjected, toeRayPosition.normal);

            animator.SetIKPositionWeight(foot, 1f);
            animator.SetIKRotationWeight(foot, 1f);
            animator.SetIKPosition(foot, footPos);
            animator.SetIKRotation(foot, footRot);
        }
        else
        {
            animator.SetIKPositionWeight(foot, 0f);
            animator.SetIKRotationWeight(foot, 0f);
        }
    }
    
    
    private void DoFootIKLeft(AvatarIKGoal foot, Transform heel, Transform toe)
    {
        Vector3 heelOrigin = heel.position + Vector3.up * 0.1f;
        
        Vector3 toeOrigin = toe.position + Vector3.up * 0.1f;
        Vector3 toeDirection = (Vector3.down + toe.forward * 0.5f).normalized;
        
        bool heelHit = Physics.Raycast(heelOrigin, Vector3.down, out RaycastHit heelRay, raycastDist, groundLayer);
        bool toeHit = Physics.Raycast(toeOrigin, toeDirection, out RaycastHit toeRay, raycastDist, groundLayer);


        if (heelHit && toeHit)
        {
            Vector3 footPos = (heelRay.point + toeRay.point) * 0.5f + Vector3.up * footYOffsetRight;
            Debug.Log(footPos);

            Vector3 footForward = (toeRay.point - heelRay.point);
            Vector3 footNormal = (heelRay.normal + toeRay.normal).normalized;

            if (footNormal == Vector3.up)
            {
                return;
            }

            if (footForward.sqrMagnitude < 0.0001f)
                footForward = toe.forward;

            Quaternion footRot = Quaternion.LookRotation(footForward, footNormal);

            animator.SetIKPositionWeight(foot, 0.5f);
            animator.SetIKRotationWeight(foot, 1f);
            animator.SetIKPosition(foot, footPos);
            animator.SetIKRotation(foot, footRot);

            float slopeAngle = Vector3.Angle(heelRay.normal, Vector3.up);
        }
        else if (toeHit)
        {
            Vector3 footPos = toeRay.point + Vector3.up * footYOffsetRight;

            Vector3 toeForwardProjected = Vector3.ProjectOnPlane(toe.forward, toeRay.normal).normalized;

            if (toeForwardProjected.sqrMagnitude < 0.0001f)
                toeForwardProjected = transform.forward;

            Quaternion footRot = Quaternion.LookRotation(toeForwardProjected, toeRay.normal);

            animator.SetIKPositionWeight(foot, 0.5f);
            animator.SetIKRotationWeight(foot, 1f);
            animator.SetIKPosition(foot, footPos);
            animator.SetIKRotation(foot, footRot);
        }
        else
        {
            animator.SetIKPositionWeight(foot, 0f);
            animator.SetIKRotationWeight(foot, 0f);
        }
    }
}