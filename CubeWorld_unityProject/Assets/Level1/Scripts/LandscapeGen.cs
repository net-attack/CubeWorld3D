using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public int type;
    public bool vis;
    public GameObject block;

    public Block(int t, bool v, GameObject b)
    {
        type = t;
        vis = v;
        block = b;
    }
}


public class LandscapeGen : MonoBehaviour {
    public static int width = 128;
    public static int height= 128;
    public static int depth = 128;
    public float detailScale = 20.0f;
    public int heightOffset = 100;
    public int heightDiff = 18;
    

    public GameObject grassPrefab;
    public GameObject stonePrefab;
    public GameObject floor;
    private GameObject player = null;

    private int maxheight = 0 ;


    Block[,,] worldBlocks = new Block[width, height, depth];
    // Use this for initialization
    void Start () {
        int seed = (int)Network.time * 10;
        
        
        for (int z = 0; z < depth; z++)
        {
            for(int x = 0; x < width; x++)
            {
                Instantiate(floor, new Vector3(x, heightOffset, z), Quaternion.identity);
                //Debug.Log(Mathf.PerlinNoise((x + seed) / detailScale, (z + seed) / detailScale));
                int y = (int)(Mathf.PerlinNoise((x+seed) / detailScale, (z+seed) / detailScale) * heightDiff + heightOffset);
                if (y >= height)
                {
                    y = height;
                }
                Vector3 blockPos = new Vector3(x, y, z);
                CreateBlock(y, blockPos, true);
                Debug.Log(Mathf.PerlinNoise((x + seed) / detailScale, (z + seed)/detailScale  ) * heightDiff + heightOffset);
                if (y > maxheight)
                {
                    maxheight = y;
                }
                while(y > 0)
                {
                    y--;
                    blockPos = new Vector3(x, y, z);
                    CreateBlock(y, blockPos, false);
                }
            }
        }
        player = GameObject.Find("Player");
        Vector3 playPos = new Vector3(width / 2, maxheight + 10, depth / 2);
        player.transform.position = playPos;
        
    }
	
	// Update is called once per frame
	void CreateBlock (int y, Vector3 blockPos, bool create) {
        GameObject newBlock = null;
        //Debug.Log(y);
        if(y < heightOffset)
        {

        }
        else
        {
            if(y > (int)(heightOffset + heightDiff * 1 / 3))
            {
                if (create)
                {
                    newBlock = (GameObject) Instantiate(grassPrefab, blockPos, Quaternion.identity);
                    worldBlocks[(int)blockPos.x, (int)blockPos.y- heightOffset, (int)blockPos.z] = new Block(1, create, newBlock);
                }
            }
            else if(y < (int)(heightOffset + heightDiff * 1 / 3))
            {
                if (create)
                {
                    newBlock = (GameObject)Instantiate(stonePrefab, blockPos, Quaternion.identity);
                    Debug.Log(blockPos);
                    worldBlocks[(int)blockPos.x, (int)blockPos.y- heightOffset, (int)blockPos.z] = new Block(2, create, newBlock);
                }
            } else
            {
                newBlock = (GameObject)Instantiate(stonePrefab, blockPos, Quaternion.identity);
                worldBlocks[(int)blockPos.x, (int)blockPos.y- heightOffset, (int)blockPos.z] = new Block(2, create, newBlock);
            }
        }
		
	}
}
