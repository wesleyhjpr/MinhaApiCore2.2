using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MinhaApiCore.Controllers
{
    [Route("api/[controller]")]
    //[ApiConventionType(typeof(DefaultApiConventions)]
    public class ValuesController : MainController
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> ObterTodos()
        {
            var valores = new string[] { "value1", "value2" };

            if (valores.Length < 5000)
                return BadRequest();

            return Ok(valores);
        }
        // GET api/values
        [HttpGet("obter-valores")]
        public IEnumerable<string> ObterValores()
        {
            var valores = new string[] { "value1", "value2" };

            if (valores.Length < 5000)
                return null;

            return valores;
        }
        // GET api/values
        [HttpGet("obter-resultado")]
        public ActionResult ObterResultado()
        {
            var valores = new string[] { "value1", "value2" };

            if (valores.Length < 5000)
                return CustomResponse();

            return CustomResponse(valores);
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        //[ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public ActionResult Post([FromBody] Product product)
        {
            if (product.Id == 0) return BadRequest();

            //add no banco
            
            return CreatedAtAction(nameof(Post), product);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put([FromRoute]int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (id != product.Id) return NotFound();

            //add no banco

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete([FromQuery]int id)
        {
        }
    }

    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
                return Ok(new { success = true, data = result });

            return BadRequest(new { success = false, errors = ObterErrors() });
        }

        public bool OperacaoValida()
        {
            //as minhas validaçoes
            return true;
        }

        protected string ObterErrors()
        {
            return "";
        }
    }

    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
