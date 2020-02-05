﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolBehaviour : StateMachineBehaviour {

    // The front and rear vision points from enemy object
    private Vector2 enemyFrontPoint;
    private Vector2 enemyRearPoint;

    [SerializeField]
    private float speed = 1;

    [SerializeField]
    private float maxVisionLength = 10f;


    private void StartChasingPlayer(Animator animator)
    {
        animator.SetBool("chase", true);
        animator.SetBool("patrol", false);
    }

    private bool SearchForPlayer(Transform enemy)
    {
        bool playerFound = false;
        enemyFrontPoint = enemy.GetComponent<Enemy>().FrontPoint.position;

        RaycastHit2D hitForward = Physics2D.Raycast(enemyFrontPoint, Vector2.right, maxVisionLength);

        if (hitForward.collider != null)
        {
            if (hitForward.transform.tag == "Player")
            {
                playerFound = true;
            }
        }

        return playerFound;
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (SearchForPlayer(animator.transform.parent))
        {
            StartChasingPlayer(animator);
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       
    }
    
}
