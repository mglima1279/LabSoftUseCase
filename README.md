# LabUseCase01 - Desenvolvimento ASP.NET Core .NET 8 (Database First)

Seja bem-vindo ao **LabUseCase01**! Neste laboratório prático, você trabalhará com um projeto ASP.NET Core .NET 8 MVC pré-configurado utilizando a abordagem **Database First** do Entity Framework Core.

O projeto já possui a estrutura base pronta, a conexão com o SQL Server configurada no `appsettings.json` e o CRUD de Funcionários 100% funcional. Sua missão será testar o sistema atual, criar o CRUD de Departamentos e implementar o módulo de Projetos.

---

## 📋 Modelo de Dados Inicial

O banco de dados do sistema é o `dbEmpresa`. Ele possui duas tabelas relacionadas (**1 Departamento : N Funcionários**):

### Tabela `Departamento`
* `Codigo`: `INT` (Primary Key, Identity)
* `Nome`: `VARCHAR(100)` (Not Null)
* `Sigla`: `VARCHAR(10)` (Not Null)

### Tabela `Funcionario`
* `Codigo`: `INT` (Primary Key, Identity)
* `Nome`: `VARCHAR(100)` (Not Null)
* `Cargo`: `VARCHAR(50)` (Not Null)
* `DepartamentoId`: `INT` (Foreign Key -> `Departamento.Codigo`)

---

## 🚀 Passo 1: Preparação do Banco de Dados Inicial

1. Abra o **SQL Server Management Studio (SSMS)** ou o **Azure Data Studio**.
2. Conecte-se à sua instância local do SQL Server.
3. Execute o script SQL abaixo para criar o banco de dados `dbEmpresa` e as tabelas iniciais:

```sql
CREATE DATABASE dbEmpresa;
GO

USE dbEmpresa;
GO

-- Tabela Departamento
CREATE TABLE Departamento (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Sigla VARCHAR(10) NOT NULL
);
GO

-- Tabela Funcionario
CREATE TABLE Funcionario (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Cargo VARCHAR(50) NOT NULL,
    DepartamentoId INT NOT NULL,
    CONSTRAINT FK_Funcionario_Departamento FOREIGN KEY (DepartamentoId) 
        REFERENCES Departamento(Codigo)
);
GO

-- Inserindo Dados Iniciais para Teste
INSERT INTO Departamento (Nome, Sigla) VALUES 
('Tecnologia da Informacao', 'TI'),
('Recursos Humanos', 'RH'),
('Financeiro', 'FIN');

INSERT INTO Funcionario (Nome, Cargo, DepartamentoId) VALUES 
('Carlos Silva', 'Desenvolvedor Senior', 1),
('Ana Oliveira', 'Analista de QA', 1),
('Roberto Santos', 'Gerente de RH', 2);
GO
```

---

## 🛠️ Passo 2: Verificação da Conexão e Teste do CRUD de Funcionário

Abra o arquivo `appsettings.json` no projeto e ajuste a `ConnectionString` conforme as credenciais do seu banco SQL Server:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "ConexaoSqlServer": "Server=LOCALHOST;Database=dbEmpresa;User Id=sa;Password=SUA_SENHA_AQUI;TrustServerCertificate=True;"
  }
}
```

1. Execute o projeto pressionando **F5** ou clicando em **Run/Start** no Visual Studio.
2. Acesse a rota `/Funcionario` e teste as operações CRUD completas (**Criar, Listar, Editar, Detalhar e Excluir**) que já vêm nativas no projeto.

---

## 🎯 Passo 3: Criando o CRUD de Departamento via Scaffold

Sua primeira missão prática será gerar o CRUD para a entidade `Departamento` utilizando as ferramentas automáticas do Visual Studio (Scaffold):

1. Na janela do **Solution Explorer**, clique com o botão direito sobre a pasta **Controllers**.
2. Selecione **Adicionar > Novo Item Scaffolded...** (ou *New Scaffolded Item...*).
3. Escolha a opção **Controlador MVC com exibições, usando o Entity Framework** (*MVC Controller with views, using Entity Framework*).
4. Preencha a caixa de diálogo com as seguintes opções:
   * **Classe de Modelo (Model class):** `Departamento (AppEmpresa.Models)`
   * **Classe do contexto de dados (Data context class):** `DbEmpresaContext (AppEmpresa.Models)`
   * **Nome do Controlador:** `DepartamentoController`
5. Clique em **Adicionar**.
6. Abra o arquivo `Views/Shared/_Layout.cshtml` e adicione um link para acessar a Controller no menu de navegação:

```html
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Departamento" asp-action="Index">Departamentos</a>
</li>
```

---

## ⚡ Passo 4: Adicionando a Tabela e Módulo de Projetos

Agora você irá expandir o banco de dados e atualizar o mapeamento no projeto.

### 4.1. Executar o Script SQL no Banco

Execute o script abaixo no SQL Server para criar a nova tabela `Projeto`:

```sql
USE dbEmpresa;
GO

