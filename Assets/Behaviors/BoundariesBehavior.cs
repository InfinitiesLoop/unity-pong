using UnityEngine;

public class BoundariesBehavior : MonoBehaviour {
    public BoxCollider2D topWall;
    public BoxCollider2D bottomWall;
    public BoxCollider2D leftWall;
    public BoxCollider2D rightWall;

    void Start () {
        var worldSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        worldSize = worldSize * 2;

        topWall.size = new Vector2(worldSize.x, 1);
        topWall.offset = new Vector2(0, worldSize.y / 2f + 0.5f);
        bottomWall.size = new Vector2(worldSize.x, 1);
        bottomWall.offset = new Vector2(0, -worldSize.y / 2f - 0.5f);

        leftWall.size = new Vector2(1, worldSize.y);
        leftWall.offset = new Vector2(-worldSize.x / 2f - 0.5f, 0);
        rightWall.size = new Vector2(1, worldSize.y);
        rightWall.offset = new Vector2(worldSize.x / 2f + 0.5f, 0);

        topWall.GetComponent<BoxCollider2D>().size = topWall.size;
        bottomWall.GetComponent<BoxCollider2D>().size = bottomWall.size;
        leftWall.GetComponent<BoxCollider2D>().size = leftWall.size;
        rightWall.GetComponent<BoxCollider2D>().size = rightWall.size;
    }

    void Update () {
	}
}
