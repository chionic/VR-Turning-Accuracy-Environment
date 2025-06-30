using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;


public class trialInput : MonoBehaviour
{
    //will need to fix the function that uses this in a bit as well - on to do list anyway for other reasons
    //private List<int[]> experimentSetups = new List<int[]>(){ new[] { 0, 180 } ,  new[] { 50, 180 } , new[] { 100, 180 }, new[] { 200, 180 }, new[] { 0, 90 }, new[] { 50, 90 }, new[] { 100, 90 }, new[] { 200, 90 },
    //                                                          new[] { 0, 45 } , new[] { 50, 45 }, new[] { 100, 45 }, new[] { 200, 45 }};
    private List<int[]> zeroGain = new List<int[]>() { new[] { 0, 180, 1 }, new[] { 0, 180, 2 }, new[] { 0, 90, 1 }, new[] { 0, 90, 2 }, new[] { 0, 45, 1 }, new[] { 0, 45, 2 } };
    private List<int[]> fiftyGain = new List<int[]>() { new[] { 50, 180, 1 }, new[] { 50, 180, 2 }, new[] { 50, 90, 1 }, new[] { 50, 90, 2 }, new[] { 50, 45, 1 }, new[] { 50, 45, 2 } };
    private List<int[]> hundredGain = new List<int[]>() { new[] { 100, 180, 1 }, new[] { 100, 180, 2 }, new[] { 100, 90, 1 }, new[] { 100, 90, 2 }, new[] { 100, 45, 1 }, new[] { 100, 45, 2 } };
    private List<int[]> twoGain = new List<int[]>() { new[] { 200, 180, 1 }, new[] { 200, 180, 2 }, new[] { 200, 90, 1 }, new[] { 200, 90, 2 }, new[] { 200, 45, 1 }, new[] { 200, 45, 2 } };
    private List<int[]> practiceGain = new List<int[]>() { new[] { 0, 45, 1 }, new[] { 0, 90, 2 }, new[] { 0, 180, 1 } };
    private List<int[]> practiceGain2 = new List<int[]>() { new[] { 200, 45, 1 }, new[] { 200, 90, 2 }, new[] { 200, 180, 1 } };

    private int[] currEl;
    
    private bool isStartTrial = true;
    public Manager man;
    public SteadyRotation sr;
    private bool quitNextTrigger = false; //used at the very end to let the last turn run smoothly
    public enum TrialSequence { officeINC, officeDEC, emptyINC, emptyDEC }
    public TrialSequence trialSequence = TrialSequence.officeINC;
    private bool isClockwise = false;
    public makeLog logger;
    public GameObject oneObj;
    public Transform rig;

    public Text text;
    private float[] realTurn;
    public Transform realTrans, virtTrans;
    private Vector3 prevRigPos;

    private Vector3 localPosLast, localDirLast;
    private float deltaDir;

    public Vector3 prevRealTurn, prevVirtTurn, prevPlayerPos;

    private int[] saveLast;

    private char rightArrow = '\u2192';
    private char leftArrow = '\u2190'; 
    public void Start()
    {
        //get any refs needed
        text.text = ("Press trigger once to start the turn, press trigger a second time to complete the turn and to see the instruction for the next turn.");
        prevRealTurn = currDir(realTrans);
        prevVirtTurn = currDir(virtTrans);
        prevPlayerPos = currDir(man.getPlayerTransform());
        localPosLast = prevPlayerPos;
        localDirLast = man.getPlayerTransform().forward;
        prevRigPos = currDir(rig);
        isClockwise = true;

    }

    public void Update()
    {
        Vector3 curr = currDir(man.getPlayerTransform());
        logger.makeLogEntry(curr, man.getPlayerTransform().forward, curr - localPosLast, Vector3.Angle(man.getPlayerTransform().forward, localDirLast), 
            currDir(rig), rig.forward);
        localPosLast = curr;
        localDirLast = man.getPlayerTransform().forward;
 
    }


