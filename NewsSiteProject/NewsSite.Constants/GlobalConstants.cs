namespace NewsSite.Constants
{
    public class GlobalConstants
    {
        public const string AdminRole = "Admin";
        public const string ReporterRole = "Reporter";

        public static string AdminEmailUsername = "zxc@zxc.zxc";
        public static string AdminPassword = "------";

        public static string YasenGithub = "https://github.com/yasenm";
        public static string PlamenGithub = "https://github.com/plamenyovchev";
        public static string StefanGithub = "https://github.com/StefanDimov";

        public static int AdminPageSize = 15;

        public static string IndexPage = "Index";

        public static string HorizontalType = "Хоризонтална";
        public static string VerticalType = "Вертикална";
        public static string SquareType = "Квадратна";

        public static string[] AdvertismentTypes =  {
                                                        HorizontalType,
                                                        VerticalType,
                                                        SquareType
                                                    };

        public static string RecaptchaPublicKey = "6LdjyQMTAAAAAG4Bxr7sd_V4GVE8TSdeaPl1ryxF";
        public static string RecaptchaSecretKey = "6LdjyQMTAAAAAMjEx8bVtZXyDfDABquEH6-VqTuK";
    }
}
