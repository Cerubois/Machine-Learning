using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveAI : Agent
{

	public Vector3 startPos = Vector3.zero;
	public float moveSpeed = 1.0f;
	[SerializeField]
	private Transform goalTransform;
	[SerializeField]
	private Material successMat;
	[SerializeField]
	private Material failMat;
	[SerializeField]
	private MeshRenderer floorRenderer;

	public override void OnEpisodeBegin() {
		transform.localPosition = startPos;
	}

	public override void CollectObservations(VectorSensor sensor) {
		sensor.AddObservation(transform.localPosition);
		sensor.AddObservation(goalTransform.localPosition);
	}

	public override void OnActionReceived(ActionBuffers actions) {
		//Debug.Log(actions.DiscreteActions[0]);
		//Debug.Log(actions.ContinuousActions[0]);

		float moveX = actions.ContinuousActions[0];
		float moveZ = actions.ContinuousActions[1];

		transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;

	}

	private void OnTriggerEnter(Collider other) {
		
		if(other.transform != null) {

			if(other.transform.tag == "Target") {

				floorRenderer.material = successMat;
				SetReward(1f);
				EndEpisode();

			}
			else if(other.transform.tag == "Wall") {

				floorRenderer.material = failMat;
				EndEpisode();

			}

		}

	}

}
