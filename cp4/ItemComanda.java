package cp4;
public class ItemComanda {

    private Produto produto = new Produto();
    private int qtd;

    public ItemComanda() {

    }

    public ItemComanda(Produto produto, int qtd) {
        this.produto = produto;
        this.qtd = qtd;
    }

    public Produto getProduto() {
        return produto;
    }

    public void setProduto(Produto produto) {
        this.produto = produto;
    }

    public int getQtd() {
        return qtd;
    }

    public void setQtd(int qtd) {
        this.qtd = qtd;
    }
}