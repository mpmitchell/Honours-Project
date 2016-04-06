using UnityEngine;

public class DragonAnimator : StateMachineBehaviour {

  public Dragon dragon;

  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (stateInfo.IsTag("Attack")) {
      dragon.attacking = false;
    }
  }
}
