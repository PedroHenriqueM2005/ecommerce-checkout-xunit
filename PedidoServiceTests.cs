using EcommerceCheckout.App;
using Xunit;

namespace EcommerceCheckout.Tests;

/// <summary>
/// Testes unitários do <see cref="PedidoService"/> utilizando o atributo [Fact]
/// com Assert.Equal, Assert.True e Assert.False.
/// </summary>
public class PedidoServiceTests
{
    private readonly PedidoService _pedidoService = new();

    // ---------------------------------------------------------------
    // Teste 1 (retorno string): valida a máscara exata do rastreio
    // ---------------------------------------------------------------
    [Fact]
    public void GerarCodigoRastreio_DeveRetornarRegiaoEmMaiusculasENumeroComQuatroDigitos()
    {
        // Arrange & Act
        var resultado = _pedidoService.GerarCodigoRastreio("sudeste", 42);

        // Assert
        Assert.Equal("SUDESTE-0042", resultado);
    }

    // ---------------------------------------------------------------
    // Teste 2 (retorno int): valida o cálculo dos pontos de fidelidade
    // ---------------------------------------------------------------
    [Fact]
    public void CalcularPontosFidelidade_DeveRetornar30PontosParaCompraDe150Reais()
    {
        // Arrange & Act
        var resultado = _pedidoService.CalcularPontosFidelidade(150);

        // Assert
        Assert.Equal(30, resultado);
    }

    // ---------------------------------------------------------------
    // Teste 3 (retorno bool): valida as regras de frete grátis
    // ---------------------------------------------------------------
    [Fact]
    public void TemDireitoAFreteGratis_DeveRetornarTrueQuandoClienteVipAbaixoDe200Reais()
    {
        // Arrange & Act — compra de R$ 150 feita por cliente VIP
        var resultado = _pedidoService.TemDireitoAFreteGratis(150, true);

        // Assert
        Assert.True(resultado);
    }

    [Fact]
    public void TemDireitoAFreteGratis_DeveRetornarFalseQuandoNaoVipAbaixoDe200Reais()
    {
        // Arrange & Act — compra de R$ 150 feita por cliente não-VIP
        var resultado = _pedidoService.TemDireitoAFreteGratis(150, false);

        // Assert
        Assert.False(resultado);
    }
}
