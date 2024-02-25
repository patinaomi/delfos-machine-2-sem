/* Dado dois vetores A e B de mesma dimensão, fazer um programa que calcule
e imprima o produto de cada elemento de A pelo correspondente elemento de B.
O resultado dve ser armazenado em um vetor C.
 */

import java.util.Scanner;

public class exArray09 {
   public int tamVetor() {
       Scanner input = new Scanner(System.in);
       System.out.print("Digite o tamanho do vetor: ");
       int tam = input.nextInt();

       return tam;
   }

   public int[] criarVetor(int tamanho) {
       Scanner input = new Scanner(System.in);
       int[] vetor = new int[tamanho];
       System.out.println("--- Novo Vetor ---");
       for(int i=0; i<vetor.length; i++) {
           System.out.printf("Digite o elemento de posição [%d]: ", i);
           vetor[i] = input.nextInt();
       }
       return vetor;
   }

   public void multiplicaVetor(int[] vetorA, int[] vetorB) {
       Scanner input = new Scanner(System.in);
       int tamanho = vetorA.length;
       int[] vetorC = new int[tamanho];

       System.out.println("--- Resultado Multiplicações ---");
       for(int i=0; i<vetorC.length; i++) {
           vetorC[i] = vetorA[i] * vetorB[i];
           System.out.printf("%d X %d = %d\n", vetorA[i], vetorB[i], vetorC[i]);
       }
   }

    public static void main(String[] args) {
        exArray09 e1 = new exArray09();
        exArray09 e2 = new exArray09();

        int tam = e1.tamVetor();

        int[] vetorA = e1.criarVetor(tam);
        int[] vetorB = e2.criarVetor(tam);

        e1.multiplicaVetor(vetorA, vetorB);
    }

}
