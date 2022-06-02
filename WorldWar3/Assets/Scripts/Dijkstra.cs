using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Dijkstra : MonoBehaviour
{


    public static int[,] Ma2132131trix = new int[,]{{ 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                            { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                            { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                            { 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0},
                                            { 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0},
                                            { 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0},
                                            { 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0},
                                            { 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0},
                                            { 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0},
                                            { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1},
                                            { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                                            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1} };
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public class GFG
    {

        static int V = 12;
        int minDistance(int[] dist,
                        bool[] sptSet)
        {
            // Initialize min value
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < V; v++)
                if (sptSet[v] == false && dist[v] <= min)
                {
                    min = dist[v];
                    min_index = v;
                }

            return min_index;
        }


        int[] printSolution(int[] dist, int n)
        {
            int[] ArrayCaminho1 = new int[50];
            // print("Vertex     Distance "+ "from Source\n");
            for (int i = 0; i < V; i++)
            {
                //print(i + " \t\t " + dist[i] + "\n");
                ArrayCaminho1[i + 1] = dist[i];


            }
            return ArrayCaminho1;
        }


        int[] dijkstra(int[,] graph, int src)
        {
            int[] dist = new int[V];

            bool[] sptSet = new bool[V];


            for (int i = 0; i < V; i++)
            {
                dist[i] = int.MaxValue;
                sptSet[i] = false;
            }


            dist[src] = 0;


            for (int count = 0; count < V - 1; count++)
            {

                int u = minDistance(dist, sptSet);


                sptSet[u] = true;


                for (int v = 0; v < V; v++)


                    if (!sptSet[v] && graph[u, v] != 0 &&
                         dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                        dist[v] = dist[u] + graph[u, v];
            }


            return (printSolution(dist, V));
        }


        public static int[] M(int[,] Matrix)
        {
            //A1 A2  A3  A4  A5  A6   A7  A8  A9 A10  A11 A12 A13
            int[,] graph = new int[,]{{ 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                      {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                      {0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                      {0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0},
                                      {0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0},
                                      {0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0},
                                      {0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0},
                                      {0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0},
                                      {0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0},
                                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1},
                                      {0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1} };

            GFG t = new GFG();
            int[] ArrayCaminho = t.dijkstra(Matrix, 0);
            return ArrayCaminho;
        }
    }





    public static int[,] MudarMatriz(int[,] Matrix)
    {
        List<int> arrayCaminhoMenor = new List<int>() { 2, 13, 14, 15, 16, 9, 6 };

        List<int> arrayCaminhoMaior = new List<int>() { 4, 5, 1, 0, 18, 17 };

        foreach (KeyValuePair<int, GameObject> x in TurretManager.placedTurrets)
        {
            if (arrayCaminhoMenor.Contains(x.Key))
            {

                if (x.Key == 2)
                {

                    Matrix[2, 1] += 10;
                    Matrix[1, 2] += 10;
                }
                else if (x.Key == 13)
                {

                    Matrix[3, 10] += 10;
                    Matrix[10, 3] += 10;

                }
                else if (x.Key == 14)
                {

                    Matrix[3, 10] += 10;
                    Matrix[10, 3] += 10;
                }
                else if (x.Key == 15)
                {

                    Matrix[3, 10] += 10;
                    Matrix[10, 3] += 10;
                }
                else if (x.Key == 16)
                {
                    Matrix[10, 11] += 10;
                    Matrix[11, 10] += 10;
                }
                else if (x.Key == 9)
                {
                    Matrix[3, 10] += 10;
                    Matrix[10, 3] += 10;
                }
                else
                {
                    Matrix[3, 10] += 10;
                    Matrix[10, 3] += 10;
                }


            }
            else if (arrayCaminhoMaior.Contains(x.Key))
            {

                if (x.Key == 4)
                {
                    Matrix[3, 4] += 10;
                    Matrix[4, 3] += 10;
                }
                else if (x.Key == 5)
                {
                    Matrix[4, 5] += 10;
                    Matrix[5, 4] += 10;
                }
                else if (x.Key == 1)
                {
                    Matrix[5, 6] += 10;
                     Matrix[6, 5] += 10;
                }
                else if (x.Key == 0)
                {

                    Matrix[6, 7] += 10;
                    Matrix[7, 6] += 10;
                }
                else if (x.Key == 18)
                {
                    Matrix[7, 8] += 10;
                    Matrix[8, 7] += 10;
                }
                else if (x.Key == 17)
                {
                    Matrix[8, 9] += 10;
                     Matrix[9, 8] += 10;
                }

            }
        }

        return Matrix;

    }



}


