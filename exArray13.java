/* Crie um algoritmo que leia 15 números aleatórios pelo teclado.
Depois de ler os 15 números, o algoritmo deve apresentar.
 - Os valores digitados;
 - Os valores digitados na ordem inversa;
 */

import java.util.Scanner;

public class exArray13 {
    public int tamVetor() {
        Scanner input = new Scanner(System.in);
        System.out.print("Digite o tamanho do vetor: ");
        int tam = input.nextInt();

        return tam;
    }

    public int[] criarVetor(int tamanho) {
        Scanner input = new Scanner(System.in);
        int[] vetor = new int[tamanho];

        for(int i=0; i<vetor.length; i++) {
            System.out.printf("Digite o elemento de posição [%d]: ", i);
            vetor[i] = input.nextInt();
        }
        return vetor;
    }

    public void imprimirVetor(int[] vetor) {
        System.out.println("--- IMPRIMIR O VETOR ---");
        for(int i=0; i<vetor.length; i++) {
            System.out.printf("Posição [%d]: %d\n", i, vetor[i]);
        }
    }

    public void imprimirInverso(int[] vetor) {
        System.out.println("--- VETOR INVERTIDO ---");
        int comp = vetor.length - 1;
        
        System.out.println(comp);
        for(int i=comp; i>=0; i--) {
            System.out.printf("Posição [%d]: %d\n", i, vetor[i]);
        }
    }

    public static void main(String[] args) {
        exArray13 e1 = new exArray13();
        int tam = e1.tamVetor();
        int[] vetor = e1.criarVetor(tam);
        e1.imprimirVetor(vetor);
        e1.imprimirInverso(vetor);
    }




}
