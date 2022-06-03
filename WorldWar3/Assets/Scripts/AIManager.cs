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

    
    public static bool DjikstraON = true;
    public static int[,] pathMatrix;
    public Link[] edges;
    public GameObject verticesParent;
    GameObject[] vertices;
    public GameObject spawnersParent;
    public static Spawner[] spanwers;
    public UIManager UIMgassign;
    public static UIManager UIMg;

    private void Start()
    {
        setUIManager();
        setSpawners();
        setVertices();
        initializePathMatrix();
        setPathMatrix();
        printMatrix();
        printDjikstra();
    }

    void setUIManager()
    {
        UIMg = UIMgassign;
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
        for (int i = 0; i < vertices.Length; i++)
        {
            for (int j = 0; j < vertices.Length; j++)
            {
                pathMatrix[i, j] = 0;
            }
        }
    }

    private void setPathMatrix()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            foreach (int edge in getEdges(vertices[i]))
            {
                pathMatrix[i, edge - 1] = 1;
            }
        }
    }

    private void setPathMatrixSpecial()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            foreach (int edge in getEdges(vertices[i]))
            {
                pathMatrix[i, edge - 1] = 1;
                pathMatrix[edge - 1, i] = 1;
            }
        }
        for (int i = 0; i < vertices.Length; i++)
        {
            pathMatrix[i, vertices.Length - 1] = 0;
            pathMatrix[vertices.Length - 1, i] = 0;
        }
        pathMatrix[vertices.Length - 1, vertices.Length - 1] = 1;
    }

    private static void setPairedWeigth(int towerPos)
    {
        List<int> arrayCaminhoMenor = new List<int>() { 2, 13, 14, 15, 16, 9, 6 };
        List<int> arrayCaminhoMaior = new List<int>() { 4, 5, 1, 0, 18, 17 };
        int weight = 10;
        foreach (KeyValuePair<int, GameObject> x in TurretManager.placedTurrets)
        {
            if (arrayCaminhoMenor.Contains(x.Key))
            {
                if (x.Key == 2)
                {
                    adjustWeight(2, 1, weight);
                    adjustWeight(1, 2, weight);
                }
                else if (x.Key == 13)
                {

                    adjustWeight(3, 10, weight);
                    adjustWeight(10, 3, weight);

                }
                else if (x.Key == 14)
                {

                    adjustWeight(3, 10, weight);
                    adjustWeight(10, 3, weight);
                }
                else if (x.Key == 15)
                {

                    adjustWeight(3, 10, weight);
                    adjustWeight(10, 3, weight);
                }
                else if (x.Key == 16)
                {
                    adjustWeight(10, 11, weight);
                    adjustWeight(11, 10, weight);
                }
                else if (x.Key == 9)
                {
                    adjustWeight(3, 10, weight);
                    adjustWeight(10, 3, weight);
                }
                else
                {
                    adjustWeight(3, 10, weight);
                    adjustWeight(10, 3, weight);
                }


            }
            else if (arrayCaminhoMaior.Contains(x.Key))
            {

                if (x.Key == 4)
                {
                    adjustWeight(3, 4, weight);
                    adjustWeight(4, 3, weight);
                }
                else if (x.Key == 5)
                {
                    adjustWeight(4, 5, weight);
                    adjustWeight(5, 4, weight);
                }
                else if (x.Key == 1)
                {
                    adjustWeight(5, 6, weight);
                    adjustWeight(6, 5, weight);
                }
                else if (x.Key == 0)
                {

                    adjustWeight(6, 7, weight);
                    adjustWeight(7, 6, weight);
                }
                else if (x.Key == 18)
                {
                    adjustWeight(7, 8, weight);
                    adjustWeight(8, 7, weight);
                }
                else if (x.Key == 17)
                {
                    adjustWeight(8, 9, weight);
                    adjustWeight(9, 8, weight);
                }
            }
        }
    }

    public static void placeTower(int towerPos, bool add)
    {

        int weight = 5;
        if (!add) { weight = weight * -1; }

        setPairedWeigth(towerPos);

        int[] menoresCaminhos = Djikstra.GFG.M(pathMatrix);
        bool shouldBeShortPath = false;

        if (DjikstraON)
        {
            if (menoresCaminhos[9] > menoresCaminhos[11])
            {
                shouldBeShortPath = true;
            }
        }
        else
        {
            shouldBeShortPath = funcaoficticaA(menoresCaminhos);
        }

        foreach (Spawner spawner in spanwers)
        {
            spawner.setPath(shouldBeShortPath);
        }
    }

    public static void changeAIs()
    {
        DjikstraON = !DjikstraON;

        int[] menoresCaminhos = Djikstra.GFG.M(pathMatrix);
        bool shouldBeShortPath = false;

        if (DjikstraON)
        {
            if (menoresCaminhos[9] > menoresCaminhos[11])
            {
                shouldBeShortPath = true;
            }
            UIMg.showAI(true);
        }
        else
        {
            shouldBeShortPath = funcaoficticaA(menoresCaminhos);
            UIMg.showAI(false);
        }

        foreach (Spawner spawner in spanwers)
        {
            spawner.setPath(shouldBeShortPath);
        }
    }

    public static void adjustWeight(int i, int j, int weight)
    {
        pathMatrix[i, j] += weight;
    }

    private int[] getEdges(GameObject vertex)
    {
        List<int> adjacentEdges = new List<int>();

        foreach (Link pair in edges)
        {
            if (pair.node1.name == vertex.name)
            {
                adjacentEdges.Add(int.Parse(pair.node2.name.Trim('a')));
            }
        }

        return adjacentEdges.ToArray();
    }

    public static void printMatrix()
    {
        string buff = "\n";
        for (int i = 0; i < pathMatrix.GetLength(0); i++)
        {
            buff += "[ ";
            for (int j = 0; j < pathMatrix.GetLength(0); j++)
            {
                buff += pathMatrix[i, j].ToString() + " ";
            }
            buff += "]\n";
        }
        Debug.Log(buff);
    }

    public static void printDjikstra()
    {
        string buff = "\n";
        buff += "[ ";

        int[] menoresCaminhos = Djikstra.GFG.M(pathMatrix);
        for (int i = 0; i < menoresCaminhos.Length; i++)
        {
            buff += menoresCaminhos[i].ToString() + " ";
        }
        buff += " ]\n";
        Debug.Log(buff);
    }


    public static bool funcaoficticaA(int[] menoresCaminhos)
    {

        List<int> arrayCaminhoMenor = new List<int>() { 2, 13, 14, 15, 16, 9, 6 };

        List<int> arrayCaminhoMaior = new List<int>() { 4, 5, 1, 0, 18, 17 };

        float turretFireR = 5f;
        float fireCountdownA = 1f;

        float rangeT = 32f;
        float turnSpeedT = 10f;

        float mediaVelo = 48f;

        float mediaVida = 625f;

        float valorDano = 10f;




        float quantidadeTiroporS = turretFireR / fireCountdownA;

        float quantoTeempoFicanoRange = rangeT / mediaVelo;

        float danorecebido = valorDano * (quantoTeempoFicanoRange / quantidadeTiroporS);

        int weight = (int)(danorecebido - mediaVida);
        foreach (KeyValuePair<int, GameObject> x in TurretManager.placedTurrets)
        {

            if (arrayCaminhoMenor.Contains(x.Key))
            {
                if (x.Key == 2)
                {
                    menoresCaminhos[4] += weight;
                }
                else if (x.Key == 13)
                {

                    menoresCaminhos[4] += weight;

                }
                else if (x.Key == 14)
                {

                    menoresCaminhos[4] += weight;
                }
                else if (x.Key == 15)
                {

                    menoresCaminhos[4] += weight;
                }
                else if (x.Key == 16)
                {
                    menoresCaminhos[11] += weight;
                }
                else if (x.Key == 9)
                {
                    menoresCaminhos[11] += weight;
                }
                else
                {
                    menoresCaminhos[12] += weight;
                }


            }
            else if (arrayCaminhoMaior.Contains(x.Key))
            {

                if (x.Key == 4)
                {
                    menoresCaminhos[5] += weight;
                }
                else if (x.Key == 5)
                {
                    menoresCaminhos[6] += weight;
                }
                else if (x.Key == 1)
                {
                    menoresCaminhos[6] += weight;
                }
                else if (x.Key == 0)
                {

                    menoresCaminhos[7] += weight;
                }
                else if (x.Key == 18)
                {

                    menoresCaminhos[9] += weight;
                }
                else if (x.Key == 17)
                {
                    menoresCaminhos[9] += weight;
                }
            }


        }

        int caminhoLongo = menoresCaminhos[4] + menoresCaminhos[5] + menoresCaminhos[6] + menoresCaminhos[7] + menoresCaminhos[8] + menoresCaminhos[9] + menoresCaminhos[10];
        int camiCurto = menoresCaminhos[4] + menoresCaminhos[11] ;

        if (caminhoLongo < camiCurto)
        {
            return false;
        }

        return true;


    }
}