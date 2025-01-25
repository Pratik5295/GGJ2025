
namespace GGJ.MetaConstants
{

    public static class EnumManager
    {
        public enum ValveState
        {
            WORKING = 0,
            BROKEN = 1,
            REPAIR = 2     //Denotes the valve is being repaired currently
        }


        public enum StationState
        {
            WORKING = 0,
            BROKEN = 1  
        }
    }
}
