namespace Toyota.Shared.Utilities
{
    public class Constants
    {
        #region UserPass

        public static readonly string ToyotaDummyPassword = "liajsdlikjawij2902mnsnc89uy1234j2l";
        public static readonly string ToyotaCustomerDummyPassword = "dt4g8ret89er4tyheyt5h1"; 
        public static readonly string ToyotaEmployeeDummyPassword = "liajsdlikjawij2902mnsnc89uy1234j2l";
        public static readonly string UserPass = "Ebi25.";

        #endregion

        #region ResultCodes
        public static readonly int ServiceErrorCode = -3;
        public static readonly int ValidationErrorCode = -2;
        public static readonly int ErrorCode = -1;
        public static readonly int SuccessCode = 0;
        public static readonly int ConfirmCode = 1;

        #endregion ResultCodes

        #region ErrorMessages
        public static readonly string EmptyErrorMessage = "Kayıt Bulunamadı.";
        public static readonly string EmptyRequestErrorMessage = "Request alanları boş bırakılamaz.";
        public static readonly string ExceptionErrorMessage = "Beklenmedik bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
        public static readonly string ErrorMessage = "İşlem başarısız. Lütfen daha sonra tekrar deneyiniz.";
        public static readonly string ExistErrorMessage = "Kayıt mevcut. Lütfen kontrol ettikten sonra tekrar deneyiniz.";
        public static readonly string FileTypeErrorMessage = "Hatalı dosya biçimi.";

        #endregion

        #region SuccessMessge
        public static readonly string TransactionSuccessfulMessage = "İşlem Başarılı.";
        #endregion

        #region HttpClientServiceNames

        public static readonly string HtppClientToyotaApiServiceName = "ToyotaApiService";
        public static readonly string HtppClientToyotaApiNoTokenServiceName = "ToyotaApiNoTokenService";

        #endregion

        #region CultureCode

        public static readonly string TRLanguage = "tr-TR";
        public static readonly string ENLanguage = "en-US";
        public static readonly string ESLanguage = "es-ES";

        #endregion

        #region PlatformCode

        public static readonly string AndroidPlatformCode = "android";
        public static readonly string IosPlatformCode = "ios";
        public static readonly string WebPlatformCode = "web";

        #endregion

        #region Verification
        public static readonly string Yes = "yes";
        #endregion

        public static readonly string SetReadUncommited = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED";
        public static readonly string SetReadCommited = "SET TRANSACTION ISOLATION LEVEL READ COMMITTED";
        public static readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        #region ConfigurationParameter
        public static readonly string SwaggerStatusOpen = "Open";
        public static readonly string SwaggerStatusClosed = "Closed";
        #endregion


        #region Environment
        public static readonly string EnvironmentStb = "stb";
        public static readonly string EnvironmentProd = "prod";
        public static readonly string EnvironmentDev = "dev";

        #endregion

    }
}