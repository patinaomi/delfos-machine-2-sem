package cp4;

import java.util.ArrayList;
import java.util.Scanner;

public class CatalogoProdutos {
    Scanner input = new Scanner(System.in);
    private ArrayList<Produto> produtos;


    //Construtor
    public CatalogoProdutos() {
        this.produtos = new ArrayList<>();
    }


    public void cadastrarProduto() {
        System.out.println("----- Cadastrar Produto -----");
        System.out.print("Digite o código do produto: ");
        int codProd = input.nextInt();
        input.nextLine();
        System.out.print("Digite a descrição do produto: ");
        String descProd = input.nextLine();
        System.out.print("Digite o preço unitário: ");
        float precoUn = input.nextFloat();

        Produto produto = new Produto(codProd, descProd, precoUn);
        produtos.add(produto);
        System.out.println("Produto adicionado ao estoque.");
        System.out.println("------------------------------");
        input.nextLine();
    }

    public void listarProdutos() {
        System.out.println("----- Listar Produtos -----");
        if(produtos.isEmpty()) {
            System.out.println("Não há produtos no estoque. ");
        } else {
            for(Produto produto : produtos) {
                System.out.println(produto);
            }
        }
        System.out.println("------------------------------");
    }


    public void deletarProduto() {
        System.out.println("----- Deletar Produto -----");

        if(produtos.isEmpty()) {
            System.out.println("Não há produtos no estoque. ");
        } else {
            boolean produtoDel = false;
            System.out.print("Digite o código do produto a ser deletado: ");
            int codProd = input.nextInt();
            for(Produto produto : produtos) {
                if(produto.getCodigo() == codProd) {
                    produtos.remove(produto);
                    System.out.println("Produto " + produto.getDescricao() + " removido.");
                    produtoDel = true;
                    break;
                }
            }
            if (!produtoDel) {
                System.out.println("Produto não encontrado.");
            }
        }
        System.out.println("------------------------------");
    }

    public void buscarProduto() {
        System.out.println("----- Buscar Produto -----");

        if(produtos.isEmpty()) {
            System.out.println("Não há produtos no estoque. ");
        } else {
            boolean produtoBusca = false;
            System.out.print("Digite o código do produto: ");
            int codProd = input.nextInt();
            for(Produto produto : produtos) {
                if(produto.getCodigo() == codProd) {
                    System.out.println("Produto Encontrado");
                    System.out.println(produto);
                    produtoBusca = true;
                    break;
                }
            }
            if (!produtoBusca) {
                System.out.println("Produto não encontrado.");
            }
        }
        System.out.println("------------------------------");
    }

    public Produto buscarProdutoPorCodigo(int codigo) {
        for (Produto produto : produtos) {
            if (produto.getCodigo() == codigo) {
                return produto;
            }
        }
        return null;
    }
}
