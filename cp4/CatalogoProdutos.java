package cp4;

import java.util.ArrayList;

public class CatalogoProdutos {
    private ArrayList<Produto> produtos = new ArrayList<Produto>();

    public CatalogoProdutos() {

    }

    public CatalogoProdutos(ArrayList<Produto> produtos) {
        this.produtos = produtos;
    }

    public void addProduto(Produto produto) {
        produtos.add(produto);
        System.out.println("Produto adicionado ao estoque.");
    }
}