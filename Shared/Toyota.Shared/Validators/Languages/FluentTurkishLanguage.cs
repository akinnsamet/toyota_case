namespace Toyota.Shared.Validators.Languages
{
    internal class FluentTurkishLanguage
    {
        public const string Culture = "tr-TR";

        public static string? GetTranslation(string key) => key switch
        {
            "EmailValidator" => "{PropertyName} geçerli bir e-posta adresi değil.",
            "GreaterThanOrEqualValidator" => "{PropertyName} değeri {ComparisonValue} değerinden büyük veya eşit olmalı.",
            "GreaterThanValidator" => "{PropertyName} değeri {ComparisonValue} değerinden büyük olmalı.",
            "LengthValidator" => "{PropertyName}, {MinLength} ve {MaxLength} arasında karakter uzunluğunda olmalı . Toplam {TotalLength} adet karakter girdiniz.",
            "MinimumLengthValidator" => "{PropertyName}, {MinLength} karakterden büyük veya eşit olmalıdır. {TotalLength} karakter girdiniz.",
            "MaximumLengthValidator" => "{PropertyName}, {MaxLength} karakterden küçük veya eşit olmalıdır. {TotalLength} karakter girdiniz.",
            "LessThanOrEqualValidator" => "{PropertyName}, {ComparisonValue} değerinden küçük veya eşit olmalı.",
            "LessThanValidator" => "{PropertyName}, {ComparisonValue} değerinden küçük olmalı.",
            "NotEmptyValidator" => "{PropertyName} boş olmamalı.",
            "NotEqualValidator" => "{PropertyName}, {ComparisonValue} değerine eşit olmamalı.",
            "NotNullValidator" => "{PropertyName} boş olamaz.",
            "PredicateValidator" => "Belirtilen durum {PropertyName} için geçerli değil.",
            "AsyncPredicateValidator" => "Belirtilen durum {PropertyName} için geçerli değil.",
            "RegularExpressionValidator" => "{PropertyName} değerinin formatı doğru değil.",
            "EqualValidator" => "{PropertyName}, {ComparisonValue} değerine eşit olmalı.",
            "ExactLengthValidator" => "{PropertyName}, {MaxLength} karakter uzunluğunda olmalı. {TotalLength} adet karakter girdiniz.",
            "InclusiveBetweenValidator" => "{PropertyName}, {From} ve {To} arasında olmalı. {PropertyValue} değerini girdiniz.",
            "ExclusiveBetweenValidator" => "{PropertyName}, {From} ve {To} (dahil değil) arasında olmalı. {PropertyValue} değerini girdiniz.",
            "CreditCardValidator" => "{PropertyName} geçerli bir kredi kartı numarası değil.",
            "ScalePrecisionValidator" => "{PropertyName}, {ExpectedScale} ondalıkları için toplamda {ExpectedPrecision} rakamdan fazla olamaz. {Digits} basamak ve {ActualScale} basamak bulundu.",
            "EmptyValidator" => "{PropertyName} boş olmalıdır.",
            "NullValidator" => "{PropertyName} boş olmalıdır.",
            "EnumValidator" => "{PropertyName}, {PropertyValue} içermeyen bir değer aralığı içeriyor.",
            // Additional fallback messages used by clientside validation integration.
            "Length_Simple" => "{PropertyName}, {MinLength} ve {MaxLength} arasında karakter uzunluğunda olmalı.",
            "MinimumLength_Simple" => "{PropertyName}, {MinLength} karakterden büyük veya eşit olmalıdır.",
            "MaximumLength_Simple" => "{PropertyName}, {MaxLength} karakterden küçük veya eşit olmalıdır.",
            "ExactLength_Simple" => "{PropertyName}, {MaxLength} karakter uzunluğunda olmalı.",
            "InclusiveBetween_Simple" => "{PropertyName}, {From} ve {To} arasında olmalı.",
            "PasswordMinLength" => "{PropertyName}, en az 8 karakter olmalıdır.",
            "PasswordUpperCase" => "{PropertyName}, en az bir büyük harf içermelidir.",
            "PasswordLowerCase" => "{PropertyName}, en az bir küçük harf içermelidir.",
            "PasswordLeastLetter" => "{PropertyName}, en az bir harf içermelidir.",
            "PasswordOneLeastNumber" => "{PropertyName}, en az bir rakam içermelidir.",
            "PasswordSpecialChar" => "{PropertyName}, en az bir özel karakter içermelidir (!@#$&*).",
            "UserRewardsCountRange" => "{PropertyName}, kazanacak üye sayısı 1'den küçük olamaz.",
            _ => null,
        };

        public static string? GetPropertyTranslation(string key) => key switch
        {
            "P_Name" => "Adı",
            "P_SurName" => "Soyadı",
            "P_Password" => "Şifre",
            "P_GroupName" => "Grup Adı",
            "P_Amount" => "Tutar",
            "P_Quantity" => "Miktar",
            "P_CouponCode" => "Kupon Kodu",
            "P_Email" => "E-Posta",
            "P_ForgotPasswordCode" => "Doğrulama Kodu",
            "P_Message" => "Mesaj",
            "P_ContactInfo" => "İletişim Bilgisi",
            "P_SubjectInfo" => "Konu İçeriği",
            "P_Culture" => "Dil Seçimi",
            "P_Location" => "Lokasyon Bilgisi",
            "P_Page" => "Sayfa Seçimi",
            "P_Type" => "Tip Seçimi",
            "P_IsOpen" => "Açık mı?",
            "P_IsDefault" => "Varsayılan mı?",
            "P_IsBanned" => "Banlansın mı?",
            "P_RankingName" => "Klasman İsmi",
            "P_UserRewardsCount" => "Kazanacak Üye Sayısı",
            _ => null,
        };

    }
}
