﻿using Domain.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraMongoDb.DTO
{
    public class PedidoDTO : BaseDTO
    {        
        public string NumPedido { get; set; }
        public Cliente Cliente { get; private set; }
        public List<Produto> Produtos { get; private set; }
        public double ValorTotal { get; private set; }        
        public string Status { get; private set; }

        public DateTime DataHora { get; private set; }

        public PedidoDTO(ObjectId _id, string numPedido, Cliente cliente, List<Produto> produtos, double valorTotal, string status, DateTime dataHora)
        {
            Id = _id;
            NumPedido = numPedido;
            Cliente = cliente;
            Produtos = produtos;
            ValorTotal = valorTotal;
            Status = status;
            DataHora = dataHora;
        }        
    }
}