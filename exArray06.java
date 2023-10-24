/* Dado um vetor de números inteiros, fazer um
programa que imprima o produto dos elementos do vetor
 */

import java.util.Scanner;

public class exArray06 {
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

   public void multVetor(int[] vetor) {
       int mult = 1;
       for(int i=0; i<vetor.length; i++) {
           mult *= vetor[i];
       }
       System.out.printf("Resultado: %d", mult);
   }

    public static void main(String[] args) {
         exArray06 e1 = new exArray06();
         int tam = e1.tamanhoVetor();
         int[] vetor = e1.criarVetor(tam);
         e1.multVetor(vetor);
    }
}
