using ListaTarefas.Data;
using ListaTarefas.Models;
using Microsoft.AspNetCore.Mvc;

namespace Listratarefas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly UsuarioContext _context;
        public TarefaController(UsuarioContext context)
        {
            _context = context;

        }
        [HttpDelete("Deletar/{id}")]
        public IActionResult DeletarTarefa(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa == null)
                return NotFound("Tarefa não encontrada.");
            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();

            return NoContent();
        }
        [HttpPut("atualizar/{id}")]

        public IActionResult AtualizarTarefas(int id, Tarefa tarefa)
        {
            var TarefaDoBanco = _context.Tarefas.Find(id);

            if (TarefaDoBanco == null)
                return NotFound("Tarefa não encontrada");
            TarefaDoBanco.Descricao = tarefa.Descricao;
            TarefaDoBanco.Status = tarefa.Status;

            _context.SaveChanges();

            return Ok("Atualizado");

        }


        [HttpGet("{id}")]
        public IActionResult ConsultarPessoaId(int id)
        {
            var tarefaDoBanco = _context.Tarefas.Where(t => t.Id.Equals(id)).ToList();
            if (!tarefaDoBanco.Any())
                return NotFound("Não encontrado");
            return Ok(tarefaDoBanco);
        }
        [HttpPost("Cadastrar")]
        public IActionResult CriarTarefas(Tarefa tarefa)
        {
            var idUsuario = HttpContext.Session.GetString("IdLogado");
            if (idUsuario == null) return Unauthorized("não autorizado");

            var sessao = Request.Cookies["IdLogado"];

            if (sessao != null)
            {

                tarefa.IdUsuario = int.Parse(sessao);

            }
            _context.Add(tarefa);
            _context.SaveChanges();
            return Created("Teste", tarefa);
        }
        [HttpGet("tarefaUsuario/{ident}")]
        public IActionResult TarefaUsuario(int ident)

        {
            var idUsuario = HttpContext.Session.GetString("IdLogado");
            if (idUsuario == null) return Unauthorized("Faça login antes! ");

            var sessao = Request.Cookies["IdLogado"];

            var resultado = from u in _context.Usuarios
                            join t in _context.Tarefas
                            on u.Id equals t.IdUsuario
                            where u.Id == int.Parse(idUsuario)
                            select new
                            {
                                Usuario = u.Nome,
                                u.Email,
                                Tarefas = t.Status,
                                t.Descricao,t.Id

                            };
            return Ok(resultado.ToList());
        }

    }

}


