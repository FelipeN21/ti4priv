using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using CFG;
public class Personagem2 : MonoBehaviour
{
    public Transform waypoint1, waypoint2, waypoint3, waypoint4, waypoint5, waypoint6, waypoint7, waypoint8, waypoint9, waypoint10, waypoint11, waypoint12;
    private Vector3 origem, destino;
    float inicio, comprimento, comprimento2;
    int i = 0;
    [SerializeField] private Vector3 _rotation;
    // Start is called before the first frame update
    public class GFG
    {
        
        static int V = 13;
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
               // print(i + " \t\t " + dist[i] + "\n");
                ArrayCaminho1[i+1] = dist[i];
            
            
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


        public static int[] M()
        {
                                              //A1 A2  A3  A4  A5  A6   A7  A8  A9 A10  A11 A12 A13
            int[,] graph = new int[,]{{ 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                      {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                      {0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                      {0, 0, 1, 0, 50, 0, 0, 0, 0, 0, 2, 0, 0},
                                      {0, 0, 0, 50, 0, 3, 0, 0, 0, 0, 0, 0, 0},
                                      {0, 0, 0, 0, 3, 0, 3, 0, 0, 0, 0, 0, 0},
                                      {0, 0, 0, 0, 0, 3, 0, 3, 0, 0, 0, 0, 0},
                                      {0, 0, 0, 0, 0, 0, 3, 0, 3, 0, 0, 0, 0},
                                      {0, 0, 0, 0, 0, 0, 0, 3, 0, 3, 0, 0, 0},
                                      {0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 1},
                                      {0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 4, 0},
                                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 1},
                                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0}}; 

            GFG t = new GFG();
            int[] ArrayCaminho = t.dijkstra(graph, 0);
            return ArrayCaminho;
        }
    }

    void Start()
    {
        origem = this.transform.position;
        inicio = Time.time;
        comprimento = Vector3.Distance(origem, destino);
        comprimento2 = Vector3.Distance(origem, waypoint2.position);

        if (waypoint1.GetComponent<Aresta>().peso * comprimento < waypoint2.GetComponent<Aresta>().peso * comprimento)
        {
            destino = waypoint1.position;

        }
        else
        {
            destino = waypoint2.position;
        }
    }

    // Update is called once per frame
    void Update()
    {//transform.Rotate(_rotation * Time.deltaTime);
        int[] AC = Personagem2.GFG.M();
        int C1 = AC[0] + AC[1] + AC[2] + AC[3] + AC[4] + AC[5] + AC[6] + AC[7] + AC[8] + AC[9];
        int C2 = AC[0] + AC[1] + AC[2] + AC[3] + AC[10] + AC[11];
       // print("C1: " + C1 + " C2 :" + C2);

        float tempo = Time.time - inicio;
         float velocidade = (tempo / comprimento) * 60; 
        
        this.transform.position = Vector3.Lerp(origem, destino, velocidade);
        if(Vector3.Distance(this.transform.position, destino) == 0)
        {
            i++;
            origem = destino;
            if (C1 < C2) { 
            switch (i)
            {
                case 0:
                    destino = waypoint1.position;
                    transform.Rotate(0.0f, 0.0f, 0.0f);
                    break;
                case 1:
                    destino = waypoint2.position;
                    transform.Rotate(0.0f, 0.0f, -90f);
                    break;
                case 2:
                    destino = waypoint3.position;
                    transform.Rotate(0.0f, 0.0f, 270f);
                    break;
                case 3:
                    destino = waypoint4.position;
                    transform.Rotate(0.0f, 0.0f, 90f);
                    break;
                case 4:
                    destino = waypoint5.position;
                    transform.Rotate(0.0f, 0.0f, 90f);
                    break;
                case 5:
                    destino = waypoint6.position;
                    transform.Rotate(0.0f, 0.0f, -90f);
                    break;
                case 6:
                    destino = waypoint7.position;
                    transform.Rotate(0.0f, 0.0f, -90f);
                    break;
                case 7:
                    destino = waypoint8.position;
                    transform.Rotate(0.0f, 0.0f, 90f);
                    break;
                case 8:
                    destino = waypoint9.position;
                    transform.Rotate(0.0f, 0.0f, 90f);
                    break;
                case 9:
                    destino = waypoint10.position;
                    transform.Rotate(0.0f, 0.0f, -90f);
                    break;
                case 10:
                    destino = waypoint11.position;
                    transform.Rotate(0.0f, 0.0f, 0.0f);
                    break;
                case 11:
                    destino = waypoint12.position;
                    transform.Rotate(0.0f, 0.0f, -90f);
                    break;
            }
            }
            if (C2 <= C1 )
            {
                switch (i)
                {
                    case 0:
                        destino = waypoint1.position;
                        transform.Rotate(0.0f, 0.0f, 0.0f);
                        break;
                    case 1:
                        destino = waypoint2.position;
                        transform.Rotate(0.0f, 0.0f, -90f);
                        break;
                    case 2:
                        destino = waypoint3.position;
                        transform.Rotate(0.0f, 0.0f, 270f);
                        break;
                    case 3:
                        destino = waypoint4.position;
                        transform.Rotate(0.0f, 0.0f, 90f);
                        break;
                    case 4:
                        destino = waypoint11.position;
                        transform.Rotate(0.0f, 0.0f, 0.0f);
                        break;
                    case 5:
                        destino = waypoint12.position;
                        transform.Rotate(0.0f, 0.0f, -90f);
                        break;
                }

            }
            if (i == 10 || i == 12) { Destroy(gameObject); }
           
            comprimento = Vector3.Distance(origem, destino);
            inicio = Time.time;

        }
    }
}
