using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

public class MoveAI : Agent
{

	public override void OnActionReceived(ActionBuffers actions) {
		Debug.Log(actions.DiscreteActions[0]);
	}

}