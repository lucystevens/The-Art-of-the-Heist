
using UnityEngine;

public enum AnimationTrigger {
    InteractOneHand,
    InteractTwoHands
}

public static class AnimationTriggerExt {
    public static void SetOnAnimator(this AnimationTrigger trigger, Animator animator) {
        animator.SetTrigger(trigger.ToString());
    }
}