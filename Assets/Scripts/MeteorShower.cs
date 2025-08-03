using UnityEngine;

public class MeteorShower : MonoBehaviour
{
    public GameObject meteorite;
    public GameObject player;
    public GameObject spawner;
    public float spawnRate = 2f;
    public float meteorSpeed = 10f;
    GameSaveData save;


    void Start()
    {
        if (GameManager.gm.levelNumber > 66)
        {
            InvokeRepeating(nameof(SpawnMeteorite), 0f, spawnRate);
        }
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
    }

    void SpawnMeteorite()
    {
        if (meteorite == null || player == null || spawner == null  || GameManager.gm.State != GameState.GamePlay) return;

        Vector3 targetPosition = player.transform.position;

        GameObject newMeteor = Instantiate(meteorite, spawner.transform.position, Quaternion.identity);
        newMeteor.transform.SetParent(null);

        Rigidbody rb = newMeteor.GetComponent<Rigidbody>();
        
        Vector3 direction = (targetPosition - spawner.transform.position).normalized;

        rb.linearVelocity = direction * meteorSpeed;
    }

}