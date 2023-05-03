using System.Collections.Generic;
using UnityEngine;

#nullable enable
public class Cube : MonoBehaviour {

    const byte TERRAIN_TYPE_STRAIGHT = 0;
    const byte TERRAIN_TYPE_PARABOLIC = 1;

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
    private byte currentBehaviour = TERRAIN_TYPE_PARABOLIC;

    private void ResetGravity() {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = Mathf.Abs(rigidbody.gravityScale);
    }

    void Start() {
        this.startTime = Time.timeAsDouble;
    }

    void Update() {
        if (this.currentBehaviour == TERRAIN_TYPE_PARABOLIC && Input.GetMouseButtonDown(0)) {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            rigidbody.AddForce(new Vector2(0, 20f), ForceMode2D.Impulse);
        }
        if (this.currentBehaviour == TERRAIN_TYPE_STRAIGHT && Input.GetMouseButtonDown(0)) {
            GetComponent<Rigidbody2D>().gravityScale *= -1;
        }
        if (this.currentBehaviour == TERRAIN_TYPE_STRAIGHT && Input.GetMouseButtonUp(0))
            this.ResetGravity();
    }

    void FixedUpdate() {
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speedFactor * (1 + Mathf.Sqrt((float) (Time.timeAsDouble - this.startTime) / 50)), rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D currentTerrain) {
        // Use Type to apply effect
        byte type = (byte) Random.Range(0, 2);
        if (this.currentBehaviour != type) this.ResetGravity();
        this.currentBehaviour = type;

        // display proper colors
        SpriteRenderer topColorEffect = currentTerrain.transform.GetChild(0)!.GetComponent<SpriteRenderer>();
        SpriteRenderer bottomColorEffect = currentTerrain.transform.GetChild(1)!.GetComponent<SpriteRenderer>();
        switch(type) {
            case TERRAIN_TYPE_STRAIGHT:
                topColorEffect.color = bottomColorEffect.color = new Color(255, 156, 0);
                sound_1?.GetComponent<AudioSource>()?.Play();
                break;
            case TERRAIN_TYPE_PARABOLIC:
                topColorEffect.color = bottomColorEffect.color = new Color(0, 208, 255);
                sound_2?.GetComponent<AudioSource>()?.Play();
                break;
        }

        // Add next terrain
        GameObject prefab = terrainPrefabs[Random.Range(0, terrainPrefabs.Count)];
        GameObject instance = Instantiate(prefab, lastTerrain.transform.position + new Vector3(lastTerrain.transform.localScale.x / 2 + prefab.transform.localScale.x / 2, 0), Quaternion.identity, terrainContainer?.transform);
        lastTerrain = instance;

        // Increment score
        text!.GetComponent<TMPro.TextMeshProUGUI>().text = "Score : " + ++this.score;
    }

    private void OnTriggerExit2D(Collider2D other) {
        Destroy(other.gameObject, 10);
    }
}
