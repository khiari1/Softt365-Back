using SimpleFrameworkApp.Models;
using SimpleFrameworkApp.Services;
using System.Web.Http;

namespace SimpleFrameworkApp.Controllers
{
    public class CreditController : ApiController
    {

        [HttpPost]
        [Route("api/credit/calculate")]
        public IHttpActionResult Calculate([FromBody] CreditInputModel input)
        {
            if (input == null)
                return BadRequest("Aucune donnée reçue.");

            if (input.MontantAchat <= 0)
                return BadRequest("Le montant d'achat doit être > 0.");
            if (input.DureeMois <= 0)
                return BadRequest("La durée doit être > 0.");
            if (input.TauxAnnuel <= 0)
                return BadRequest("Le taux annuel doit être > 0.");
            if (input.FondsPropres < 0)
                return BadRequest("Les fonds propres ne peuvent pas être négatifs.");

            var service = new CreditCalculatorService();
            var result = service.Calculate(input);
            return Ok(result);
        }
    }
}