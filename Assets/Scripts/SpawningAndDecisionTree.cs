using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningAndDecisionTree : MonoBehaviour
{
    public GameObject Tank;
    public GameObject peasant;
    Djikstra.Node SpawningPoint;
    int NumberOfTowers;
    int NumberOfCannons;
    int NumberOfSnipers;
    int NumberOfTurrets;
    //chooses how many of each type of unit depending on what types of towers there are.
    public void Spawning()
    {
        CheckTowers();
        Djikstra Starting = GetComponent<Djikstra>();
        SpawningPoint = Starting.NodePath.First.Value;
        int NumberToSpawn = NumberOfTowers / 2;
        if (NumberOfTowers != 0)
        {
            Vector3 SpawnPercents = new Vector3(NumberOfCannons / NumberOfTowers, NumberOfSnipers / NumberOfTowers, NumberOfTurrets / NumberOfTowers);
            if (SpawnPercents.x > SpawnPercents.z && SpawnPercents.y > SpawnPercents.z)
            {
                if (SpawnPercents.x > SpawnPercents.z)
                {
                    Spawn(NumberToSpawn, SpawningPoint.position, new Vector2(.33f, .67f));
                }
                else
                {
                    Spawn(NumberToSpawn, SpawningPoint.position, new Vector2(.67f, .33f));
                }
            }
            else if (SpawnPercents.x < SpawnPercents.z && SpawnPercents.y < SpawnPercents.z)
            {
                Spawn(NumberToSpawn, SpawningPoint.position, new Vector2(.90f, .10f));
            }
            else
            {
                Spawn(NumberToSpawn, SpawningPoint.position, new Vector2(.50f, .50f));
            }
        }
    }
    //used to spawn the enemys.
    void Spawn(int numberSpawning,Vector3 spawnPoint,Vector2 spawnPercents)
    {
        for (int i = 0; i < numberSpawning*spawnPercents.x;i++)
        {
            Instantiate(Tank, spawnPoint, Quaternion.identity);
        }
        for (int i = 0; i < numberSpawning * spawnPercents.y; i++)
        {
            Instantiate(peasant, spawnPoint, Quaternion.identity);
            Instantiate(peasant, spawnPoint, Quaternion.identity);
            Instantiate(peasant, spawnPoint, Quaternion.identity);
        }
    }
    //Checks how many towers there are and there type.
    void CheckTowers()
    {
        GameObject[] s = GameObject.FindGameObjectsWithTag("Turret");
        NumberOfTowers = s.Length;
        for (int i =0; i < s.Length; i++)
        {
            if(s[i].GetComponent<Turret>().type == 0) { NumberOfCannons++; }
            if (s[i].GetComponent<Turret>().type == 1) { NumberOfSnipers++; }
            if (s[i].GetComponent<Turret>().type == 2) { NumberOfTurrets++; }
        }
    }
}
