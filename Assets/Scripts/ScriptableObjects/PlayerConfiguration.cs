using UnityEngine;

namespace ScriptableObj
{
    [CreateAssetMenu(fileName = "PlayerConfiguration", menuName = "ScriptableObjects/NewPlayerConfig")]
    public class PlayerConfiguration : ScriptableObject
    {
        public KeyCode PlayerInput_Up;
        public KeyCode PlayerInput_Down;
        public KeyCode PlayerInput_Left;
        public KeyCode PlayerInput_Right;
        public KeyCode PlayerCellSelection;
    }
}

