package cp4;

public class ItemComanda {
    private Produto produto = new Produto();
    private int qtd;


    //Construtor
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


    public double calcularValorItem() {
        return this.qtd * this.produto.getPrecoUn();
    }

    @Override
    public String toString() {
        return "Cod #" + this.getProduto().getCodigo() + "  -  " + this.getProduto().getDescricao() + "  R$" +
                this.getProduto().getPrecoUn() +  " x "+ this.getQtd() + " un.  --- R$" +
                String.format("%.2f", calcularValorItem());
    }
}