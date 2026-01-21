using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public GameObject FirstCharacter;
    public GameObject SecondCharacter;
    public Renderer FirstCharRend;
    public Renderer SecondCharRend;
    public NavMeshAgent FirstCharNavAG;
    public NavMeshAgent SecondCharNavAG;
    public Transform obstacle1;
    public Transform obstacle2;
    private bool ControlsFirst = true;
    private bool ControlsSecond = true;

    private void Update()
    {
        Console.Write("tick");
    }

    private void Start()
    {
        Console.WriteLine("start of game");
        FirstCharRend.material.color = Color.white;
        SecondCharRend.material.color = Color.black;
        StartCoroutine(PlayerInput());
    }
    //apres avoir rajouter IEnumerator, je ne ressois plus aucun des console.write / console.writeline, aucune erreur dans la console sauf pour un type de host avec la rendering pipeline
    //le code ici devrais normalement fonctioner sauf pour le retour vers la couleur originel car ne me souvenant plus et n'ayent pas trouver une métode fonctionnel (essais avec NavMeshAgent.destination, isstoped et pathStatus)
    //et le retour des controles apres l'aproche d'un monolith
    IEnumerator PlayerInput()
    {
        Console.WriteLine("player can input");
        if (Input.GetMouseButton(0) & ControlsFirst)
        {
            Console.WriteLine("player moved c1");
            FirstCharRend.material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            //Change couleur
            yield return PlayerMovement(FirstCharNavAG, FirstCharacter, ControlsFirst);
            //déplace

        }
        if (Input.GetMouseButton(1) & ControlsSecond)
        {
            Console.WriteLine("player moved c2");
            SecondCharRend.material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            //Change couleur
            yield return PlayerMovement(SecondCharNavAG, SecondCharacter, ControlsSecond);
            //déplace

        }

    }
    IEnumerator PlayerMovement(NavMeshAgent myNavMeshAgent, GameObject character, bool control)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Console.WriteLine("hit something");
            myNavMeshAgent.SetDestination(hit.point);
        }
        if (Vector3.Distance(obstacle1.position, transform.position) > Vector3.Distance(obstacle2.position, transform.position))
        {
            //detecte si il est plus proche du 1er ou 2em monolith
            if (Vector3.Distance(obstacle1.position, transform.position) < 3)
            {
                //detecte si il est asser proche du 1er monolith
                Console.WriteLine("in the 1stmonolith's grasp");
                control = false;
                myNavMeshAgent.SetDestination(obstacle2.position);
                //Déplace vers le 2em
            }

        }
        else
        {
            if (Vector3.Distance(obstacle2.position, transform.position) < 3)
            {
                //detecte si il est asser proche du 2em monolith
                Console.WriteLine("in the 2ndmonolith's grasp");
                control = false;
                myNavMeshAgent.SetDestination(obstacle1.position);
                //Déplace vers le 1er
            }
        }
        yield return new WaitForEndOfFrame();
    }
   

}
