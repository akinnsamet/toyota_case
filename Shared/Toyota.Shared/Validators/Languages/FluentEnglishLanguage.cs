namespace Toyota.Shared.Validators.Languages
{
    internal class FluentEnglishLanguage
    {
		public const string Culture = "en";
		public const string AmericanCulture = "en-US";
		public const string BritishCulture = "en-GB";

		public static string? GetTranslation(string key) => key switch
		{
			"EmailValidator" => "{PropertyName} is not a valid email address.",
			"GreaterThanOrEqualValidator" => "{PropertyName} must be greater than or equal to {ComparisonValue}.",
			"GreaterThanValidator" => "{PropertyName} must be greater than {ComparisonValue}.",
			"LengthValidator" => "{PropertyName} must be between {MinLength} and {MaxLength} characters. You entered {TotalLength} characters.",
			"MinimumLengthValidator" => "The length of {PropertyName} must be at least {MinLength} characters. You entered {TotalLength} characters.",
			"MaximumLengthValidator" => "The length of {PropertyName} must be {MaxLength} characters or fewer. You entered {TotalLength} characters.",
			"LessThanOrEqualValidator" => "{PropertyName} must be less than or equal to {ComparisonValue}.",
			"LessThanValidator" => "{PropertyName} must be less than {ComparisonValue}.",
			"NotEmptyValidator" => "{PropertyName} must not be empty.",
			"NotEqualValidator" => "{PropertyName} must not be equal to {ComparisonValue}.",
			"NotNullValidator" => "{PropertyName} must not be empty.",
			"PredicateValidator" => "The specified condition was not met for {PropertyName}.",
			"AsyncPredicateValidator" => "The specified condition was not met for {PropertyName}.",
			"RegularExpressionValidator" => "{PropertyName} is not in the correct format.",
			"EqualValidator" => "{PropertyName} must be equal to {ComparisonValue}.",
			"ExactLengthValidator" => "{PropertyName} must be {MaxLength} characters in length. You entered {TotalLength} characters.",
			"InclusiveBetweenValidator" => "{PropertyName} must be between {From} and {To}. You entered {PropertyValue}.",
			"ExclusiveBetweenValidator" => "{PropertyName} must be between {From} and {To} (exclusive). You entered {PropertyValue}.",
			"CreditCardValidator" => "{PropertyName} is not a valid credit card number.",
			"ScalePrecisionValidator" => "{PropertyName} must not be more than {ExpectedPrecision} digits in total, with allowance for {ExpectedScale} decimals. {Digits} digits and {ActualScale} decimals were found.",
			"EmptyValidator" => "{PropertyName} must be empty.",
			"NullValidator" => "{PropertyName} must be empty.",
			"EnumValidator" => "{PropertyName} has a range of values which does not include {PropertyValue}.",
			// Additional fallback messages used by clientside validation integration.
			"Length_Simple" => "{PropertyName} must be between {MinLength} and {MaxLength} characters.",
			"MinimumLength_Simple" => "The length of {PropertyName} must be at least {MinLength} characters.",
			"MaximumLength_Simple" => "The length of {PropertyName} must be {MaxLength} characters or fewer.",
			"ExactLength_Simple" => "{PropertyName} must be {MaxLength} characters in length.",
			"InclusiveBetween_Simple" => "{PropertyName} must be between {From} and {To}.",
			"PasswordMinLength" => "{PropertyName}, length must be at least 8.",
			"PasswordUpperCase" => "{PropertyName}, must contain at least one uppercase letter.",
			"PasswordLowerCase" => "{PropertyName}, must contain at least one lowercase letter.",
			"PasswordLeastLetter" => "{PropertyName}, must contain at least one letter.",
			"PasswordOneLeastNumber" => "{PropertyName}, must contain at least one number.",
			"PasswordSpecialChar" => "{PropertyName}, must contain at least one (!@#$&*).",
            "UserRewardsCountRange" => "{PropertyName}, The number of members to win cannot be less than 1.",
            _ => null,
		};

        public static string? GetPropertyTranslation(string key) => key switch
        {
            "P_Name" => "Name",
            "P_SurName" => "SurName",
            "P_Password" => "Password",
            "P_GroupName" => "Group Name",
            "P_Amount" => "Amount",
            "P_Quantity" => "Quantity",
			"P_CouponCode" => "Coupon Code",
            "P_Email" => "Email",
            "P_ForgotPasswordCode" => "Confirmation Code",
            "P_Message" => "Message",
            "P_ContactInfo" => "Contact Information",
            "P_SubjectInfo" => "Subject Information",
            "P_Culture" => "Language Selection",
            "P_Location" => "Location Information",
            "P_Page" => "Page Selection",
            "P_Type" => "Type Selection",
            "P_IsOpen" => "Is Open?",
            "P_IsDefault" => "Is Default?",
            "P_IsBanned" => "Is Banned?",
            "P_RankingName" => "Ranking Name",
            "P_UserRewardsCount" => "User Rewards Count",
            _ => null,
        };
	}
}
