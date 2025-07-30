namespace SimpleFrameworkApp.Models
{
    public class CreditInputModel
    {
        public decimal MontantAchat { get; set; }
        public decimal FondsPropres { get; set; }
        public int DureeMois { get; set; }
        public decimal TauxAnnuel { get; set; }
        public decimal? FraisAchat { get; set; } 
        public decimal? MontantEmprunte { get; set; } 
        public bool FraisAchatAuto { get; set; } = true;
        public bool MontantEmprunteAuto { get; set; } = true;
    }
}