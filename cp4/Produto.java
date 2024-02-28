package cp4;

/**
 * Representa um produto na Panificadora Fiapão.
 * Esta classe armazena as informações de um produto, incluindo seu código,
 * descrição e preço unitário.
 *
 * @author Patricia Naomi, Igor Gabriel
 * @version 1.0
 */
public class Produto {
    //Atributos
    private int codigo;
    private String descricao;
    private float precoUn;


    /**
     * Construtor padrão que cria um produto vazio.
     */
    public Produto() {

    }

    /**
     * Constrói um produto com os detalhes especificados.
     *
     * @param codigo O código identificador do produto.
     * @param descricao A descrição textual do produto.
     * @param precoUn O preço unitário do produto.
     */
    public Produto(int codigo, String descricao, float precoUn) {
        this.codigo = codigo;
        this.descricao = descricao;
        this.precoUn = precoUn;
    }

    //Getters

    public int getCodigo() {
        return codigo;
    }

    public String getDescricao() {
        return descricao;
    }

    public float getPrecoUn() {
        return precoUn;
    }


    /**
     * Gera uma representação em string deste produto, incluindo o código,
     * descrição e preço unitário.
     * @return Uma string que representa este produto.
     */
    @Override
    public String toString() {
        return String.format("Cod #%d  -  %s  R$%.2f", codigo, descricao, precoUn);
    }
}