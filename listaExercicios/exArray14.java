/* Crie um vetor para armazenar 5 números inteiros digitados pelo
usuário e, em seguida, determine a média dos valores do vetor.
 */

import java.util.Scanner;

public class exArray14 {
    public int tamVetor() {
        Scanner input = new Scanner(System.in);
        System.out.print("Digite o tamanho do vetor: ");
        int tam = input.nextInt();

        return tam;
    }

    public int[] criarVetor(int tam) {
        Scanner input = new Scanner(System.in);
        int[] vetor = new int[tam];

        System.out.println("--- Criar Vetor ---");
        for(int i=0; i<vetor.length; i++) {
            System.out.printf("Digite o elemento de posição [%d]: ", i);
            vetor[i] = input.nextInt();
        }
        return vetor;
    }

    public void mediaVetor(int[] vetor) {
        float soma = 0;
        int comp = vetor.length;
        for(int i=0; i<vetor.length; i++) {
            soma += vetor[i];
        }
        float media = soma / comp;

            System.out.printf("Média dos valores: %.2f / %d = %.2f", soma, comp, media);
    }

    public static void main(String[] args) {
        exArray14 e1 = new exArray14();

        int tam = e1.tamVetor();
        int[] vetor = e1.criarVetor(tam);
        e1.mediaVetor(vetor);
    }

}
