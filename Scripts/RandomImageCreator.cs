using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomImageCreator : MonoBehaviour
{

    public SpriteRenderer bgRenderer;
    public SpriteRenderer fgRenderer;

    [SerializeField]
    private Sprite[] fgSprites;
    private Sprite[] bgSprites;

    public float randomXPosRange = 5.0f;
    public float randomYPosRange = 2.0f;

    public bool drawDebugBounds = false;

    // Start is called before the first frame update
    void Start()
    {

        bgSprites = Resources.LoadAll<Sprite>("Backgrounds");
        fgSprites = Resources.LoadAll<Sprite>("Cutouts/Cats");

        RandomiseImage();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump"))
            RandomiseImage();

    }

    public void RandomiseImage() {

        int newFG = Random.Range(0, fgSprites.Length - 1);
        fgRenderer.sprite = fgSprites[newFG];
        int newBG = Random.Range(0, bgSprites.Length - 1);
        bgRenderer.sprite = bgSprites[newBG];

        float fgXPos = Random.Range(-randomXPosRange, randomXPosRange);
        float fgYPos = Random.Range(-randomYPosRange, randomYPosRange);

        fgRenderer.transform.localPosition = new Vector2(fgXPos, fgYPos);

        if (drawDebugBounds)
            DebugDrawBounds();

    }

    public Vector4 GetFGBounds() {

        Vector4 fgBounds = new Vector4();
        fgBounds.x = fgRenderer.bounds.min.x;
        fgBounds.y = fgRenderer.bounds.min.y;
        fgBounds.z = fgRenderer.bounds.max.x;
        fgBounds.w = fgRenderer.bounds.max.y;

        return fgBounds;

    }

    public void DebugDrawBounds() {

        Vector4 newBounds = GetFGBounds();
        //Debug.Log(newBounds);

        //Left
        Debug.DrawLine(new Vector3(newBounds.x, newBounds.y), new Vector3(newBounds.x, newBounds.w), Color.yellow, 3);
        //Right
        Debug.DrawLine(new Vector3(newBounds.z, newBounds.y), new Vector3(newBounds.z, newBounds.w), Color.yellow, 3);
        //Top
        Debug.DrawLine(new Vector3(newBounds.x, newBounds.w), new Vector3(newBounds.z, newBounds.w), Color.yellow, 3);
        //Bottom
        Debug.DrawLine(new Vector3(newBounds.x, newBounds.y), new Vector3(newBounds.z, newBounds.y), Color.yellow, 3);

    }


}
