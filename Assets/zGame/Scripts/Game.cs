using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Game : MonoBehaviour
{
    public static Game inst;
    public GameObject gridPrefab;
    public GameObject characterPrefab;
    public List<Color> colors = new List<Color>();
    public List<Transform> startGrids = new List<Transform>();
    public List<Transform> characters = new List<Transform>();
    public CameraController camCtrl;

    Transform player;
    // Start is called before the first frame update
    private void Awake()
    {
        inst = this;
    }
    void Start()
    {
        createCharacters();
        camCtrl.init(player);
        StartCoroutine(startGame());
        
    }
    IEnumerator startGame()
    {


        player.gameObject.SetActive(true);
        player.GetComponent<PlayerInput>().enabled = false;
        List<Transform> enemys = new List<Transform>(characters);
        enemys.Remove(player);
        
        for(int i = 0; i < enemys.Count; i++)
        {
            enemys[i].gameObject.SetActive(true);
            enemys[i].GetComponent<AI>().enabled = false;
            yield return new WaitForSeconds(0.1f);
        }

        //板子开始消失
        startGrids.ForEach(grid =>
        {
            grid.Find("collider").GetComponent<Grid>().manualDie();
        });
        //玩家脚本和敌人ai
        yield return new WaitForSeconds(1.6f);
        player.GetComponent<PlayerInput>().enabled = true;
        enemys.ForEach(e =>
        {
            e.GetComponent<AI>().enabled = true;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void createMap()
    {
        for(int i = 0; i < 5; i +=1)
        {
            createOneFloor(i);
        }
        createStartFloor();
    }

    //生成地图
    public void createOneFloor(int index)
    {
        GameObject map=GameObject.Find("map");
        if (map == null)
        {
            map = new GameObject("map");
        }

        int y = index * 10;
        int xNum = 15;
        int zNum = 9;
        float xDis = Mathf.Cos(30f / 180 * Mathf.PI) * 2; //x间距
        float zDis = (2 - Mathf.Sin(30f / 180 * Mathf.PI));//y间距
        float xStart = -xNum * xDis/2 + xDis / 2;
        float zStart = -zNum * zDis/2 + zDis / 2; 

        var floor=new GameObject("floor"+index);
        floor.transform.SetParent(map.transform);
        for(int z = 0; z < zNum; z++)
        {
            for(int x = 0; x < xNum; x++)
            {
                var grid=Instantiate(gridPrefab,floor.transform);
                float _x = 0;
                if (z % 2 == 0)
                {
                    _x = x * xDis;
                }
                else
                {
                    _x = x * xDis + xDis / 2;
                }
                float _z = z * zDis;
                grid.transform.position = new Vector3(xStart+_x, y, zStart+_z);
                grid.transform.Find("collider").GetComponent<Grid>().floor = index;
            }
        }
    }
    void createStartFloor()
    {
        startGrids.Clear();
        int index = 5;
        GameObject map = GameObject.Find("map");
        if (map == null)
        {
            map = new GameObject("map");
        }
        int y = 50;
        int xNum = 15;
        int zNum = 9;
        float xDis = Mathf.Cos(30f / 180 * Mathf.PI) * 2; //x间距
        float zDis = (2 - Mathf.Sin(30f / 180 * Mathf.PI));//y间距
        float xStart = -xNum * xDis / 2 + xDis / 2;
        float zStart = -zNum * zDis / 2 + zDis / 2;

        var floor = new GameObject("floor5" );
        floor.transform.SetParent(map.transform);
        for (int z = 0; z < zNum; z++)
        {
            for (int x = 0; x < xNum; x++)
            {
                if((x==1 || x==4 ||x==7|| x==10 || x == 13)&& (z==1 || z==3 || z==5|| z==7))
                {
                    var grid = Instantiate(gridPrefab, floor.transform);
                    float _x = 0;
                    if (z % 2 == 0)
                    {
                        _x = x * xDis;
                    }
                    else
                    {
                        _x = x * xDis + xDis / 2;
                    }
                    float _z = z * zDis;
                    grid.transform.position = new Vector3(xStart + _x, y, zStart + _z);
                    grid.transform.Find("collider").GetComponent<Grid>().floor = index;
              
                    startGrids.Add(grid.transform);
                    
                }
                
            }
        }
    }

    void createCharacters()
    {
        //指定一个为主角
        int playerIndex = Random.Range(0, 20);
        
        for(int i = 0; i < 20; i++)
        {
            GameObject go = Instantiate(characterPrefab);
            characters.Add(go.transform);
            go.transform.position = startGrids[i].position + Vector3.up;
            if (i == playerIndex)
            {
                go.AddComponent<PlayerInput>();
                player = go.transform;
                go.SetActive(true);
            }
            else
            {
                addAI(go);
                go.SetActive(false);
            }
        }
        //打乱characters
        NanFunc.Reshuffle( characters);
    }
    void addAI(GameObject go)
    {
        float r = Random.value;
        if (r < 0.2f)
        {
            go.AddComponent<NoobAi>();
        }else if (r < 0.4f)
        {
            go.AddComponent<NoobPlusAi>();
        }else if (r < 0.7f)
        {
            go.AddComponent<NormalAi>();
        }
        else
        {
            go.AddComponent<Normal2Ai>();
        }
    }
}