    public void selectList()
    {

        if (quitNextTrigger)
        {
            trialForward(twoGain);
        }

            else if (trialSequence == TrialSequence.officeDEC || trialSequence == TrialSequence.emptyDEC)
            {
                //200, 100, 50, 0
                if(practiceGain2.Count > 0)
            {
                trialForward(practiceGain2);
                Debug.Log("Preactice gain 2 " + practiceGain2.Count);
            }
                else if (twoGain.Count > 0)
                {
                    trialForward(twoGain);
                }
                else if (hundredGain.Count > 0)
                {
                    trialForward(hundredGain);
                }
                else if (fiftyGain.Count > 0)
                {
                    trialForward(fiftyGain);
                }
                else
                {
                    trialForward(zeroGain);
                }
            }
            else
            {
            if (practiceGain.Count > 0)
            {
                trialForward(practiceGain);
                Debug.Log("Preactice gain" + practiceGain.Count);
            }
            
               //0,50,100,200
              else if (zeroGain.Count > 0)
                {
                    trialForward(zeroGain);
                }
                else if (fiftyGain.Count > 0)
                {
                    trialForward(fiftyGain);
                }
                else if (hundredGain.Count > 0)
                {
                    trialForward(hundredGain);
                }
                else
                {
                    trialForward(twoGain);
                }
            }
    
 
    }

