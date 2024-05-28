## Projeto de Gestão de Pedidos de Lanchonete - Tech Challenger FIAP

Este projeto é uma aplicação para gestão de pedidos de uma lanchonete utilizando .NET 8 e MongoDB. A aplicação está configurada para ser executada em contêineres Docker, facilitando a implantação e execução.

## Tecnologias utilizadas no Projeto

| Docker | Mongo DB   | .Net     |   
|--------|------------|----------|
| *      | *          | 8.0.x    |

## Membros do Projeto

| Nome                   | RM        |
|------------------------|-----------|
| Pedro Luis Batista     | RM354432  |

## Liks uteis para entendimento do projeto, Documentação e Diagramas
- [Notion - Anotações e entendimento do Projeto](https://www.notion.so/TECH-CHALLENGER-PEDRO-BATISTA-27057ee595924f90b3052224ab932058)
- [Miro - Diagrama completo contendo Brainstorming e Event Storming](https://miro.com/app/board/uXjVKMBlPDc=/)
- [GitHub - Projeto do Tech Challenger](https://github.com/PBatista/TechChallenge-Lanchonete/tree/master)

## Estrutura do Projeto detalhada

A estrutura do projeto é organizada em vários diretórios, cada um contendo uma parte da aplicação:

- `1 - Presentation`: Esta é a camada de apresentação, responsável pela interface do usuário (Frontend). 
	É a parte do projeto onde os usuários interagem diretamente, podendo ser uma aplicação web, mobile ou desktop. 	
	Aqui são recebidas as requisições que são encaminhadas para as camadas internas para processamento.

- `2 - Core`: A camada Core é o coração do sistema, contendo as regras de negócio e lógica da aplicação.
	- `Application`: Esta subcamada contém as regras de aplicação, ou seja, os casos de uso específicos da aplicação. Ela é responsável por orquestrar a execução das regras de negócio e é composta por:
		* `ApplicationDTO`: Objetos de Transferência de Dados usados para a comunicação entre diferentes partes da aplicação, facilitando a troca de informações de forma clara e coesa.
		* `IUseCase`: Interfaces que definem os contratos para os casos de uso, garantindo que a lógica de negócio seja implementada de maneira consistente.
		* `UseCase`: Implementações concretas dos casos de uso, onde reside a lógica específica de aplicação que interage com o domínio.
	- `Domain`: A subcamada de Domain contém a essência das regras de negócio. É independente de frameworks e tecnologias externas, focando puramente na lógica de negócio. Esta subcamada é composta por:
		* `Base`: Classes base e utilitárias usadas por outras partes do domínio.
		* `Entities`: Entidades do domínio que representam os objetos principais do negócio, com suas propriedades e comportamentos.
		* `Repositories`: Interfaces que definem os contratos para persistência das entidades, permitindo que a infraestrutura de dados seja trocada sem impactar a lógica de negócio.
		
- `3 - Adapter`: A camada de Adaptadores e Conexões é responsável por fazer a ponte entre o Core e as tecnologias externas.
	- `Driven`: Adaptadores Driven são aqueles que recebem chamadas do Core e interagem com sistemas externos.
		* `Infra`: Contém a infraestrutura necessária para o funcionamento do projeto, como bancos de dados.
			* `InfraMongoDB`: Implementações específicas para integração com o MongoDB.
		* `Pagamento`: Contém a infraestrutura necessária para o funcionamento do projeto, como bancos de dados.
			* `MercadoPagaIntegracao`: Implementação para integração com o serviço de pagamento MercadoPago.
	- `Driven`: Adaptadores Driver são aqueles que recebem entradas externas e encaminham para o Core.
		* `Api`: Contém os controladores e endpoints que expõem os serviços da aplicação, recebendo requisições externas e encaminhando para os casos de uso apropriados no Core.

## Estrutura do Projeto

O projeto segue uma arquitetura Hexagonal (Ports and Adapters) e adota conceitos de Domain-Driven Design (DDD). Abaixo, a estrutura detalhada do projeto:

	Solution - Lanchonete
	├── 1 - Presentation
	├── 2 - Core
	│   ├── Application
	│   │   ├── ApplicationDTO
	│   │   ├── IUseCase
	│   │   ├── UseCase
	│   ├── Domain	
	│   │   ├── Base
	│   │   ├── Entities
	│   │   ├── Repositories
	├── 3 - Adapter
	│   ├── Driven
	│   │   ├── Infra
	│   │       ├── InfraMongoDB
	│   │   ├── Pagamento
	│   │       ├── MercadoPagaIntegracao
	│   ├── Driver
	│   │   ├── Api		


## Pré-requisitos

Antes de rodar o projeto, certifique-se de que os seguintes pré-requisitos estão atendidos:

## Ferramentas Necessárias

1. **Docker**
   - Instale o Docker a partir do [site oficial](https://www.docker.com/get-started).
   - Verifique a instalação executando `docker --version` no terminal.

2. **Docker Compose**
   - Verifique a instalação executando `docker-compose --version` no terminal.
   - Se necessário, instale seguindo as instruções [aqui](https://docs.docker.com/compose/install/).

3. **.NET 8 SDK**
   - Baixe e instale o .NET 8 SDK a partir do [site oficial da Microsoft](https://dotnet.microsoft.com/download/dotnet/8.0).
   - Verifique a instalação executando `dotnet --version` no terminal.

## Executando o Projeto

1. **Clone o Repositório**
   ```bash
   git clone https://github.com/PBatista/TechChallenge-Lanchonete.git
   branch master   

2. **Acesse a pasta do projeto**:
   - cd Lanchonete

3. **Inicie os serviços usando o Docker Compose**:
   - docker-compose up

4. **Acesse a url do Swagger**:
   - URL: http://localhost:8080/swagger/index.html

## Parando o Projeto
Para parar o projeto e os serviços em execução, você pode pressionar Ctrl + C no terminal onde o docker-compose up está sendo executado. Em seguida, você pode limpar os contêineres com:

1. **Parar o projeto**
   - docker-compose down

## Exemplos de JSON para a API

1. **Cadastro de Categorias**
   - Endpoint: POST /api/v1/categorias
   - Exemplo de JSON:
	```
	{
 	   "nome": "Lanche"
	}
	```
	```
	{
	   "nome": "Acompanhamento"
	}
	```
 	```
	{
 	   "nome": "Bebida"
	}
	```
  	```
	{
 	   "nome": "Sobremesa"
	}
	```
   
2. **Cadastro de Clientes**
    - Endpoint: POST /api/v1/clientes
    - Exemplo de JSON:
    ```
	{
      "nome": "Pedro Batista",
      "cpf": "45012334503",
      "email": "teste@gmail.com"
    }
	```
3. **Cadastro de Produtos**
    - Endpoint: POST /api/v1/produtos
    - Exemplo de JSON:
    ```
	{
      "nome": "X Bacon",
      "categoria": "Lanche",
      "preco": 13.50,
      "descricao": "200g de hamburger, bacon, cebola, salada",
      "imagens": [
        "pasta/foto1.png",
        "pasta/foto2.png",
        "pasta/foto3.png"
      ]
    }
	```
    ```
	{
      "nome": "X Salada",
      "categoria": "Lanche",
      "preco": 11.50,
      "descricao": "200g de hamburger, salada, tomate, cebola",
      "imagens": [
        "pasta/foto1.png",
        "pasta/foto2.png",
        "pasta/foto3.png"
      ]
    }
	```
    ```
	{
      "nome": "Batata Frita",
      "categoria": "Acompanhamento",
      "preco": 10.00,
      "descricao": "400g de batata frita",
      "imagens": [
        "pasta/foto1.png",
        "pasta/foto2.png"
      ]
    }
	```
    ```
	{
      "nome": "Coca Cola",
      "categoria": "Bebida",
      "preco": 6.00,
      "descricao": "Refrigerante de cola",
      "imagens": [
        "pasta/foto1.png"
      ]
    }
	```
    ```
	{
      "nome": "Brownie,
      "categoria": "Sobremesa",
      "preco": 7.00,
      "descricao": "Brownie é uma sobremesa de chocolate típico da culinária dos Estados Unidos",
      "imagens": [
        "pasta/foto1.png"
      ]
    }
	```
4. **Realização de Pedidos**
    - Endpoint: POST /api/v1/pedidos
    - Exemplo de JSON     
   * Pedido Se Identificando:
    ```
	{
      "cpf": "45012334503",
      "produtos": [
        {
          "nome": "X Bacon",
          "quantidade": 1
        }
      ],
      "descricao": "Retirar a salada"
    }
    ```
    * Pedido não se identificando:
    ```
	{
      "cpf": "",
      "produtos": [
        {
          "nome": "X Salada",
          "quantidade": 3
        }
      ],
      "descricao": ""
    }
    ```
4. **Realizar checkout**
   - Endpoint: POST /api/v1/checkouts
   - Exemplo de JSON
    ```
	{
  	"numPedido": "string"
	}
    ```	

