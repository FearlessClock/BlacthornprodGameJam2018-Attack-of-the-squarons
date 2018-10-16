using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToBehaviour : StateMachineBehaviour {

    private MonsterController controller;
    private GameObject player;
    private Vector3 target;
    public float speed;

    private bool idleState;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller = animator.transform.gameObject.GetComponent<MonsterController>();
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        idleState = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float dis = Vector3.Distance(controller.transform.position, player.transform.position);
        //Debug.Log("WalkTo " + dis);
        if (dis < controller.attackRange)
        {
            animator.SetTrigger("Attack");
        }
        else if(dis > controller.sightRange)
        {
            animator.SetTrigger("Idle");
        }
        else
        {
            target = player.transform.position ;
            Vector3 toTarget = (- target + controller.transform.position).normalized * controller.distanceToPlayer;
            Vector3 moveTo = (target + toTarget - controller.transform.position).normalized;
            Collider2D hit = Physics2D.OverlapCircle(controller.transform.position + moveTo, controller.collisionCheckSize, controller.wallLayerMask);

            if (hit == null)
            {
                controller.transform.position = Vector3.Lerp(controller.transform.position, controller.transform.position + moveTo * speed, Time.deltaTime);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
