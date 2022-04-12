using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloFilme;
using ControleCinema.ConsoleApp.ModuloSala;
using System;


namespace ControleCinema.ConsoleApp.ModuloSessao
{
    public class Sessao : EntidadeBase
    {
        public Filme filme;
        public Sala sala;
        private readonly string _hora;

        public string Hora { get => _hora; }
        public Sessao(Filme filmeSelecionado, Sala salaSelecionada, string hora)
        {
            _hora = hora;
            filme = filmeSelecionado;
            sala = salaSelecionada;
        }
        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Filme: " + filme.Titulo + Environment.NewLine +
                "Sala: " + sala.Nome + Environment.NewLine +
                "Horario: " + Hora + Environment.NewLine;
        }
    }
}
