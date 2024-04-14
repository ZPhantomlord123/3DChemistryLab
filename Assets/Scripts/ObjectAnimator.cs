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

        animator.CrossFade(animationName, 0f);
        return GetAnimationLength(animationName);
    }

    private float GetAnimationLength(string animationName)
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator is not assigned in ObjectAnimator.");
            return 0f;
        }

        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }

        Debug.LogWarning("Animation clip not found: " + animationName);
        return 0f;
    }
}
