package cp4;

import java.util.Scanner;

/**
 * Gerencia as interações do usuário com o sistema da Panificadora Fiapão,
 * permitindo acesso aos menus de produtos e vendas.
 *
 * @author Patricia Naomi, Igor Gabriel
 * @version 1.0
 */

public class Menu {

    Comanda comanda = new Comanda();
    private CatalogoProdutos catalogoProdutos = new CatalogoProdutos();
    private Scanner input = new Scanner(System.in);
    private boolean sair = false;
    private int opcao;


    //Getters & Setters
    public int getOpcao() {
        return opcao;
    }

    public void setOpcao(int opcao) {
        this.opcao = opcao;
    }

    //Métodos

    /**
     * Inicia a execução do menu principal, permitindo ao usuário interagir
     * com as opções do sistema.
     */
    public void executarMenu() {

        while(!sair) {
            System.out.println("=====  PANIFICADORA FIAPÃO  =====");
            System.out.println("|      [1] Menu Produtos        |");
            System.out.println("|      [2] Menu Vendas          |");
            System.out.println("|      [0] Sair do Programa     |");
            System.out.println("=================================");
            System.out.print("Digite uma opção: ");
            this.setOpcao(input.nextInt());
            switch (getOpcao()) {
                case 1:
                    //Abre o menu de produtos (estoque)
                    menuProdutos();
                case 2:
                    //Abre o menu das comandas (vendas)
                    menuVendas();
                case 0:
                    sair = true;
                    break;
                default:
                    System.out.println("Opção Inválida, digite novamente\n");
            }
        }
    }

    /**
     * Exibe e gerencia o menu de produtos, permitindo ao usuário realizar operações
     * como cadastrar, buscar, listar e deletar produtos.
     */
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
            this.setOpcao(input.nextInt());
            switch (getOpcao()) {
                case 1:
                    //Cadastrar Produto no Estoque
                    catalogoProdutos.cadastrarProduto();
                    break;

                case 2:
                    //Buscar Produto Específico do Estoque
                    catalogoProdutos.buscarProduto();
                    break;

                case 3:
                    // Impressão de Produtos Cadastrados
                    catalogoProdutos.listarProdutos();
                    break;

                case 4:
                    //Deletar Produto do Estoque
                    catalogoProdutos.deletarProduto();
                    break;

                case 5:
                    //Volta ao Menu Principal
                    executarMenu();

                case 0:
                    sair = true;
                    break;
                default:
                    System.out.println("Opção Inválida, digite novamente\n");
            }
        }
    }

    /**
     * Exibe e gerencia o menu de vendas, permitindo ao usuário realizar operações
     * como cadastrar comandas, adicionar itens a uma comanda e pagar comandas.
     */
    public void menuVendas() {
        while(!sair) {
            System.out.println("\n=======   MENU VENDAS   =======");
            System.out.println("|    [1] Cadastrar Comanda     |");
            System.out.println("|    [2] Realizar Venda        |");
            System.out.println("|    [3] Pagar Comanda         |");
            System.out.println("|    [4] Menu Principal        |");
            System.out.println("|    [0] Sair do Programa      |");
            System.out.println("=================================");
            System.out.print("Digite uma opção: ");
            this.setOpcao(input.nextInt());
            switch (getOpcao()) {
                case 1:
                    //Cadastrar nova Comanda
                    comanda.cadastrarComanda();
                    break;

                case 2:
                    //Adicionar item na Comanda
                    fazerVenda();
                    break;

                case 3:
                    //Para o cliente pagar a comanda
                    comanda.pagarComanda();
                    break;

                case 4:
                    //Volta ao menu principal
                    executarMenu();

                case 0:
                    sair = true;
                    break;
                default:
                    System.out.println("Opção Inválida, digite novamente\n");
            }
        }
    }

    /**
     * Permite ao usuário adicionar um produto a uma comanda especificada,
     * digitando o número da comanda e as informações do produto.
     */
    public void fazerVenda() {
        System.out.print("Digite o número da comanda: ");
        int numComanda = input.nextInt();
        Comanda comandaEscolhida = null;

        for (Comanda comanda : comanda.getComandas()) {
            if (comanda.getNumComanda() == numComanda) {
                comandaEscolhida = comanda;
                break;
            }
        }

        if (comandaEscolhida == null) {
            System.out.println("Comanda não encontrada");
            return;
        }

        System.out.print("Digite o código do produto: ");
        int codProd = input.nextInt();
        Produto produto = catalogoProdutos.buscarProdutoPorCodigo(codProd);

        if (produto == null) {
            System.out.println("Produto não encontrado");
            return;
        }

        System.out.print("Digite a quantidade: ");
        int quantidade = input.nextInt();

        comandaEscolhida.adicionarItem(new ItemComanda(produto, quantidade));
        System.out.println("Produto adicionado na comanda");
    }
}
