package cp4;

import java.util.ArrayList;

public class ItemComanda {

    private Produto produto = new Produto();

    private ArrayList<ItemComanda> itensComanda = new ArrayList<ItemComanda>();
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


    //Vai calcular o preço do item * quantidade
    public double calcularValorTotal() {
        double valorTotal = 0;
        for (ItemComanda item : itensComanda) {
            valorTotal += item.getProduto().getPrecoUn() * item.getQtd();
        }
        return valorTotal;
    }

    public double calcularValorItem() {
        return this.qtd * this.produto.getPrecoUn();
    }


}