﻿using Domain.Base;

namespace Domain.Entities
{
    public class Produto : IAggregateRoot
    {
        public string Nome { get; private set; }
        public string Categoria { get; private set; }
        public double Preco { get; private set; } 
        public string Descricao { get; private set; }
        public List<string> Imagens { get; private set; }
        

        public Produto(string nome, string categoria, double preco, string descricao, List<string> imagens) 
        { 
            Nome = nome;
            Categoria = categoria.ToUpper();
            Preco = preco;
            Descricao = descricao;
            Imagens = imagens;
            ValidateEntity();
        }        

        public void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Nome, "O nome não pode estar vazio");
            AssertionConcern.AssertArgumentNotEmpty(Categoria, "A categoria não pode estar vazia");
            AssertionConcern.AssertArgumentNotNull(Preco, "O Preço não pode estar vazio");
            AssertionConcern.AssertArgumentNotEmpty(Descricao, "A descrição não pode estar vazia");
            AssertionConcern.AssertArgumentNotNull(Imagens, "As imagens não pode estar vazia");
        }

    }
}
