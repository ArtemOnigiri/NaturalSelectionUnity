using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public Vector2 area = new Vector2(10f, 10f);

    public GameObject bacteriumPrefab;
    public GameObject boidPrefab;
    public GameObject foodPrefab;
    public GameObject antibiotics;
    public GameObject walls;

    public int CountBoids = 50;
    public int CountBacterium = 1000;
    public int CountFood = 100;

    public bool SpawnBoids = false;
    public bool SpawnBacterium = false;
    public bool SpawnFood = false;
    public bool SpawnFoodInfinity = false;
    public bool SpawnBoidInfinity = false;
    public bool Antibiotics = false;

    private int frame = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (SpawnBacterium) Evolution();
        if (SpawnBoids) Boids();
        if (SpawnFood) Food();
        if (Antibiotics) antibiotics.SetActive(true);
        if (SpawnBoidInfinity) Invoke("DestroyWalls", 10);
    }

    void DestroyWalls()
    {
        Destroy(walls);
    }

    private void Boids()
    {
        for (int i = 0; i < CountBoids; i++)
        {
            GameObject b = Instantiate(boidPrefab, new Vector3(Random.Range(-area.x, area.x), Random.Range(-area.y, area.y), 0), Quaternion.identity);
            b.name = "Boid";

            if (SpawnBoidInfinity)
            {
                b.GetComponent<Boid>().Dying = true;
            }
        }
    }

    private void Evolution()
    {
        for (int i = 0; i < CountBacterium; i++)
        {
            Genome genome = new Genome(64);
            GameObject b = Instantiate(bacteriumPrefab, new Vector3(Random.Range(-area.x, area.x), Random.Range(-area.y, area.y), 0), Quaternion.identity);
            b.name = "bacterium";
            b.GetComponent<AI>().Init(genome);
            b.GetComponent<AI>().Antibiotics = Antibiotics;
        }

    }

    private void Food()
    {
        for (int i = 0; i < CountFood; i++)
        {
            GameObject food = Instantiate(foodPrefab, new Vector3(Random.Range(-area.x, area.x), Random.Range(-area.y, area.y), 0), Quaternion.identity);
            food.name = "food";
        }
    }

    void FixedUpdate()
    {
        if (SpawnFoodInfinity)
        {
            if (frame % 1 == 0)
            {
                GameObject food = Instantiate(foodPrefab, new Vector3(Random.Range(-area.x, area.x), Random.Range(-area.y, area.y), 0), Quaternion.identity);
                food.name = "food";
            }
        }

        if (SpawnBoidInfinity)
        {
            if (frame % 1 == 0)
            {
                GameObject b = Instantiate(boidPrefab, new Vector3(Random.Range(-area.x, area.x), Random.Range(-area.y, area.y), 0), Quaternion.identity);
                b.GetComponent<Boid>().Dying = true;
                b.name = "Boid";
            }
        }

        frame++;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
