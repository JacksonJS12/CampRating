namespace CampRating.Common;

public static class EntityValidationConstants
{
    public static class Camp
    {
        public const int NameMinLength = 3;
        public const int NameMaxLength = 64;
        
        public const int DescriptionMinLength = 3;
        public const int DescriptionMaxLength = 255;
        
        public const int DecimalScale = 2;
        public const int DecimalPrecision = 18;
    }

    public static class Rating
    {
        public const int RatingMinValue = 1;
        public const int RatingMaxValue = 5;
        
        public const int CommentMaxLenght = 255;
    }

    public static class User
    {
        public const int FirstNameMinLength = 2;
        public const int FirstNameMaxLength = 64;
        
        public const int LastNameMinLength = 2;
        public const int LastNameMaxLength = 64;
        
        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 128;
    }
}