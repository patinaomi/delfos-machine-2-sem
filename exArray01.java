/* Dado um vetor de números inteiros,
fazer um programa que imprime todos os elementos do vetor.
 */

import java.util.Scanner;

public class exArray01 {
    public int tamanhoVetor() {
        Scanner input = new Scanner(System.in);
        System.out.print("Digite o tamanho do vetor: ");
        int tamanho = input.nextInt();

        return tamanho;
    }

    public int[] criarVetor(int tamanho) {
        Scanner input = new Scanner(System.in);
        System.out.println("--Novo Vetor --");
        int[] vetor = new int[tamanho];

        for(int i=0; i<vetor.length; i++) {
            System.out.printf("Digite o número de posição [%d]: ", i);
            vetor[i] = input.nextInt();
        }
        return vetor;
    }

    public void imprimirVetor(int[] vetor) {
        System.out.println("-- Imprimir Vetor --");

        for(int i=0; i<vetor.length; i++) {
            System.out.printf("Posição [%d]: %d\n", i, vetor[i]);
        }
    }

    public static void main(String[] args) {
        exArray01 e1 = new exArray01();
        int tam = e1.tamanhoVetor();
        int[] vetor = e1.criarVetor(tam);
        e1.imprimirVetor(vetor);

    }

}
