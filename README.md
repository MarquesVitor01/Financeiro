# Projeto Financeiro – Backend

## 1. Visão Geral

O **Projeto Financeiro** é uma aplicação backend construída em **ASP.NET Core 7**, que gerencia categorias, transações e relatórios financeiros.  
A API é **RESTful**, permitindo operações de **CRUD** (Criar, Ler, Atualizar, Excluir) e consultas detalhadas. O sistema utiliza **DTOs** para validação e transferência de dados entre a camada de serviço e os controllers.

---

## 2. Estrutura do Projeto

financeiro/
│
├─ Controllers/
│ ├─ CategoriaController.cs # Endpoints CRUD para categorias
│ ├─ TransacaoController.cs # Endpoints CRUD para transações
│ └─ RelatorioController.cs # Endpoints de relatórios financeiros
│
├─ Data/ # Contexto do Entity Framework e configurações do DB
│
├─ Dto/ # Data Transfer Objects
│ ├─ Categoria/
│ │ ├─ CategoriaCriacaoDto.cs
│ │ └─ CategoriaEdicaoDto.cs
│ ├─ Transacao/
│ │ ├─ TransacaoCriacaoDto.cs
│ │ └─ TransacaoEdicaoDto.cs
│ └─ Relatorio/
│ └─ RelatorioDto.cs
│
├─ Migrations/ # Migrations do Entity Framework
│
├─ Models/
│ ├─ CategoriaModel.cs
│ ├─ TransacaoModel.cs
│ └─ ResponseModel.cs # Modelo de resposta padrão da API
│
├─ Services/ # Implementações de serviços (negócio)
│
├─ appsettings.json # Configurações da aplicação (ex.: string de conexão)
├─ Program.cs # Arquivo principal para inicialização da API
└─ financeiro.http # Arquivo com exemplos de requisições HTTP


---

## 3. Tecnologias Utilizadas

- **ASP.NET Core 7**
- **Entity Framework Core (EF Core)**
- **SQL Server**
- **DTOs** para validação de entrada e saída
- **API RESTful**

---

## 4. Endpoints Principais

### 4.1 CategoriaController

| Método | Endpoint | Descrição |
|--------|---------|-----------|
| GET    | `/api/Categoria/ListarCategorias` | Lista todas as categorias |
| GET    | `/api/Categoria/BuscarCategoriaPorId/{id}` | Retorna uma categoria pelo ID |
| GET    | `/api/Categoria/BuscarCategoriaPorIdTransacao/{idTransacao}` | Retorna a categoria associada a uma transação |
| POST   | `/api/Categoria/AdicionarCategoria` | Adiciona uma nova categoria |
| PUT    | `/api/Categoria/EditarCategoria` | Edita uma categoria existente |
| DELETE | `/api/Categoria/ExcluirCategoria/{id}` | Exclui uma categoria |

### 4.2 TransacaoController

| Método | Endpoint | Descrição |
|--------|---------|-----------|
| GET    | `/api/Transacao/ListarTransacoes` | Lista todas as transações |
| GET    | `/api/Transacao/BuscarTransacaoPorId/{id}` | Retorna uma transação pelo ID |
| GET    | `/api/Transacao/BuscarTransacaoPorIdCategoria/{idCategoria}` | Retorna todas as transações de uma categoria |
| POST   | `/api/Transacao/AdicionarTransacao` | Adiciona uma nova transação |
| PUT    | `/api/Transacao/EditarTransacao` | Edita uma transação existente |
| DELETE | `/api/Transacao/ExcluirTransacao/{id}` | Exclui uma transação |

### 4.3 RelatorioController

| Método | Endpoint | Descrição |
|--------|---------|-----------|
| GET | `/api/Relatorio/resumo?dataInicio=&dataFim=` | Retorna resumo financeiro (saldo, receitas, despesas) |
| GET | `/api/Relatorio/por-categoria?dataInicio=&dataFim=` | Retorna totais de transações agrupadas por categoria |

---

## 5. DTOs Principais

- **CategoriaCriacaoDto**: `{ Nome, Tipo, Ativo }`
- **CategoriaEdicaoDto**: `{ Nome, Tipo, Ativo }`
- **TransacaoCriacaoDto**: `{ Descricao, Valor, CategoriaId, Observacoes, DataCriacao }`
- **TransacaoEdicaoDto**: `{ Id, Descricao, Valor, CategoriaId, Observacoes, DataCriacao }`
- **ResumoRelatorioDto**: `{ SaldoTotal, TotalReceitas, TotalDespesas }`
- **RelatorioCategoriaDto**: `{ Categoria, Total }`

---

## 6. Banco de Dados

**Tabelas principais**: `Categoria`, `Transacao`

Exemplo de inserção de categoria:

