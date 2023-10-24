/* Dado um vetor de número interios e um número inteiro, fazer um
programa que verifique se o número está presente no vetor
 */

import java.util.Scanner;

public class exArray08 {
   public int tamVetor() {
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

   public void verificaNumero(int[] vetor) {
       Scanner input = new Scanner(System.in);
       System.out.print("Digite o elemento que procura: ");
       int busca = input.nextInt();
       int cont = 0;

       for(int i=0; i<vetor.length;i++) {
           if(vetor[i] == busca) {
               cont++;
           }
       }
       System.out.printf("O número %d está presente no vetor %d vezes.", busca, cont);
   }

    public static void main(String[] args) {
        exArray08 e1 = new exArray08();
        int tam = e1.tamVetor();
        int[] vetor = e1.criarVetor(tam);
        e1.verificaNumero(vetor);
    }
}
