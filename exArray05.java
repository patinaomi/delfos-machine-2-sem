/* Dado um vetor de números inteiros, fazer um
programa que imprima a média dos elementos do vetor
 */

import java.util.Scanner;

public class exArray05 {
   public int tamanhoVetor() {
       Scanner input = new Scanner(System.in);
       System.out.print("Digite o tamanho do vetor: ");
       int tamanho = input.nextInt();
       return tamanho;
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

   public void mediaVetor(int[] vetor) {
       int tam = vetor.length;

       float soma = 0;
       for(int i=0; i<vetor.length; i++) {
           soma += vetor[i];
       }

       float media = soma / tam;
       System.out.printf("Média %.2f / %d = %.2f", soma, tam, media);
   }

    public static void main(String[] args) {
        exArray05 e1 = new exArray05();
        int tam = e1.tamanhoVetor();
        int[] vetor = e1.criarVetor(tam);
        e1.mediaVetor(vetor);

    }
}
