/* Crie um vetor para armazendar 10 números inteiros digitados pelo
usuário e, em seguida, faça a soma de todos os valores do vetor
 */

import java.util.Scanner;

public class exArray12 {
    public int tamanhoVetor() {
        Scanner input = new Scanner(System.in);
        System.out.print("Digite o tamanho do vetor: ");
        int tam = input.nextInt();
        return tam;
    }

    public int[] criarVetor(int tam) {
        Scanner input = new Scanner(System.in);
        int[] vetor = new int[tam];

        for(int i=0; i<vetor.length; i++) {
            System.out.printf("Digit o elemento de posição [%d]: ", i);
            vetor[i] = input.nextInt();
        }

        return vetor;
    }

    public void somaVetor(int[] vetor) {
        int soma = 0;
        for(int i=0; i<vetor.length; i++) {
            soma+= vetor[i];
        }
        System.out.printf("Soma dos números: %d", soma);
    }

    public static void main(String[] args) {
        exArray12 e1 = new exArray12();
        int tam = e1.tamanhoVetor();
        int[] vetor = e1.criarVetor(tam);
        e1.somaVetor(vetor);
        
    }

}
