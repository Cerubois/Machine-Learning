using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class CatDetector : Agent
{

	[SerializeField]
	private RandomImageCreator randomImageCreator;
	private Vector4 fgImagePos; // The actual position of the image we're looking for.
	private Vector4 guessVector;
	[SerializeField]
	private Camera myCamera;

	public bool drawDebugBounds = false;

	public override void OnEpisodeBegin() {

		randomImageCreator.RandomiseImage();
		fgImagePos = randomImageCreator.GetFGBounds();

	}

	public override void OnActionReceived(ActionBuffers actions) {

		guessVector = new Vector4();

		guessVector.x = transform.position.x + actions.ContinuousActions[0] * myCamera.orthographicSize * Screen.width / Screen.height;
		guessVector.y = transform.position.y + actions.ContinuousActions[1] * myCamera.orthographicSize;
		guessVector.z = transform.position.x + actions.ContinuousActions[2] * myCamera.orthographicSize * Screen.width / Screen.height;
		guessVector.w = transform.position.y + actions.ContinuousActions[3] * myCamera.orthographicSize;

		int guessesCorrect = 0;

		if (drawDebugBounds)
			DebugDrawBounds(guessVector, Color.red, 0.2f);

		if (Mathf.Abs(fgImagePos.x - guessVector.x) < 0.5f) {
			AddReward(0.1f);
			guessesCorrect++;
		}
		if (Mathf.Abs(fgImagePos.y - guessVector.y) < 0.5f) {
			AddReward(0.1f);
			guessesCorrect++;
		}
		if (Mathf.Abs(fgImagePos.z - guessVector.z) < 0.5f) {
			AddReward(0.1f);
			guessesCorrect++;
		}
		if (Mathf.Abs(fgImagePos.w - guessVector.w) < 0.5f) {
			AddReward(0.1f);
			guessesCorrect++;
		}

		if(guessesCorrect == 4) {
			AddReward(1f);
			EndEpisode();
			if (drawDebugBounds)
				DebugDrawBounds(guessVector, Color.green, 2f);
		}

	}


	public void DebugDrawBounds(Vector4 newBounds, Color lineColor, float duration) {

		//Debug.Log(newBounds);

		//Left
		Debug.DrawLine(new Vector3(newBounds.x, newBounds.y), new Vector3(newBounds.x, newBounds.w), lineColor, duration);
		//Right
		Debug.DrawLine(new Vector3(newBounds.z, newBounds.y), new Vector3(newBounds.z, newBounds.w), lineColor, duration);
		//Top
		Debug.DrawLine(new Vector3(newBounds.x, newBounds.w), new Vector3(newBounds.z, newBounds.w), lineColor, duration);
		//Bottom
		Debug.DrawLine(new Vector3(newBounds.x, newBounds.y), new Vector3(newBounds.z, newBounds.y), lineColor, duration);

	}

}
