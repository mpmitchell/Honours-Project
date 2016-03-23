using UnityEngine;

public class BlobAnimator : StateMachineBehaviour {

  public Blob controller;

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (stateInfo.IsTag("Jump")) {
      controller.jumping = false;
    }
	}
}
