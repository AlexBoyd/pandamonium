using UnityEngine;
using System.Collections;

public class ElevatorCheckpoint : Checkpoint {
	

	public override void saveCheckpoint(){
		SaveManager.SaveElevatorCheckpointData(this);
		SaveManager.SaveCheckpointData(this);
	}
}
