package cp4;

import java.util.ArrayList;
import java.util.Scanner;

/**
 * Gerencia o catálogo de produtos da Panificadora Fiapão.
 * Esta classe fornece funcionalidades para adicionar, listar, deletar e buscar
 * produtos no estoque.
 *
 * @author Patricia Naomi, Igor Gabriel
 * @version 1.0
 */

public class CatalogoProdutos {
    Scanner input = new Scanner(System.in);
    private ArrayList<Produto> produtos;

    /**
     * Constrói um catálogo de produtos vazio.
     */
    public CatalogoProdutos() {
        this.produtos = new ArrayList<>();
    }


    //MÉTODOS

    /**
     * Cadastra um novo produto no catálogo.
     * Solicita ao usuário o código, a descrição e o preço unitário do produto,
     * criando um novo produto e adicionando-o ao catálogo.
     */
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

    /**
     * Lista todos os produtos no catálogo.
     * Imprime as informações de cada produto, incluindo código, descrição e preço.
     * Se o catálogo estiver vazio, informa ao usuário.
     */
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


    /**
     * Deleta um produto do catálogo.
     * Solicita ao usuário o código do produto a ser deletado e remove o produto
     * correspondente do catálogo. Se o produto não for encontrado, informa ao usuário.
     */
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
                    System.out.println("Produto " + produto.getDescricao() + " removido");
                    produtoDel = true;
                    break;
                }
            }
            if (!produtoDel) {
                System.out.println("Produto não encontrado");
            }
        }
        System.out.println("------------------------------");
    }

    /**
     * Busca um produto no catálogo pelo seu código.
     * Solicita ao usuário o código do produto a ser buscado e exibe as informações
     * do produto encontrado. Se o produto não for encontrado, informa ao usuário.
     */
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

    /**
     * Busca um produto no catálogo pelo seu código e retorna o objeto {@link Produto}.
     * Se nenhum produto com o código fornecido for encontrado, retorna {@code null}.
     *
     * @param codigo O código do produto a ser buscado.
     * @return O produto encontrado ou {@code null} se nenhum produto for encontrado.
     */
    public Produto buscarProdutoPorCodigo(int codigo) {
        for (Produto produto : produtos) {
            if (produto.getCodigo() == codigo) {
                return produto;
            }
        }
        return null;
    }
}
