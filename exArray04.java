/* Dado um vetor de números reais, fazer um
programa que imprima o menor elemento do vetor.
 */

import java.util.Scanner;

public class exArray04 {
    public int tamanhoVetor() {
        Scanner sc = new Scanner(System.in);
        System.out.print("Digite o tamanho do vetor: ");
        int tam = sc.nextInt();
        return tam;
    }

    public float[] criarVetor(int tam) {
        Scanner sc = new Scanner(System.in);
        float[] vetor = new float[tam];

        for(int i=0; i<vetor.length; i++) {
            System.out.printf("Digite o número de posição [%d]: ", i);
            vetor[i] = sc.nextFloat();
        }
        return vetor;
    }

    public void menorVetor(float[] vetor) {
        float menor = 0;
        for(int i=0; i<vetor.length; i++) {
            if(i == 0) {
                menor = vetor[i];
            } else {
                if(vetor[i] < menor) {
                   menor = vetor[i];
                }
            }
        }
        System.out.printf("Menor Número: %.2f", menor);
    }

    public static void main(String[] args) {
        exArray04 e1 = new exArray04();
        int tam = e1.tamanhoVetor();
        float[] vetor = e1.criarVetor(tam);
        e1.menorVetor(vetor);

    }
}
