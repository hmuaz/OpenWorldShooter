using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Enemy : MonoBehaviour
{
    public Rig rig;
    public MultiAimConstraint headAimConstraint;

    public Transform headTarget;
    public Transform shoulderTarget;
    public Vector3 defaultHeadTargetPos = new Vector3(0f, 1.6f, 0.5f);
    public Vector3 defaultShoulderTargetPos = new Vector3(0f, 1.2f, 0.7f);
    public Coroutine headMoveRoutine;
    public float enemyHealth = 100f;

    public Animator animator;

    public Rigidbody[] ragdollBodies;
    public Coroutine shoulderMoveRoutine;


    void Start()
    {
        defaultHeadTargetPos = headTarget.localPosition;
        defaultShoulderTargetPos = shoulderTarget.localPosition;
    }

    public void DestroyTheGoddamnRig()
    {
        Debug.Log("Eventten çağrıldı");
        rig.weight = 0;
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
    
    public IEnumerator ShoulderHitReaction(Vector3 targetPos, float duration, float returnDelay)
    {
        Vector3 startPos = shoulderTarget.localPosition;
        float timer = 0f;

        while (timer < duration)
        {
            shoulderTarget.localPosition = Vector3.Lerp(startPos, targetPos, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        shoulderTarget.localPosition = targetPos;

        yield return new WaitForSeconds(returnDelay);

        timer = 0f;
        while (timer < duration)
        {
            shoulderTarget.localPosition = Vector3.Lerp(targetPos, defaultShoulderTargetPos, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        shoulderTarget.localPosition = defaultShoulderTargetPos;
    }

    public void CheckIfEnemyDies(string animationName)
    {
        if (enemyHealth <= 0)
        {
        }
    }

    public void PlayDeathAnimation(string animationName)
    {
        animator.SetTrigger(animationName);
        if (animationName == "HeadshotFromBackDeath")
        {
            StartCoroutine(WaitMethod(0.5f));
        }
    }

    IEnumerator WaitMethod(float time)
    {
        yield return new WaitForSeconds(time);
        rig.weight = 0;
    }

    public void DisableAnimator()
    {
        foreach (var rb in ragdollBodies)
        {
            Transform bone = rb.transform;
            bone.position = bone.position;
            bone.rotation = bone.rotation;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        animator.enabled = false;
    }
}