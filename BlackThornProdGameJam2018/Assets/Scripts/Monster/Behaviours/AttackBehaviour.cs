using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour {

    private GameObject player;
    private MonsterController monster;

    private int direction;

    private bool spawnOne = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        direction = Random.Range(-1f, 1f) > 0 ? 1 : -1;
        player = GameObject.FindObjectOfType<PlayerController>().gameObject;
        monster = animator.transform.gameObject.GetComponent<MonsterController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float disToPlayer = Vector3.Distance(player.transform.position, animator.transform.position);
        //Debug.Log("STATE: Attack " + disToPlayer);
        if (disToPlayer <= monster.scaredRange || disToPlayer > monster.attackRange)
        {
            animator.SetTrigger("WalkTo");
        }
        else if (disToPlayer < monster.attackRange)
        {
            Vector3 aimAtPlayer = (monster.transform.position - player.transform.position).normalized;
            Vector3 strafeDirection = new Vector3(direction * aimAtPlayer.y, -direction * aimAtPlayer.x);
            monster.rigidbodyComp.MovePosition(monster.transform.position + strafeDirection * Time.deltaTime * monster.movementSpeed);

            float angle = -Mathf.Atan2(aimAtPlayer.x, aimAtPlayer.y);
            monster.spellDirection.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
            monster.LaunchSpell(0);
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
