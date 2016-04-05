using UnityEngine;

public class DeathAnimator : StateMachineBehaviour {

  public GameObject target;

  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (stateInfo.IsTag("Dead")) {
      target.SendMessage("Dead");
    } else if (stateInfo.IsTag("Damaged")) {
      target.SendMessage("Recover");
    }
  }
}
