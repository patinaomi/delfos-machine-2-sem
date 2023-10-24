/* Dado um vetor de números inteiros,
 fazer um programa que imprima o maior elemento do vetor
 */

import java.util.Scanner;

public class exArray03 {
    public int tamanhoVetor() {
        Scanner input = new Scanner(System.in);
        System.out.print("Digite o tamanho do vetor: ");
        int tamanho = input.nextInt();
        return tamanho;
    }

    public int[] criarVetor(int tamanho) {
        Scanner input = new Scanner(System.in);
        int[] vetor = new int[tamanho];

        for(int i=0; i<vetor.length; i++){
            System.out.printf("Digite o elemento de posição [%d]: ", i);
            vetor[i] = input.nextInt();
        }
        return vetor;
    }

    public void maiorVetor(int[] vetor) {
        int maior = 0;

        for(int i=0; i<vetor.length; i++) {
            if(i == 0) {
                maior = vetor[i];
            }
            else {
                if(vetor[i] > maior) {
                    maior = vetor[i];
                }
            }
        }
        System.out.printf("Maior número: %d", maior);
    }

    public static void main(String[] args) {
        exArray03 ex1 = new exArray03();
        int tam = ex1.tamanhoVetor();
        int[] vetor = ex1.criarVetor(tam);
        ex1.maiorVetor(vetor);
        
    }

}
