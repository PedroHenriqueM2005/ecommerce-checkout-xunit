# ecommerce-checkout-xunit

Solução **.NET 10** criada via terminal (.NET CLI) para gerenciar o cálculo de cupons, itens e frete de uma loja online, com **testes unitários em xUnit** (`Assert.Equal`, `Assert.True` e `Assert.False`).

> Atividade da disciplina **Garantia da Qualidade de Software** — Prof. Daniel Henrique Matos de Paiva

## Estrutura da solução

```
ecommerce-checkout-xunit/
├── EcommerceCheckout.slnx              # Solução (gerada por dotnet new sln)
├── EcommerceCheckout.App/              # Projeto console (código de produção)
│   ├── EcommerceCheckout.App.csproj
│   ├── PedidoService.cs                # Regras de negócio do checkout
│   └── Program.cs
└── EcommerceCheckout.Tests/            # Projeto de testes unitários (xUnit)
    ├── EcommerceCheckout.Tests.csproj
    └── PedidoServiceTests.cs           # Testes com [Fact]
```

##  Métodos implementados (`PedidoService.cs`)

| # | Assinatura | Retorno | Regra de negócio | Exemplo |
|---|------------|---------|------------------|---------|
| 1 | `GerarCodigoRastreio(string regiao, int numeroPedido)` | `string` | Região em MAIÚSCULAS + `-` + número do pedido com zeros à esquerda (4 dígitos) | `("sudeste", 42)` → `"SUDESTE-0042"` |
| 2 | `CalcularPontosFidelidade(int valorTotal)` | `int` | A cada R$ 10 em compras, o cliente ganha 2 pontos de fidelidade | `150` → `30` (15 parcelas de R$ 10 × 2 pontos) |
| 3 | `TemDireitoAFreteGratis(int valorTotal, bool eClienteVIP)` | `bool` | Frete grátis se o valor total for **≥ R$ 200** OU se o comprador for **cliente VIP** | `(150, true)` → `true` · `(150, false)` → `false` |

##  Cobertura dos testes (`PedidoServiceTests.cs`)

Todos os testes utilizam o atributo **`[Fact]`** (teste único, sem parâmetros):

| Teste | Método testado | Cenário coberto | Assert utilizado |
|-------|----------------|-----------------|------------------|
| `GerarCodigoRastreio_DeveRetornarRegiaoEmMaiusculasENumeroComQuatroDigitos` | `GerarCodigoRastreio` (string) | Máscara exata `SUDESTE-0042` | `Assert.Equal("SUDESTE-0042", resultado)` |
| `CalcularPontosFidelidade_DeveRetornar30PontosParaCompraDe150Reais` | `CalcularPontosFidelidade` (int) | 150 reais → 30 pontos | `Assert.Equal(30, resultado)` |
| `TemDireitoAFreteGratis_DeveRetornarTrueQuandoClienteVipAbaixoDe200Reais` | `TemDireitoAFreteGratis` (bool) | Compra VIP abaixo de R$ 200 | `Assert.True(resultado)` |
| `TemDireitoAFreteGratis_DeveRetornarFalseQuandoNaoVipAbaixoDe200Reais` | `TemDireitoAFreteGratis` (bool) | Compra não-VIP abaixo de R$ 200 | `Assert.False(resultado)` |

**Resultado esperado: 4 testes aprovados (Passed! - 4).**

##  Como executar

Pré-requisito: [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) instalado.

```bash
# Na raiz do repositório, execute a suíte de testes:
dotnet test
```

Saída esperada:

```
Passed!  - Failed: 0, Passed: 4, Skipped: 0, Total: 4
```

##  Como a solução foi criada (.NET CLI)

```bash
# 1. Cria a Solução
dotnet new sln -n EcommerceCheckout

# 2. Cria o projeto da aplicação (Código de Produção)
dotnet new console -n EcommerceCheckout.App -f net10.0

# 3. Cria o projeto de Testes Unitários com xUnit
dotnet new xunit -n EcommerceCheckout.Tests -f net10.0

# 4. Adiciona ambos os projetos à Solução
dotnet sln add EcommerceCheckout.App/EcommerceCheckout.App.csproj
dotnet sln add EcommerceCheckout.Tests/EcommerceCheckout.Tests.csproj

# 5. Adiciona a referência do projeto de Produção no projeto de Testes
dotnet add EcommerceCheckout.Tests/EcommerceCheckout.Tests.csproj reference EcommerceCheckout.App/EcommerceCheckout.App.csproj
```

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
