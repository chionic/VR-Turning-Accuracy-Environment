using UnityEngine;
using System;

//Note: this logging system is simplified, since there are fundamentally only three types of logs in this system and three base classes accordingly
//events call the log type directly in a function rather than going through a pattern.
public class logBase
{

    private string scriptType; //The type of event that happened (eg an object becoming visible)
    private string log1 = "";
    private static int id = 0; //all log classes share this number, used to count the number of logs

    public logBase(string script)
    {
        scriptType = script;
    }

    public virtual void log(string s, string scene)
    {
        if (string.Equals(s, "start")) startedGame(scene);
        else if (string.Equals(s, "stop")) stoppedGame(scene);
        else Debug.Log("logStart No correct log type found " + s);
    }

    public virtual void log(Vector3 playerPos, Vector3 playerDir, Vector3 deltaPos, float deltaDir, Vector3 controllerPos1, Vector3 controllerPos2, Vector3 controllerDir1, Vector3 controllerDir2, Vector3 goalPos)
    {
        if (controllerPos1 != null) //if there is no object attached to the event, log only the event type and player position
        {
            log1 = "{ " +getId() + ", " + getTimestamp() +  ", Player Position: " + playerPos.x + " " + playerPos.y + " " + playerPos.z + ", Player Direction: " + playerDir.x 
                + " " + playerDir.y + " " + playerDir.z + ", Delta Position: " + deltaPos.x + " " + deltaPos.y + " " + deltaPos.z + ", Delta Direction: " + deltaDir
                + ", Controller 1 Position: " + controllerPos1.x + " " + controllerPos1.y + " " + controllerPos1.z 
                + ", Controller 2 Position: " + controllerPos2.x + " " + controllerPos2.y + " " + controllerPos2.z + ", Controller 1 Direction: " + controllerDir1.x + " " + controllerDir1.y + " " 
                + controllerDir1.z + ", Controller 2 Direction: " + controllerDir2.x + " " + controllerDir2.y + " " + controllerDir2.z
                + ", Goal Position: " + goalPos.x + " " + goalPos.y + " " + goalPos.z + "}";
        }
        else //otherwise log the object name and position as well
        {
            log1 = "{ "+ getId() + ", " + getTimestamp() + ", Player Position: " + playerPos.x + " " + playerPos.y + " " + playerPos.z + ", Player Direction: " + playerDir.x
                + " " + playerDir.y + " " + playerDir.z + ", Delta Position: " + deltaPos.x + " " + deltaPos.y + " " + deltaPos.z + ", Delta Direction: " + deltaDir
                +  ", Goal Position: " + goalPos.x + " " + goalPos.y + " " + goalPos.z + "}";
        }
        writeToLog.WriteString(log1); //send to writeLog to write into text file
    }

    public virtual void log(Vector3 playerPos, Vector3 playerDir, Vector3 deltaPos, float deltaDir,  Vector3 goalPos)
    {
        log1 = "{ " + getId() + ", " + getTimestamp() + ", Player Position: " + playerPos.x + " " + playerPos.y + " " + playerPos.z + ", Player Direction: " + playerDir.x
                + " " + playerDir.y + " " + playerDir.z + ", Delta Position: " + deltaPos.x + " " + deltaPos.y + " " + deltaPos.z + ", Delta Direction: " + deltaDir
                +  ", Goal Position: " + goalPos.x + " " + goalPos.y + " " + goalPos.z + "}";
    
    writeToLog.WriteString(log1); //send to writeLog to write into text file
    }

