using UnityEngine;

public class DamageAnimator : StateMachineBehaviour {

  public GameObject target;

  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (stateInfo.IsTag("Damaged")) {
      target.SendMessage("Recover");
    }
  }
}
