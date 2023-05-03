using System.Collections.Generic;
using UnityEngine;

#nullable enable
public class Cube : MonoBehaviour {

    public List<GameObject> terrainPrefabs = new List<GameObject>();

    [SerializeField]
    public int speedFactor = 5;

    private int score = 0;

    [SerializeField]
    private GameObject? terrainContainer;

    [SerializeField]
    private GameObject? text;

    [SerializeField]
    private GameObject? sound_1;
    [SerializeField]
    private GameObject? sound_2;

    [SerializeField]
    private GameObject lastTerrain;

    private double startTime;

    [SerializeField]
    private TerrainType currentBehaviour = TerrainType.PARABOLIC;

    private void ResetGravity() {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = Mathf.Abs(rigidbody.gravityScale);
    }

    void Start() {
        this.startTime = Time.timeAsDouble;
    }

    void Update() {
        if (this.currentBehaviour == TerrainType.PARABOLIC && Input.GetMouseButtonDown(0)) {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            rigidbody.AddForce(new Vector2(0, 20f), ForceMode2D.Impulse);
        }
        if (this.currentBehaviour == TerrainType.STRAIGHT && Input.GetMouseButtonDown(0)) {
            GetComponent<Rigidbody2D>().gravityScale *= -1;
        }
        if (this.currentBehaviour == TerrainType.STRAIGHT && Input.GetMouseButtonUp(0))
            this.ResetGravity();
    }

    void FixedUpdate() {
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(speedFactor * (1 + Mathf.Sqrt((float) (Time.timeAsDouble - this.startTime) / 50)), rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D currentTerrain) {
        // Use Type to apply effect
        this.ApplyTerrainType(currentTerrain.GetComponent<Terrain>().type);
        currentTerrain.GetComponent<Terrain>().OnReached();

        // Add next terrain
        GameObject prefab = terrainPrefabs[Random.Range(0, terrainPrefabs.Count)];
        GameObject instance = Instantiate(prefab, lastTerrain.transform.position + new Vector3(lastTerrain.transform.localScale.x / 2 + prefab.transform.localScale.x / 2, 0), Quaternion.identity, terrainContainer?.transform);
        instance.GetComponent<Terrain>().type = (TerrainType) Random.Range(0, 2);
        lastTerrain = instance;

        // Increment score
        text!.GetComponent<TMPro.TextMeshProUGUI>().text = "Score : " + ++this.score;
    }

    private void OnTriggerExit2D(Collider2D other) {
        Destroy(other, 10);
    }

    void ApplyTerrainType(TerrainType type) {
        if (this.currentBehaviour != type) this.ResetGravity();
        this.currentBehaviour = type;
        switch (type) {
            case TerrainType.STRAIGHT:
                sound_1?.GetComponent<AudioSource>()?.Play();
                break;
            case TerrainType.PARABOLIC:
                sound_2?.GetComponent<AudioSource>()?.Play();
                break;
        }
    }
}
