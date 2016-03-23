using UnityEngine;

public class ExplosionAnimator : StateMachineBehaviour {

    public GameObject gameObject;

  	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (stateInfo.IsTag("Explosion")) {
        Destroy(gameObject);
      }
  	}
}
