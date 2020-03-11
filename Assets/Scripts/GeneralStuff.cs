using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStuff : MonoBehaviour
{
    public bool MainTowerPlaced = false;
    public bool Started = false;
    public bool lost = false;
    public int playerHealth = 100;
    public GameObject MainTower;
    public GameObject Wall;
    public GameObject Sniper;
    public GameObject Turret;
    public GameObject Cannon;
    public GameObject AIControler;
    Djikstra DJ;
    SpawningAndDecisionTree Spawner;
    // Start is called before the first frame update
    void Start()
    {
        AIControler = GameObject.FindGameObjectWithTag("EventSystem");
        Spawner = AIControler.GetComponent<SpawningAndDecisionTree>();
        DJ = AIControler.GetComponent<Djikstra>();
    }

    // Update is called once per frame
    void Update()
    {

            if (Started == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray;
                    RaycastHit hit;
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit))
                    {
                        GameObject Dropdown = GameObject.FindGameObjectWithTag("Dropdown");
                        Tower Tpain = Dropdown.GetComponent<Tower>();
                        if (hit.transform.tag == "Grid")
                        {
                        if (MainTowerPlaced == true)
                        {
                            if (Tpain.selected == 0 && hit.collider.gameObject.GetComponent<NodeDisplayScript>().node.IsWall != true)
                            {
                                GameObject a = Instantiate(Wall, new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z), hit.collider.transform.rotation);
                                Wall b = a.GetComponent<Wall>();
                                b.Node = hit.collider.gameObject;
                                hit.collider.gameObject.GetComponent<NodeDisplayScript>().node.IsWall = true;
                            }
                        }
                        else
                        {
                            GameObject a = Instantiate(MainTower, new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z), hit.collider.transform.rotation);
                            MainTowerPlaced = true;
                        }
                        }
                        if (hit.transform.tag == "Wall")
                        {
                            if (Tpain.selected == 1 && hit.collider.gameObject.GetComponent<Wall>().hasTurret != true)
                            {
                                Instantiate(Turret, new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z), hit.collider.transform.rotation, hit.collider.gameObject.transform);
                                hit.collider.gameObject.GetComponent<Wall>().hasTurret = true;
                            }
                            if (Tpain.selected == 2 && hit.collider.gameObject.GetComponent<Wall>().hasTurret != true)
                            {
                                Instantiate(Sniper, new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z), hit.collider.transform.rotation, hit.collider.gameObject.transform);
                                hit.collider.gameObject.GetComponent<Wall>().hasTurret = true;
                            }
                            if (Tpain.selected == 3 && hit.collider.gameObject.GetComponent<Wall>().hasTurret != true)
                            {
                                Instantiate(Cannon, new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z), hit.collider.transform.rotation, hit.collider.gameObject.transform);
                                hit.collider.gameObject.GetComponent<Wall>().hasTurret = true;
                            }
                            if (Tpain.selected == 4)
                            {
                                hit.collider.gameObject.GetComponent<Wall>().Node.GetComponent<NodeDisplayScript>().node.IsWall = false;
                                Destroy(hit.collider.gameObject);
                            }
                        }
                        if (hit.transform.tag == "Turret")
                        {
                            if (Tpain.selected == 4)
                            {
                                hit.collider.gameObject.GetComponentInParent<Wall>().hasTurret = false;
                                Destroy(hit.collider.gameObject);
                            }
                        }
                    }

                }
            }
            if (Started == true)
            {
                
            }
        

    }

    public void start()
    {
        if (MainTowerPlaced)
        {
            if (Started == false)
            {
                Started = true;
                DJ.FindPath();
                Spawner.Spawning();
                DJ.Move();
            }
        }
    }
}
