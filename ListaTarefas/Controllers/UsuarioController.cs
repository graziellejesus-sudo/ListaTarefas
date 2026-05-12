using ListaTarefas.Data;
using ListaTarefas.Models;
using Microsoft.AspNetCore.Mvc;
namespace ListaTarefas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioContext _context;
        public UsuarioController(UsuarioContext context)
        {
            _context = context;

        }

        [HttpPut("Atualizar/{id}")]

        public IActionResult AtualizarPessoa(int id, Usuario usuario)
        {
            var pessoaDoBanco = _context.Usuarios.Find(id);

            if (pessoaDoBanco == null)
                return NotFound("Pessoa não encontrada!");
            pessoaDoBanco.Nome = usuario.Nome;
            pessoaDoBanco.Email = usuario.Email;
            pessoaDoBanco.Senha = usuario.Senha;
            _context.SaveChanges();

            return Ok("Atualizado");
        }

        [HttpDelete("Deletar/{id}")]
        public IActionResult DeletarPessoa(int id)
        {
            var pessoa = _context.Usuarios.Find(id);
            if (pessoa == null)
                return NotFound("Pessoa não encontrada!");
            _context.Usuarios.Remove(pessoa);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost("Cadastrar")]
        public IActionResult CadastrarUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return Created("", usuario);
        }

        [HttpGet("{id}")]
        public IActionResult ConsultarPessoaId(int id)
        {
            var pessoaDoBanco = _context.Usuarios.Find(id);

            if (pessoaDoBanco == null)
                return NotFound("Não encontrada.");
            return Ok("Vou consultar uma pessoa.");
        }

        [HttpPost("Login")]
        public IActionResult Login(Usuario dadosLogin)
        {
            var Entrar = _context.Usuarios.Where(u => u.Email.Equals(dadosLogin.Email) && u.Senha.Equals(dadosLogin.Senha)).ToList();


            if (Entrar.Count == 0)
                return Unauthorized("Email ou senha incorreta!");

            HttpContext.Session.SetString("email", dadosLogin.Email);
            Response.Cookies.Append("Idusado", Entrar[0].Id.ToString(),
             new CookieOptions
             {
                 Expires = DateTime.Now.AddMinutes(38),
                 Secure = true,
                 HttpOnly = true

             });
            return Ok("Login realizado com sucesso!");
        }

      
    }
}