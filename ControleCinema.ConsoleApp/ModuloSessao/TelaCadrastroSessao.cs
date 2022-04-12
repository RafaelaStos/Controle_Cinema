using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloSala;
using ControleCinema.ConsoleApp.ModuloFilme;
using System;
using System.Collections.Generic;

namespace ControleCinema.ConsoleApp.ModuloSessao
{
    public class TelaCadrastroSessao : TelaBase, ITelaCadastravel
    {
    private readonly IRepositorio<Sessao> _repositorioSessao;
    private readonly Notificador _notificador;
    private readonly IRepositorio<Sala> repositorioSala;
    private readonly TelaCadastroSala telaCadastroSala;
    private readonly IRepositorio<Filme> repositorioFilme;
    private readonly TelaCadastroFilme telaCadastroFilme;
        public TelaCadrastroSessao(IRepositorio<Sessao> repositorioSessao, TelaCadastroSala telaCadastroSala, IRepositorio<Sala> repositorioSala, TelaCadastroFilme telaCadastroFilme, IRepositorio<Filme> repositorioFilme, Notificador notificador)
        : base("Cadastro de Filme")
    {
        _notificador = notificador;
        this.telaCadastroSala = telaCadastroSala;
        this.repositorioFilme = repositorioFilme;
        this.repositorioSala = repositorioSala;
        this.telaCadastroSala = telaCadastroSala;
    }

    public void Inserir()
    {
        MostrarTitulo("Cadastro  Filme");

            Sala salaSelecionado = ObtemSala();
            Filme filmeSelecionado = ObtemFilme();
            Sessao novaSessao = ObterSessao(filmeSelecionado, salaSelecionado);
            _repositorioSessao.Inserir(novaSessao);

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

        int numeroSessao = ObterNumeroRegistro();
        Sala salaSelecionado = ObtemSala();
        Filme filmeSelecionado = ObtemFilme();
        Sessao sessaoAtualizado = ObterSessao(filmeSelecionado,salaSelecionado);

        bool conseguiuEditar = _repositorioSessao.Editar(numeroSessao, sessaoAtualizado);

        if (!conseguiuEditar)
            _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
        else
            _notificador.ApresentarMensagem(" Filme editado com sucesso!", TipoMensagem.Sucesso);
    }

    public void Excluir()
    {
        MostrarTitulo("Excluindo  Filme");

        bool temSessaoRegistrados = VisualizarRegistros("Pesquisando");

        if (temSessaoRegistrados == false)
        {
            _notificador.ApresentarMensagem("Nenhum filme cadastrado para excluir.", TipoMensagem.Atencao);
            return;
        }

        int numeroFilme = ObterNumeroRegistro();

        bool conseguiuExcluir = _repositorioSessao.Excluir(numeroFilme);

        if (!conseguiuExcluir)
            _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
        else
            _notificador.ApresentarMensagem(" Filme excluído com sucesso1", TipoMensagem.Sucesso);
    }

    public bool VisualizarRegistros(string tipoVisualizacao)
    {
        if (tipoVisualizacao == "Tela")
            MostrarTitulo("Visualização dos Filme");

        List<Sessao> sessoes= _repositorioSessao.SelecionarTodos();

        if (sessoes.Count == 0)
        {
            _notificador.ApresentarMensagem("Nenhum filme de filme disponível.", TipoMensagem.Atencao);
            return false;
        }

        foreach (Sessao Sessao in sessoes)
            Console.WriteLine(Sessao.ToString());

        Console.ReadLine();

        return true;
    }


    private Sessao ObterSessao(Filme filmeSelecionado, Sala salaSelecionada
        )
    {
        Console.Write("Digite o horario da Sessao: ");
        string hora = Console.ReadLine();



        return new Sessao(filmeSelecionado, salaSelecionada, hora);
    }

    private Filme ObtemFilme()
    {

        bool temFilmeCadastrado = telaCadastroFilme.VisualizarRegistros("");
        if (!temFilmeCadastrado)
        {
            _notificador.ApresentarMensagem("Você precisa cadastrar um filme antes de um filme!", TipoMensagem.Atencao);
            return null;
        }

        Console.Write("Digite o ID do filme que deseja Selecionar: ");
        int numeroFilmeSelecionado = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine();

        Filme filmeSelecionado = repositorioFilme.SelecionarRegistro(numeroFilmeSelecionado);

        return filmeSelecionado;

    }
        private Sala ObtemSala()
        {

            bool temSalaCadastrado = telaCadastroSala.VisualizarRegistros("");
            if (!temSalaCadastrado)
            {
                _notificador.ApresentarMensagem("Você precisa cadastrar uma sala antes de um filme!", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o ID da sala que deseja Selecionar: ");
            int numeroSalaSelecionado = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            Sala salaSelecionado = repositorioSala.SelecionarRegistro(numeroSalaSelecionado);

            return salaSelecionado;

        }
        public int ObterNumeroRegistro()
    {
        int numeroRegistro;
        bool numeroRegistroEncontrado;

        do
        {
            Console.Write("Digite o ID do gênero de filme que deseja editar: ");
            numeroRegistro = Convert.ToInt32(Console.ReadLine());

            numeroRegistroEncontrado = _repositorioSessao.ExisteRegistro(numeroRegistro);

            if (numeroRegistroEncontrado == false)
                _notificador.ApresentarMensagem("ID do gênero de filme não foi encontrado, digite novamente", TipoMensagem.Atencao);

        } while (numeroRegistroEncontrado == false);

        return numeroRegistro;
    }
    
    }
}
