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

    private ArrayList<Produto> produtos;
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

    public int getNumComanda() {
        return numComanda;
    }

    public void setNumComanda(int numComanda) {
        this.numComanda = numComanda;
    }

    public ArrayList<Comanda> getComandas() {
        return comandas;
    }

    public void setComandas(ArrayList<Comanda> comandas) {
        this.comandas = comandas;
    }

    //Metodos
    public void cadastrarComanda() {
        System.out.println("----- Cadastrar Comanda -----");
        System.out.print("Digite o código da comanda: ");
        int codComanda = input.nextInt();
        input.nextLine();
        Comanda comanda = new Comanda(dataHoraFormatada, codComanda);
        comandas.add(comanda);
        System.out.println("Comanda Criada: " + comanda.getNumComanda());
        System.out.println("------------------------------");
    }

    public double calcularValorTotalComanda() {
        double valorTotal = 0;
        for (ItemComanda item : itensComanda) {
            valorTotal += item.calcularValorItem();
        }
        return valorTotal;
    }


    public void pagarComanda() {
        System.out.println("----- Pagamento Comandas -----");
        if (comandas.isEmpty()) {
            System.out.println("Não há comandas.");
        } else {
            System.out.print("Digite o código da comanda: ");
            int cod = input.nextInt();
            boolean comandaEncontrada = false;

            for (Comanda comanda : comandas) {
                if (comanda.getNumComanda() == cod) {
                    comandaEncontrada = true;
                    System.out.println("Comanda Nº: " + comanda.getNumComanda());
                    System.out.println("Data/Hora: " + comanda.dataHoraFormatada);
                    System.out.println("Itens da Comanda:");

                    // Exibe os itens da comanda
                    if (comanda.itensComanda.isEmpty()) {
                        System.out.println("Nenhum item registrado nesta comanda.");
                    } else {
                        for (ItemComanda item : comanda.itensComanda) {
                            System.out.println("- Produto: " + item.getProduto().getDescricao() +
                                    ", Quantidade: " + item.getQtd() +
                                    ", Preço Unitário: R$" + item.getProduto().getPrecoUn() +
                                    ", Subtotal: R$" + item.calcularValorItem());
                        }

                        // Se quiser exibir o valor total da comanda aqui, você pode adicionar:
                        double valorTotal = comanda.calcularValorTotalComanda();
                        System.out.println("Valor Total da Comanda: R$" + valorTotal);
                    }
                    break; // Sai do loop após encontrar e processar a comanda
                }
            }

            if (!comandaEncontrada) {
                System.out.println("Comanda Não Encontrada");
            }
        }
        System.out.println("------------------------------");
    }



    public void adicionarItem(ItemComanda item) {
        this.itensComanda.add(item);
    }
        public String toString () {
            return "Cod #" + this.numComanda + "  -  " + this.dataHoraFormatada;
        }
}

