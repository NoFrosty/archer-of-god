using UnityEngine;

namespace ArcherOfGod.Shared
{
    public class CharacterFaction : MonoBehaviour
    {
        [SerializeField] private Faction faction;
        public Faction Faction => faction;

        public bool IsEnemy(Faction other)
        {
            return faction != other;
        }

        public bool IsAlly(Faction other)
        {
            return faction == other;
        }

        public Faction GetEnemyFaction()
        {
            return faction == Faction.Player ? Faction.Enemy : Faction.Player;
        }
    }
}
