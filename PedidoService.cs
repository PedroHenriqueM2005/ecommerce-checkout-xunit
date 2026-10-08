namespace EcommerceCheckout.App;

/// <summary>
/// Serviço com as regras de negócio do checkout da loja online:
/// geração de código de rastreio, cálculo de pontos de fidelidade
/// e validação de frete grátis.
/// </summary>
public class PedidoService
{
    /// <summary>
    /// Gera o código de rastreio unindo a região em MAIÚSCULAS
    /// com o número do pedido preenchido com zeros à esquerda (4 dígitos).
    /// </summary>
    /// <example>
    /// GerarCodigoRastreio("sudeste", 42) retorna "SUDESTE-0042".
    /// </example>
    public string GerarCodigoRastreio(string regiao, int numeroPedido)
    {
        return $"{regiao.ToUpperInvariant()}-{numeroPedido:D4}";
    }

    /// <summary>
    /// Calcula os pontos de fidelidade: a cada R$ 10 em compras,
    /// o cliente ganha 2 pontos.
    /// </summary>
    /// <example>
    /// CalcularPontosFidelidade(150) retorna 30
    /// (150 / 10 = 15 parcelas de R$ 10 -> 15 * 2 = 30 pontos).
    /// </example>
    public int CalcularPontosFidelidade(int valorTotal)
    {
        return (valorTotal / 10) * 2;
    }

    /// <summary>
    /// Verifica se o pedido tem direito a frete grátis.
    /// Regra: valor total maior ou igual a R$ 200 OU cliente VIP.
    /// </summary>
    /// <example>
    /// TemDireitoAFreteGratis(150, true) retorna true (cliente VIP).
    /// TemDireitoAFreteGratis(150, false) retorna false.
    /// </example>
    public bool TemDireitoAFreteGratis(int valorTotal, bool eClienteVIP)
    {
        return valorTotal >= 200 || eClienteVIP;
    }
}
