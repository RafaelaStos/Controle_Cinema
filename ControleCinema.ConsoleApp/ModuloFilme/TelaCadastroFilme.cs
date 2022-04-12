using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloGenero;
using System;
using System.Collections.Generic;

namespace ControleCinema.ConsoleApp.ModuloFilme
{
    public class TelaCadastroFilme : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Filme> _repositorioFilme;
        private readonly Notificador _notificador;
        private readonly IRepositorio<Genero> repositorioGenero;
        private readonly TelaCadastroGenero telaCadastroGenero;

        public TelaCadastroFilme(IRepositorio<Filme> repositorioFilme, TelaCadastroGenero telaCadastroGenero, IRepositorio<Genero> repositorioGenero, Notificador notificador)
            : base("Cadastro de Filme")
        {
            _repositorioFilme = repositorioFilme;
            _notificador = notificador;
            this.repositorioGenero = repositorioGenero;
            this.telaCadastroGenero = telaCadastroGenero;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro  Filme");
            Genero generoSelecionado = ObtemGenero();
         
            Filme novoFilme =   ObterFilme(generoSelecionado);
            _repositorioFilme.Inserir(novoFilme);

            _notificador.ApresentarMensagem("Gênero de Filme cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando o Filme");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum filme cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroFilme = ObterNumeroRegistro();
            Genero generoSelecionado = ObtemGenero();
            Filme FilmeAtualizado = ObterFilme(generoSelecionado);

            bool conseguiuEditar = _repositorioFilme.Editar(numeroFilme, FilmeAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem(" Filme editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo  Filme");

            bool temFuncionariosRegistrados = VisualizarRegistros("Pesquisando");

            if (temFuncionariosRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum filme cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroFilme = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioFilme.Excluir(numeroFilme);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Gênero de Filme excluído com sucesso1", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização dos Filme");

            List<Filme> Filmes = _repositorioFilme.SelecionarTodos();

            if (Filmes.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum filme de filme disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Filme Filme in Filmes)
                Console.WriteLine(Filme.ToString());

            Console.ReadLine();

            return true;
        }
         

        private Filme ObterFilme(Genero generoSelecionado)
        {
            Console.Write("Digite o titulo do filme: ");
            string titulo = Console.ReadLine(); 

            
            Console.Write("Digite a ducação do filme(em min): ");
            int duracao = int.Parse(Console.ReadLine());

            return new Filme(titulo, duracao,generoSelecionado);
        }

        private Genero ObtemGenero( )
        {
           
            bool temGeneroCadastrado = telaCadastroGenero.VisualizarRegistros("");
            if (!temGeneroCadastrado)
            {
                _notificador.ApresentarMensagem("Você precisa cadastrar um genero antes de um filme!", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o ID do gênero que deseja Selecionar: ");
            int numerogeneroSelecionado = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            Genero generoSelecionado = repositorioGenero.SelecionarRegistro(numerogeneroSelecionado);

            return generoSelecionado;

        }
        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do gênero de filme que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioFilme.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do gênero de filme não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

    }
}
