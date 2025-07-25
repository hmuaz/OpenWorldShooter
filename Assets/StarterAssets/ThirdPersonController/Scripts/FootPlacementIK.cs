using UnityEngine;

public class FootPlacementIK : MonoBehaviour
{
    public Animator animator;
    public Transform leftFootHeel, leftFootToe;
    public Transform rightFootHeel, rightFootToe;
    public LayerMask groundLayer;
    public float raycastDist = 0.5f;
    public float footYOffset = 0.02f;

    void OnAnimatorIK(int layerIndex)
    {
        Debug.Log("ik çalıştı");
        DoFootIK(AvatarIKGoal.LeftFoot, leftFootHeel, leftFootToe);
        DoFootIK(AvatarIKGoal.RightFoot, rightFootHeel, rightFootToe);
    }

    private void DoFootIK(AvatarIKGoal foot, Transform heel, Transform toe)
    {
        Vector3 heelOrigin = heel.position + Vector3.up * 0.1f;
        Vector3 toeOrigin  = toe.position  + Vector3.up * 0.1f;

        bool heelHit = Physics.Raycast(heelOrigin, Vector3.down, out RaycastHit heelRay, raycastDist, groundLayer);
        bool toeHit  = Physics.Raycast(toeOrigin,  Vector3.down, out RaycastHit toeRay,  raycastDist, groundLayer);

        if (heelHit && toeHit)
        {
            Vector3 footPos = (heelRay.point + toeRay.point) / 2f + Vector3.up * footYOffset;

            Vector3 footForward = (toeRay.point - heelRay.point).normalized;
            Vector3 footNormal = (heelRay.normal + toeRay.normal) / 2f;

            Quaternion footRot = Quaternion.LookRotation(footForward, footNormal);

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
}