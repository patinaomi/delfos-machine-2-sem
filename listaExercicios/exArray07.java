/* Dado um vetor de números inteiros e um número inteiro, fazer um
programa que multiplique todos os elementos do vetor pelo número real
dado e imprima o resultado
 */

import java.util.Scanner;

public class exArray07 {
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
       Scanner input = new Scanner(System.in);
       System.out.print("Digite um número para multiplicar: ");
       int mult = input.nextInt();
       int tam = vetor.length;

       int[] vetorMult = new int[tam];


       for(int i=0; i<vetor.length; i++) {
           vetorMult[i] = vetor[i] * mult;
       }

       for(int i=0; i<vetor.length; i++) {
           System.out.printf("%d X %d = %d\n", vetor[i], mult, vetorMult[i]);
       }
   }

    public static void main(String[] args) {
         exArray07 e1 = new exArray07();
         int tam = e1.tamanhoVetor();
         int[] vetor = e1.criarVetor(tam);
         e1.multVetor(vetor);

    }
}
