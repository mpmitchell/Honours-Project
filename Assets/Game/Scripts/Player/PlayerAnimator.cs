using UnityEngine;

public class PlayerAnimator : StateMachineBehaviour {

  public PlayerController controller;

  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (stateInfo.IsTag("Attack")) {
      controller.attacking = false;
    }
  }
}
