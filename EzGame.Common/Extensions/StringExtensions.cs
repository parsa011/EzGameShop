namespace EzGame.Common.Extensions
{
    public static class StringExtensions
    {
        public static string MakeUrlString(this string str)
        {
            return str.Replace("#", "sharp").Replace(" ", "_");
        }

        public static string ExecuteUrlString(this string str,bool replaceSharp)
        {
            return replaceSharp ? str.Replace("sharp", "#").Replace("_", " ") : str.Replace("_", " ");
        }
    }
}