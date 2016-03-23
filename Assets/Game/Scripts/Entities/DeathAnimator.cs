using UnityEngine;

public class DeathAnimator : StateMachineBehaviour {

  GameObject target;

  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (stateInfo.IsTag("Death")) {
      target.SendMessage("Dead");
    }
	}
}
