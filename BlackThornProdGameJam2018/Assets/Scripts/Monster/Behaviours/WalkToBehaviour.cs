using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToBehaviour : StateMachineBehaviour {

    private MonsterController monster;
    private GameObject player;
    private Vector3 target;

    private bool idleState;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monster = animator.transform.gameObject.GetComponent<MonsterController>();
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        idleState = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float dis = Vector3.Distance(monster.transform.position, player.transform.position);
        //Debug.Log("STATE: WalkTo " + dis);
        if (dis <= monster.scaredRange)
        {
            target = -(player.transform.position - monster.transform.position).normalized * monster.scaredRange;
            Vector3 stapNaDieTarget = (monster.transform.position - monster.transform.position + target).normalized * monster.movementSpeed;
            monster.rigidbodyComp.MovePosition(monster.transform.position + stapNaDieTarget * Time.deltaTime);
        }
        else if(dis < monster.attackRange)
        {
            animator.SetTrigger("Attack");
        }
        else if(dis > monster.sightRange)
        {
            animator.SetTrigger("Idle");
        }
        else
        {
            target = player.transform.position ;
            Vector3 stapNaDieTarget = -(monster.transform.position - target).normalized * monster.movementSpeed;
            monster.rigidbodyComp.MovePosition(monster.transform.position + stapNaDieTarget* Time.deltaTime);
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