    //Task 1 - in between logs (just keeps track of player position and rotation in the real (playerpos) and virtual (rigPos) worlds)
    public virtual void log(Vector3 playerDirFlat, Vector3 playerDir, Vector3 deltaDirFlat, float deltaDir, Vector3 rigDirFlat, Vector3 rigDir)
    {
        log1 = "{" + getId() + ", " + getTimestamp() + ", Player Flattened Direction: [" + playerDirFlat.x + " " + playerDirFlat.y + " " + playerDirFlat.z + "], Player Direction: [" + playerDir.x
                + " " + playerDir.y + " " + playerDir.z + "], Delta Flattened Direction: [" + deltaDirFlat.x + " " + deltaDirFlat.y + " " + deltaDirFlat.z + "], Delta Direction: " + deltaDir +
                ", Rig Flattened Direction: [" + rigDirFlat.x + " " +rigDirFlat.y + " "+ rigDirFlat.z+ "], Rig Direction: [" + rigDir.x + " "+ rigDir.y + " "+ rigDir.z + "]}";
        writeToLog.WriteString(log1); //send to writeLog to write into text file
    }

    //Task 2
    public virtual void log(Vector3 playerPos, Vector3 playerDir, Vector3 deltaPos, float deltaDir, Vector3 goalPos, int waypointCount)
    {
        log1 = "{ " + getId() + ", " + getTimestamp() + ", Player Position: " + playerPos.x + " " + playerPos.y + " " + playerPos.z + ", Player Direction: " + playerDir.x
                + " " + playerDir.y + " " + playerDir.z + ", Delta Position: " + deltaPos.x + " " + deltaPos.y + " " + deltaPos.z + ", Delta Direction: " + deltaDir
                + ", Goal Position: " + goalPos.x + " " + goalPos.y + " " + goalPos.z + ", Waypoint Count: " + waypointCount + "}";

        writeToLog.WriteString(log1); //send to writeLog to write into text file
    }

    public virtual void log(Vector3 playerPos, Vector3 playerDir, Vector3 deltaPos, float deltaDir, Vector3 goalPos, Transform t, float distance, Ray r)
    {
        log1 = "{ " + getId() + ", " + getTimestamp() + ", Player Position: " + playerPos.x + " " + playerPos.y + " " + playerPos.z + ", Player Direction: " + playerDir.x
                + " " + playerDir.y + " " + playerDir.z + ", Delta Position: " + deltaPos.x + " " + deltaPos.y + " " + deltaPos.z + ", Delta Direction: " + deltaDir
                + ", Goal Position: " + goalPos.x + " " + goalPos.y + " " + goalPos.z + ", Looking at: " + t.gameObject.name + " [" + t.position.x + " " + t.position.y + " "
                + t.position.z + "], Distance from player: " + distance + ", Ray direction: [" + r.direction.x + " " + r.direction.y + " " + r.direction.z + "]}";

        writeToLog.WriteString(log1); //send to writeLog to write into text file
    }

    public virtual void log(Vector3 playerPos, Vector3 playerDir, Vector3 deltaPos, float deltaDir, Vector3 goalPos, Ray r)
    {
        log1 = "{ " + getId() + ", " + getTimestamp() + ", Player Position: " + playerPos.x + " " + playerPos.y + " " + playerPos.z + ", Player Direction: " + playerDir.x
                + " " + playerDir.y + " " + playerDir.z + ", Delta Position: " + deltaPos.x + " " + deltaPos.y + " " + deltaPos.z + ", Delta Direction: " + deltaDir
                + ", Goal Position: " + goalPos.x + " " + goalPos.y + " " + goalPos.z
                + ", Ray direction: [" + r.direction.x + " " + r.direction.y + " " + r.direction.z + "]}";

        writeToLog.WriteString(log1); //send to writeLog to write into text file
    }


    //Task 1 - end trial turn
    public virtual void log(bool isStartTrial, int i, int experimentSetups, string trialSequence, bool isClockwise, Vector3 playerForward,
        Vector3 virtForward, Vector3 realForward, float[] virtPlayer, float[] realPlayer, float errorVirt, float errorReal)
    {
        string x = "[" + i + "," + isClockwise + "," + experimentSetups + "]";
        string y = "[" + virtPlayer[0] + " " + virtPlayer[1] + " " + virtPlayer[2] + "]";
        string z = "[" + realPlayer[0] + " " + realPlayer[1] + " " + realPlayer[2] + "]";
        log1 = "{" + getId() + ", " + getTimestamp() + ", Player facing direction: " + playerForward + ", Virtual facing direction: " + virtForward + ", Real facing direction: " + realForward +
             ", Virtual Numbers: "+ y + ", Real  Numbers: "+ z +", Trial Sequence: " + trialSequence + ", Start of trial " + i + ": " + isStartTrial + ", isClockwise: " + isClockwise +
            ", Experiment conditions: " + x+", Virtual turn amount: " + virtPlayer[2] + ", Virtual turn error: " + errorVirt + ", Real turn amount: " + realPlayer[2] + ", Real turn error: " + errorReal + "}";
        writeToLog.WriteString(log1); //send to writeLog to write into text file
    }

