using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ModuloApi.Context;
using ModuloApi.Entities;
using System.Xml;

namespace ModuloApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        private readonly AgendaContext _context;

        public ContatoController(AgendaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(Contato contato)
        {
            try
            {
                if (contato != null)
                {
                    _context.Add(contato);
                    _context.SaveChanges();
                    return CreatedAtAction(nameof(ObterPorId), new { id = contato.Id }, contato); // retorna o link para obter o contato recem criado
                }
                else
                {
                    return NotFound("O objeto de contato fornecido é nulo.");
                }
            }
            catch (ArgumentNullException ex)
            {
                // Trata especificamente o caso em que contato é nulo
                return BadRequest("O objeto de contato não pode ser nulo.");
            }
            catch (Exception ex)
            {
                // Trata outras exceções genéricas
                return BadRequest("Ocorreu um erro ao processar a requisição: " + ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public IActionResult ObterPorId(int id)
        {
            try
            {
                if (id != 0)
                {
                    var contato = _context.Contatos.Find(id);

                    if (contato == null)
                        return NotFound("Id não encontrado na base de dados");
                    else
                        return Ok(contato);
                }
                else
                {
                    return NotFound("Id invalido");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ObterPorNome")]
        public IActionResult ObterPorNome(string nome)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(nome))
                {
                    var contato = _context.Contatos.Where(x => x.Nome == nome);

                    if (contato.IsNullOrEmpty())
                        return NotFound("Nome não encontrado na base de dados");
                    else
                        return Ok(contato);
                }
                else
                {
                    return NotFound("Nome invalido");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // poderia usar o path que posso atualizar parcialmente e não completamente os dados da tabela
        [HttpPut("{Id}")]
        public IActionResult Atualizar(int Id, Contato contato)
        {
            try
            {
                if (Id != 0)
                {
                    var contatoBanco = _context.Contatos.Find(Id);

                    if (contatoBanco == null)
                        return NotFound("Id não encontrado na base de dados");
                    else
                    {
                        contatoBanco.Nome = contato.Nome;
                        contatoBanco.Telefone = contato.Telefone;
                        contato.Ativo = contato.Ativo;

                        _context.Contatos.Update(contatoBanco);
                        int rowsAffected = _context.SaveChanges();

                        if (rowsAffected > 0)
                            return Ok(contatoBanco);
                        else
                            return NotFound("erro ao salvar no banco de dados");
                    }
                }
                else
                {
                    return NotFound("Id invalido");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult deletar(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    var contatoBanco = _context.Contatos.Find(Id);

                    if (contatoBanco == null)
                        return NotFound("Id não encontrado na base de dados");
                    else
                    {
                        _context.Contatos.Remove(contatoBanco);
                        int rowsAffected = _context.SaveChanges();

                        if (rowsAffected > 0)
                            return NoContent();
                        else
                            return NotFound("erro ao deletar do banco de dados");
                    }
                }
                else
                {
                    return NotFound("Id invalido");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}