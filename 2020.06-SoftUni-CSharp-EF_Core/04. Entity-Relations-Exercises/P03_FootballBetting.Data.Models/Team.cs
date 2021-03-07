namespace P03_FootballBetting.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

    public class Team
    {
        [Key]
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        [Column(TypeName= "char(3)")]
        public string Initials { get; set; }
        public decimal Budget { get; set; }
        public int PrimaryKitColorId { get; set; }
        public Color PrimaryKitColor { get; set; }
        public int SecondaryKitColorId { get; set; }
        public Color SecondaryKitColor { get; set; }
        public int TownId { get; set; }
        public Town Town { get; set; }

        [InverseProperty("HomeTeam")]
        public virtual ICollection<Game> HomeGames { get; set; } = new HashSet<Game>();
        [InverseProperty("AwayTeam")]
        public virtual ICollection<Game> AwayGames { get; set; } = new HashSet<Game>();

        public virtual ICollection<Player> Players { get; set; } = new HashSet<Player>();
    }
}
