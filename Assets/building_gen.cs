using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class building_gen : MonoBehaviour
{
    public GameObject h1;
    public GameObject h2;
    public GameObject h3;
    public GameObject h4;
    public GameObject h5;
    public int max_houses = 30;

    public int grid_width;
    public int grid_length;
    public int grid_elements = 100;

    private int house_here = 1;
    private int un_explored = 0;
    private int neighbors = -1;

    private int curr_together = 0;
    


    private void generate_buildings()
    {
        int[] grid;
        int i;
        int house_count = 0;
        int house_type;
        int barncount = 0;
        grid = new int[grid_elements];
        //fill grid with 0s
        for (i = 0; i <100; i++)
        {
            grid[i] = 0;
        }
        while (house_count < max_houses)
        {
            //search for neighbor tiles
            List<int> temp = new List<int>();
            for (i = 0; i<100; i++)
            {
                if (grid[i] == neighbors)
                {
                    temp.Add(i);
                }
            }
            for (i = 0; i<temp.Count; i++)
            {
                float rand = Random.value;
                if (rand >= 0.5)
                {
                    house_type = Random.Range(2, 5);
                    if (house_type == 4 )
                    {
                        barncount += 1;
                        if (barncount >= 4)
                        {
                            continue;
                        }
                        else
                        {
                            if (temp[i] + 1 < 100 && temp[i] + 1 > -1 && grid[temp[i] + 1] == 0)
                            {
                                grid[temp[i] + 1] = 0;
                            }
                            if (temp[i] - 1 < 100 && temp[i] - 1 > -1 && grid[temp[i] - 1] == 0)
                            {
                                grid[temp[i] - 1] = 0;
                            }
                            if (temp[i] + 10 < 100 && temp[i] + 10 > -1 && grid[temp[i] + 10] == 0)
                            {
                                grid[temp[i] + 10] = 0;
                            }
                            if (temp[i] - 10 < 100 && temp[i] - 10 > -1 && grid[temp[i] - 10] == 0)
                            {
                                grid[temp[i] - 10] = 0;
                            }

                        }
                    }
                    grid[temp[i]] = house_type;
                    house_count += 1;
                    curr_together += 1;

                    
                    //pick a new direction to start generating
                    if (curr_together >= 6)
                    {
                        i = 0;
                        curr_together = 0;
                    }
                    if (temp[i] + 1 < 100 && temp[i] + 1 > -1 && grid[temp[i] + 1] == 0)
                    {
                        grid[temp[i] + 1] = -1;
                    }
                    if (temp[i] - 1 < 100 && temp[i] - 1 > -1 && grid[temp[i] - 1] == 0)
                    {
                        grid[temp[i] - 1] = -1;
                    }
                    if (temp[i] + 10 < 100 && temp[i] + 10 > -1 && grid[temp[i] + 10] == 0)
                    {
                        grid[temp[i] + 10] = -1;
                    }
                    if (temp[i] - 10 < 100 && temp[i] - 10 > -1 && grid[temp[i] - 10] == 0)
                    {
                        grid[temp[i] - 10] = -1;
                    }
                }

            }
            if (temp.Count == 0)
            {
                float rand_1 = Random.value * 100;
                int toInt = (int)rand_1;
                toInt = toInt % 10;
                grid[toInt] = house_here;
                house_count += 1;
                curr_together += 1;         
                if (toInt + 1 < 100 && toInt + 1 > -1 && grid[toInt + 1] == 0)
                {
                    grid[toInt + 1] = 2;
                }
                if (toInt - 1 < 100 && toInt - 1 > -1 && grid[toInt - 1] == 0)
                {
                    grid[toInt - 1] = 5;
                }
                if (toInt + 10 < 100 && toInt + 10 > -1 && grid[toInt + 10] == 0)
                {
                    grid[toInt + 10] = -1;
                }
                if (toInt - 10 < 100 && toInt - 10 > -1 && grid[toInt - 10] == 0)
                {
                    grid[toInt - 10] = -1;
                }
            }
            temp.Clear();
        }
        //create houses from grid
        for (i = 0; i < 100; i++)
        {
            float randx, randy;
            if (grid[i] == 1)
            {
                Instantiate(h1, new Vector3(transform.position.x + i/10 + i%10 * grid_width, 2.313424f,transform.position.z + i/10 * grid_length - 40), new Quaternion(Quaternion.identity.x, 180, Quaternion.identity.z, Quaternion.identity.w));
            }

            else if (grid[i] == 2)
            {
                randx = Random.value * 10;
                randy = Random.value * 10;
                Instantiate(h2, new Vector3(transform.position.x + i / 10 + i % 10 * grid_width + randx, 0f, transform.position.z + i / 10 * grid_length + randy), Quaternion.identity);
            }

            else if (grid[i] == 3)
            {
                randx = Random.value * 10;
                randy = Random.value * 10;
                Instantiate(h3, new Vector3(transform.position.x + i / 10 + i % 10 * grid_width + randx, 5.33f, transform.position.z + i / 10 * grid_length + randy), Quaternion.identity);
            }

            else if (grid[i] == 4)
            {

                Instantiate(h4, new Vector3(transform.position.x + i / 10 + i % 10 * grid_width , 10.8f, transform.position.z + i / 10 * grid_length), Quaternion.identity);
            }

            else if (grid[i] == 5)
            {
                randx = Random.value * 10;
                randy = Random.value * 10;
                Instantiate(h5, new Vector3(transform.position.x + i / 10 + i % 10 * grid_width + randx, 2.5f, transform.position.z + i / 10 * grid_length + randy), Quaternion.identity);
            }


        }
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            var clones = GameObject.FindGameObjectsWithTag("Generated");
            foreach (var clone in clones)
            {
                Destroy(clone);
            }
            print("Generating Houses");
            generate_buildings();
        }
    }
}
