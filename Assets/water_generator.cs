using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water_generator : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    MeshCollider meshCollider;
    int[] triangles;

    public int xSize = 100;
    public int zSize = 100;

    public static int River_Max = 0;
    public static int River_Min = 100;
    public static int River_Start;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();


    }
    void CreateShape()
    {
        //
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        int river_index = 0;
        int river_straight_away_length = (int)(Random.value * 10);
        int temp_river = 0;
        while (river_index < 30 | river_index > 70)
        {
            river_index = (int)(Random.value * xSize);
        }
        river_index = 50;
        River_Start = river_index;
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y;
                if (x == river_index)
                {
                    y = -1;
                    temp_river++;
                    if (temp_river >= river_straight_away_length)
                    {
                        if (Random.value > 0.5)
                        {
                            river_index += 1;
                        }
                        else
                        {
                            river_index -= 1;
                        }
                        temp_river = 0;
                        river_straight_away_length = (int)(Random.value * 10);
                        if (river_index <= River_Min)
                            River_Min = river_index;
                        if (river_index >= River_Max)
                            River_Max = river_index;
                    }
                }
                else
                    y = 0;
                if (x == 0 || x == xSize || z == 0 || z == zSize)
                {
                    y = 0;
                }
                vertices[i] = new Vector3(x * 10, y, z*10);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;

                //yield return new WaitForSeconds(0.05f);
            }
            vert++;
        }
    }
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        GetComponent<MeshCollider>().sharedMesh = mesh;

    }
    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }

}
