using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeGeneration : MonoBehaviour
{
    public SquareGeneration generationScript;

    public int numberOfBiomes;
    public int startingPointsPerBiome;
    public List<Color> biomeColours = new List<Color>();
    public List<Vector2Int> startingPoints = new List<Vector2Int>();
    public List<Vector2Int>[] biomeSquares;

    public float expansionInterval;

    //public float timer;
    public bool running = true;

    // Start is called before the first frame update
    void Start()
    {
        running = true;
        biomeSquares = new List<Vector2Int>[numberOfBiomes];
        //StartCoroutine(StartBiomes(0.1f));
        StartBiomes();
        /**for (int i = 0; i < 15; i++)
        {
            Debug.Log("FOR LOOP I = " + i);
            ExpandBiomes();
            Debug.Log("END OF FOR LOOP");
        }**/
    }

    private void StartBiomes()
    {
        for (int i = 0; i < numberOfBiomes; i++)
        {
            List<Vector2Int> currentList = new List<Vector2Int>();
            for (int j = 0; j < startingPointsPerBiome; j++)
            {
                bool loop = true;
                while (loop)
                {
                    int x = Random.Range(0, generationScript.xNum + 1);
                    int y = Random.Range(0, generationScript.yNum + 1);
                    Vector2Int v = new Vector2Int(x, y);

                    if (!startingPoints.Contains(v))
                    {
                        try
                        {
                            startingPoints.Add(v);
                            currentList.Add(v);
                            Debug.Log("line1 +" + generationScript.squares[x, y]);
                            Debug.Log("line2 " + biomeColours[i]);
                            generationScript.squares[x, y].GetComponent<SpriteRenderer>().color = biomeColours[i];
                            generationScript.squares[x, y].GetComponent<SquareInfo>().biome = i;
                            loop = false;
                        }
                        catch
                        {
                            loop = true;
                        }
                    }
                }
            }
            biomeSquares[i] = (currentList);
        }
    }


    private bool ExpandBiomes()
    {
        bool space = false;
        for (int i = 0; i < numberOfBiomes; i++)
        {
            if (ExpandBiome(i) == true)
            {
                space = true;
            }
        }
        return space;
    }
    private bool ExpandBiome(int biomeIndex)
    {
        bool loop = true;
        if (biomeSquares[biomeIndex].Count == 0)
        {
            Debug.Log("no loop for biome " + biomeIndex);
            return false;
        }
        while (loop)
        {
            Vector2Int chosen = biomeSquares[biomeIndex][Random.Range(0, biomeSquares[biomeIndex].Count)];
            //Debug.Log("chosen = " + chosen);
            bool innerLoop = true;
            List<Vector2Int> tried = new List<Vector2Int>();
            while (innerLoop && tried.Count < 9)
            {
                //Debug.Log("inner loop, no tests passed yet");
                Vector2Int coord = new Vector2Int(Random.Range(chosen.x - 1, chosen.x + 2), Random.Range(chosen.y - 1, chosen.y + 2));
                if (coord != chosen)
                {
                    //Debug.Log("test 1 passed, != chosen");
                    if (!tried.Contains(coord))
                    {
                        //Debug.Log("test 2 passed, not been tried");
                        try
                        {
                            GameObject square = generationScript.squares[coord.x, coord.y];
                            if (square.GetComponent<SquareInfo>().biome == -1)
                            {
                                //Debug.Log("test 3 passed, biome empty");
                                innerLoop = false;
                                biomeSquares[biomeIndex].Add(coord);
                                square.GetComponent<SquareInfo>().biome = biomeIndex;
                                square.GetComponent<SpriteRenderer>().color = biomeColours[biomeIndex];
                                break;
                            }
                        }
                        catch
                        {
                            innerLoop = true;
                        }
                    }
                }
                tried.Add(coord);
                if (tried.Count == 9)
                {
                    biomeSquares[biomeIndex].Remove(coord);
                    //Debug.Log("removed inner, " + coord);
                }
                //Debug.Log("end of inner loop");
            }

            if (innerLoop == false)
            {
                loop = false;
            }
            //Debug.Log("end of outer loop");
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        /**if (running)
        {
            timer += Time.deltaTime;
        }
        if (timer >= expansionInterval)
        {
            timer = 0;
            if (ExpandBiomes() == false)
            {
                running = false;
            }
        }**/
        if (running)
        {
            if (ExpandBiomes() == false)
            {
                running = false;
            }
        }
    }
}
