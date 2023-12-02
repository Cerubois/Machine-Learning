using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageRandomiser : MonoBehaviour
{

	public SpriteRenderer fgRenderer;
	public SpriteRenderer bgRenderer;

	private Sprite[] cats;
	private Sprite[] backgrounds;

	public float randomXBounds = 2.0f;
	public float randomYBounds = 2.0f;

	public bool drawFGBounds = true;

    // Start is called before the first frame update
    void Start()
    {

		cats = Resources.LoadAll<Sprite>("Cutouts/Cats");
		backgrounds = Resources.LoadAll<Sprite>("Backgrounds");

    }

    // Update is called once per frame
    void Update()
    {

		if (Input.GetButtonDown("Jump"))
			RandomiseImage();

    }

	public void RandomiseImage() {

		int newFG = Random.Range(0, cats.Length - 1);
		fgRenderer.sprite = cats[newFG];
		int newBG = Random.Range(0, backgrounds.Length - 1);
		bgRenderer.sprite = backgrounds[newBG];

		float xPos = Random.Range(-randomXBounds, randomXBounds);
		float yPos = Random.Range(-randomYBounds, randomYBounds);

		fgRenderer.transform.localPosition = new Vector2(xPos, yPos);

		if (drawFGBounds)
			DrawDebugBoundaries();

	} 

	private void DrawDebugBoundaries() {

		Vector4 fgBounds = new Vector4();

		fgBounds.x = fgRenderer.bounds.min.x;
		fgBounds.y = fgRenderer.bounds.min.y;
		fgBounds.z = fgRenderer.bounds.max.x;
		fgBounds.w = fgRenderer.bounds.max.y;
		
		// Left
		Debug.DrawLine(new Vector3(fgBounds.x, fgBounds.y, 0), new Vector3(fgBounds.x, fgBounds.w, 0), Color.yellow, 3);
		// Right
		Debug.DrawLine(new Vector3(fgBounds.z, fgBounds.y, 0), new Vector3(fgBounds.z, fgBounds.w, 0), Color.yellow, 3);
		// Top
		Debug.DrawLine(new Vector3(fgBounds.x, fgBounds.w, 0), new Vector3(fgBounds.z, fgBounds.w, 0), Color.yellow, 3);
		// Bottom
		Debug.DrawLine(new Vector3(fgBounds.x, fgBounds.y, 0), new Vector3(fgBounds.z, fgBounds.y, 0), Color.yellow, 3);

	}

	public Vector4 GetFGBounds() {

		Vector4 fgBounds = new Vector4();

		fgBounds.x = fgRenderer.bounds.min.x;
		fgBounds.y = fgRenderer.bounds.min.y;
		fgBounds.z = fgRenderer.bounds.max.x;
		fgBounds.w = fgRenderer.bounds.max.y;

		return fgBounds;

	}



}
