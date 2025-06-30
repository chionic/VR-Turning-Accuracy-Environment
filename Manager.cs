using System.Collections;
using System.Collections.Generic;
using System.Net;
//using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class Manager : MonoBehaviour
{
    //player related
    GameObject player;
    Transform playerTransform;
    public float deltaDir; //change in player direction between this frame and the previous frame
    private Vector3 currDir, prevDir;
    private Transform playerRig;

    //World related
    public GameObject virtualWorld;
    public GameObject realWorld;
    Manager m;
    public bool isWorldTurn = true; //defines if the world turns around the player or if the viewport of the player is turned

    //state related
    public bool isSim = true;
    public bool isRealWorldTurn = true;

    private makeLog logger;
    private int waypointCount = 0;

    private float currDistance = 0f;


    void Awake()
    {
        player = GameObject.Find("Player");
        m = this;
        prevDir = player.transform.forward;
        if (isSim) //gets the moving body of our simulated player
        {
            playerTransform = player.transform.GetChild(1).Find("PlayerBody");
            //Debug.Log("Returned player collider script: " + playerTransform.GetComponent<PlayerCollider>());
            playerTransform.GetComponent<PlayerCollider>().setMan(m);
        }
        else //gets moving body of the player (attached to VR camerarig)
        {
            playerTransform = player.transform.GetChild(0).Find("Camera");
            player.transform.GetChild(0).Find("PlayerBody").GetComponent<PlayerCollider>().setMan(m);
            playerRig = player.transform.GetChild(0);
            Valve.VR.OpenVR.Chaperone.ForceBoundsVisible(false);
        }
        //Debug.Log("Player transform is : " + playerTransform.gameObject.name);
        logger = this.GetComponent<makeLog>();
    }
            
    private void Start()
    {
        if (isSim)
        {
            pScript = playerTransform.GetComponent<PlayerCollider>();
        }
        else
        {
            pScript = player.transform.GetChild(0).Find("PlayerBody").GetComponent<PlayerCollider>();
        }
        pScript.setMan(m);
       // Debug.Log(pScript);
        walkingDistances = new List<float>();
        
    }

    // Update is called once per frame
   void Update()
    {
        updateCurDir();
        updateDeltaDir();
        updatePrevDir();
        // logger.makeLogEntry(currDir, deltaDir);
    }


    public float GetDeltaTime()
    {
        // if (useManualTime)
        //     return 1.0f / targetFPS;
        // else
        return Time.deltaTime;
    }

    private void updateDeltaDir()
    {
        deltaDir = Utilities.GetSignedAngle(prevDir, currDir);
    }

    private void updateCurDir()
    {
        currDir = Utilities.FlattenedDir3D(playerTransform.forward);
    }

    private void updatePrevDir()
    {
        prevDir = Utilities.FlattenedDir3D(playerTransform.forward);
    }

    public float getDeltaDir()
    {
        return deltaDir;
    }

    public GameObject getPlayer()
    {
        return player;
    }

    public Transform getPlayerTransform()
    {
        return playerTransform;
    }

    public Vector3 getCurrDir()
    {
        return currDir;
    }

    public makeLog getLogger()
    {
        //Debug.Log("Logger: " + logger);
        return logger;
    }
}
