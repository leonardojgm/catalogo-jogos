using System.Collections.Generic;
using ApiCatalogoJogos.Exceptions;
using ApiCatalogoJogos.Entities;
using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.Repositories;
using ApiCatalogoJogos.ViewModel;

namespace ApiCatalogoJogos.Services
{
    public class JogoService(IJogoRepository jogoRepository) : IJogoService
    {
        private readonly IJogoRepository _jogoRepository = jogoRepository;

        public async Task<List<JogoViewModel>> Obter(int pagina, int quantidade)
        {
            List<Jogo> jogos = await _jogoRepository.Obter(pagina, quantidade);

            return jogos.Select(jogo => new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            }).ToList();
        }

        public async Task<JogoViewModel> Obter(Guid id)
        {
            Jogo jogo = await _jogoRepository.Obter(id) ?? throw new JogoNaoCadastradoException();
            
            return new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task<JogoViewModel> Inserir(JogoInputModel jogo)
        {
            List<Jogo> jogosExistente = await _jogoRepository.Obter(jogo.Nome, jogo.Produtora);

            if (jogosExistente.Count > 0) throw new JogoJaCadastradoException();

            Jogo novoJogo = new()
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };

            await _jogoRepository.Inserir(novoJogo);

            return new JogoViewModel
            {
                Id = novoJogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task Atualizar(Guid id, JogoInputModel jogo)
        {
            Jogo entidadeJogo = await _jogoRepository.Obter(id) ?? throw new JogoNaoCadastradoException();

            entidadeJogo.Nome = jogo.Nome;
            entidadeJogo.Produtora = jogo.Produtora;
            entidadeJogo.Preco = jogo.Preco;

            await _jogoRepository.Atualizar(entidadeJogo);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            Jogo entidadeJogo = await _jogoRepository.Obter(id) ?? throw new JogoNaoCadastradoException();

            entidadeJogo.Preco = preco;

            await _jogoRepository.Atualizar(entidadeJogo);
        }

        public async Task Remover(Guid id)
        {
            Jogo jogo = await _jogoRepository.Obter(id) ?? throw new JogoNaoCadastradoException();

            await _jogoRepository.Remover(id);
        }

        public void Dispose()
        {
            _jogoRepository?.Dispose();
        }
    }
}