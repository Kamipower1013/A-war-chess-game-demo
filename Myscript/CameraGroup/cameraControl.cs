using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class cameraControl : MonoBehaviour
{
    public CinemachineFreeLook[] cameraGroup;
    public AlltheEnemy alltheEnemyScript;
    private void Awake()
    {
        cameraGroup = GetComponentsInChildren<CinemachineFreeLook>();
    }
    void Start()
    {
        initCameraGrouplookat_enemyList();
    }

    public void initCameraGrouplookat_enemyList()
    {
        for (int i = 0; i < cameraGroup.Length; i++)
        {
            cameraGroup[i].Follow = alltheEnemyScript.enemyList[i].transform;
            cameraGroup[i].LookAt = alltheEnemyScript.enemyList[i].transform;
        }

    }

    public void setoff_deadenemy(int index)
    {
        cameraGroup[index].gameObject.SetActive(false);
    }
    public void AllRoundend_activeallEnemy()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
}
