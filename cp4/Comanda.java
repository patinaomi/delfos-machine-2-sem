package cp4;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.Scanner;

public class Comanda {
    LocalDateTime dataHoraAtual = LocalDateTime.now();
    DateTimeFormatter formatador = DateTimeFormatter.ofPattern("dd/MM/yyyy HH:mm:ss");
    String dataHoraFormatada = dataHoraAtual.format(formatador);
    private ArrayList<ItemComanda> itensComanda = new ArrayList<ItemComanda>();

    private CatalogoProdutos catalogoProdutos = new CatalogoProdutos();

    private int numComanda;
    private ArrayList<Comanda> comandas = new ArrayList<>();
    private static Scanner input = new Scanner(System.in);

    private ItemComanda ic = new ItemComanda();


    // Construtores

    public Comanda() {

    }
    public Comanda(String dataHoraFormatada, int numComanda) {
        this.dataHoraFormatada = formatador.format(dataHoraAtual);
        this.numComanda = numComanda;
    }

    // Getters & Setters



    public void registrarItem(CatalogoProdutos catalogoProdutos) {
        System.out.println("Digite o número da comanda:");
        numComanda = input.nextInt();
        for (Comanda comanda : comandas) {
            if (comanda.getNumComanda() == numComanda) {
                System.out.print("Digite o código do produto: ");
                int codProd = input.nextInt();
                System.out.print("Digite a quantidade do produto: ");
                int qtdProd = input.nextInt();
            } else {
                System.out.println("Não foi encontrado comanda");
            }
        }
    }


    public void novaComanda() {
        System.out.println("Escolha uma opção: [1] Criar nova comanda [2] Adicionar item a comanda existente");
        int opcao = input.nextInt();
        switch (opcao) {
            case 1:
                // Cria nova comanda e adiciona à lista
                int numComanda = 10 + (int) (Math.random() * 90);
                Comanda novaComanda = new Comanda(dataHoraFormatada, numComanda); // Ajustado para usar o novo construtor
                comandas.add(novaComanda);
                System.out.println("Nova comanda criada. Número da comanda: " + novaComanda.getNumComanda());
                break;
            case 2:
                System.out.println("Digite o número da comanda:");
                numComanda = input.nextInt();
                for (Comanda comanda : comandas) {
                    if (comanda.getNumComanda() == numComanda) {
                        comanda.registrarItem(catalogoProdutos);
                        return;
                    }
                }
                System.out.println("Comanda não encontrada.");
                break;
            default:
                System.out.println("Opção inválida.");
                break;
        }
    }


    public void imprimirComanda() {
        System.out.println("Digite o número da comanda:");
        numComanda = input.nextInt();

        for (Comanda comanda : comandas) {
            if (comanda.getNumComanda() == numComanda) {
                comanda.registrarItem(catalogoProdutos);
                return;
            }

        System.out.println("Comanda Nº: " + numComanda + " Data/Hora: " + dataHoraFormatada);
        for (ItemComanda item : itensComanda) {
            System.out.println("- Produto: " + item.getProduto().getDescricao() + ", Quantidade: " + item.getQtd() + ", Subtotal: " + item.calcularValorItem());
        }
        System.out.println("Valor Total: " + calcularValorTotalComanda());
    }

    public double calcularValorTotalComanda() {
        double valorTotal = 0;
        for (ItemComanda item : this.itensComanda) {
            valorTotal += item.calcularValorItem(); // Usa o método calcularValorItem de cada item
        }
        return valorTotal;
    }

    public int getNumComanda() {
        return numComanda;
    }

    public void setNumComanda(int numComanda) {
        this.numComanda = numComanda;
    }
}