    private void trialForward(List<int[]> l)
    {
        if (
            
            (((trialSequence == TrialSequence.officeDEC || trialSequence == TrialSequence.emptyDEC) && zeroGain.Count <= 0) ||
            ((trialSequence == TrialSequence.officeINC || trialSequence == TrialSequence.emptyINC) && twoGain.Count <= 0))
           && !quitNextTrigger
           )
        {
            int[] element = saveLast;

            quitNextTrigger = true;
            text.enabled = false;
            prevRealTurn = currDir(realTrans);
            prevVirtTurn = currDir(virtTrans);
            prevPlayerPos = currDir(man.getPlayerTransform());
            prevRigPos = currDir(rig);
            logger.makeLogEntry(isStartTrial, element[0], element[1], trialSequence.ToString(), isClockwise, man.getPlayerTransform().forward, prevVirtTurn, prevRealTurn);
            Debug.Log("quit trigger set");
        }
        else if(quitNextTrigger)
        {
            Debug.Log("in quitNext Triigeer subsection"); //prev player pos is actually a direction 
            int[] element = saveLast;
            float[] virtPlayer;
            float[] realPlayer;
            if (element[1] == 180)
            {
                virtPlayer = (relativeTurn180(currDir(man.getPlayerTransform()), prevPlayerPos, currDir(virtTrans), prevVirtTurn, virtTrans.up, element[2]));
                realPlayer = (relativeTurn180(currDir(man.getPlayerTransform()), prevPlayerPos, currDir(realTrans), prevRealTurn, realTrans.up, element[2]));
            }
            else
            {
                virtPlayer = relativeTurn(currDir(man.getPlayerTransform()), prevPlayerPos, currDir(virtTrans), prevVirtTurn, virtTrans.up);
                realPlayer = relativeTurn(currDir(man.getPlayerTransform()), prevPlayerPos, currDir(realTrans), prevRealTurn, realTrans.up);
            }

            float[] virtTurn = virtTurnSigned(currDir(man.getPlayerTransform()), prevPlayerPos, virtTrans.up, element[2]);

            if (element[1] == 180)
            {
                realTurn = relTurn180(currDir(rig), prevRigPos, currDir(man.getPlayerTransform()), prevPlayerPos, realTrans.up, element[2]);
            }
            else
            {
                realTurn = relTurn180(currDir(rig), prevRigPos, currDir(man.getPlayerTransform()), prevPlayerPos, realTrans.up, element[2]);
            }
            logger.makeLogEntry(isStartTrial, element[0], element[1], trialSequence.ToString(), isClockwise, man.getPlayerTransform().forward, virtTrans.forward, realTrans.forward,
                virtPlayer, realPlayer, virtPlayer[2] - element[1], realPlayer[2] - element[1]);
            logger.makeLogEntry(isStartTrial, element[0], element[1], trialSequence.ToString(), isClockwise, man.getPlayerTransform().forward, virtTrans.forward, realTrans.forward,
                virtTurn, realTurn, virtTurn[2] - element[1], realTurn[2] - element[1]);
            text.enabled = true;
            text.text = "Trial complete, please take off the headset";
            quitNextTrigger = false;
            return;
        }
        if (l.Count > 0)
        {
           
            
            if (!isStartTrial)
            {
                //end of trial calculations
                int[] element = currEl;
                float[] virtPlayer;
                float[] realPlayer;
                if (element[1] == 180)
                {
                   virtPlayer = (relativeTurn180(currDir(man.getPlayerTransform()), prevPlayerPos, currDir(virtTrans), prevVirtTurn, virtTrans.up, element[2]));
                    realPlayer = (relativeTurn180(currDir(man.getPlayerTransform()), prevPlayerPos, currDir(realTrans), prevRealTurn, realTrans.up, element[2]));
                }
                else
                {
                    virtPlayer = relativeTurn(currDir(man.getPlayerTransform()), prevPlayerPos, currDir(virtTrans), prevVirtTurn, virtTrans.up );
                    realPlayer = relativeTurn(currDir(man.getPlayerTransform()), prevPlayerPos, currDir(realTrans), prevRealTurn, realTrans.up);
                }

                float[] virtTurn = virtTurnSigned(currDir(man.getPlayerTransform()), prevPlayerPos, virtTrans.up, element[2]);
                
                if (element[1]== 180)
                {
                    realTurn = relTurn180(currDir(rig), prevRigPos, currDir(man.getPlayerTransform()), prevPlayerPos, realTrans.up, element[2]);
                }
                else
                {
                    realTurn = relTurn180(currDir(rig), prevRigPos, currDir(man.getPlayerTransform()), prevPlayerPos, realTrans.up, element[2]);
                }
                logger.makeLogEntry(isStartTrial, element[0], element[1], trialSequence.ToString(), isClockwise, man.getPlayerTransform().forward, virtTrans.forward, realTrans.forward,
                    virtPlayer, realPlayer, virtPlayer[2] - element[1], realPlayer[2] - element[1]);
                logger.makeLogEntry(isStartTrial, element[0], element[1], trialSequence.ToString(), isClockwise, man.getPlayerTransform().forward, virtTrans.forward, realTrans.forward,
                    virtTurn, realTurn, virtTurn[2] - element[1], realTurn[2] - element[1]);

                //start of next trial text
                int x = Random.Range(0, l.Count - 1);
                element = l[x];
                currEl = element;
                saveLast = l[x];
                l.RemoveAt(x);

                text.enabled = true;
                
                if (oneObj != null)
                {
                    //set yellow ball to be in front of player
                    Transform p = man.getPlayerTransform();
                    oneObj.transform.position = p.position + (p.forward * 10f);
                }
                if (element[2] == 1)
                {
                    isClockwise = true;
                }
                else
                {
                    isClockwise = false;
                }
                sr.gain_multiplier = element[0];
                setText(element);
            }

            else
            {
                int[] element = new int[3];
                if (currEl != null)
                {
                    element = currEl;
                }
                else
                {
                    currEl = l[0];
                }
                
                //work with previous and current direction changes
                text.enabled = false;
                prevRealTurn = currDir(realTrans);
                prevVirtTurn = currDir(virtTrans);
                prevPlayerPos = currDir(man.getPlayerTransform());
                prevRigPos = currDir(rig);
                logger.makeLogEntry(isStartTrial,element[0],element[1], trialSequence.ToString(), isClockwise, man.getPlayerTransform().forward, prevVirtTurn, prevRealTurn);
                
            }
            isStartTrial = !isStartTrial;
        }
    }
    private void setText(int[] element)
    {
       
        string s = (isClockwise) ? element[1] + " " + rightArrow.ToString()  : leftArrow.ToString() + " "+ element[1];
        text.text =  s + "\n "; // + element[0];
    }

    private float[] relativeTurn(Vector3 playerStart, Vector3 playerEnd, Vector3 worldStart, Vector3 worldEnd, Vector3 worldUp)
    {
        float[] x = new float[3];
        //player rotation (separate from the plane)
        float playerTurn = Vector3.Angle(playerStart, playerEnd);

        //plane rotation
        float worldTurn = Vector3.Angle(worldStart, worldEnd);
        //Debug.Log("player turn: " + playerTurn + "; World turn: " + worldTurn + " player direction: " + AngleDir(playerStart, playerEnd, man.getPlayerTransform().up) + "; World direction: " + AngleDir(worldStart, worldEnd, worldUp));
        x[0] = playerTurn;
        x[1] = worldTurn;
        //check direction of each rotation and get the total rotation of the player relative to the plane
        if (worldTurn == 0)
        {
            x[2] = playerTurn;
            return x;
        }
        else if (AngleDir(playerStart, playerEnd, man.getPlayerTransform().up) == AngleDir(worldStart, worldEnd, worldUp))
        {
            x[2] = playerTurn - worldTurn;
            return x;
        }
        else {
            x[2] = playerTurn + worldTurn;
            return x;
        }
    }

