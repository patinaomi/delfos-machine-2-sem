package cp4;

import java.util.ArrayList;
import java.util.Scanner;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;

public class Menu {
    LocalDateTime dataHoraAtual = LocalDateTime.now();
    DateTimeFormatter formatador = DateTimeFormatter.ofPattern("dd/MM/yyyy HH:mm:ss");
    String dataHoraFormatada = dataHoraAtual.format(formatador);

    Comanda comanda = new Comanda();
    ArrayList<Produto> produtos = new ArrayList<Produto>();


    private CatalogoProdutos catalogoProdutos = new CatalogoProdutos();

    Scanner input = new Scanner(System.in);
    boolean sair = false;
    private int opcao;

    //getters & setters
    public int getOpcao() {
        return opcao;
    }

    public void setOpcao(int opcao) {
        this.opcao = opcao;
    }

    //métodos
    public void menuProdutos() {
        while(!sair) {
            System.out.println("\n=======  MENU PRODUTOS  =======");
            System.out.println("|    [1] Cadastrar Produto     |");
            System.out.println("|    [2] Buscar Produto        |");
            System.out.println("|    [3] Listar Produto        |");
            System.out.println("|    [4] Deletar Produto       |");
            System.out.println("|    [5] Menu Principal        |");
            System.out.println("|    [0] Sair do Programa      |");
            System.out.println("=================================");
            System.out.print("Digite uma opção: ");
            opcao = input.nextInt();
            switch (opcao) {
                case 1:
                    //Cadastrar Produto (código, descrição e preço unitário)
                    catalogoProdutos.cadastrarProduto();
                    break;
                case 2:
                    //Buscar Produto do Estoque
                    catalogoProdutos.buscarProduto();
                    break;
                case 3:
                    // Impressão de Produtos Cadastrados
                    catalogoProdutos.listarProdutos();
                    break;
                case 4:
                    //Deletar Produto do Estoque
                    catalogoProdutos.deletarProduto();
                case 5:
                    //Volta ao Menu Principal
                    executarMenu();

                case 0:
                    sair = true;
                    break;
                default:
                    System.out.println("Opção Inválida, digite novamente");
            }
        }
    }

    public void menuComanda() {

        while(!sair) {
            System.out.println("\n=======  MENU VENDAS  =======");
            System.out.println("|    [1] Cadastrar Comanda     |");
            System.out.println("|    [2] Fazer Venda           |");
            System.out.println("|    [3] Ver Comandas          |");
            System.out.println("|    [4] Menu Principal        |");
            System.out.println("|    [0] Sair do Programa      |");
            System.out.println("=================================");
            System.out.print("Digite uma opção: ");
            opcao = input.nextInt();
            switch (opcao) {
                case 1:
                    //Cadastrar Comanda
                    comanda.cadastrarComanda();
                    break;
                case 2:
                    //Fazer Venda
                   comanda.fazerVenda();

                    break;
                case 3:
                    comanda.pagarComanda();
                case 4:
                    //Volta ao menu principal
                    executarMenu();

                case 0:
                    sair = true;
                    break;
                default:
                    System.out.println("Opção Inválida, digite novamente");
            }
        }

    }
    public void executarMenu() {

        while(!sair) {
            System.out.println("===== PANIFICADORA DA PATI =====");
            System.out.println("|      [1] Menu Produtos        |");
            System.out.println("|      [2] Menu Vendas          |");
            System.out.println("|      [0] Sair do Programa     |");
            System.out.println("=================================");
            System.out.print("Digite uma opção: ");
            opcao = input.nextInt();
            switch (opcao) {
                case 1:
                    //Abre o menu de produtos (estoque)
                    menuProdutos();
                case 2:
                    //Abre o menu das comandas (vendas)
                    menuComanda();
                case 0:
                    sair = true;
                    break;
                default:
                    System.out.println("Opção Inválida, digite novamente");
            }
        }
    }






}
