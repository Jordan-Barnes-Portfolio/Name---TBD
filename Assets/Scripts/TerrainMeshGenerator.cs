using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TerrainMeshGenerator : MonoBehaviour
{
    //instantiating a reference for a mesh
    Mesh mesh;
    

    //creating a vector 3 array and integer array for storing our vertices and triangles
    Vector3[] vertices;
    int[] triangles;

    //defining the grid size by x and z

    public int xSize = 20;
    public int zSize = 20;
    public float scale;

    Color[] colors;
    public Gradient gradient;

    private void Update()
    {
        //defining a new mesh within our reference above
        mesh = new Mesh();
        

        //pulling the meshfilter component from our mesh
        GetComponent<MeshFilter>().mesh = mesh;

        //initiating our functions created below to create the shape and mesh
        CreateShape();
        UpdateMesh();
    }

    //creating the shape of the mesh by defining each vertex point
    void CreateShape()
    {
 
        //creating a array of vertices based on the defined size above in xSize and zSize
        //we need to multiply xSize + 1 by zSize + 1 BECAUSE our array starts at 0, but our point in space starts at 1,
        //and we are defining the length and width of an object (which is length * width)

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        //here we loop through each point in our mesh populating it with vertices
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                //TODO: Add noise generation into the y value
                vertices[i] = new Vector3(x, Mathf.PerlinNoise(xSize, zSize) * scale, z);
                i++;
            }
        }


        //defining a triangles array of our mesh size multiplied by 6
        //We multiply the array by 6 because we need to define the amount of points within the mesh to be populated
        //


        triangles = new int[xSize * zSize * 6];

        //giving each vertex and triangle a bookmark to be updated within our for loop below for population purposes
        //defined by multiplying by 6 because each cubic plane requires 6 points of
        //contact to be populated within our mesh
        // 0, 1 , 2, 3, 4, 5
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
            }
            vert++;
        }
        //a for loop that moves through each vertex in our mesh and provides it with an index for the triangle population

        colors = new Color[vertices.Length];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                //TODO: Add noise generation into the y value
                float height = vertices[i].y;
                colors[i] = gradient.Evaluate(height);
                i++;
            }
        }

    }



    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;

        mesh.RecalculateNormals();
    }

}