CREATE TABLE Projeto (
    Codigo INT IDENTITY(1,1) PRIMARY KEY,
    NomeProjeto VARCHAR(200) NOT NULL,
    Orcamento DECIMAL(12,2) NOT NULL,
    Status VARCHAR(30) NOT NULL -- 'Em Planejamento', 'Em Andamento', 'Concluido'
);
GO
```

### 4.2. Atualizar o Mapeamento via Scaffold (CLI)

Para atualizar o seu `DbEmpresaContext` e gerar a Model `Projeto` automaticamente sem perder suas configurações, abra o **Package Manager Console** (no Visual Studio em *Ferramentas > Gerenciador de Pacotes NuGet > Console do Gerenciador de Pacotes*) ou o Terminal na raiz do projeto e execute:

**Pelo Package Manager Console:**

*Use esse comando para adicionar novas tabelas, se seu projeto já contém a classe context configurada:*
```powershell
Scaffold-DbContext "Name=ConexaoSqlServer" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
```

*Use somente esse se deseja recriar toda estrutura ou quando um projeto novo:*
```powershell
Scaffold-DbContext "Server=LOCALHOST;Database=dbEmpresa;User Id=sa;Password=SUA_SENHA_AQUI;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables Projeto -Force
```

> **Nota:** Certifique-se de substituir `SUA_SENHA_AQUI` pela senha configurada no seu SQL Server.

### 4.3. Criar o CRUD de Projeto via Scaffold

Com a nova Model `Projeto.cs` criada na pasta `Models`:
1. Clique com o botão direito na pasta **Controllers > Adicionar > Novo Item Scaffolded...**
2. Selecione **Controlador MVC com exibições, usando o Entity Framework**.
3. Defina as opções:
   * **Classe de Modelo:** `Projeto (AppEmpresa.Models)`
   * **Classe do contexto de dados:** `DbEmpresaContext (AppEmpresa.Models)`
   * **Nome do Controlador:** `ProjetoController`
4. Clique em **Adicionar**.

🎉 Teste o funcionamento completo do novo CRUD executando o projeto e navegando até `/Projeto`!

---

## 🛠️ Revisão Dev A: Banco de Dados & SQL

Para trabalhar com a abordagem Database First, é essencial dominar os comandos de manipulação e as estruturas de dados no SQL Server. Abaixo estão os conceitos e comandos fundamentais utilizados neste laboratório:

| Conceito / Sintaxe | Tipo | O que faz / Explicação | Exemplo de Uso |
| :--- | :--- | :--- | :--- |
| **INT** | Tipo de Dado | Armazena números inteiros (positivos ou negativos) sem casas decimais. | `Codigo INT` |
| **VARCHAR(N)** | Tipo de Dado | Armazena texto/caracteres de tamanho variável até o limite N especificado. | `Nome VARCHAR(100)` |
| **PRIMARY KEY** | Restrição | Identificador único da tabela. Garante que não existam registros duplicados e que o valor nunca seja nulo. | `Codigo INT PRIMARY KEY` |
| **FOREIGN KEY** | Restrição | Cria um relacionamento entre duas tabelas, garantindo a integridade referencial com a chave primária de outra tabela. | `FOREIGN KEY (DepartamentoId) REFERENCES Departamento(Codigo)` |
| **SELECT** | Comando DML | Consulta e recupera dados de uma ou mais tabelas do banco de dados. | `SELECT * FROM Departamento;` |
| **INSERT** | Comando DML | Insere novos registros/linhas em uma tabela. | `INSERT INTO Departamento (Nome, Sigla) VALUES ('Vendas', 'VEN');` |

---

## 🏗️ Revisão Dev B: Arquitetura ASP.NET Core & Entity Framework

Para que a conexão e o gerenciamento de dados funcionem corretamente em uma aplicação ASP.NET Core MVC com Entity Framework Core, é necessário compreender os seguintes componentes e conceitos centrais:

| Componente / Conceito | Descrição e Papel na Aplicação |
| :--- | :--- |
| **appsettings.json** | Arquivo de configuração global da aplicação. É nele que armazenamos a `ConnectionString` (string de conexão), contendo o servidor, nome do banco, usuário e senha para conectar ao SQL Server. |
| **DbContext** | Classe do Entity Framework que representa uma sessão com o banco de dados. Ela mapeia as tabelas em coleções `DbSet<T>` e traduz operações C# em comandos SQL equivalentes no banco. |
| **CRUD** | Acrônimo para as 4 operações básicas de armazenamento persistente:<br>• **Create** (Criar/Inserir - HTTP POST)<br>• **Read** (Ler/Consultar - HTTP GET)<br>• **Update** (Atualizar/Editar - HTTP POST/PUT)<br>• **Delete** (Deletar/Excluir - HTTP POST/DELETE) |

---

## 💻 Revisão Dev C: Conceitos de Orientação a Objetos (C#)

As classes geradas pelo Entity Framework baseiam-se nos pilares da Orientação a Objetos. Veja como esses conceitos se aplicam ao código C# do nosso projeto:

| Conceito | Explicação | Exemplo do Código (Departamento.cs / DbContext) |
| :--- | :--- | :--- |
| **Classe** | Estrutura/molde que define as características e comportamentos de um objeto no sistema. | `public class Departamento { ... }` |
| **Atributos (Propriedades)** | Características ou dados que a classe armazena (no EF, representam as colunas da tabela). | `public string Sigla { get; set; }` |
| **Método** | Bloco de código dentro de uma classe que executa uma ação ou comportamento específico. | `public async Task<IActionResult> Index() { ... }` |
| **Parâmetros** | Valores/informações de entrada passados para um método executar sua lógica. | `public IActionResult Details(int? id)` (onde `id` é o parâmetro) |
| **Return** | Instrução que encerra a execução de um método e devolve um resultado para quem o chamou. | `return View(departamento);` |

---

## 💼 Você na Entrevista de Emprego

Testes práticos e simulações de entrevistas técnicas frequentemente abordam a integração entre SQL, C# e ASP.NET Core. Responda às 10 questões abaixo para avaliar o seu domínio sobre o conteúdo:

### 🗄️ Questões de Banco de Dados & SQL

#### ❓ Questão 1: Tipos de Dados e Restrições
*Cenário:* Durante a modelagem do banco de dados `dbEmpresa`, você precisa definir uma coluna para armazenar o código identificador principal da tabela `Departamento`. Esse código deve ser gerado automaticamente pelo SQL Server a cada novo registro e não pode se repetir.  
Qual combinação de tipos e restrições SQL deve ser utilizada?  
* A) `Codigo VARCHAR(50) NOT NULL`  
* B) `Codigo INT IDENTITY(1,1) PRIMARY KEY`  
* C) `Codigo INT FOREIGN KEY`  
* D) `Codigo TEXT UNIQUE`  
* E) `Codigo INT NULL`  

#### ❓ Questão 2: Relacionamento entre Tabelas
*Cenário:* A tabela `Funcionario` possui a coluna `DepartamentoId`, que faz referência à coluna `Codigo` da tabela `Departamento`. Essa configuração garante que um funcionário não seja vinculado a um departamento inexistente.  
Como chamamos essa regra de integridade no banco de dados relacional?  
* A) Primary Key (Chave Primária)  
* B) Identity Constraint  
* C) Foreign Key (Chave Estrangeira)  
* D) Index Clustered  
* E) Database First Constraint  

#### ❓ Questão 3: Manipulação de Dados (DML)
*Cenário:* O sistema precisa registrar um novo projeto no banco de dados via script manual. A tabela `Projeto` possui as colunas `NomeProjeto`, `Orcamento` e `Status`.  
Qual instrução SQL realiza a inserção desse novo registro corretamente?  
* A) `SELECT INTO Projeto VALUES ('Sistema Web', 50000.00, 'Em Planejamento');`  
* B) `UPDATE Projeto SET NomeProjeto = 'Sistema Web';`  
* C) `CREATE TABLE Projeto ('Sistema Web', 50000.00, 'Em Planejamento');`  
* D) `INSERT INTO Projeto (NomeProjeto, Orcamento, Status) VALUES ('Sistema Web', 50000.00, 'Em Planejamento');`  
* E) `ADD REGISTRO TO Projeto VALUES ('Sistema Web', 50000.00, 'Em Planejamento');`  

---

### ⚙️ Questões de ASP.NET Core, EF Core & Orientação a Objetos

#### ❓ Questão 4: String de Conexão
*Cenário:* Ao publicar a aplicação ASP.NET Core em um novo ambiente de homologação, o sistema apresentou erro informando que não conseguiu conectar ao SQL Server. O desenvolvedor precisa ajustar a URL do servidor e as credenciais de acesso.  
Em qual arquivo padrão do projeto ASP.NET Core a ConnectionString fica armazenada?  
* A) `DbContext.cs`  
* B) `Program.cs`  
* C) `appsettings.json`  
* D) `_Layout.cshtml`  
* E) `DepartamentoController.cs`  

#### ❓ Questão 5: O papel do DbContext
*Cenário:* Em uma entrevista de emprego, o entrevistador pergunta: "Qual é a principal função da classe `DbEmpresaContext` que herdou de `DbContext` no nosso projeto?"  
Qual das respostas abaixo descreve corretamente o papel dessa classe?  
* A) Renderizar as páginas HTML e gerenciar o CSS da aplicação.  
* B) Atuar como a ponte entre o código C# e o banco de dados SQL Server, gerenciando a conexão e os conjuntos de dados (`DbSet`).  
* C) Executar scripts de criação de tabelas automaticamente toda vez que a aplicação é iniciada.  
* D) Armazenar as credenciais de login dos usuários de forma criptografada.  
* E) Criar as rotas de navegação no menu principal da aplicação.  

#### ❓ Questão 6: Conceito de CRUD
*Cenário:* Um analista de sistemas pediu para você criar o "CRUD de Departamentos".  
O que a sigla CRUD representa no ciclo de desenvolvimento de software?  
* A) Class, Resource, User, Data  
* B) Connect, Run, Undo, Disconnect  
* C) Compile, Read, Update, Deploy  
* D) Create, Read, Update, Delete  
* E) Code, Refactor, Use, Debug  

---

### 💻 Questões de Orientação a Objetos & Scaffold (C# e EF Core)

#### ❓ Questão 7: Abordagem Database First
*Cenário:* Durante a execução do laboratório, a tabela `Projeto` foi criada diretamente no SQL Server via script SQL. Em seguida, foi executado o comando de scaffold no terminal para atualizar o projeto em C#.  
O que a abordagem Database First faz nesse processo?  
* A) Apaga o banco de dados SQL Server e o recria a partir das classes C#.  
* B) Lê a estrutura existente do banco de dados e gera automaticamente as classes de modelo (Models) e o DbContext no projeto.  
* C) Converte o projeto ASP.NET Core em uma aplicação desktop executável.  
* D) Impede qualquer alteração futura nas tabelas do banco de dados.  
* E) Gera relatórios em PDF com base nos dados armazenados nas tabelas.  

#### ❓ Questão 8: Propriedades e Atributos de Classe
*Cenário:* Na classe de modelo `Departamento`, temos a declaração da propriedade `Sigla` com os acessores `get` e `set`.  
Dentro dos conceitos da Programação Orientada a Objetos (POO) aplicados ao Entity Framework Core, o que essa estrutura representa?  
* A) Um método responsável por salvar a sigla do departamento diretamente no banco de dados.  
* B) Uma propriedade (atributo) da classe `Departamento` com métodos `get` e `set` acessores, que mapeia uma coluna de texto da tabela.  
* C) Uma variável local que só existe enquanto o formulário HTML estiver aberto.  
* D) Um parâmetro obrigatoriamente passado para o construtor da classe `DbEmpresaContext`.  
* E) Um comando SQL executado para alterar o tipo de dado da coluna.  

#### ❓ Questão 9: Assinatura e Parâmetros de Métodos na Controller
*Cenário:* Ao analisar o código gerado pelo Scaffold na `DepartamentoController`, você encontra o método `Details` que recebe como entrada um parâmetro de ID inteiro opcional e retorna a View com os dados do departamento.  
Sobre os conceitos de Métodos, Parâmetros e Return, qual afirmação está correta?  
* A) `Details` é a classe, o `ID` é o retorno do método e a `View` é o parâmetro de entrada.  
* B) `Details` é o método, o `ID` é um parâmetro opcional (que aceita nulo) e a instrução `return View` devolve o resultado para ser renderizado na tela.  
* C) A instrução `return View` apaga a instância do departamento da memória do servidor.  
* D) O tipo de retorno `Task` indica que o método não pode receber nenhum parâmetro de entrada.  
* E) O parâmetro do ID indica que o método só aceita números inteiros negativos.  

#### ❓ Questão 10: Roteamento e Ações do Controlador MVC
*Cenário:* Um usuário acessa o navegador e clica no link de navegação que aponta para a rota `/Departamento/Index`.  
O que acontece na arquitetura ASP.NET Core MVC para que a lista de departamentos apareça na tela?  
* A) O navegador executa uma instrução SQL diretamente no banco de dados sem passar pelo servidor C#.  
* B) O arquivo `appsettings.json` intercepta a requisição e renderiza o HTML diretamente para o usuário.  
* C) A requisição atinge a `DepartamentoController`, que executa o método de ação `Index`, consulta o `DbContext` e passa os dados para a exibição (`View`).  
* D) O Entity Framework ignora a requisição pois a ação `Index` é reservada apenas para o administrador do sistema.  
* E) O arquivo `_Layout.cshtml` compila o código C# e gera um novo banco de dados temporário.  

---

### 🔑 Gabarito Completo das 10 Questões de Entrevista

<details>
<summary>Clique para expandir o Gabarito e Explicações</summary>

* **Questão 1: B) Codigo INT IDENTITY(1,1) PRIMARY KEY**
  * *Explicação:* `INT` armazena números inteiros, `IDENTITY(1,1)` gera o auto-incremento sequencial a cada registro inserido e `PRIMARY KEY` garante a unicidade e obrigatoriedade da chave primária.
* **Questão 2: C) Foreign Key (Chave Estrangeira)**
  * *Explicação:* A Foreign Key garante a integridade referencial ao vincular o campo `DepartamentoId` da tabela de funcionários à chave primária `Codigo` da tabela de departamentos.
* **Questão 3: D) INSERT INTO Projeto (NomeProjeto, Orcamento, Status) VALUES ('Sistema Web', 50000.00, 'Em Planejamento');**
  * *Explicação:* O comando DML `INSERT INTO` especifica a tabela, a lista de colunas receptoras e os valores no parâmetro `VALUES`.
* **Questão 4: C) appsettings.json**
  * *Explicação:* O arquivo `appsettings.json` armazena as configurações globais de ambiente da aplicação ASP.NET Core, incluindo a seção de `ConnectionStrings`.
* **Questão 5: B) Atuar como a ponte entre o código C# e o banco de dados SQL Server, gerenciando a conexão e os conjuntos de dados (DbSet).**
  * *Explicação:* O `DbContext` traduz as chamadas LINQ/C# em consultas SQL e gerencia o ciclo de vida das conexões e conjuntos de dados (`DbSet<T>`).
* **Questão 6: D) Create, Read, Update, Delete**
  * *Explicação:* CRUD representa o ciclo das quatro operações básicas de persistência de dados no desenvolvimento de software.
* **Questão 7: B) Lê a estrutura existente do banco de dados e gera automaticamente as classes de modelo (Models) e o DbContext no projeto.**
  * *Explicação:* Na abordagem Database First, o comando de Scaffold faz a engenharia reversa do banco de dados para a aplicação em C#.
* **Questão 8: B) Uma propriedade (atributo) da classe Departamento com métodos get e set acessores, que mapeia uma coluna de texto da tabela.**
  * *Explicação:* Propriedades públicas C# com `get` e `set` atuam como atributos que o EF Core mapeia diretamente para as colunas das tabelas.
* **Questão 9: B) Details é o método, o ID é um parâmetro opcional (que aceita nulo) e a instrução return View devolve o resultado para ser renderizado na tela.**
  * *Explicação:* `Details` é a Action (método), `int? id` permite que a entrada seja nula ou inteira, e `return View` devolve o resultado para renderização Razor.
* **Questão 10: C) A requisição atinge a DepartamentoController, que executa o método de ação Index, consulta o DbContext e passa os dados para a exibição (View).**
  * *Explicação:* No padrão MVC, o Controller trata a requisição da rota, consulta o banco via `DbContext` e injeta a lista de dados na `View` de resposta.

</details>
