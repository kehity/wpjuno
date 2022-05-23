using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using pjunoDatos;

namespace wpjuno.Controllers
{


    [RoutePrefix("api/dna")]
    public class dnaController : ApiController
    {
        
        private humandnaEntities Context = new humandnaEntities();
        private clsCadenaAdn objCadenaAdn = new clsCadenaAdn();

        [HttpPost]
        [Route("mutant")]
        public IHttpActionResult mutant([FromBody] string [] dna)
        {
            bool esMutante = false;
            try
            {
                if (dna != null)
                {

                    esMutante = objCadenaAdn.IsMutant(dna);

                    //Almacenar cadena de adn
                    cadenaADN strAdn = new cadenaADN();

                    foreach (string secuencia in dna)
                    {
                        strAdn.cadena = strAdn.cadena + secuencia + "|";
                    }

                    strAdn.esMutante = esMutante;
                    strAdn.fechaRegistro = DateTime.Now;

                    if (ModelState.IsValid)
                    {
                        Context.cadenaADN.Add(strAdn);
                        Context.SaveChanges();
                        // pendiente controlar si se almacenó el registro
                    }

                }

                if (esMutante)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(HttpStatusCode.Forbidden);
                }

            }
            catch
            {
                return BadRequest();
            }
                      
        }


        [HttpGet]
        [Route("stats")] 
        public IEnumerable<string> stats()
        {
            try
            {
                double totalCadenas;
                double cadenasMutantes;
                double ratio = 0;

                using (humandnaEntities bd = new humandnaEntities())
                {
                    totalCadenas = bd.cadenaADN.Count();
                    cadenasMutantes = bd.cadenaADN.Count(x => x.esMutante == true);
                    if (totalCadenas > 0)
                    {
                        ratio = cadenasMutantes / totalCadenas;
                    }
                }
                return new string[] { "count_mutant_dna: " + cadenasMutantes.ToString(), "count_human_dna: " + totalCadenas.ToString(), "ratio: " + ratio.ToString("0.00") };
            }
            catch(Exception e)
            {
                return new string[] { "Error: " + e.Message };
            }        
        }

    }
}
