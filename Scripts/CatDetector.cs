using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class CatDetector : Agent
{

	[SerializeField]
	ImageRandomiser imageRandomiser;
	Vector4 fgActualBounds;
	Vector4 fgGuessBounds;
	[SerializeField]
	Camera myCamera;

	public bool drawDebugBounds = true;

    public override void OnEpisodeBegin() {

		imageRandomiser.RandomiseImage();
		fgActualBounds = imageRandomiser.GetFGBounds();

	}

	public override void OnActionReceived(ActionBuffers actions) {

		fgGuessBounds = new Vector4();

		fgGuessBounds.x = transform.position.x + (myCamera.orthographicSize * Screen.width / Screen.height) * actions.ContinuousActions[0];
		fgGuessBounds.y = transform.position.y + myCamera.orthographicSize * actions.ContinuousActions[1];
		fgGuessBounds.z = transform.position.x + (myCamera.orthographicSize * Screen.width / Screen.height) * actions.ContinuousActions[2];
		fgGuessBounds.w = transform.position.y + myCamera.orthographicSize * actions.ContinuousActions[3];

		if (drawDebugBounds)
			DrawDebugBoundaries(fgGuessBounds, Color.red, 0.1f);

		Debug.Log(fgGuessBounds);

		int guessesCorrect = 0;

		if(Mathf.Abs(fgActualBounds.x - fgGuessBounds.x) < 0.1f) {
			guessesCorrect++; // Increase by 1
			AddReward(0.1f);
		}
		if (Mathf.Abs(fgActualBounds.y - fgGuessBounds.y) < 0.1f) {
			guessesCorrect++; // Increase by 1
			AddReward(0.1f);
		}
		if (Mathf.Abs(fgActualBounds.z - fgGuessBounds.z) < 0.1f) {
			guessesCorrect++; // Increase by 1
			AddReward(0.1f);
		}
		if (Mathf.Abs(fgActualBounds.w - fgGuessBounds.w) < 0.1f) {
			guessesCorrect++; // Increase by 1
			AddReward(0.1f);
		}

		if(guessesCorrect == 4) {
			AddReward(0.6f);
			EndEpisode();
			if(drawDebugBounds)
				DrawDebugBoundaries(fgGuessBounds, Color.green, 1);
		}

	}


	private void DrawDebugBoundaries(Vector4 newBounds, Color newColor, float duration) {

		// Left
		Debug.DrawLine(new Vector3(newBounds.x, newBounds.y, 0), new Vector3(newBounds.x, newBounds.w, 0), newColor, duration);
		// Right
		Debug.DrawLine(new Vector3(newBounds.z, newBounds.y, 0), new Vector3(newBounds.z, newBounds.w, 0), newColor, duration);
		// Top
		Debug.DrawLine(new Vector3(newBounds.x, newBounds.w, 0), new Vector3(newBounds.z, newBounds.w, 0), newColor, duration);
		// Bottom
		Debug.DrawLine(new Vector3(newBounds.x, newBounds.y, 0), new Vector3(newBounds.z, newBounds.y, 0), newColor, duration);

	}

}
