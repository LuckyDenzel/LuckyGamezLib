using UnityEngine;
using LuckyGamezLib;

public class CheckpointVolume : MonoBehaviour {


    [Tooltip("The objects that should be affected by the volume")]
    [SerializeField] private string[] affectableObjectsTag;


    private void OnTriggerEnter(Collider other) {
        for (int i = 0; i < affectableObjectsTag.Length; i++) {
            if (other.CompareTag(affectableObjectsTag[i])) {
                SaveCheckpoint();

                break;
            }
        }
    }

    public void SaveCheckpoint() {
        CheckpointSaveData data = new CheckpointSaveData {
            xPos = transform.position.x,
            yPos = transform.position.y,
            zPos = transform.position.z
        };

        SaveSystem.SaveDataToFile(data, CheckpointSaveData.CHECKPOINT_SAVE_DATA_FILE_NAME);

        Debug.Log("Saved data to file!");
    }

}