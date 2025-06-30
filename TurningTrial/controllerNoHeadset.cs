using UnityEngine;

public class controllerNoHeadset : MonoBehaviour
{
    // Start is called before the first frame update
    Transform controllerThis; //the controller that will be moved and used to press the trigger
    //public Transform controllerOther; //the controller that will be held steady at chest height

    private Vector3 prevTransThis, prevTransOther;
    public makeLog logger;
    private int count = 0;
    private Vector3 prevFacingDir;
    private Vector3 currFacingDir;

    void Start()
    {
        //get log reference
        controllerThis = this.gameObject.transform;
        prevTransThis = controllerThis.position;
        prevFacingDir = Utilities.FlattenedDir3D(controllerThis.forward);
        currFacingDir = Utilities.FlattenedDir3D(controllerThis.forward);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Mathf.Round(controllerThis.position.x*10f)*0.1f)!= (Mathf.Round(prevTransThis.x * 10f) * 0.1f) ||
            (Mathf.Round(controllerThis.position.y * 10f) * 0.1f) != (Mathf.Round(prevTransThis.y* 10f) * 0.1f)||
            (Mathf.Round(controllerThis.position.z * 10f) * 0.1f) != (Mathf.Round(prevTransThis.z * 10f) * 0.1f)
            )
        {
            //each frame update where controller is in space and rotation
            logger.makeLogEntry(controllerThis);
        }
        prevTransThis = controllerThis.position;
        

    }

    //when user presses trigger send log message
    public void triggerPress()
    {
        if(count%2 == 0)
        {
            prevFacingDir = currFacingDir;
            currFacingDir = Utilities.FlattenedDir3D(controllerThis.forward);
            float turnAmount = Vector3.SignedAngle(prevFacingDir, currFacingDir, Vector3.up);
            logger.makeLogEntry(controllerThis, true, prevFacingDir, currFacingDir, turnAmount); //start of a trial
            
        }
        else
        {
            prevFacingDir = currFacingDir;
            currFacingDir = Utilities.FlattenedDir3D(controllerThis.forward);
            float turnAmount = Vector3.SignedAngle(prevFacingDir, currFacingDir, Vector3.up);
            logger.makeLogEntry(controllerThis, true, prevFacingDir, currFacingDir, turnAmount); //end of a trial

        }
        count++;
        Debug.Log("trigger pressed");
    }
}
