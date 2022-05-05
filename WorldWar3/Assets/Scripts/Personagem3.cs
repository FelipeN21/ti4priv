using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem3 : MonoBehaviour
{
    public Transform waypoint1, waypoint2, waypoint3, waypoint4, waypoint5, waypoint6, waypoint7, waypoint8, waypoint9, waypoint10, waypoint11, waypoint12;
    private Vector3 origem, destino;
    float inicio, comprimento, comprimento2;
    int i = 0;
    [SerializeField] private Vector3 _rotation;
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
       float tempo = Time.time - inicio;
       float velocidade = (tempo / comprimento) * 60;
       this.transform.position = Vector3.Lerp(origem, destino, velocidade);
        if (Vector3.Distance(this.transform.position, destino) == 0)
        {
            i++;
            origem = destino;
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
           
            comprimento = Vector3.Distance(origem, destino);
            inicio = Time.time;

        }
    }
}
