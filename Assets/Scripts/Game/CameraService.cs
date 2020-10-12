using UnityEngine;

namespace Game
{
    public class CameraService : MonoBehaviour
    {
        public GridBehaviour GridInstance;
        public Camera camera;

        void Start()
        {
            // Finding scrren ration and grid area ratio
            float screenRatio = (float)Screen.width / (float)Screen.height;
            float targetRatio = GridInstance.RowLength / GridInstance.ColumnLength;

            // Setting the Camera position to adjust grid area in view
            camera.transform.position = new Vector3(GridInstance.RowLength / 2, GridInstance.ColumnLength / 2,
                                                                        camera.transform.position.z);

            if (screenRatio >= targetRatio)
            {
                // Applying column length on camera size
                camera.orthographicSize = (GridInstance.ColumnLength / 2) + 2;
            }
            else
            {
                // Applying column length with difference of scrren ration and grid area ration on camera size
                float differenceInSize = targetRatio / screenRatio;
                camera.orthographicSize = GridInstance.ColumnLength / 2 * differenceInSize + 2;
            }
        }

    }
}
