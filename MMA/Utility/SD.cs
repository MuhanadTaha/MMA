namespace MMA.Utility
{
    public static class SD
    {
        public const string ManagerUser = "Admin";
        public const string CusotmerEndUser = "User";
        public const string TeacherUser = "Teacher";




        public static string ConvertToRawHtml(string source) //static function
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                if (i < 25)
                {
                    char let = source[i];
                    if (let == '<')
                    {
                        inside = true;
                        continue;
                    }
                    if (let == '>')
                    {
                        inside = false;
                        continue;
                    }
                    if (!inside)
                    {
                        array[arrayIndex] = let;
                        arrayIndex++;
                    }
                }
                else
                {
                    break;
                }
               
            }
            return new string(array, 0, arrayIndex);
        }
    }
}
