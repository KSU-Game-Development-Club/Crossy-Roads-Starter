using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform Body;

    [SerializeField] private float moveDuration = 0.2f;

    private bool isMoving = false;

    [SerializeField] private Transform terrainHolder;
    [SerializeField] private Grass grassPrefab;

    private int spawnPos = 1;
    [SerializeField] private int spawnDistance = 20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NewLevel();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPos = new(Body.position.x, 4, Body.position.z -3);
        cameraPos.z = Mathf.Clamp(cameraPos.z, -5, 6);
        Camera.main.transform.position = cameraPos;
    }

    private void NewLevel()
    {
        spawnPos = 1;
        Body.position = new(0, 0.7f, 0);
        foreach(Transform t in terrainHolder)
        {
            Destroy(t.gameObject);
        }

        for(int i = 0; i < spawnDistance; i++)
        {
            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        Grass g = Instantiate(grassPrefab, terrainHolder);
        g.Init(spawnPos);
        spawnPos++;
    }

    public void Move(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.performed || isMoving) return;
        Vector2 v = callbackContext.ReadValue<Vector2>();
        Vector3 wantedPos = new(Body.position.x + v.y, 0.7f, Body.position.z - v.x);
        if (wantedPos.z >= 6 || wantedPos.z <= -6) return;
        if (spawnPos - wantedPos.x < spawnDistance) SpawnObstacle();
        isMoving = true;
        StartCoroutine(AnimateMoveTo(wantedPos));
    }

    IEnumerator AnimateMoveTo(Vector3 v)
    {
        float elapsed = 0f;
        Vector3 startPos = Body.position;

        while(elapsed < moveDuration)
        {
            float percent = elapsed / moveDuration;
            Vector3 newPoint = Vector3.Lerp(startPos, v, percent);
            elapsed += Time.deltaTime;
            Body.position = newPoint;
            yield return null;
        }
        Body.position = v;
        isMoving = false;
    }
}
