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

    public int getNumComanda() {
        return numComanda;
    }

    public void setNumComanda(int numComanda) {
        this.numComanda = numComanda;
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

    public void pagarComanda() {
        System.out.println("----- Visualizar Comandas -----");
        if(comandas.isEmpty()) {
            System.out.println("Não há comandas. ");
        } else {
            for(Comanda comanda : comandas) {
                System.out.print("Digite o código da comanda: ");
                int cod = input.nextInt();
                if(comanda.getNumComanda() == cod) {
                    System.out.println("MOSTRAR PAGAMENTO");
                } else {
                    System.out.println("Comanda Não Encontrada");

                    }
        }
        }
        System.out.println("------------------------------");
    }

    public String toString() {
        return "Cod #" + this.numComanda + "  -  " + this.dataHoraFormatada;
    }
}
