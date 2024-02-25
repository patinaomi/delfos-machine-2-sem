/* Dado um vetor de números reais e um número real, fazer um programa
que multiplique os elementos de posição ímpar do vetor pelo número
real dado e imprima o resultado.
 */

import java.util.Scanner;

public class exArray10 {
   public int tamanhoVetor() {
       Scanner input = new Scanner(System.in);
       System.out.print("Digite o tamanho do vetor: ");
       int tamanho = input.nextInt();

       return tamanho;
   }

   public float[] criarVetor(int tamanho) {
       Scanner input = new Scanner(System.in);
       float[] vetor = new float[tamanho];
       for(int i=0; i<vetor.length; i++) {
           System.out.printf("Digite o elemento de posição [%d]: ", i);
           vetor[i] = input.nextFloat();
       }
       return vetor;
   }

   public void imprimirImpar(float[] vetor) {
       System.out.println("----- Elementos de posição ímpar ------");
       for(int i=0; i<vetor.length; i++) {
           if (i %2 != 0) {
               System.out.printf("Posição [%d]: %.2f\n", i, vetor[i]);
           }
       }
   }

    public static void main(String[] args) {
        exArray10 e1 = new exArray10();
        int tam = e1.tamanhoVetor();
        float[] vetor = e1.criarVetor(tam);
        e1.imprimirImpar(vetor);
    }
}
