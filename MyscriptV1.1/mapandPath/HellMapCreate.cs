using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class HellMapCreate : MonoBehaviour
{
    public GameObject wallCube;
    public GameObject pathfloor;
    [HideInInspector] public float maplength;
    Collider mapMeshcollider;
    public GameObject[,] wallmap;
    public GameObject[,] pathMap;
    public GameObject AllWall_centreHEll;
    public GameObject Allpath_centrehell;
    List<GameObject> WallList = new List<GameObject>();
    List<GameObject> pathList = new List<GameObject>();
    public int[,] passableMap;
    private void Awake()
    {

    }
    void Start()
    {
      
        maplength = GetComponent<MeshFilter>().mesh.bounds.size.x * transform.localScale.x;
        wallmap = new GameObject[(int)maplength, (int)maplength];
        pathMap = new GameObject[(int)maplength, (int)maplength];
        passableMap = new int[(int)maplength, (int)maplength];
        createfullWall_Path();
        Iniwall();
        setALL_in_parent();
        Debug.Log(wallmap.GetLength(0));
    }


    public void createfullWall_Path()
    {
        for (float z = -maplength / 2; z < maplength / 2; z += 1)
        {
            for (float x = -maplength / 2; x < maplength / 2; x += 1)
            {

                WallList.Add(Instantiate(wallCube, new Vector3(x + 0.5f, 0.5f, z + 0.5f), new Quaternion(0, 0, 0, 0)));
                pathList.Add(Instantiate(pathfloor, new Vector3(x + 0.5f, 0.2f, z + 0.5f), pathfloor.gameObject.transform.rotation));

            }
        }
    }
    public void setALL_in_parent()
    {
        for(int i = 0; i < WallList.Count; i++)
        {
            WallList[i].transform.parent = AllWall_centreHEll.transform;
            pathList[i].transform.parent = Allpath_centrehell.transform;

        }
    }
    public void Iniwall()
    {
        int count = 0;
        for (int i = 0; i < maplength; i++)
        {
            for (int j = 0; j < maplength; j++)
            {
               
                wallmap[i, j] = WallList[count];
                pathMap[i, j] = pathList[count];
                wallmap[i, j].SetActive(false);
                //pathMap[i, j].SetActive(false);
                count++;
            }
        }
    }
}
