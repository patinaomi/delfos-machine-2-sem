package cp4;

import java.util.ArrayList;
import java.util.Scanner;

public class CatalogoProdutos {
    private ArrayList<Produto> produtos = new ArrayList<Produto>();
    Scanner input = new Scanner(System.in);
    public CatalogoProdutos() {

    }

    public CatalogoProdutos(ArrayList<Produto> produtos) {
        this.produtos = produtos;
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
        System.out.println("----- Visualizar Produtos -----");

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
            System.out.print("Digite o código do produto a ser deletado: ");
            int codProd = input.nextInt();
                for(Produto produto : produtos) {
                    if(produto.getCodigo() == codProd) {
                        produtos.remove(produto);
                        System.out.println("Produto " + produto.getDescricao() + " removido.");
                        break;
                    }
                }
        }
        System.out.println("------------------------------");
    }

    public void buscarProduto() {
        System.out.println("----- Buscar Produto -----");

        if(produtos.isEmpty()) {
            System.out.println("Não há produtos no estoque. ");
        } else {
            System.out.print("Digite o código do produto: ");
            int codProd = input.nextInt();
            for(Produto produto : produtos) {
                if(produto.getCodigo() == codProd) {
                    System.out.println("Produto Encontrado");
                    System.out.println(produto);
                    break;
                }
            }
        }
        System.out.println("------------------------------");
    }
}
