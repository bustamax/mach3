using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeGrid : MonoBehaviour
{
	
public GameObject[] mass = new GameObject[4];
public GameObject startP;
public float startPosX;
public float startPosY;
public int countX;
public int countY;
public float outX;
public float outY;
public string objName = "Block_";
private int id;
private GameObject[,] grid;
	
    // Start is called before the first frame update
void Start () 
{
	id = 0;
	float posXreset = startPosX;
	grid = new GameObject[countX,countY];
	for(int y = 0; y < countY; y++)
	{
		startPosY += outY;
		for(int x = 0; x < countX; x++)
		{
			id++;
			startPosX += outX;
			grid[x,y] = Instantiate(mass[Random.Range(0, mass.Length)], new Vector2(startPosX,startPosY), Quaternion.identity) as GameObject;
			grid[x,y].name = objName + id;
			grid[x,y].transform.parent = transform;
		}
		startPosX = posXreset;
	}
}

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void generate(float xpos, float ypos){
		Instantiate(mass[Random.Range(0, mass.Length)], new Vector2(xpos,ypos+10f), Quaternion.identity);
	}
	
}
