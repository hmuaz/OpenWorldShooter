using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Enemy : MonoBehaviour
{
    public Rig headAimRig;
    public Transform headTarget;
    public Vector3 defaultHeadTargetPos = new Vector3(0f, 1.6f, 0.5f);
    public Coroutine headMoveRoutine;

    
    void Start()
    {
        defaultHeadTargetPos = headTarget.localPosition;
    }
    public void SetHeadAimWeight(float weight)
    {
        headAimRig.weight = weight;
    }
    
    public IEnumerator HeadHitReaction(Vector3 hitPos, float reactionTime, float returnTime)
    {
        float t = 0;
        Vector3 startPos = headTarget.localPosition;
        while (t < 1f)
        {
            headTarget.localPosition = Vector3.Lerp(startPos, hitPos, t);
            t += Time.deltaTime / reactionTime;
            yield return null;
        }
        headTarget.localPosition = hitPos;

        yield return new WaitForSeconds(0.05f);

        t = 0;
        while (t < 1f)
        {
            headTarget.localPosition = Vector3.Lerp(hitPos, defaultHeadTargetPos, t);
            t += Time.deltaTime / returnTime;
            yield return null;
        }
        headTarget.localPosition = defaultHeadTargetPos;
    }
}
