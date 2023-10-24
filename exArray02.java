/* Dado um vetor de números reais, fazer um programa que some todos
os elementos do vetor e imprima o resultado
 */

import java.util.Scanner;

public class exArray02 {
   public int tamanhoVetor() {
       Scanner input = new Scanner(System.in);
       System.out.print("Digite o tamanho do vetor: ");
       int tam = input.nextInt();

       return tam;
   }

   public float[] criarVetor(int tamanho) {
       Scanner input = new Scanner(System.in);
       float[] vetor = new float[tamanho];

       for(int i=0; i<vetor.length; i++){
           System.out.printf("Digite o número de posição [%d]: ", i);
           vetor[i] = input.nextFloat();
       }
       return vetor;
   }

   public void somarVetor(float[] vetor) {
       float soma = 0;

       for(int i=0; i<vetor.length; i++) {
           soma += vetor[i];
       }
       System.out.printf("Soma dos elementos do vetor: %.2f", soma);
   }

   public void imprimirVetor(float[] vetor) {
       System.out.println("---Imprimir o Vetor ---");
       for(int i=0; i<vetor.length; i++) {
           System.out.printf("Elemento de posição [%d]: %.2f\n", i, vetor[i]);
       }
   }

    public static void main(String[] args) {
        exArray02 e1 = new exArray02();
        int tam = e1.tamanhoVetor();
        float[] vetor = e1.criarVetor(tam);
        e1.imprimirVetor(vetor);
        e1.somarVetor(vetor);
    }
}
