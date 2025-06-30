using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using Valve.VR.InteractionSystem;
using UnityEngine.SceneManagement;

//Instantiates instances of the various log classes and sends data to the last log class in the list
public class makeLog : MonoBehaviour
{
    private logBase basicLog; //basic log
    private Transform player; //used to get the position of the player in the Memory Palace

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        logMaker();
        Scene scene = SceneManager.GetActiveScene();
        string sceneName = scene.name;
        Debug.Log(scene);
        basicLog.log("start", sceneName);
    }

    private void logMaker() //instantiate the various log types
    {
        basicLog = new logBase("basic");
    }

    public void makeLogEntry(ViveSR.anipal.Eye.EyeData_v2 eye)
    {
        basicLog.log(eye);
    }
    public void makeLogEntry(Vector3 playerPos, Vector3 playerDir, Vector3 deltaPos, float deltaDir, Vector3 controllerPos1, Vector3 controllerPos2, Vector3 controllerDir1, Vector3 controllerDir2, Vector3 goalPos)
    {
        basicLog.log(playerPos,  playerDir, deltaPos, deltaDir, controllerPos1, controllerPos2, controllerDir1, controllerDir2, goalPos);
    }

    public void makeLogEntry(Vector3 playerPos, Vector3 playerDir, Vector3 deltaPos, float deltaDir, Vector3 goalPos)
    {
        basicLog.log(playerPos, playerDir, deltaPos, deltaDir,  goalPos);
    }

    public void makeLogEntry(Vector3 playerPos, Vector3 playerDir, Vector3 deltaPos, float deltaDir, Vector3 goalPos, int waypointCount)
    {
        basicLog.log(playerPos, playerDir, deltaPos, deltaDir, goalPos, waypointCount);
    }

    public void makeLogEntry(Vector3 playerPos, Vector3 playerDir, Vector3 deltaPos, float deltaDir, Vector3 goalPos, Transform t, float distance, Ray GazeRay)
    {
        basicLog.log(playerPos, playerDir, deltaPos, deltaDir, goalPos, t, distance, GazeRay);
    }

    public void makeLogEntry(Vector3 playerPos, Vector3 playerDir, Vector3 deltaPos, float deltaDir, Vector3 goalPos, Ray GazeRay)
    {
        basicLog.log(playerPos, playerDir, deltaPos, deltaDir, goalPos, GazeRay);
    }

    public void makeLogEntry(bool isStartTrial, int i, int experimentSetups, string trialSequence, bool isClockwise, Vector3 playerForward,
        Vector3 virtForward, Vector3 realForward, float[] virtPlayer, float[] realPlayer, float errorVirt, float errorReal)
    {
        basicLog.log(isStartTrial, i, experimentSetups, trialSequence.ToString(), isClockwise, playerForward, virtForward, realForward,
                virtPlayer, realPlayer, errorVirt, errorReal);
    }

    public void makeLogEntry(bool isStartTrial, int i, int experimentSetups, string trialSequence, bool isClockwise, Vector3 playerForward,
    Vector3 virtForward, Vector3 realForward)
    {
        basicLog.log(isStartTrial, i, experimentSetups, trialSequence.ToString(), isClockwise, playerForward, virtForward, realForward);
    }

    public void makeLogEntry(int numCollisions, float distance, float avgDistance, double avgAlignment)
    {
        //Debug.Log("reset called scripty thing");
        basicLog.log(numCollisions, distance, avgDistance, avgAlignment);
    }

    public void makeLogEntry(Vector3 playerPos, Vector3 playerDir, Vector3 deltaPos, float deltaDir, Vector3 rigPos, Vector3 rigDir)
    {
        basicLog.log(playerPos, playerDir, deltaPos, deltaDir, rigPos, rigDir);
    }

    //When the unity application is quit out of/stops running, sends the event that will close the text writer.
    void OnApplicationQuit()
    {
        basicLog.log("stop", SceneManager.GetActiveScene().name);
    }

    public void makeLogEntry(Transform leftController)
    {
        basicLog.log(leftController);
    }
    public void makeLogEntry(Transform leftController, bool x, Vector3 prev, Vector3 curr, float angle)
    {
        basicLog.log(leftController, x, prev, curr, angle);
    }
}