using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SimpleStatemachineBehaviour : StateMachineBehaviour
{
    public string id;

    public delegate void StateEventHandler();
    public StateEventHandler OnStateEntered;
    public StateEventHandler OnStateExited;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (OnStateEntered != null)
            OnStateEntered();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (OnStateExited != null)
            OnStateExited();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

}

public static class SimpleStatemachineBehaviourExtensions
{
    public static T GetBehaviour<T>(this Animator animator, string id) where T : SimpleStatemachineBehaviour
    {
        return animator.GetBehaviours<T>().ToList().First(behaviour => behaviour.id == id);
    }
}