```sql
INSERT INTO Categoria (Nome, Tipo, Ativo)
VALUES ('Alimentação', 'Despesa', 1);
Exemplo de inserção de transação:
INSERT INTO Transacao (Descricao, Valor, CategoriaId, Observacoes, DataCriacao)
VALUES ('Supermercado', 250.00, 1, 'Compra semanal', GETDATE());

Execução

Configurar o SQL Server e atualizar a string de conexão em appsettings.json.
```
Restaurar pacotes:
dotnet restore
Rodar a aplicação:
dotnet run
https://localhost:5001/api/Categoria/ListarCategorias


## 8. Observações

Todos os endpoints seguem o padrão RESTful.

Recomenda-se utilizar Postman ou outro cliente HTTP para testes.

DTOs garantem a validação de dados antes de persistir no banco.

A aplicação já está pronta para extensão de relatórios e integrações futuras.


Projeto Financeiro – Frontend
1. Visão Geral

O frontend do Projeto Financeiro é desenvolvido em Angular, utilizando TypeScript, CSS e componentes standalone modernos.
Ele fornece uma interface completa para gerenciar transações e categorias, com funcionalidades como:

Dashboard com resumo financeiro

Lista de transações com filtros e paginação

Formulário para adicionar e editar transações

Gestão básica de categorias

O frontend consome a API backend para persistência e leitura de dados.

2. Estrutura do Projeto

financeiro-frontend/
│
├─ src/
│   ├─ app/
│   │   ├─ components/         # Componentes reutilizáveis
│   │   │   ├─ categorias/     # CRUD de categorias
│   │   │   ├─ dashboard/      # Cards e gráfico do resumo financeiro
│   │   │   └─ formulario/     # Formulário de transação (adicionar/editar)
│   │   │
│   │   ├─ models/             # Interfaces e modelos de dados
│   │   │
│   │   ├─ pages/              # Páginas principais
│   │   │   ├─ cadastro/       # Tela de cadastro de transações
│   │   │   ├─ editar/         # Tela de edição de transações
│   │   │   ├─ detalhes/       # Tela de detalhes de transação
│   │   │   └─ home/           # Tela inicial com lista de transações
│   │   │
│   │   ├─ services/           # Serviços HTTP para consumir API
│   │   ├─ app.routes.ts       # Rotas da aplicação
│   │   ├─ app.config.ts       # Configurações gerais
│   │   └─ app.ts              # Bootstrapping
│   │
│   ├─ environments/           # Variáveis de ambiente
│   ├─ index.html
│   └─ styles.css
│
├─ package.json
├─ angular.json
└─ tsconfig.json

3. Componentes Principais
3.1 Home

Exibe a lista de transações com filtros por descrição, ID e período.

Possui paginação configurável.

Cards com total de transações, receitas e despesas.

Chama o serviço Transacoes para consumir endpoints do backend.

3.2 Cadastro / Editar

Reutilizam o componente Formulario para criar ou atualizar transações.

Capturam dados do formulário e enviam para o backend via serviço Transacoes.

Após operação, redirecionam o usuário para a tela inicial (Home).

3.3 Detalhes

Exibe informações completas de uma transação.

Utiliza o serviço Transacoes para buscar dados por ID.

Formatação de datas com DatePipe.

3.4 Categorias

Lista e gerencia categorias: criar, editar e deletar.

Suporte a paginação de categorias.

Sincroniza dados com o backend via serviço CategoriasService.

3.5 Dashboard

Exibe resumo financeiro:

Saldo total

Total de receitas

Total de despesas

Exibe gráfico de Pizza por categoria utilizando Chart.js.

Possui filtros por período (data início/fim).

3.6 Formulario

Componente standalone reutilizável em cadastro e edição de transações.

Conecta campos de categoria para selecionar o id automaticamente a partir do nome.

Permite emissão de evento onSubmit com os dados do formulário.

4. Serviços

Transacoes: realiza requisições HTTP para listar, criar, editar e deletar transações.

CategoriasService: realiza operações CRUD com categorias.

RelatorioService: busca dados do resumo financeiro e totais por categoria.

5. Rotas
export const routes: Routes = [
  { path: '', component: Home },
  { path: 'cadastro', component: Cadastro },
  { path: 'editar/:id', component: Editar },
  { path: 'detalhes/:id', component: Detalhes },
  { path: 'categorias', component: CategoriasComponent },
  { path: 'dashboard', component: DashboardComponent },
];

6. Observações Técnicas

Todos os componentes utilizam standalone components do Angular 15+, sem necessidade de módulos específicos.

FormsModule e ReactiveFormsModule são utilizados para formulários e validação.

NgIf / NgFor são usados para renderização condicional e listas.

Chart.js é usado para visualização gráfica no dashboard.

Serviços são responsáveis pela comunicação com o backend, mantendo o frontend desacoplado.

