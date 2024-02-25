/* Criação de Matriz com Métodos
 */

import java.util.Scanner;

public class exArray11 {
   public int[] tamanhoMatriz() {
       Scanner input = new Scanner(System.in);
       int[] tamMatriz = new int[2];
       System.out.print("Digite a quantidade de linhas: ");
       tamMatriz[0] = input.nextInt();

       System.out.print("Digite a quantidade de colunas: ");
       tamMatriz[1] = input.nextInt();

       return tamMatriz;
   }

   public int[][] criarMatriz(int[] vetor) {
       Scanner input = new Scanner(System.in);
       int[][] matriz = new int[vetor[0]][vetor[1]];

       for(int i=0; i<matriz.length; i++) {
           System.out.printf("--- Elementos da %dª linha ---\n", i+1);

           for(int j=0; j<matriz[i].length; j++) {
               System.out.printf("Matriz [%d] [%d]: ", i, j);
               matriz[i][j] = input.nextInt();
           }
       }
        return matriz;
   }

   public void imprimirMatriz(int[][] matriz) {
       System.out.println("--- Imprimir a Matriz ---");
       for(int i=0; i<matriz.length; i++){
           for(int j=0; j<matriz[i].length; j++) {
               System.out.print("[" + matriz[i][j] + "]");
           }
           System.out.println();
       }
   }

    public static void main(String[] args) {
       exArray11 e1 = new exArray11();
        int[] tam = e1.tamanhoMatriz();
        int[][] matriz = e1.criarMatriz(tam);
        e1.imprimirMatriz(matriz);
    }
}
