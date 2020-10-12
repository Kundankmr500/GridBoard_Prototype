using UnityEngine;

namespace ScriptableObj
{
    [CreateAssetMenu(fileName = "GameConfiguration", menuName = "ScriptableObjects/NewGameConfig")]
    public class GameConfiguration : ScriptableObject
    {
        [Range(0, 100)]
        public int SpecialCellCount;
        [Range(0, 100)]
        public int ScoreValuePerCell;
        [Range(0, 100)]
        public int MaxPlayerChance;
        [Range(1, 10)]
        public int RangeForChekingCellWithinGrid;

        public Material SpecialCellMaterial;
        public Material RedCellMaterial;
        public Material GreenCellMaterial;
        public Material YellowCellMaterial;
        public Material DefaultMaterial;
    }
}