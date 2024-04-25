namespace API.Utils
{
    public static class Labels
    {
        public const string DOLLAR = "dollar";
        public const string DOLLARS = "dollars";
        public const string CENT = "cent";
        public const string CENTS = "cents";

        public const string HUNDRED = "hundred";
        public const string THOUSAND = "thousand";
        public const string MILLION = "million";

        public const string AND = "and";

        public static Dictionary<int, string> ONES = new Dictionary<int, string>()
        {
            { 0, "zero" },
            { 1, "one" },
            { 2, "two" },
            { 3, "three" },
            { 4, "four" },
            { 5, "five" },
            { 6, "six" },
            { 7, "seven" },
            { 8, "eight" },
            { 9, "nine" },
        };

        public static Dictionary<int, string> TEENS = new Dictionary<int, string>()
        {
            { 10, "ten" },
            { 11, "eleven" },
            { 12, "twelve" },
            { 13, "thirteen" },
            { 14, "fourteen" },
            { 15, "fifteen" },
            { 16, "sixteen" },
            { 17, "seventeen" },
            { 18, "eighteen" },
            { 19, "nineteen" },
        };

        public static Dictionary<int, string> TEES = new Dictionary<int, string>()
        {
            { 20, "twenty" },
            { 30, "thirty" },
            { 40, "forty" },
            { 50, "fifty" },
            { 60, "sixty" },
            { 70, "seventy" },
            { 80, "eighty" },
            { 90, "ninety" },
        };
    }
}