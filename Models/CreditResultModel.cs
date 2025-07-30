using System.Collections.Generic;

namespace SimpleFrameworkApp.Models
{
    public class CreditResultModel
    {
        public decimal MontantEmprunteBrut { get; set; }
        public decimal FraisAchat { get; set; }
        public decimal FraisHypotheque { get; set; }
        public decimal MontantEmprunteNet { get; set; }
        public decimal Mensualite { get; set; }
        public decimal TauxMensuel { get; set; }
        public decimal FondsPropres { get; set; }
        public List<AmortissementLigne> TableauAmortissement { get; set; }
    }
}