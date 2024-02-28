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

    private int numComanda;
    private ArrayList<Comanda> comandas = new ArrayList<>();
    Scanner input = new Scanner(System.in);


    // Construtores

    public Comanda() {

    }

    public Comanda(String dataHoraFormatada, int numComanda) {
        this.dataHoraFormatada = formatador.format(dataHoraAtual);
        this.numComanda = numComanda;
    }

    // Getters

    public int getNumComanda() {
        return numComanda;
    }


    public ArrayList<Comanda> getComandas() {
        return comandas;
    }


    //Metodos
    public void cadastrarComanda() {
        System.out.println("----- Cadastrar Comanda -----");
        System.out.print("Digite o código da comanda: ");
        int codComanda = input.nextInt();
        input.nextLine();
        Comanda comanda = new Comanda(dataHoraFormatada, codComanda);
        comandas.add(comanda);
        System.out.println("Comanda registrada: Nº" + comanda.getNumComanda());
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
        System.out.println("----- Pagamento Comanda -----");
        if (comandas.isEmpty()) {
            System.out.println("Não há comandas.");
        } else {
            System.out.print("Digite o código da comanda: ");
            int cod = input.nextInt();
            boolean comandaEncontrada = false;

            for (Comanda comanda : comandas) {
                if (comanda.getNumComanda() == cod) {
                    comandaEncontrada = true;
                    System.out.println("\n------------------------------");
                    System.out.println(" *****   COMANDA Nº" + comanda.getNumComanda() + "    *****");
                    System.out.println("Data/Hora  " + comanda.dataHoraFormatada);
                    System.out.println("\n----    Itens da Comanda    ----");

                    if (comanda.itensComanda.isEmpty()) {
                        System.out.println("Nenhum item registrado na comanda");
                    } else {
                        for (ItemComanda item : comanda.itensComanda) {
                            System.out.println(item);
                        }

                        double valorTotal = comanda.calcularValorTotalComanda();
                        System.out.println("\nTotal dos itens:    R$" + String.format("%.2f", valorTotal));
                    }
                    break;
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