    private float [] relativeTurn180(Vector3 playerStart, Vector3 playerEnd, Vector3 worldStart, Vector3 worldEnd, Vector3 worldUp, int direction)
    {
        float[] x = new float[3];
        //float finalRotation = 0;
        //player rotation (separate from the plane)
        //check which way is the smaller angle
        float playerTurn = Vector3.SignedAngle(playerStart, playerEnd, man.getPlayerTransform().up);

        //plane rotation
        float worldTurn = Vector3.Angle(worldStart, worldEnd);

        //check if the sign is the same as the intended turn direction
        //if the player has a counterclockwise turn but a clockwise instruction
        //Debug.Log("PlayerTurn before if: " + playerTurn);
        if(playerTurn < -0.01f && direction == 2)
        {
            playerTurn = 360 + playerTurn;
           
            
        }
        else if(playerTurn > 0.01f && direction == 1)
        {
            playerTurn = 360 - playerTurn;
     
        }

        //Debug.Log("PlayerTurn after if: " + playerTurn);
        x[0] = playerTurn;
        x[1] = worldTurn;

        if(worldTurn > 0 && playerTurn > 0)
        {
            x[2] = playerTurn - worldTurn;
        }
        else if(worldTurn < 0 && playerTurn < 0)
        {
            //     - p         - - w = -p + w
            x[2] = playerTurn - worldTurn;
        }
        else if(worldTurn > 0 && playerTurn < 0)
        {
            x[2] = playerTurn + worldTurn;
        }
        else if(worldTurn < 0 && playerTurn > 0)
        {
            x[2] = playerTurn + worldTurn;
        }
        else
        {
            x[2] = playerTurn;
        }

        //Debug.Log(x[0] + " " + x[1] + " " + x[2]);
        //Debug.Log("player turn: " + playerTurn + "; World turn: " + worldTurn + " player direction: " + AngleDir(playerStart, playerEnd, man.getPlayerTransform().up) + "; World direction: " + AngleDir(worldStart, worldEnd, worldUp));
        return x;
        //check direction of each rotation and get the total rotation of the player relative to the plane
        /*if (worldTurn == 0) return playerTurn;
        if (finalRotation != 0) return finalRotation;
        else if (AngleDir(playerStart, playerEnd, man.getPlayerTransform().up) == AngleDir(worldStart, worldEnd, worldUp)) return playerTurn - worldTurn;
        else return playerTurn + worldTurn;*/
    }


    private float relTurn(Vector3 rigStart, Vector3 rigEnd, Vector3 camStart, Vector3 camEnd, Vector3 worldUp)
    {
        //how much the rig turned - added virt turn amount
        float rigTurn = Vector3.SignedAngle(rigStart, rigEnd, worldUp);

        //how much player turned - real
        float playerTurn = Vector3.SignedAngle(camStart, camEnd, worldUp);

        if (rigTurn == 0)
        {
            return playerTurn;
        }

        //Debug.Log("virtual turn amount: " + (rigTurn - playerTurn));
        if ((rigTurn <= 0 && playerTurn <= 0)|| (rigTurn>=0 && playerTurn >= 0))
        {
            return Mathf.Abs(playerTurn - rigTurn);
        }
       
        return -1;
        
    }