    //Task 1 - start trial turn
    public virtual void log(bool isStartTrial, int i, int experimentSetups, string trialSequence, bool isClockwise, Vector3 playerForward,
        Vector3 virtForward, Vector3 realForward)
    {
        string x = "["+ i + ","+isClockwise+","+experimentSetups+"]";
        log1 = "{" + getId() + ", " + getTimestamp() + ", Player facing direction: " + playerForward + ", Virtual facing direction: " + virtForward + ", Real facing direction: " + realForward + 
            ", Trial Sequence: " + trialSequence + ", Start of trial " + i + ": " + isStartTrial + ", isClockwise: " + isClockwise +
             ", Experiment conditions: " + x + "}";
         writeToLog.WriteString(log1); //send to writeLog to write into text file
    }

    //Task 2
    public virtual void log(int numCollisions, float distance, float avgDistance, double avgAlignment)
    {
        //Debug.Log("reset log called");
        log1 = "{ " + getId() + ", " + getTimestamp() + ", Number of collisions: " + numCollisions + ", Distance since last collision: " + distance + ", Average distance: "
        + avgDistance + ", Average alignment: " + avgAlignment + "}";
        writeToLog.WriteString(log1); //send to writeLog to write into text file
    }


    //Controller only log - in between (ie no trigger press)
    public virtual void log(Transform a)
    {
        log1 ="{"+ getId() + ", " + getTimestamp() + ", Controller A position: " + a.position.ToString("f2") + ", Controller A rotation: " + a.rotation.ToString("f2") + "}";
        writeToLog.WriteString(log1);
    }

    //controller only log - when trigger is pressed
    public virtual void log(Transform a, bool x, Vector3 prevDir, Vector3 currDir, float angle)
    {
        log1 = "{"+ getId() + ", " + getTimestamp() + ", Is start of trial: " + x + ", Controller A position: " + a.position.ToString("f2") + ", Controller A rotation: " + a.rotation.ToString("f2") 
            +", Controller A previous forward: "+prevDir + ", Controller A current forward: "+ currDir + ", Angle turned: "+ angle + "}";
        writeToLog.WriteString(log1);
    }

    public virtual void log(ViveSR.anipal.Eye.EyeData_v2 eye)
    {
        log1 = "{" + getId() + ", " + getTimestamp() + eye + "}";
        writeToLog.WriteString(log1);
    }

    public void startedGame(String scene) //if the game just started say so and give a date and time (based on the computer's date and time)
    {
        writeToLog.makeLogFile(scene);
        writeToLog.WriteString("{" +getId() + ", " + getTimestamp() + "\"action\": \"Started Game\", \"date\": \"" + DateTime.Now.ToString("HH:mm dd MMMM, yyyy")
            + "\"}");
    }

    public void stoppedGame(String scene) //if the game stopped say so and give a date and time (based on the computer's date and time)
    {
        writeToLog.closeLog("{" + getId() + ", " + getTimestamp() + "\"action\": \"Stopped Game\", \"date\": \"" + DateTime.Now.ToString("HH:mm dd MMMM, yyyy")
            + "\"}");
    }
    
    
    //Get timestamp - gets the timestamp to six decimal places
    protected string getTimestamp()
    {
        return Time.time.ToString("f6");
    }

    //increases the static id (same across all log classes) and returns to be used in the log
    protected int getId()
    {
        id++;
        return id;
    }
}
