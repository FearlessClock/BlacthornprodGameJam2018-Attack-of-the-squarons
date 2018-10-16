using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour {
    private MonsterController controller;
    private GameObject player;
    private Vector3 target;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        controller = animator.gameObject.GetComponent<MonsterController>();
        target = Random.insideUnitSphere + controller.transform.position;
        target.z = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 moveTo = Vector3.Lerp(controller.transform.position, target, Time.deltaTime);

        Collider2D hit = Physics2D.OverlapCircle(moveTo, controller.collisionCheckSize, controller.wallLayerMask);

        if(hit == null)
        {
            controller.transform.position = Vector3.Lerp(controller.transform.position, target, Time.deltaTime);
        }
        else
        {
            ChangeTarget();
        }

        if (Vector3.Distance(controller.transform.position, target) < 0.1f)
        {
            ChangeTarget();
        }
        //Check if collision if moving in certain direction
        //Allow movement if none
        float dis = Vector3.Distance(controller.transform.position, player.transform.position);
        //Debug.Log("Idle distance " +dis);
        if (dis < controller.sightRange)
        {
            animator.SetTrigger("WalkTo");
        }
    }

    private void ChangeTarget()
    {
        target = Random.insideUnitSphere * 2 + controller.transform.position;
        target.z = 0;
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