    private float[] relTurn180(Vector3 rigStart, Vector3 rigEnd, Vector3 camStart, Vector3 camEnd, Vector3 worldUp, int direction) {
        //Array: 1 = clockwise, 2 = anticlockwise
        //Signed angle: -1 = anticlockwise, 1 = clockwise
        float[] x = new float[3];
        //how much the rig turned - added virt turn amount
        float rigTurn = Vector3.SignedAngle(rigStart, rigEnd, worldUp);
        x[1] = rigTurn;
        //how much player turned - real
        float playerTurn = Vector3.SignedAngle(camStart, camEnd, worldUp);
        x[0] = playerTurn;
        if (rigTurn == 0)
        {
            x[2] = playerTurn;
        }

        if (rigTurn < 0 && direction == 1)
        {
            rigTurn = 360 + rigTurn;
        }
        else if(rigTurn > 0 && direction == 2)
        {
            rigTurn = 360 - rigTurn;
        }

        if (playerTurn < 0 && direction == 1)
        {
            playerTurn = 360 + playerTurn;
        }
        else if (playerTurn > 0 && direction == 2)
        {
            playerTurn = 360 - playerTurn;
        }

        if (rigTurn <= 0 && playerTurn <= 0)
        {
            x[2] = playerTurn - rigTurn;
        }
        else if (rigTurn >= 0 && playerTurn >= 0)
        {
            x[2] = playerTurn - rigTurn;
        }
        else
        {
            x[2] = Mathf.Abs(playerTurn - rigTurn);
        }
        //Debug.Log("different signs!");
        return x;
    }

    private float[] virtTurnSigned(Vector3 camStart, Vector3 camEnd, Vector3 worldUp, int direction)
    {
        float[] x = new float[3];
        float playerTurn = Vector3.SignedAngle(camStart, camEnd, worldUp);
        x[0] = playerTurn;
        x[1] = 0;
        //Debug.Log("virt turn signed before: " + playerTurn + " " + direction);
        if (playerTurn < 0 && direction == 1)
        {
            playerTurn = 360 + playerTurn;
        }
        else if (playerTurn > 0 && direction == 2)
        {
            playerTurn = 360 - playerTurn;
        }
        x[2] = playerTurn;
        //Debug.Log("virt turn signed after: "+ playerTurn + " " + direction);
        return x;
    }
    private Vector3 currDir(Transform ob)
    {
        Vector3 currDir = Utilities.FlattenedDir3D(ob.forward);
        return currDir;
    }

    private float AngleDir(Vector3 start, Vector3 end, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(start, end);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0)
        {
            return 1; //Clock wise
        }
        else if (dir < 0)
        {
            return -1; //counter clockwise
        }
        else
        {
            return 0; //no turn
        }
    }
}


/* public void trialAdvance()
 {
     //log format send
     //change experiement conditions (if trial end)
     if (!isStartTrial)
     {
         float virtPlayer = relativeTurn(man.getPlayerTransform().forward, prevPlayerPos, currDir(virtTrans), prevVirtTurn, virtTrans.up);
         float realPlayer = relativeTurn(man.getPlayerTransform().forward, prevPlayerPos, currDir(realTrans), prevRealTurn, realTrans.up);
         Debug.Log("Virtual: " + virtPlayer);
         Debug.Log("Real: " + realPlayer);
         Debug.Log(logger);
         //logger.makeLogEntry(isStartTrial, i, experimentSetups[i], trialSequence.ToString(), isClockwise, man.getPlayerTransform().forward, virtTrans.forward, realTrans.forward, 
         //    virtPlayer, realPlayer, virtPlayer-experimentSetups[i][1], realPlayer - experimentSetups[i][1]);
         text.enabled = true;
         Vector3 playerTransform = man.getPlayerTransform().position;
         if (isClockwise)
         {
             //text goes counterclockwise
             setText(experimentSetups[i]);
         }
         else
         {
             i++;
             sr.gain_multiplier = experimentSetups[i][0];
             Debug.Log(experimentSetups[i][0] + " " + experimentSetups[i][1]);
             setText(experimentSetups[i]);
         }
         isClockwise = !isClockwise;
     }
     else
     {
         //work with previous and current direction changes
         text.enabled = false;
         prevRealTurn = currDir(realTrans);
         prevVirtTurn = currDir(virtTrans);
         prevPlayerPos = currDir(man.getPlayerTransform());
        // logger.makeLogEntry(isStartTrial, i, experimentSetups[i], trialSequence.ToString(), isClockwise, man.getPlayerTransform().forward, prevVirtTurn, prevRealTurn);
     }
     isStartTrial = !isStartTrial;
 }*/
