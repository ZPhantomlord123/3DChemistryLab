using UnityEngine;

public class ObjectAnimator : MonoBehaviour
{
    public Animator animator;

    public float PlayAnimation(string animationName)
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator is not assigned in ObjectAnimator.");
            return 0f;
        }

        if (gameObject == null)
        {
            Debug.LogWarning("GameObject is null.");
            return 0f;
        }

        Animator targetAnimator = GetComponent<Animator>();
        if (targetAnimator == null)
        {
            Debug.LogWarning("Animator component not found on the GameObject.");
            return 0f;
        }

        targetAnimator.CrossFade(animationName, 0f);
        return GetAnimationLength(animationName);
    }

    private float GetAnimationLength(string animationName)
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator is not assigned in ObjectAnimator.");
            return 0f;
        }

        if (!animator.HasState(0, Animator.StringToHash(animationName)))
        {
            Debug.LogWarning("Animation not found: " + animationName);
            return 0f;
        }

        AnimationClip clip = animator.runtimeAnimatorController.animationClips[0];
        if (clip == null)
        {
            Debug.LogWarning("Animation clip not found: " + animationName);
            return 0f;
        }

        return clip.length;
    }
}
