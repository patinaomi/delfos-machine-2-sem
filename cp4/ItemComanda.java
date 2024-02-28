package cp4;

/**
 * Representa um item de comanda, incluindo o produto e a quantidade solicitada.
 * Cada item de comanda está associado a um produto específico e inclui a quantidade
 * desse produto que foi pedida.
 *
 * @author Patricia Naomi, Igor Gabriel
 * @version 1.0
 */

public class ItemComanda {
    private Produto produto = new Produto();
    private int qtd;


    /**
     * Constrói um novo item de comanda com o produto e a quantidade especificados.
     *
     * @param produto O produto associado a este item de comanda.
     * @param qtd A quantidade do produto.
     */
    public ItemComanda(Produto produto, int qtd) {
        this.produto = produto;
        this.qtd = qtd;
    }

    //Getters
    public Produto getProduto() {
        return produto;
    }

    public int getQtd() {
        return qtd;
    }


    /**
     * Calcula o valor total deste item de comanda, multiplicando a quantidade
     * pelo preço unitário do produto.
     * @return O valor total do item de comanda.
     */
    public double calcularValorItem() {
        return this.qtd * this.produto.getPrecoUn();
    }

    /**
     * Gera uma representação em string deste item de comanda, incluindo o código,
     * a descrição e o preço unitário do produto, além da quantidade solicitada e
     * o valor total do item.
     * @return Uma string representando este item de comanda.
     */
    @Override
    public String toString() {
        return String.format("Cod #%d  -  %s  R$%.2f x %d un.  --- R$%.2f",
                this.getProduto().getCodigo(),
                this.getProduto().getDescricao(),
                this.getProduto().getPrecoUn(),
                this.getQtd(),
                this.calcularValorItem());
    }
}