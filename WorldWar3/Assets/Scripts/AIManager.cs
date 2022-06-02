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
    public static int [,] pathMatrix;
    public Link [] edges;
    public GameObject verticesParent;
    GameObject [] vertices;
    public GameObject spawnersParent;
    public static Spawner[] spanwers;

    private void Start() {
        setSpawners();
        setVertices();
        initializePathMatrix();
        setPathMatrix();
        printMatrix();
        printDjikstra();
    }

    void setSpawners()
    {
        int index = 0;
        spanwers = new Spawner[spawnersParent.transform.childCount];
        foreach (Transform child in spawnersParent.transform)
        {
            spanwers[index++] = child.GetComponent<Spawner>();
        }
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
                pathMatrix[i,j] = 0;
            }
        }
    }

    private void setPathMatrix()
    {
        for(int i = 0; i < vertices.Length; i++)
        {
            foreach (int edge in getEdges(vertices[i]))
            {
                pathMatrix[i,edge-1] = 1;
            }
        }
    }

    private void setPathMatrixSpecial()
    {
        for(int i = 0; i < vertices.Length; i++)
        {
            foreach (int edge in getEdges(vertices[i]))
            {
                pathMatrix[i,edge-1] = 1;
                pathMatrix[edge-1,i] = 1;
            }
        }
        for (int i = 0; i < vertices.Length; i++)
        {
            pathMatrix[i,vertices.Length-1] = 0;
            pathMatrix[vertices.Length-1,i] = 0;
        }
        pathMatrix[vertices.Length-1,vertices.Length-1] = 1;
    }

    public static void placeTower(int towerPos, bool add)
    {        
        List<int> arrayCaminhoMenor = new List<int>() { 2, 13, 14, 15, 16, 9, 6 };

        List<int> arrayCaminhoMaior = new List<int>() { 4, 5, 1, 0, 18, 17 };
        int weight = 5;
        if (!add) { weight  = weight * -1; }


        if (arrayCaminhoMaior.Contains(towerPos))
        {
            adjustWeight(3,4,weight);
        }
        else if(arrayCaminhoMenor.Contains(towerPos))
        {            
            adjustWeight(3,10,weight);
        }

        int [] menoresCaminhos = Djikstra.GFG.M(pathMatrix);
        bool shouldBeShortPath = false;
        if (menoresCaminhos[9] > menoresCaminhos[11])
        {
            shouldBeShortPath = true;
        }

        foreach (Spawner spawner in spanwers){
            spawner.setPath(shouldBeShortPath);
        }
    }

    public static void adjustWeight(int i, int j, int weight)
    {
        pathMatrix[i,j] += weight;
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

    public static void printMatrix(){
        string buff = "\n";
        for (int i = 0; i < pathMatrix.GetLength(0); i++)
        {
           buff += "[ ";
           for (int j = 0; j < pathMatrix.GetLength(0); j++)
           {
               buff += pathMatrix[i,j].ToString() + " ";
           } 
           buff += "]\n";
        }
        Debug.Log(buff);
    }

    public static void printDjikstra(){
        string buff = "\n";
        buff += "[ ";
        
        int [] menoresCaminhos = Djikstra.GFG.M(pathMatrix);
        for (int i = 0; i < menoresCaminhos.Length; i++)
        {
               buff += menoresCaminhos[i].ToString() + " ";
        }
        buff += " ]\n";
        Debug.Log(buff);
    }
}
