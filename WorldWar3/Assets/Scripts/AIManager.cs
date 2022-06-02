using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Link
{
	public GameObject node1;
	public GameObject node2;
}

public class AIManager : MonoBehaviour
{
    int [,] pathMatrix;
    public Link [] edges;
    public GameObject verticesParent;
    GameObject [] vertices;

    private void Start() {
        setVertices();
        initializePathMatrix();
        setPathMatrix();
        printMatrix();
    }

    private void setVertices()
    {
        int index = 0;
        vertices = new GameObject[verticesParent.transform.childCount];
        foreach (Transform child in verticesParent.transform)
        {
            vertices[index++] = child.gameObject;
        }
    }

    private void initializePathMatrix()
    {
        pathMatrix = new int[vertices.Length, vertices.Length];
        for(int i = 0; i < vertices.Length; i++)
        {
            for(int j = 0; j < vertices.Length; j++)
            {
                pathMatrix[i,j] = -1;
            }
        }
    }

    private void setPathMatrix()
    {
        for(int i = 0; i < vertices.Length; i++)
        {
            foreach (int edge in getEdges(vertices[i]))
            {
                pathMatrix[i,edge-1] = 99;
            }
        }
    }

    private int[] getEdges(GameObject vertex)
    {
        List<int> adjacentEdges =  new List<int>();

        foreach (Link pair in edges)
        {
            if (pair.node1.name == vertex.name)
            {
                adjacentEdges.Add(int.Parse(pair.node2.name.Trim('a')));
            }
        }
        
        return adjacentEdges.ToArray();
    }

    public void printMatrix(){
        string buff = "\n";
        for (int i = 0; i < pathMatrix.GetLength(0); i++)
        {
           buff += "[ ";
           for (int j = 0; j < pathMatrix.GetLength(0); j++)
           {
               buff += pathMatrix[i,j].ToString() + " ";
           } 
           buff += " ]\n";
        }
        Debug.Log(buff);
    }
}
