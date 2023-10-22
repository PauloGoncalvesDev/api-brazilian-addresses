# Brazilian Addresses API

A **Brazilian Addresses API** é uma Web API construída em C# utilizando o framework .NET 7, focada na gestão de endereços brasileiros. Ela permite o armazenamento de informações essenciais sobre cidades, incluindo nome, código IBGE e estado. Além disso, a API oferece funcionalidades como autenticação, criação de usuários e endpoints para manipulação completa de endereços.

- Link para API: https://brazillianaddressapi.azurewebsites.net/
- Link para documentação:

## Índice

- [Visão Geral](#visão-geral)
- [Motivação](#motivação)
- [Built With](#built-with)  
- [Recursos](#recursos)
- [Como Usar](#como-usar)
- [Exemplos](#exemplos)
- [Desenvolvedores](#desenvolvedores)

## Visão Geral

A API permite o gerenciamento eficiente de informações sobre cidades brasileiras, armazenando dados essenciais como nome, código IBGE e estado. Além disso, ela oferece funcionalidades de autenticação e gestão de usuários, tornando-a uma solução completa para a gestão de endereços.

## Motivação

Este projeto foi gerado como parte de um desafio proposto pelo canal do [Balta.io](https://www.youtube.com/c/baltaio). O desafio foi uma oportunidade de aprimorar habilidades práticas em desenvolvimento de APIs utilizando C# e .NET 7, ao mesmo tempo que proporcionou uma experiência prática na resolução de problemas relacionados à gestão de endereços brasileiros.

Ao participar desse desafio, buscamos não apenas consolidar conhecimentos técnicos, mas também aplicar boas práticas de desenvolvimento de software, garantindo um código limpo, eficiente e facilmente mantido. O aprendizado adquirido ao enfrentar desafios específicos deste projeto contribui significativamente para o nosso crescimento como desenvolvedores.

Agradecemos ao [Balta.io](https://www.youtube.com/c/baltaio) pela oportunidade de participar deste desafio, que não apenas ampliou nosso conhecimento, mas também fortaleceu nossa paixão pelo desenvolvimento de software e a comunidade de tecnologia como um todo.


## Built With
- .NET 7
- SQL Server
- Fluent Validator
- Swagger
- Dapper
- Entity Framework
- AutoMapper
- AspNetCore
- Dependency Injection
- Identity Model
- Fluent Migrator

## Recursos

- **Autenticação:** Endpoint para autenticação de usuários.

- **Criação de Usuário:** Endpoint para registro de novos usuários.

- **Consulta de Endereços:** Recuperação de informações sobre endereços armazenados.

- **Criação de Endereço:** Adição de novos endereços ao banco de dados.

- **Edição de Endereço:** Atualização de informações de endereços existentes.

- **Remoção de Endereço:** Exclusão de endereços do banco de dados.

## Como Usar
Realize o download do arquivo Api-Brazilian-Addressespostman_collection.json e abra-o utilizando Postman. Após isso selecione a chave apontada para produção e realize as requisições.

- Primeiro crie um usuário com o EndPoint **User**, o acesso a criação, edição e atualização de endereços é permitido somente para usuários com Role 0 (Admin).
- Após criar o usuário realize o login com o EndPoint **Login**, a response gerada terá um token de autenticação. Insira este token na Authorization do Postman como **Bearer Token**. Com isso você terá permissão para execução dos outros EndPoints.

## Exemplos



## Desenvolvedores
- Paulo Guilherme: https://github.com/PauloGoncalvesDev
- Eliaquim Jorras: https://github.com/EliaquimJorras
- Igor Cavalcante: https://github.com/igorcs08
