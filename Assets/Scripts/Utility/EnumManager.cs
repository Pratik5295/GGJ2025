
namespace GGJ.MetaConstants
{

    public static class EnumManager
    {
        public enum ValveState
        {
            WORKING = 0,
            BROKEN = 1,
            REPAIR = 2     //Denotes Valve repair as well as Oxygen Valve refill
        }

        public enum OxyState
        {
            INUSE = 0,
            EMPTY = 1,
            RECHARE = 2,
            FULL = 3     //Denotes Valve repair as well as Oxygen Valve refill
        }


        public enum StationState
        {
            WORKING = 0,
            BROKEN = 1  
        }

        /// <summary>
        /// Type of object the player is carrying
        /// </summary>
        public enum CarryType
        {
            DEFAULT = 0,
            OXYGEN = 1
        }


        public enum PickState
        {
            DROP = 0,
            HELD = 1
        }

        public enum OxyStatonState
        {
            FULL = 0,
            NEEDOXY = 1
        }
    }
}
