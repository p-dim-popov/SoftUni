using System.ComponentModel.DataAnnotations;

namespace BattleCards.ViewModels.Cards
{
    public class CardAddInputModel
    {
        [Required(ErrorMessage =
            "Name" + ValidationHelper.RequiredErrorMessage)] 
        [RegularExpression(@"^\S{5,15}$", ErrorMessage = 
            "Name" + ValidationHelper.RegexErrorMessage+" . Minimum length is 5, maximum length is 15")]
        public string Name { get; set; }

        [Required(ErrorMessage = 
            "Image url"+ ValidationHelper.RequiredErrorMessage)]
        public string Image { get; set; }

        [Required(ErrorMessage = 
            "Keyword" + ValidationHelper.RequiredErrorMessage)]
        public string Keyword { get; set; }

        [Required(ErrorMessage = 
            "Attack" + ValidationHelper.RequiredErrorMessage)]
        [Range(0, int.MaxValue, ErrorMessage =
            "Attack" + ValidationHelper.RangeErrorMessage + "0 - 2147483647")]
        public int Attack { get; set; }

        [Required(ErrorMessage = 
            "Health" + ValidationHelper.RequiredErrorMessage)]
        [Range(0, int.MaxValue, ErrorMessage = 
            "Health" + ValidationHelper.RangeErrorMessage + "0 - 2147483647")]
        public int Health { get; set; }

        [Required(ErrorMessage = 
            "Description" + ValidationHelper.RequiredErrorMessage)]
        [RegularExpression(@"^.{0,200}$", ErrorMessage = 
            "Description" + ValidationHelper.MaxLengthErrorMessage + "200")]
        public string Description { get; set; }
    }
}
