using ControleCinema.ConsoleApp.Compartilhado;
using System;

namespace ControleCinema.ConsoleApp.ModuloSala
{
    public class Sala : EntidadeBase
    {

        private readonly int _capacidade;
        private readonly string _nome;

        public string Nome { get => _nome; }
        public int Capacidade { get => _capacidade; }

        public Sala(int capacidade, string nome)
        {
            _capacidade = capacidade;
            _nome = nome;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Nome: " + _nome + Environment.NewLine +
                "Capacidade: " + _capacidade + Environment.NewLine;
        }
    }
}
