using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiCatalogoJogos.Exceptions;
using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.Services;
using ApiCatalogoJogos.ViewModel;

namespace ApiCatalogoJogos.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    public class JogosController(IJogoService jogoService) : ControllerBase
    {
        private readonly IJogoService _jogoService = jogoService;

        /// <summary>Buscar todos os jogos de forma paginada </summary>
        /// <remarks>Não é possível retornar os jogos sem paginação</remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de jogos</response>
        /// <response code="204">Caso não haja jogos</response>   
        [HttpGet]
        public async Task<ActionResult<List<JogoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            List<JogoViewModel> resultado = await _jogoService.Obter(pagina, quantidade);

            if (resultado.Count() == 0) return NoContent();

            return Ok(resultado);
        }

        /// <summary>Buscar um jogo pelo seu Id</summary>
        /// <param name="id">Id do jogo buscado</param>
        /// <response code="200">Retorna o jogo filtrado</response>
        /// <response code="204">Caso não haja jogo com este id</response>   
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid id)
        {
            try
            {
                JogoViewModel resultado = await _jogoService.Obter(id);

                return Ok(resultado);
            }
            catch(JogoNaoCadastradoException)
            {
                return NoContent();
            }
        }

        /// <summary>Inserir um jogo no catálogo</summary>
        /// <param name="jogo">Dados do jogo a ser inserido</param>
        /// <response code="200">Cao o jogo seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um jogo com mesmo nome para a mesma produtora</response>   
        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> Inserir([FromBody] JogoInputModel jogo)
        {
            try
            {
                JogoViewModel resultado = await _jogoService.Inserir(jogo);

                return Ok(resultado);
            }
            catch(JogoJaCadastradoException)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
        }

        /// <summary>Atualizar um jogo no catálogo</summary>
        /// <param name="id">Id do jogo a ser atualizado</param>
        /// <param name="jogo">Novos dados para atualizar o jogo indicado</param>
        /// <response code="200">Cao o jogo seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>  
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, JogoInputModel jogo)
        {
            try
            {
                await _jogoService.Atualizar(id, jogo);

                return Ok();
            }
            catch(JogoNaoCadastradoException)
            {
                return NotFound("Não existe este jogo");
            }
        }

        /// <summary>Atualizar o preço de um jogo</summary>
        /// <param name="id">Id do jogo a ser atualizado</param>
        /// <param name="preco">Novo preço do jogo</param>
        /// <response code="200">Cao o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>   
        [HttpPatch("{id:guid}/preco/{preco:double}")]
        public async Task<ActionResult> Atualizar(Guid id, double preco)
        {
            try
            {
                await _jogoService.Atualizar(id, preco);
                
                return Ok();
            }
            catch(JogoNaoCadastradoException)
            {
                return NotFound("Não existe este jogo");
            }
        }

        /// <summary>Excluir um jogo</summary>
        /// <param name="id">Id do jogo a ser excluído</param>
        /// <response code="200">Cao o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um jogo com este Id</response>  
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            try
            {
                await _jogoService.Remover(id);
                
                return Ok();
            }
            catch(JogoNaoCadastradoException)
            {
                return NotFound("Não existe este jogo");
            }
        }
    }
}