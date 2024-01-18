namespace Cars_automobile.Helpers
{
    public static class Common
    {

        public static bool CheckId(int? id)
        {
            if (id < 1 || id == null) return false;

            return true;
        }

        public static bool CheckGuId(Guid? guId)
        {
            if (guId == null) return false;

            return true;
        }
    }
}
