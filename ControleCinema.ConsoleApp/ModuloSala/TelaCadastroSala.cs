using ControleCinema.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;


namespace ControleCinema.ConsoleApp.ModuloSala
{
    public class TelaCadastroSala : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Sala> _repositorioSala;
        private readonly Notificador _notificador;

        public TelaCadastroSala(IRepositorio<Sala> repositorioSala, Notificador notificador)
            : base("Cadastro de Sala")
        {
            _repositorioSala = repositorioSala;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Sala");

            Sala novaSala = ObterSala();

            _repositorioSala.Inserir(novaSala);

            _notificador.ApresentarMensagem("Sala cadastrada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Sala");

            bool temSalaCadastrada = VisualizarRegistros("Pesquisando");

            if (temSalaCadastrada == false)
            {
                _notificador.ApresentarMensagem("Nenhum Sala cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroSala = ObterNumeroRegistro();

            Sala salaAtualizado = ObterSala();

            bool conseguiuEditar = _repositorioSala.Editar(numeroSala, salaAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Funcionário editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Sala");

            bool temFuncionariosRegistrados = VisualizarRegistros("Pesquisando");

            if (temFuncionariosRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum sala cadastrada para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroSala = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioSala.Excluir(numeroSala);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Sala excluído com sucesso1", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Salas");

            List<Sala> salas = _repositorioSala.SelecionarTodos();

            if (salas.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum sala disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Sala sala in salas)
                Console.WriteLine(sala.ToString());

            Console.ReadLine();

            return true;
        }

        private Sala ObterSala()
        {
            Console.Write("Digite o nome da sala: ");
            string nome = Console.ReadLine();

            Console.Write("Digite a capacidade: ");
            int capacidade = int.Parse(Console.ReadLine());

            return new Sala(capacidade, nome);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do sala que deseja editar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioSala.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID da Sala não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }

    
    }
}
