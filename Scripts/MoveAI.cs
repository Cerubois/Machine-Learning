using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveAI : Agent
{

	public float moveSpeed = 1.0f;
	[SerializeField]
	private Transform goalTransform;

	public override void OnEpisodeBegin() {
		transform.position = Vector3.zero;
	}

	public override void CollectObservations(VectorSensor sensor) {
		sensor.AddObservation(transform.position);
		sensor.AddObservation(goalTransform.position);
	}

	public override void OnActionReceived(ActionBuffers actions) {
		//Debug.Log(actions.DiscreteActions[0]);
		//Debug.Log(actions.ContinuousActions[0]);
		float moveX = actions.ContinuousActions[0];
		float moveZ = actions.ContinuousActions[1];

		transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;


	}

	private void OnTriggerEnter(Collider other) {
		SetReward(1f);
		EndEpisode();
	}



}
