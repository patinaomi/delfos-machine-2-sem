package cp4;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.Scanner;

public class Menu {
    LocalDateTime dataHoraAtual = LocalDateTime.now();
    DateTimeFormatter formatador = DateTimeFormatter.ofPattern("dd/MM/yyyy HH:mm:ss");
    String dataHoraFormatada = dataHoraAtual.format(formatador);
    Scanner input = new Scanner(System.in);

    boolean sair = false;
    int opcao;

    CatalogoProdutos catalogoProdutos = new CatalogoProdutos();

    ArrayList<Produto> produtos = new ArrayList<Produto>();


    public void executarMenu() {

        while(!sair) {
            System.out.println("===== PANIFICADORA DA PATI =====");
            System.out.println("|   [1] Cadastrar Produtos      |");
            System.out.println("|   [2] Editar Produtos         |");
            System.out.println("|   [3] Listar Produtos         |");
            System.out.println("|   [4] Deletar Produtos        |");

            System.out.println("|   [5] Registrar Produto na Comanda |");
            System.out.println("|   [6] Finalizar Compra      |");
            System.out.println("|   [7] Sair do Programa     |");
            System.out.println("=================================");
            System.out.print("Digite uma opção: ");
            opcao = input.nextInt();

            switch (opcao) {
                case 1:
                    //Cadastrar Produto (código, descrição e preço unitário)

                    System.out.println("----- Cadastrar Produto -----");
                    System.out.print("Digite o código do produto: ");
                    int codProd = input.nextInt();
                    input.nextLine();
                    System.out.print("Digite a descrição do produto: ");
                    String descProd = input.nextLine();
                    System.out.print("Digite o preço unitário: ");
                    float precoUn = input.nextFloat();

                    Produto produto = new Produto(codProd, descProd, precoUn);
                    catalogoProdutos.addProduto(produto);

                    break;

                case 3:
                    // Listar Produtos Cadastrados

                    System.out.println("----- Visualizar Produtos em Estoque -----");
                    System.out.println("Quantidade de Produtos: "+ produtos.size());
                    System.out.println(produtos.get(0));
                    System.out.println(produtos.get(1));

                    break;

                case 5:
                    System.out.println("Saindo do Programa...");
                    sair = true;
            }
    }
    }

}
