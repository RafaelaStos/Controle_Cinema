using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloGenero;
using System;



namespace ControleCinema.ConsoleApp.ModuloFilme
{
    public class Filme : EntidadeBase
    {
        private readonly string _titulo;
        private readonly int _duracao;
        public Genero genero;

        public string Titulo { get => _titulo; }
        public int Duracao { get => _duracao; }

        public Filme(string titulo, int duracao, Genero generoSelecionado)
        {
            _titulo = titulo;
            _duracao = duracao;

            genero = generoSelecionado;

        }
        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Titulo: " + Titulo + Environment.NewLine +
                "Duração: " + Duracao + Environment.NewLine +
                "Descrição do genero: " + genero.Descricao + Environment.NewLine;
        }
    }
}
